﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
 {

	 public bool gameMenuOpen;
	 public bool dialogActive;
	 public bool fadingBetweenAreas;

	 public string[] itemsHeld;
	 public int[] numberOfItems;
	 public Item[] referenceItems;

	 public static GameManager instance;

	 public CharStats[] playerStats;

	// Use this for initialization
	void Start () 
	{
		instance = this;

		DontDestroyOnLoad(gameObject);	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameMenuOpen || dialogActive || fadingBetweenAreas)
		{
			PlayerController.instance.canMove = false;
		}
		else
		{
			PlayerController.instance.canMove = true;
		}

		if(Input.GetKey(KeyCode.J))
		{
			AddItem("Iron Armor");
			AddItem("BlahBlah");

			RemoveItem("Health Potion");
			RemoveItem("Bleep");
		}
	}

	public Item GetItemDetails(string itemToGrab)
	{
		for(int i = 0; i < referenceItems.Length; i++)
		{
			if(referenceItems[i].itemName == itemToGrab)
			{
				return referenceItems[i];
			}
		}
		
		return null;
	}

	public void SortItems()
	{
		bool itemAfterSpace = true;

		while (itemAfterSpace)
		{
			itemAfterSpace = false;
			for (int i = 0; i < itemsHeld.Length - 1; i++) // notice the -1 so we don't leave the array
			{
				if(itemsHeld[i] == "")
				{
					itemsHeld[i] = itemsHeld[i + 1];
					itemsHeld[i + 1] = "";

					numberOfItems[i] = numberOfItems[i + 1];
					numberOfItems[i + 1] = 0;

						if(itemsHeld[i] != "")
						{
							itemAfterSpace = true;
						}
				}
			}

		}
	}

	public void AddItem(string itemToAdd)
	{
		int newItemPosition = 0;
		bool foundSpace = false;

		for(int i = 0; i < itemsHeld.Length; i++)
		{
			if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
			{
				newItemPosition = i;
				i = itemsHeld.Length;
				foundSpace = true;
			}
		}

		if(foundSpace)
		{
			bool itemExists = false;
			for(int i = 0; i < referenceItems.Length; i++)
			{
				if(referenceItems[i].itemName == itemToAdd)
				{
					itemExists = true;

					i = referenceItems.Length;
				}
			}

			if(itemExists)
			{
				itemsHeld[newItemPosition] = itemToAdd;
				numberOfItems[newItemPosition]++;
			}
			else
			{
				Debug.LogError(itemToAdd + " Does not exist");
			}
		}

		GameMenu.instance.ShowItems();
	}

	public void RemoveItem(string itemToRemove)
	{
		bool foundItem = false;
		int itemPosition = 0;
		
		for(int i = 0; i < itemsHeld.Length; i++)
		{
			if (itemsHeld[i] == itemToRemove)
			{
				foundItem = true;
				itemPosition = i;

				i = itemsHeld.Length;
			}
		}

		if(foundItem)
		{
			numberOfItems[itemPosition]--;

			if(numberOfItems[itemPosition] <= 0)
			{
				itemsHeld[itemPosition] = "";
			}

			GameMenu.instance.ShowItems();
		}
		else
		{
			Debug.LogError("Couldn't find " + itemToRemove);
		}		
	}
}
