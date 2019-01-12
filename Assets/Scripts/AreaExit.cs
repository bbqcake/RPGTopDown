using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {

	public string areaToLoad;

	public string areaTransitionName;

	public AreaEntrance theEntrance;

	[SerializeField] float waitToLoad = 1f;

	private bool shouldLoadAfterFade;

	// Use this for initialization

	
	void Start ()
	{
		theEntrance.transitionName = areaTransitionName;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(shouldLoadAfterFade)
		{
			waitToLoad -= Time.deltaTime;
			if (waitToLoad <= 0)
			{
				shouldLoadAfterFade = false;
				SceneManager.LoadScene(areaToLoad);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{			
			shouldLoadAfterFade = true;
			GameManager.instance.fadingBetweenAreas = true;
			
			UIFade.instance.FadeToBlack();

			PlayerController.instance.areaTransitionName = areaTransitionName;
		}
	}
}


