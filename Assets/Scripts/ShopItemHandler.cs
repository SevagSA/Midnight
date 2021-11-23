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

    public GameObject itemPanel;
    public GameObject itemImage;
    public GameObject itemPrice;
    public GameObject itemDescription;

    public TMP_Text goldAmnt;

    void Start() {
        goldAmnt = GameObject.Find("GoldAmntHolder").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() { }

    /**
     * Have a JSON file where you would have all of the info about each item
     *  so, the image name (all images will be in the same directory so you just need the name of the file),
     *  the name of the item, the description, and the price.
     *  
     *  
     *  Then, create a class for an Item object so you can have all of the params of it
     *      (image, description, price, name of item, etc)
     *   
     *   Then, you would have a Item object initialized as a global param (check the line before Start())
     *   
     *   Once you get the itemName from the GetItemData, you're going to fetch the corresponding JSON object
     *   and load that data into the empty ShopItem object.
     *   
     *   Then, you will get all of the GameObjects contained in the IndividualItem panel
     *      (i.e. the descrioptiopn, image, price, etxc.)
     *      
     *  and you will load the appropriate data into those slots -> descript.SetText(shopItem.description) (for example)
        ---------------------------------DONE-------------------------------------
    *  Then, make sure that when he user clicks on the "buy" button, the item will be aqcuired by the player t
     *      (this may mena that the shopItem object and the JSON object need to contain something that
     *      identifies what the power up is.)
     *  and that the correct amount of gold will be reduced.
     * 
     */
    public void GetItemData(String itemName)
    {
        Debug.Log(itemName);

        // Get JSON object of item
        JObject o = JObject.Parse(File.ReadAllText(@"Assets\DataFiles\shopItems.json"));
        
        int price = Int32.Parse(o[itemName]["price"].ToString());
        string description = o[itemName]["description"].ToString();


        itemPanel.SetActive(true);

        itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ShopItems/" + itemName);
        itemPrice.GetComponent<Text>().text = price.ToString() + " Gold";
        itemDescription.GetComponent<Text>().text = description;
    }

    public void CloseItemPanel()
    {
        itemPanel.SetActive(false);
    }

    public void BuyItem()
    {
        // TODO

      //  shopItem = new ShopItem(itemName, description, Int32.Parse(o1[itemName]["price"]));

    }
}
