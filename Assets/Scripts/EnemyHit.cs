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
                Input.GetMouseButtonDown(0))
            {
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
        enemyHealth--;
        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
