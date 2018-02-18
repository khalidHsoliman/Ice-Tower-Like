using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.CompareTag("Player"))
		{
			if(GameManager.Instance)
			{
				if(!GameManager.Instance.gameIsOver)
					collider.gameObject.GetComponent<PlayerController>().KillPlayer(); 
			}
		}
	}
}
