using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BulletVFXPoolSO[] bulletPools;

    public float bulletCD = 0.5f;
    private new Camera camera;
    private float cdTimer;
    private BulletVFXPoolSO currentPool;
    private Vector2 direction;

    private Vector2 mousePos;
    public int currentIndex;

    private void Start()
    {
        camera = Camera.main;
        bulletPools.ForEach(_ => _.Prewarm(_.size));
        currentPool = bulletPools.First();
    }

    private void Update()
    {
        if (camera is { }) mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        CutPool();
        Shoot();
    }

    private void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (Input.GetMouseButtonDown(0)) Fire();
        if (Input.GetMouseButton(1))
        {
            cdTimer += Time.deltaTime;
            if (cdTimer >= bulletCD)
            {
                cdTimer = 0;
                Fire();
            }
        }
    }

    void CutPool()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentIndex++;
            if (currentIndex >= bulletPools.Length)
                currentIndex = 0;

            currentPool = bulletPools[currentIndex];
        }
    }


    private void Fire()
    {
        var bullet = currentPool.Request();
        bullet.bulletVFXPool = currentPool;

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
            var pos = camera.ScreenToWorldPoint(Input.mousePosition);
            bullet.transform.position = new Vector3(pos.x, pos.y + 1, 0);
        }
    }
}