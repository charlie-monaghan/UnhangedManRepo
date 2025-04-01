using UnityEngine;
using Pathfinding;
using System.IO;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    [SerializeField] private GameObject Attack;
    [SerializeField] private float enemyAttackLength;
    bool isAttacking = false;
    [SerializeField] private float attackCooldownTime;
    bool coolDownActive = false;
    [SerializeField] private bool bodyIsWeapon = false;
    [SerializeField] private bool groundedEnemy = false;
    [SerializeField] private bool canJump = false;
    [SerializeField] private float jumpForce;
    private bool isJumping = false;

    //Wall detection
    [Header("Wall Detection")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isTouchingWall = false;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;

    public Transform enemyGFX;

    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool playerDetected = false;
    [SerializeField] float playerDetectionRange;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (!bodyIsWeapon)
        {
            Attack.SetActive(false);
        }
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (groundedEnemy)
        {
            rb.gravityScale = 1.0f;
        }
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    void UpdatePath()
    {
        if (!playerDetected && Mathf.Abs(rb.position.x - target.position.x) < playerDetectionRange)
        {
            playerDetected = true;
        }
        if (seeker.IsDone() && playerDetected)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, groundLayer);
        if (path == null)
            return;
        if (bodyIsWeapon)
        {
            rb.MoveRotation(rb.rotation + 137f * Time.fixedDeltaTime);
        }

        if (reachedEndOfPath)
        {
            if (groundedEnemy)
            {
                rb.linearVelocity = Vector2.zero;
            }
            if (!coolDownActive && !bodyIsWeapon)
            {
                StartCoroutine(AttackPlayer());
                StartCoroutine(Cooldown());
            }
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath= false;
        }
        //Jump for grounded enemy
        if (isTouchingWall && !isJumping && canJump)
        {
            StartCoroutine(Jump());
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f && !isAttacking)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f && !isAttacking)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    private IEnumerator AttackPlayer()
    {
        if (isAttacking) { yield break; }
        isAttacking = true;
        Attack.SetActive(true); // turn damage field on
        yield return new WaitForSeconds(enemyAttackLength); // wait for attack to finish
        Attack.SetActive(false); // turn damage field off
        isAttacking= false;
    }
    private IEnumerator Cooldown()
    {
        coolDownActive = true;
        yield return new WaitForSeconds(attackCooldownTime);
        coolDownActive = false;
    }
    private IEnumerator Jump()
    {
        isJumping = true;
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        isJumping = false;
    }
}
