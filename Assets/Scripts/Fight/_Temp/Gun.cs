using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject prefab;

    Vector2 mousePos;
    Vector2 direction;
    private new Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (camera is { }) mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Shoot();
    }

    void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        var bullet = Instantiate(prefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetSpeed(direction);
    }
}