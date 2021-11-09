using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Bandit bandit = null;
    private Vector3 banditPosition;
    private Vector3 enemyPosition;
    private int banditHealth = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bandit != null)
        {
            if (Vector3.Distance(enemyPosition, banditPosition) < 1.5)
            {
                HandleEnemyAttacked();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Idle_1")
        {
            bandit = collision.GetComponent<Bandit>();
            banditPosition = collision.transform.position;
            enemyPosition = gameObject.transform.position;
        }
    }
    private void HandleEnemyAttacked()
    {
        banditHealth--;
        if (banditHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
