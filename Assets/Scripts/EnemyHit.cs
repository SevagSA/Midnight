using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private Bandit bandit = null;
    private Vector3 banditPosition;
    private Vector3 enemyPosition;
    private int enemyHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bandit != null)
        {
            if (Vector3.Distance(banditPosition, enemyPosition) < 1.5 &&
                bandit.is_attacking)
            {
                Debug.Log("attacking");
                HandleEnemyAttacked();
            } else
            {
                Debug.Log("not attacking");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /**
        Bandit bandit = null;
        if (collision.transform.name == "LightBandit")
        {
            bandit = collision.GetComponent<Bandit>();
            Vector3 banditPosition = collision.transform.position;
            Vector3 enemyPosition = gameObject.transform.position;
            
            if (Vector3.Distance(banditPosition, enemyPosition) < 1.5)
            {
                if (bandit.is_attacking)
                {
                    Debug.Log("attacking");
                }
                else
                {
                    Debug.Log("not attacking");
                }
            }
        }

        if (bandit.is_attacking)
        {
            Debug.Log("attacking outside");
        }
        */

        if (collision.transform.name == "LightBandit")
        {
            bandit = collision.GetComponent<Bandit>();
            banditPosition = collision.transform.position;
            enemyPosition = gameObject.transform.position;
        }
    }

    private void HandleEnemyAttacked()
    {
        enemyHealth--;
        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}