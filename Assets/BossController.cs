using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BossController : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float playerFollowRange;

    private Animator m_animator;
    private Transform target;
    private float enemyToPlayerDistance;

    public GameObject enemyHealthBar;

    private Bandit bandit = null;
    private Vector3 banditPosition;
    private Vector3 enemyPosition;
    private int enemyHealth = 100;

    private TMP_Text goldAmnt;
    public int enemeyKillGoldAmnt = 10;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_animator = GetComponent<Animator>();
        goldAmnt = GameObject.Find("GoldAmntHolder").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        enemyToPlayerDistance = Vector2.Distance(transform.position, target.position);
        if (playerFollowRange > enemyToPlayerDistance && enemyToPlayerDistance > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        if (bandit != null)
        {
            if (Vector3.Distance(banditPosition, enemyPosition) < 5.5 &&
                Input.GetMouseButtonDown(0))
            {
                m_animator.SetTrigger("Hurt");
                HandleEnemyAttacked();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "LightBandit")
        {
            bandit = collision.GetComponent<Bandit>();
            banditPosition = collision.transform.position;
            enemyPosition = gameObject.transform.position;
        }
    }

    private void HandleEnemyAttacked()
    {
        
        enemyHealthBar.transform.localScale = new Vector3((enemyHealth - 1) * 0.01f, 1f);
        
        enemyHealth -= 5;
       
        if (enemyHealth == 0)
        {
            m_animator.SetTrigger("Death");
           // yield WaitForSeconds(0.2f);
            Destroy(gameObject);
            Debug.Log(goldAmnt.text);
            goldAmnt.text = (Int32.Parse(goldAmnt.text) + enemeyKillGoldAmnt).ToString();
        }
    }
   
    
}