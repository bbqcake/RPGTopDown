﻿using System.Collections;
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
		buyMenu.SetActive(true);
		sellMenu.SetActive(false);
	}

		public void OpenSellMenu()
	{
		buyMenu.SetActive(false);
		sellMenu.SetActive(true);
	}

	public void CloseShopMenu()
	{
		buyMenu.SetActive(false);
		sellMenu.SetActive(true);
	}

}