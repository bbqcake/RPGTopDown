using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour 
{

	public Image buttonImage;
	public Text amountText;
	public int buttonValue;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Press()
	{
		if (GameMenu.instance.theMenu.activeInHierarchy)
		{
			if(GameManager.instance.itemsHeld[buttonValue] != "")
			{
				GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
			}
		}

		if(Shop.Instance.shopMenu.activeInHierarchy)
		{
			if(Shop.Instance.buyMenu.activeInHierarchy)
			{
				Shop.Instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.Instance.itemsForSale[buttonValue]));
			}

			if(Shop.Instance.sellMenu.activeInHierarchy)
			{
				Shop.Instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
			}
		}
	}
}
