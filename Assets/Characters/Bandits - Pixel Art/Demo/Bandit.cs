using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Bandit : MonoBehaviour {

    static float speed = 4.0f;
    float m_speed = speed;
    float m_jumpForce = 7.5f;
    public GameObject healthBar;

    private Transform target;

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

    public int maxHealth = 100;
    public int health = 100;
    public int currentHealth;
    public HealthBar healthB;

    private GameMaster gm;

    // Use this for initialization
    void Start() {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

      //  target = GameObject.FindGameObjectWithTag("HeavyBandit").GetComponent<Transform>();
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

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
        healthB.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            if (!m_isDead)
            {
                m_animator.SetTrigger("Death");
                KillPlayer();
            }
            m_isDead = !m_isDead;
        }

        m_animator.SetTrigger("Hurt");
    }
    void HealPlayer(int addedHealth)
    {
        GameObject currentHealth = GameObject.FindWithTag("PlayerHealthBar");
        Vector3 currentHealthScale = currentHealth.transform.localScale;
        HealthBar healthBar = currentHealth.GetComponent<HealthBar>();

        float newHealthAmnt = healthBar.slider.value + addedHealth;
        if (newHealthAmnt > 1)
        {
            newHealthAmnt = newHealthAmnt - (newHealthAmnt % 1);
        }
        healthBar.SetHealth((int)newHealthAmnt);
    }

    void KillPlayer()
    {
        m_animator.SetTrigger("Death");
        SceneManager.LoadScene("GameOverScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.transform.CompareTag("Enemy"))
        {
            HurtPlayer(10);
            m_body2d.AddForce(new Vector2(-3000f, 100f));
        }
        */
        /*
        if (collision.transform.name == "Enemy")
        {
            HurtPlayer(10);
            if (transform.position.x > collision.transform.position.x)
            {
                m_body2d.AddForce(new Vector2(3000f, 100f));
            }
            else
            {
                m_body2d.AddForce(new Vector2(-3000f, 100f));
            }
        }
        */
        if (collision.transform.name == "HeavyBandit")
        {
            HurtPlayer(10);
            if (transform.position.x > collision.transform.position.x)
            {
                m_body2d.AddForce(new Vector2(9000f, 500f));
            }
            else
            {
                m_body2d.AddForce(new Vector2(-9000f, 500f));
            } 
        }
        if (collision.transform.CompareTag("HealthPickup"))
        {
            HealPlayer(10);
            Destroy(collision.gameObject);
        }
    }
    IEnumerator DelayAction(float time)
    {
        yield return new WaitForSeconds(time);
        HurtPlayer(10);
    } 
}
