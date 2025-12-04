using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceText;
    public Text NameText;
    public ShopManager shopManager; 

    void Start()
    {
        if (shopManager != null)
        {
            int price = shopManager.shopItems[ItemID];
            PriceText.text = "Price: $" + price.ToString();
        }
    }
}
