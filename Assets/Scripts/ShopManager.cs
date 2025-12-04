using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Dictionary<int, int> shopItems = new Dictionary<int, int>();
    public Text scoreCountText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;

        // Define shop items (ID Price)
        shopItems[1] = 10;
        shopItems[2] = 20;
        shopItems[3] = 30;

        UpdateFundsUI();
    }

    private void Update()
    {
        UpdateFundsUI();
    }

    public void Buy()
    {
        GameObject buttonRef = EventSystem.current.currentSelectedGameObject;
        ButtonInfo buttonInfo = buttonRef.GetComponent<ButtonInfo>();

        int itemID = buttonInfo.ItemID;
        int price = shopItems[itemID];

        if (gameManager.GetScore() >= price)
        {
            // Deduct funds
            gameManager.setScore(gameManager.GetScore() - price);

            UpdateFundsUI();
        }
        else
        {
            Debug.Log("Not enough funds!");
        }
    }

    private void UpdateFundsUI()
    {
        if (scoreCountText != null)
        {
            scoreCountText.text = "Funds: " + gameManager.GetScore();
        }
    }
}
