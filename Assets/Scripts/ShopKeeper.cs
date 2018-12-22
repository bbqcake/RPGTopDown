using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour 
{
	private bool canOpen;
	// Use this for initialization

	public string[] itemsForSale;
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !Shop.Instance.shopMenu.activeInHierarchy)
		{
			
			Shop.Instance.itemsForSale = itemsForSale;
			Shop.Instance.OpenShop();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			canOpen = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			canOpen = false;
		}
	}

}
