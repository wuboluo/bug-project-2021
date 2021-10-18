using System;
using Sirenix.Utilities;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BulletVFXPoolSO[] bulletPools;

    public UpdateSkillCdChannelSO _shootUpdateViewCD;
    public SkillInCDChannelSO _skillCDState;
    private Camera mainCam;
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

        if (Input.GetMouseButtonDown(0))
        {
            Fire(0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Fire(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") >= 0.1f)
        {
        }
        else if (Input.GetAxis("Mouse ScrollWheel") <= -0.1f)
        {
            Fire(2);
        }
    }

    private void Fire(int poolIndex)
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

            bullet.GetComponent<BulletVFX>().SetSpeed(direction);
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