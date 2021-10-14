using UnityEngine;

public class Gun : MonoBehaviour
{
    public BulletVFXPoolSO bulletPool;
    public int initialSize;
    private new Camera camera;
    private Vector2 direction;

    private Vector2 mousePos;

    private void Start()
    {
        camera = Camera.main;
        bulletPool.Prewarm(initialSize);
    }

    private void Update()
    {
        if (camera is { }) mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Shoot();
    }

    private void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (Input.GetMouseButtonDown(0)) Fire();
    }

    private void Fire()
    {
        var bullet = bulletPool.Request();
        bullet.bulletVFXPool = bulletPool;

        transform.right = (Vector3) mousePos - transform.position;
        bullet.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        bullet.transform.position = transform.position;

        bullet.GetComponent<BulletVFX>().SetSpeed(direction);
    }
}