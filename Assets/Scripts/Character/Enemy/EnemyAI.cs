using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200;
    public float nextWayPointDistance = 3;

    private Path path;
    private int currentWayPoint;
    private bool reachedEndOfPath;

    private Seeker seeker;
    private Rigidbody2D rb;

    private Transform hpBar;
    private Vector2 startScale, hpScale;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        hpBar = transform.GetChild(0);
        startScale = transform.localScale;
        hpScale = hpBar.localScale;

        InvokeRepeating(nameof(UpdatePath), 0, 0.5f);
    }

    private void FixedUpdate()
    {
        if (path == null) return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        reachedEndOfPath = false;

        var direction = ((Vector2) path.vectorPath[currentWayPoint] - rb.position).normalized;
        var force = direction * (speed * Time.deltaTime);

        rb.AddForce(force);

        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector2(-startScale.x, startScale.y);
            hpBar.localScale = new Vector2(-hpScale.x, hpScale.y);
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = startScale;
            hpBar.localScale = hpScale;
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
}