using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{

    public GameObject shopPanel;
    public GameObject openShopText;
    public GameObject closeShopText;

    void Start()
    {
        
    }

    void Update()
    {
        if (shopPanel.activeSelf)
        {
            openShopText.SetActive(false);
            if (Input.GetKey(KeyCode.Escape))
            {
                shopPanel.SetActive(false);
                closeShopText.SetActive(false);
            }

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (Input.GetKey("t"))
            {
                shopPanel.SetActive(true);
                closeShopText.SetActive(true);
            }
            else
            {
                openShopText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            shopPanel.SetActive(false);
            openShopText.SetActive(false);
            closeShopText.SetActive(false);
        }
    }
}
