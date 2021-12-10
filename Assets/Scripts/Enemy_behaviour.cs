using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Enemy_behaviour : MonoBehaviour
{

    public float stoppingDistance;
    public float playerFollowRange;
    public float speed;
    public float attackDistance; //Minimum distance to trigger attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //Check if Player is in range
    public GameObject detectZone;
    public GameObject triggerArea;

    //private RaycastHit2D hit;
    private Animator anim;
    private float distance; //The distance from enemy to player
    private bool attackMode;
    private bool cooling; //Check if Enemy is in cooldown after attack
    private float intTimer;
    

    private TMP_Text goldAmnt;
    public int enemeyKillGoldAmnt = 10;
    private int enemyHealth = 3;

    private Bandit bandit = null;
    private Vector3 banditPosition;
    private Vector3 enemyPosition;
    private Rigidbody2D m_body2d;

    private float enemyToPlayerDistance;

    void Awake()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        banditPosition = target.transform.position;
        enemyPosition = gameObject.transform.position;

        m_body2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();

        SelectTarget();
        intTimer = timer; //Value of timer
        anim = GetComponent<Animator>();

        goldAmnt = GameObject.Find("GoldAmntHolder").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        banditPosition = target.transform.position;
        enemyPosition = gameObject.transform.position;

        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }
        if (bandit != null)
        {
            if (Vector3.Distance(banditPosition, enemyPosition) <= 2 &&
                Input.GetMouseButtonDown(0))
            {
              
                StartCoroutine(BossHurt(0.4f));
              
            }
        }
    }
    IEnumerator BossHurt(float time)
    {
        yield return new WaitForSeconds(time);
        
        HandleEnemyAttacked();

        if (transform.position.x > target.position.x)
        {
            m_body2d.AddForce(new Vector2(200f, 100f));
        }
        else
        {
            m_body2d.AddForce(new Vector2(-200f, 100f));
        }

    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        Debug.Log(distance);
        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);

        enemyToPlayerDistance = Vector2.Distance(transform.position, target.position);
        if (playerFollowRange > enemyToPlayerDistance && enemyToPlayerDistance > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

    }

    void Attack()
    {
        timer = intTimer; //Reset timer when player enters attack range
        attackMode = true; //Check if Enemy can attack

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        //Ternary Operator
        //target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x) 
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        //Ternary Operator
        //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

        transform.eulerAngles = rotation;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Bandit>().HurtPlayer(1);
            bandit = collision.GetComponent<Bandit>();
            banditPosition = collision.transform.position;
            enemyPosition = gameObject.transform.position;
        }
    }
    private void HandleEnemyAttacked()
    {
        
        m_body2d.AddForce(new Vector2(-2000f, 1000f));
        enemyHealth--;
        if (enemyHealth == 0)
        {

            Destroy(gameObject);
            goldAmnt.text = (Int32.Parse(goldAmnt.text) + enemeyKillGoldAmnt).ToString();
        }
    }
   
}
