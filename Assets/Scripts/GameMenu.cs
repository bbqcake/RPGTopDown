using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour 
{
	public GameObject theMenu;

	private CharStats[] playerStats;
	public GameObject[] windows;

	public Text[] nameText, hpText, mpText, lvlText, expText;
	public Slider[] expSlider;
	public Image[] charImage;
	public GameObject[] charStatHolder;
	public GameObject[] statusButtons;
	public Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEquipped, statusWpnPwr, statusArmorEquipped, statusArmrPwr, statusExp;
	public Image statusImage;

	public ItemButton[] itemButtons;
	public string selectedItem;
	public Item activeItem;
	public Text itemName;
	public Text itemDescription;
	public Text useButtontext;

	public GameObject itemCharChoiceMenu;
	public Text[] itemCharacterChoiceName;	
	public Text goldText;
	public static GameMenu instance;

	public string mainMenuName;




	// Use this for initialization
	void Start () 
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Fire2"))
		{
			if(theMenu.activeInHierarchy)
			{
				//theMenu.SetActive(false);
				//GameManager.instance.gameMenuOpen = false;

				CloseMenu();
			}
			else
			{
				theMenu.SetActive(true);
				UpdateMainStats();
				GameManager.instance.gameMenuOpen = true;
			}			
		}
	}

	public void UpdateMainStats()
	{
		playerStats = GameManager.instance.playerStats;

		for(int i = 0; i < playerStats.Length; i++)
		{
			if(playerStats[i].gameObject.activeInHierarchy)
			{
				charStatHolder[i].SetActive(true);
				nameText[i].text = playerStats[i].charName;
				hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
				mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
				lvlText[i].text = "lvl: " + playerStats[i].playerLevel;
				expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextlevel[playerStats[i].playerLevel];
				expSlider[i].maxValue = playerStats[i].expToNextlevel[playerStats[i].playerLevel];
				expSlider[i].value = playerStats[i].currentEXP;
				charImage[i].sprite = playerStats[i].charIamge;
			}
			else
			{
				charStatHolder[i].SetActive(false);
			}
		}
			goldText.text = GameManager.instance.currentGold.ToString() + "g";
	}

	public void ToggleWindow(int windowNumber)
	{
		UpdateMainStats();

		for(int i = 0; i < windows.Length; i++)
		{
			if (i == windowNumber)
			{
				windows[i].SetActive(!windows[i].activeInHierarchy);
			}
			else
			{
				windows[i].SetActive(false);
			}
		}

		itemCharChoiceMenu.SetActive(false);
	}

	public void CloseMenu()
	{
		for(int i = 0; i <windows.Length; i++)
		{
			windows[i].SetActive(false);
		}
		AudioManager.instance.PlaySFX(5);
		theMenu.SetActive(false);
		
		GameManager.instance.gameMenuOpen = false;
		itemCharChoiceMenu.SetActive(false);

	}

	public void OpenStatus()
	{

		UpdateMainStats();
		
		StatusChar(0); //updates the character info

		for(int i = 0; i <statusButtons.Length; i++)
		{
			statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
			statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
		}

	}

	public void StatusChar(int selected)
	{
		statusName.text = playerStats[selected].charName;
		statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
		statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
		statusStr.text = playerStats[selected].strength.ToString();
		statusDef.text = playerStats[selected].defence.ToString();

		if(playerStats[selected].equippedWpn != "")
		{
			statusWpnEquipped.text = playerStats[selected].equippedWpn;
		}
		statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();

		if(playerStats[selected].equippedArmr != "")
		{
			statusArmorEquipped.text = playerStats[selected].equippedArmr;
		}
		statusArmrPwr.text = playerStats[selected].armrPwr.ToString();
		statusExp.text = (playerStats[selected].expToNextlevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();

		statusImage.sprite = playerStats[selected].charIamge;

	}

	public void ShowItems()
	{
		GameManager.instance.SortItems();

		for(int i = 0; i < itemButtons.Length; i++)
		{
			itemButtons[i].buttonValue = i;

			if(GameManager.instance.itemsHeld[i] != "")
			{
				
				itemButtons[i].buttonImage.gameObject.SetActive(true);
				itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
				itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
			}
			else
			{
				
				itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
			}
		}
	}

	public void SelectItem(Item newItem)
	{
		activeItem = newItem;

		if(activeItem.isItem)
		{
			useButtontext.text = "Use";
		}

		if(activeItem.isWeapon || activeItem.isArmor)
		{
			useButtontext.text = "Equip";
		}

		itemName.text = activeItem.itemName;
		itemDescription.text = activeItem.description;
	}	

	public void DiscardItem()
	{
		if(activeItem != null)
		{
			GameManager.instance.RemoveItem(activeItem.itemName);
		}
	}

	public void OpenItemCharacterChoice()
	{
		itemCharChoiceMenu.SetActive(true);

		for(int i = 0; i < itemCharacterChoiceName.Length; i++)
		{
			itemCharacterChoiceName[i].text = GameManager.instance.playerStats[i].charName;
			itemCharacterChoiceName[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
		}
	}

	public void CloseItemCharacterChoice()
	{
		itemCharChoiceMenu.SetActive(false);
	}	


	public void UseItem(int selectChar)
	{
		activeItem.Use(selectChar);
		CloseItemCharacterChoice();
	}

	public void SaveGame()
	{
		GameManager.instance.SaveData();
		QuestManager.instance.SaveQuestData();
	}

	public void PlayButtonSound()
	{
		AudioManager.instance.PlaySFX(4);
	}

	public void QuitGame()
	{		
		SceneManager.LoadScene(mainMenuName);
		Destroy(GameManager.instance.gameObject);
		Destroy(PlayerController.instance.gameObject);
		Destroy(AudioManager.instance.gameObject);
		Destroy(gameObject);
	}
}
