using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public float upForce = 10f; 
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>(); 		
			if(rb != null)
			{
				Vector2 velocity = new Vector2(); 
				velocity = rb.velocity; 
				velocity.y = upForce; 
				rb.velocity = velocity;
			}
		}	
	}
}
