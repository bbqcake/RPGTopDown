using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour
 {
	 public static BattleReward instance;
	 public Text xpText, itemtext;
	 public GameObject rewardScreen;

	 public string[] rewardItems;
	 public int xpEarned;

	// Use this for initialization
	void Start () 
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			OpenRewardScreen(54, new string[] { "Iron sword", "Iron armor"});
		}
	}

	public void OpenRewardScreen(int xp, string[] rewards)
	{
		xpEarned = xp;
		rewardItems = rewards;

		xpText.text = "Everyone earned " + xpEarned + " exp!";
		itemtext.text = "";

		for (int i = 0; i < rewardItems.Length; i++)
		{
			itemtext.text += rewards[i] + "\n";
		}

		rewardScreen.SetActive(true);
	}

	public void CloseRewardscreen()
	{

	}
}
