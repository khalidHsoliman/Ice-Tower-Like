using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject[]  spawnObjects; 
	public GameObject winPlatform; 
	
	public int numOfPlatforms = 100; 
	public float levelWidth = 3f; 
	public float minY = 0.5f; 
	public float maxY = 2.0f; 

	bool _levelGenerated = false; 

	void Update()
	{
		if(!GameManager.Instance.gameIsOver && !_levelGenerated)
		{
			GenerateLevel();
			_levelGenerated = true; 

		}else if(GameManager.Instance.gameIsOver && _levelGenerated)
		{
			_levelGenerated = false; 
			DestroyLevel();
		}
	}

	void GenerateLevel () {
		Vector3 newPos = new Vector3(); 
		GameObject spawnedObject = null; 

		for (int i = 0; i <= numOfPlatforms; i++)
		{	
			newPos.y += Random.Range(minY, maxY); 
			newPos.x  = Random.Range(-levelWidth, levelWidth); 

			if(i < numOfPlatforms)
			{
				int objectToSpawn = Random.Range (0, spawnObjects.Length);
				spawnedObject = Instantiate(spawnObjects[objectToSpawn], newPos, Quaternion.identity);  
			}else if(i == numOfPlatforms)
			{
				if(winPlatform)
					spawnedObject = Instantiate(winPlatform, newPos, Quaternion.identity);
			}
		
			spawnedObject.transform.parent = gameObject.transform;  
		}
	}

	void DestroyLevel()
	{
		foreach (Transform child in transform)
		{
			GameObject.Destroy(child.gameObject); 
		}
	}

}
