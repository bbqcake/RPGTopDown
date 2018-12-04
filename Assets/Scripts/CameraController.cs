using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] Transform target;

	// Use this for initialization
	void Start ()
	{
		target = PlayerController.instance.transform;
	}
	
	// LateUpdate is called once per frame after update
	void LateUpdate () 
	{
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}
}
