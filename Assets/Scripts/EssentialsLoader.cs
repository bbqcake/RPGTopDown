using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

	[SerializeField] GameObject UIScreen;
	[SerializeField] GameObject player;
	public GameObject gameMan;

	public GameObject audioPlayer;

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

		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
