using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
 {

	 public bool gameMenuOpen;
	 public bool dialogActive;
	 public bool fadingBetweenAreas;

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
	}
}
