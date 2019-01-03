using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour 
{
	public static Shop Instance;
	public GameObject shopMenu;
	public GameObject buyMenu;
	public GameObject sellMenu;
	public Text goldText;

	public string[] itemsForSale;

	public ItemButton[] buyItemButtons;
	public ItemButton[] sellItemButtons;

	public Item selectedItem;
	public Text buyItemName;
	public Text buyItemDescription;
	public Text buyItemValue;
	public Text sellItemName;
	public Text sellItemDescription;
	public Text sellItemValue;


	// Use this for initialization
	void Start () 
	{
		Instance = this;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.K) && !shopMenu.activeInHierarchy)
		{
			OpenShop();
		}
	}

	public void OpenShop()
	{
		shopMenu.SetActive(true);
		OpenBuyMenu();

		GameManager.instance.shopActive = true;

		goldText.text = GameManager.instance.currentGold.ToString() + "g";
	}

	public void CloseShop()
	{
		shopMenu.SetActive(false);
		GameManager.instance.shopActive = false;
	}

	public void OpenBuyMenu()
	{
		buyItemButtons[0].Press(); // this is so we dont get into the menu with no item selected

		buyMenu.SetActive(true);
		sellMenu.SetActive(false);

		for(int i = 0; i < buyItemButtons.Length; i++)
		{
			buyItemButtons[i].buttonValue = i;

			if(itemsForSale[i] != "")
			{
				
				buyItemButtons[i].buttonImage.gameObject.SetActive(true);
				buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
				buyItemButtons[i].amountText.text = "";
			}
			else
			{
				
				buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
			}		
	}
	}

	public void OpenSellMenu()
	{
		sellItemButtons[0].Press();

		buyMenu.SetActive(false);
		sellMenu.SetActive(true);

		ShowSellItems();
		
	}

	public void CloseShopMenu()
	{
		buyMenu.SetActive(false);
		sellMenu.SetActive(true);
	}

	private void ShowSellItems()
	{
		GameManager.instance.SortItems();
		for(int i = 0; i < sellItemButtons.Length; i++)
		{
			sellItemButtons[i].buttonValue = i;

			if(GameManager.instance.itemsHeld[i] != "")
			{
				
				sellItemButtons[i].buttonImage.gameObject.SetActive(true);
				sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
				sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
			}
			else
			{
				
				sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
			}
		}
	}

	public void SelectBuyItem(Item buyItem)
	{
		selectedItem = buyItem;
		buyItemName.text = selectedItem.itemName;
		buyItemDescription.text = selectedItem.description;
		buyItemValue.text = "value: " + selectedItem.value + "g";;
	}

	public void SelectSellItem(Item sellItem)
	{
		selectedItem = sellItem;
		sellItemName.text = selectedItem.itemName;
		sellItemDescription.text = selectedItem.description;
		sellItemValue.text = "value: " + Mathf.FloorToInt(selectedItem.value * 0.5f).ToString() + "g";
	}

	public void BuyItem()
	{
		if(selectedItem != null)
		{

		
		if (GameManager.instance.currentGold >= selectedItem.value)
		{
			GameManager.instance.currentGold -= selectedItem.value;

			GameManager.instance.AddItem(selectedItem.itemName);
		}
		goldText.text = GameManager.instance.currentGold.ToString() + "g";
		}
	}

	public void Sellitem()
	{
		if(selectedItem != null)
		{
			GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * 0.5f);

			GameManager.instance.RemoveItem(selectedItem.itemName);
		}

		goldText.text = GameManager.instance.currentGold.ToString() + "g";
		ShowSellItems();

	}

}
