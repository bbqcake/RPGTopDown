using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour 
{
	public string charName;
	public int playerLevel = 1;
	public int currentEXP;
	public int[] expToNextlevel;
	public int maxLevel = 100;
	public int baseEXP = 1000;
	public int currentHP;
	public int maxHP = 100;
	public int currentMP;
	public int maxMP = 30;
	public int[] mpLevelBonus;
	public int strength;
	public int defence;
	public int wpnPwr;
	public int armrPwr;
	public string equippedWpn;
	public string equippedArmr;
	public Sprite charIamge;



	// Use this for initialization
	void Start ()
	{
		expToNextlevel = new int[maxLevel];
		expToNextlevel[1] = baseEXP; // go to position 1

		for(int i = 2; i < expToNextlevel.Length; i++)
		{
			expToNextlevel[i] = Mathf.FloorToInt(expToNextlevel[i - 1] *1.05f); // float to int
		}		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.K))
		{
			AddEXP(500);
		}
	}

	public void AddEXP(int expToAdd)
	{
		currentEXP += expToAdd;
		if (playerLevel < maxLevel)
		{		
			if(currentEXP > expToNextlevel[playerLevel])
			{
				currentEXP -= expToNextlevel[playerLevel];

				playerLevel++;

				//determine wheter to add to str or def based on odd or even
				if(playerLevel%2 == 0) // % means modular. whatevers left
				{
					strength++;
				}
				else
				{
					defence++;
				}

				maxHP = Mathf.FloatToHalf(maxHP * 1.05f);
				currentHP = maxHP; // full health on levle up

				maxMP += mpLevelBonus[playerLevel];
				currentHP = maxMP;

			}
		}

		if(playerLevel >= maxLevel)
		{
			currentEXP = 0;
		}
	}
}
