using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
 {
	[SerializeField] Rigidbody2D rigidbody2D;
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] Animator animator;
	public static PlayerController instance;

	public string areaTransitionName;

	//public Rigidbody2D rigidbody2D;


	// Use this for initialization
	void Start ()	
	{
		if (instance == null)
		{
			instance = this; // this instance value has to be equal to this script
		}
		else
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{		
		rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

		animator.SetFloat("moveX", rigidbody2D.velocity.x);
		animator.SetFloat("moveY", rigidbody2D.velocity.y);

		if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
		{
			animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
			animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
		}
	}
}
