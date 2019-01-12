﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour 
{
	public static BattleManager instance;

	private bool battleActive;
	public GameObject battleScene;

	public Transform[] playerPositions;
	public Transform[] enemyPosition;
	public BattleChar[] playerPrefabs;
	public BattleChar[] enemyPrefabs;
	public List<BattleChar> activeBattlers = new List<BattleChar>(); // list lets us remove and add. arrays are harder to work with that way

	public int currentTurn;
	public bool turnWaiting;

	public GameObject uiButtonsHolder;

	// Use this for initialization
	void Start () 
	{
		instance = this;
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
			BattleStart(new string[] {"EyeBall", "Spider", "Skeleton", "Spider"});
		}

		if(battleActive)
		{
			if(turnWaiting)
			{
				if(activeBattlers[currentTurn].isPlayer)
				{
					uiButtonsHolder.SetActive(true);
				}
				else
				{
					uiButtonsHolder.SetActive(false);

					// TODO enemy should attack
					StartCoroutine(EnemyMoveCo());
				}
			}

			if(Input.GetKeyDown(KeyCode.N))
			{
				NextTurn();
			}
		}
	}

	public void BattleStart(string[] enemiesToSpawn)
	{
		if(!battleActive)
		{
			battleActive = true;

			GameManager.instance.battleActive = true;

			transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
			battleScene.SetActive(true);

			AudioManager.instance.PlayBGM(0);

			for(int i = 0; i < playerPositions.Length; i++) //checks if player is active
			{
				if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
				{
					for(int j = 0; j < playerPrefabs.Length; j++)
					{
						if(playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
						{
							BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
							newPlayer.transform.parent = playerPositions[i]; // so you can move your players
							activeBattlers.Add(newPlayer);

							CharStats thePlayer = GameManager.instance.playerStats[i];
							activeBattlers[i].currentHP = thePlayer.currentHP;
							activeBattlers[i].maxHP = thePlayer.maxHP;
							activeBattlers[i].currentMP = thePlayer.currentMP;
							activeBattlers[i].maxMP = thePlayer.maxMP;
							activeBattlers[i].strength = thePlayer.strength;
							activeBattlers[i].defence = thePlayer.defence;
							activeBattlers[i].wpnPower = thePlayer.wpnPwr;
							activeBattlers[i].armrPower = thePlayer.armrPwr;


						}
					}


				}
			}

			for(int i = 0; i < enemiesToSpawn.Length; i++)
			{
				if(enemiesToSpawn[i] != "")
				{
					for(int j = 0; j < enemyPrefabs.Length; j++)
					{
						if(enemyPrefabs[j].charName == enemiesToSpawn[i])
						{
							BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPosition[i].position, enemyPosition[i].rotation);
							newEnemy.transform.parent = enemyPosition[i];
							activeBattlers.Add(newEnemy);
						}
						
					}
				}
			}

			turnWaiting = true;
			currentTurn = Random.Range(0, activeBattlers.Count);
		}
	}

	public void NextTurn()
	{
		currentTurn++;
		if(currentTurn >= activeBattlers.Count)
		{
			currentTurn = 0;
		}

		turnWaiting = true;

		UpdateBattle();
	}

	public void UpdateBattle()
	{
		bool allEnemiesDead = true;
		bool allPlayersDead = true;

		for(int i = 0; i < activeBattlers.Count; i++)
		{
			if(activeBattlers[i].currentHP < 0) // <= ?????
			{
				activeBattlers[i].currentHP = 0;
			}

			if(activeBattlers[i].currentHP == 0) 
			{
				//handle dead battler
			}
			else
			{
				if(activeBattlers[i].isPlayer)
				{
					allPlayersDead = false;					
				}
				else
				{
					allEnemiesDead = false;
				}
			}
		}

		if(allEnemiesDead || allPlayersDead)
		{
			if(allEnemiesDead)
			{
				// end battle in victory
			}
			else
			{
				// end battle in failure
			}

			battleScene.SetActive(false);
			GameManager.instance.battleActive = false;
			battleActive = false;			
		}
	}

	public IEnumerator EnemyMoveCo()
	{
		turnWaiting = false;
		yield return new WaitForSeconds(1f);
		EnemyAttack();
		yield return new WaitForSeconds(1f);
		NextTurn();
	}

	public void EnemyAttack()
	{
		List<int> players = new List<int>();

		for (int i = 0; i < activeBattlers.Count; i++)
		{	
			if(activeBattlers[i].isPlayer && activeBattlers[i].currentHP > 0)
			{
				players.Add(i);
			}
		}

		int selectedTarget = players[Random.Range(0, players.Count)];

		activeBattlers[selectedTarget].currentHP -= 20;
	}
}
