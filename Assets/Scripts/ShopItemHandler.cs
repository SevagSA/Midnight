using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;


public class ShopItemHandler : MonoBehaviour
{
    ShopItem shopItem = new ShopItem();

    public GameObject itemPanel;
    public GameObject itemImage;
    public GameObject itemPrice;
    public GameObject itemDescription;

    void Start() {
    }

    // Update is called once per frame
    void Update() { }


    public void GetItemData(String itemName)
    {
        shopItem.name = itemName;

        // Get JSON object of item
        JObject o = JObject.Parse(File.ReadAllText(@"Assets\DataFiles\shopItems.json"));
        
        shopItem.price = Int32.Parse(o[shopItem.name]["price"].ToString());
        shopItem.description = o[shopItem.name]["description"].ToString();
        shopItem.healthAmount = float.Parse(o[shopItem.name]["healthAmount"].ToString());

        itemPanel.SetActive(true);

        itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ShopItems/" + shopItem.name);
        itemPrice.GetComponent<Text>().text = shopItem.price.ToString() + " Gold";
        itemDescription.GetComponent<Text>().text = shopItem.description;
    }

    public void CloseItemPanel()
    {
        itemPanel.SetActive(false);
    }

    public void BuyItem()
    {   
        TextMeshProUGUI goldAmntHolder;
        int goldAmnt;
        
        goldAmntHolder = GameObject.Find("GoldAmntHolder").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        goldAmnt = Int32.Parse(goldAmntHolder.GetParsedText());
        
        GameObject currentHealth = GameObject.FindWithTag("PlayerHealthBar");
        Vector3 currentHealthScale = currentHealth.transform.localScale;
        HealthBar healthBar = currentHealth.GetComponent<HealthBar>();

        
        float newHealthAmnt = healthBar.slider.value + shopItem.healthAmount;

        if (newHealthAmnt > 1) {
            newHealthAmnt = newHealthAmnt - (newHealthAmnt % 1);
        }

        if (shopItem.price <= goldAmnt) {
            goldAmnt -= shopItem.price;
            goldAmntHolder.SetText(goldAmnt.ToString());

            if (newHealthAmnt > 1)
            {
                newHealthAmnt = newHealthAmnt - (newHealthAmnt % 1);
            }
            Debug.Log(newHealthAmnt);
            healthBar.SetHealth((int)newHealthAmnt);

            CloseItemPanel();
        } else {
            Debug.Log("Not enough gold!");
        }
    }
}
