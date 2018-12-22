﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	[Header("Item Type")]
	public bool isItem;
	public bool isWeapon;
	public bool isArmor;

	[Header("Item Details")]
	public string itemName;
	public string description;
	public int value;
	public Sprite itemSprite;

	[Header("Affect Details")]
	public int amountToChange;
	public bool affectHP, affectMP, affectStrength;

	[Header("Equipment Details")]
	public int weaponStrength;
	public int armorStrength;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Use(int charToUseOn)
	{
		CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

		if(isItem)
		{
			if(affectHP)
			{
				selectedChar.currentHP += amountToChange;

				if (selectedChar.currentHP > selectedChar.maxHP)
				{
					selectedChar.currentHP = selectedChar.maxHP;
				}

			}

			if(affectMP)
			{
				selectedChar.currentMP += amountToChange;

				if (selectedChar.currentHP > selectedChar.maxMP)
				{
					selectedChar.currentHP = selectedChar.maxMP;
				}
			}

			if(affectStrength)
			{
				selectedChar.strength += amountToChange;
			}
		}

		if(isWeapon)
		{
			if (selectedChar.equippedWpn != "")
			{
				GameManager.instance.AddItem(selectedChar.equippedWpn);				
			}

			selectedChar.equippedWpn = itemName;
			selectedChar.wpnPwr = weaponStrength;
		}

		if(isArmor)
		{
			if (selectedChar.equippedArmr != "")
				{
					GameManager.instance.AddItem(selectedChar.equippedArmr);				
				}

				selectedChar.equippedArmr = itemName;
				selectedChar.armrPwr = armorStrength;
				}

			GameManager.instance.RemoveItem(itemName);
		}

}
