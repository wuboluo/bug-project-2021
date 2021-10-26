using System;
using System.Linq;
using Bug.Project21.Player;
using Sirenix.Utilities;
using UnityEngine;

public class Attacker : MonoBehaviour, IInteractor
{
    public BulletVFXPoolSO[] bulletPools;

    public UpdateSkillCdChannelSO _shootUpdateViewCD;
    public SkillInCDChannelSO _skillCDState;

    public GeneralAtkDataSO generalAtkData;
    public GameObject generalCollider;

    private Camera mainCam;
    private Transform player;

    private Vector2 direction;
    private Vector2 mousePos;

    private void Start()
    {
        mainCam = Camera.main;
        bulletPools.ForEach(_ =>
        {
            _.Prewarm(_.size);
            _.isCd = false;
        });

        player = transform.parent;
        generalAtkData.Init();
    }

    private void OnEnable()
    {
        _skillCDState.OnEventRaised += UpdateSkillCD;
    }

    private void OnDisable()
    {
        _skillCDState.OnEventRaised -= UpdateSkillCD;
    }

    private void Update()
    {
        if (mainCam is { }) mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Shoot();
    }

    private void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (Input.GetKeyDown(KeyCode.Alpha1)) SkillAttack(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SkillAttack(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SkillAttack(2);

        if (Input.GetMouseButtonDown(0)) GeneralAttack();
    }

    void GeneralAttack()
    {
        transform.localEulerAngles = Vector3.zero;
        var anims = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);

        var currentAnim = anims.First().clip.name;
        var dir = currentAnim.Substring(currentAnim.LastIndexOf('_') + 1);

        player.GetComponent<Animator>().Play("Player_Attack_" + dir);
        generalCollider.transform.localPosition = generalAtkData.atkColliderPos[dir].Item1;
        generalCollider.transform.localScale = generalAtkData.atkColliderPos[dir].Item2;
        player.GetComponent<PlayerMovement>().isMove = false;
        generalCollider.GetComponent<Collider2D>().enabled = true;
    }

    public void OnNearTriggerChange(bool entered, GameObject who)
    {
        if (entered)
            generalAtkData.Hit(who.GetComponent<Collider2D>(), who.transform.position - transform.position, transform);
    }

    void SkillAttack(int poolIndex)
    {
        // 发射条件：1-不在冷却 2-处于施法范围内
        if (bulletPools[poolIndex].isCd) return;
        if (bulletPools[poolIndex].castingDistance != 0 &&
            !(Vector2.Distance(mainCam.ScreenToWorldPoint(Input.mousePosition), transform.position) <=
              bulletPools[poolIndex].castingDistance)) return;

        var bullet = bulletPools[poolIndex].Request();
        bullet.bulletVFXPool = bulletPools[poolIndex];

        var isShoot = bullet.isShoot;
        if (isShoot)
        {
            transform.right = (Vector3) mousePos - transform.position;
            bullet.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            bullet.transform.position = transform.position;

            bullet.GetComponent<Skill>().SetSpeed(direction);
        }
        else
        {
            var pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            bullet.transform.position = new Vector3(pos.x, pos.y + 1, 0);
        }

        _shootUpdateViewCD?.RaiseEvent(poolIndex, bulletPools[poolIndex].cd);
    }

    void UpdateSkillCD(int index, bool isCD)
    {
        bulletPools[index].isCd = isCD;
    }
}