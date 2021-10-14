using System.Linq;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200;
    public float nextWayPointDistance = 3;

    private Path path;
    private int currentWayPoint;

    private Seeker seeker;
    private Rigidbody2D rb;

    private EnemyToward enemyToward;
    
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyToward = GetComponent<EnemyToward>();

        var playerDis = Vector2.Distance(transform.position, GetComponent<Enemy>().player.position);
        var warnDis = GetComponent<EnemyFSM>().warningDistance;
        target = playerDis > warnDis ? GetComponent<EnemyFSM>().path.First() : GetComponent<Enemy>().player;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(UpdatePath), 0, 0.5f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(UpdatePath));
    }

    private void FixedUpdate()
    {
        if (path == null) return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            return;
        }

        var direction = ((Vector2) path.vectorPath[currentWayPoint] - rb.position).normalized;
        var force = direction * (speed * Time.deltaTime);

        rb.AddForce(force);

        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        enemyToward.toward = force.x;
        // if (force.x >= 0.01f)
        // {
        //     transform.localScale = new Vector2(-startScale.x, startScale.y);
        //     hpBar.localScale = new Vector2(-hpScale.x, hpScale.y);
        // }
        // else if (force.x <= -0.01f)
        // {
        //     transform.localScale = startScale;
        //     hpBar.localScale = hpScale;
        // }
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