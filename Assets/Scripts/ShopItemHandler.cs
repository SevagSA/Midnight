using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System;


public class ShopItemHandler : MonoBehaviour
{

    ShopItem shopItem;
    public GameObject indivItemPanel;

    void Start()
    {
        //goldAmnt = GameObject.Find("GoldAmntHolder").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
     
     ---------------------------------DONE-------------------------------------
     
     *   Then, you will get/select all of the GameObjects contained in the IndividualItem panel
     *      (i.e. the descrioptiopn, image, price, etxc.)
     *  and you will load the appropriate data into those slots -> descript.SetText(shopItem.description) (for example)
     *  
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
        JObject o1 = JObject.Parse(File.ReadAllText(@"Assets\DataFiles\shopItems.json"));
        string description = o1[itemName]["description"].ToString();
        Debug.Log(description + " | " + description.GetType());

        //shopItem = new ShopItem(itemName, description, Int32.Parse(o1[itemName]["price"]));
    }
}
