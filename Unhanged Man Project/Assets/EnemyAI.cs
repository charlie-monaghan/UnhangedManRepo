using UnityEngine;
using Pathfinding;
using System.IO;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    [SerializeField] private GameObject Attack;
    [SerializeField] private float attackChargeLength = 0f;
    [SerializeField] private float enemyAttackLength;
    bool isAttacking = false;
    [SerializeField] private float attackCooldownTime;
    bool coolDownActive = false;
    [SerializeField] private bool bodyIsWeapon = false;
    [SerializeField] private bool groundedEnemy = false;
    [SerializeField] private bool canJump = false;
    [SerializeField] private float jumpForce;
    private bool isJumping = false;
    [SerializeField] private bool isFlipped = false;

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
    Animator anim;

    [SerializeField] AudioClip idleClip;
    [SerializeField] AudioClip attackClip;
    AudioSource audioSource;
    int idleSoundCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idleSoundCooldown = 0;
        if (!bodyIsWeapon)
        {
            Attack.SetActive(false);
        }
        anim = GetComponentInChildren<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        if (groundedEnemy)
        {
            rb.gravityScale = 3.0f;
        }
        else
        {
            rb.gravityScale = 0f;
        }
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (!playerDetected && Mathf.Abs(rb.position.x - target.position.x) < playerDetectionRange && Mathf.Abs(rb.position.y - target.position.y) < playerDetectionRange * 0.5f)
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
        if (idleSoundCooldown > 0)
            idleSoundCooldown--;

        if (Random.Range(0f, 1f) <= 0.01f && idleSoundCooldown == 0 && idleClip != null)
        {
            audioSource.PlayOneShot(idleClip);
            idleSoundCooldown = 300;
        }

        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, groundLayer);
        if (path == null)
            return;
        
        if (!groundedEnemy)
        {
            rb.MoveRotation(rb.rotation - 0.5f * speed * Time.fixedDeltaTime);
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
            anim.SetBool("Chasing", false);
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
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

        if (force.x >= 0.01f && !isAttacking)// && groundedEnemy)
        {
            if (isFlipped)
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            else
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f && !isAttacking)// && groundedEnemy)
        {
            if (isFlipped)
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            else
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (rb.linearVelocityX != 0)
            anim.SetBool("Chasing", true);
        else
            anim.SetBool("Chasing", false);
    }
    private IEnumerator AttackPlayer()
    {
        if (isAttacking) { yield break; }
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackChargeLength); // wait for attack charge up time (so they don't attack on frame 1)
        isAttacking = true;
        Attack.SetActive(true); // turn damage field on
        audioSource.PlayOneShot(attackClip);
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
        anim.SetTrigger("Jump");
        isJumping = true;
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        isJumping = false;
    }
}
