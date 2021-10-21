using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    private Animator m_animator;


    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        Debug.Log(m_animator);
    }

    /**
    Rigidbody2D enemyRigidBody2D;
    public int UnitsToMove = 5;
    public float EnemySpeed = 500;
    public bool _isFacingRight;
    private float _startPos;
    private float _endPos;

    public bool _moveRight = true;




    void Start()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        _startPos = transform.position.x;
        _endPos = _startPos + UnitsToMove;
        _isFacingRight = transform.localScale.x > 0;
        // rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveRight)
        {
            enemyRigidBody2D.AddForce(Vector2.right * EnemySpeed * Time.deltaTime);
            if (!_isFacingRight)
                Flip();
        }

        if (enemyRigidBody2D.position.x >= _endPos)
            _moveRight = false;

        if (!_moveRight)
        {
            enemyRigidBody2D.AddForce(-Vector2.right * EnemySpeed * Time.deltaTime);
            if (_isFacingRight)
                Flip();
        }
        if (enemyRigidBody2D.position.x <= _startPos)
            _moveRight = true;
        //Vector3 direction = bandit.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        //direction.Normalize();
        //movement = direction;
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }
    private void FixedUpdate()
    {
        //moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction)
    {
        //rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    */
}
