using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

	ObjectPooler objectPooler;
	
	void Start()
	{
		objectPooler = ObjectPooler.instance; 
	}
	
	void FixedUpdate()
	{
		ObjectPooler.instance.SpawnFromPool("Platform", transform.position, Quaternion.identity); 

	}
}
