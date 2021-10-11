using UnityEngine;

public class Gun : MonoBehaviour
{
    public TomatoPoolSO tomatoPool;
    public int initialSize;
    private new Camera camera;
    private Vector2 direction;

    private Vector2 mousePos;

    private void Start()
    {
        camera = Camera.main;
        tomatoPool.Prewarm(initialSize);
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
        var tomato = tomatoPool.Request();
        tomato.tomatoPool = tomatoPool;

        tomato.transform.position = transform.position;
        tomato.GetComponent<Tomato>().SetSpeed(direction);
    }
}