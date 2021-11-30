using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyHit : MonoBehaviour
{
    private Bandit bandit = null;
    private Vector3 banditPosition;
    private Vector3 enemyPosition;
    private int enemyHealth = 3;

    private TMP_Text goldAmnt;
    public int enemeyKillGoldAmnt = 10;


    // Start is called before the first frame update
    void Start()
    {
        goldAmnt = GameObject.Find("GoldAmntHolder").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bandit != null)
        {
            if (Vector3.Distance(banditPosition, enemyPosition) < 2 &&
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
            goldAmnt.text = (Int32.Parse(goldAmnt.text) + enemeyKillGoldAmnt).ToString();
        }
    }
}
