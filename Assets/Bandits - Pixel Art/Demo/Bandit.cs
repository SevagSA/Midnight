using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Bandit : MonoBehaviour {

    static float speed = 4.0f;
    float m_speed = speed;
    float m_jumpForce = 7.5f;
    public GameObject healthBar;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;
    private bool wallJumping;

    bool hasDoubleJumped = false;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    int health = 100;

    private GameMaster gm;

    // Use this for initialization
    void Start() {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }

    // Update is called once per frame
    void Update() {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // Wall Jumping
        if (Input.GetKeyDown("space") && m_body2d.velocity.y == 0 && !m_grounded)
        {
            wallJumping = true;
        }

        if (wallJumping)
        {
            Jump();
            Invoke("SetWallJumpToFalse", 0.08f);
            m_grounded = true;
        }

        // Double Jumping
        if (Input.GetKeyDown("space") && !m_grounded && !hasDoubleJumped)
        {
            Jump();
            hasDoubleJumped = true;
        }

        // -- Handle Animations --
        //Death
        if (health == 0) {
            if (!m_isDead)
            {
                KillPlayer();
            }
            m_isDead = !m_isDead;
        }

        else if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            m_animator.SetTrigger("Death");
            m_animator.SetTrigger("Recover");
        }

        //Attack
        else if (Input.GetMouseButtonDown(0)) {
            m_animator.SetTrigger("Attack");
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded) {
            Jump();
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    private void Jump()
    {
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        hasDoubleJumped = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    private void SetWallJumpToFalse()
    {
        wallJumping = false;
    }

    private void ResetCharacterSpeed() {
        m_speed = speed;
    }

    void HurtPlayer(int damage)
    {
        healthBar.transform.localScale = new Vector3((health - damage) * 0.01f, 1f);
        health -= damage;
        m_animator.SetTrigger("Hurt");
    }
    void HealPlayer(int addedHealth)
    {
        if (health != 100)
        {
            healthBar.transform.localScale = new Vector3((health + addedHealth) * 0.01f, 1f);
            health += addedHealth;
        }

    }
    void KillPlayer()
    {
        m_animator.SetTrigger("Death");
        SceneManager.LoadScene("GameOverScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Idle_1")
        {
            HurtPlayer(10);
            m_body2d.AddForce(new Vector2(-3000f, 100f));
        }
        if (collision.transform.name == "HeavyBandit")
        {
            HurtPlayer(10);
            m_body2d.AddForce(new Vector2(-3000f, 100f));
        }
        if (collision.transform.name == "HealthPickUp")
        {
            HealPlayer(10);
            Destroy(collision.gameObject);
        }
    }
}
