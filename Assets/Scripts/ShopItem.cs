public class ShopItem
{
    // the image will have the same name
    public string name { get; set; }
    public string description { get; set; }
    public int price { get; set; }

    public ShopItem(string name, string description, int price) {
        this.name = name;
        this.description = description;
        this.price = price;
    }
}
