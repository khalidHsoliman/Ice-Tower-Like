using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

	public Transform target; 
	public float smoothTime = 1f; 

	Vector3 _currentVelocity; 
	
	void Awake () {
		if(target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player").transform; 
		}
	}

	void Update () {
	
		if (target == null)
			return;

		if(target.position.y > transform.position.y)
		{
			Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.y); 
			transform.position = Vector3.SmoothDamp(transform.position, newPos, ref _currentVelocity, smoothTime); 
		}
	}
}
