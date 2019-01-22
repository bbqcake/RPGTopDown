using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

	[SerializeField] GameObject UIScreen;
	[SerializeField] GameObject player;
	public GameObject gameMan;
	public GameObject audioPlayer;
	//public GameObject battleManager;

	// Use this for initialization
	void Start () 
	{
		if (UIFade.instance == null)
		{
			UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
		}

		if (PlayerController.instance == null)
		{
			PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
			PlayerController.instance = clone;
		}

		if(GameManager.instance == null)
		{
			Instantiate(gameMan);
		}

		if(AudioManager.instance == null)
		{
			Instantiate(audioPlayer);
		}
/* 
		if(BattleManager.instance == null)
		{
			Instantiate(battleManager);
		}
*/
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
