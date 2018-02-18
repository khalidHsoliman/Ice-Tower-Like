using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

	public bool gameIsOver = false;

	public float timeToBeatTheLevel = 60f;  

	public AudioSource backgroundMusic; 

	public AudioClip playerWin;
	public AudioClip playerLose; 

	public Image fillImage; 
	public Text timeDisplay; 
	public Text victoryDisplay; 
	public Text gameOverDisplay; 
	public GameObject playAgain; 

	float time; 

	GameObject _player; 

	Vector3 _cameraPos;
	Vector3 _spawnPos;  


	void Start () {
		if(fillImage)
			fillImage.gameObject.SetActive(true); 

		if(timeDisplay)
			timeDisplay.gameObject.SetActive(true); 

		if(gameOverDisplay)
			gameOverDisplay.gameObject.SetActive(false); 

		if(victoryDisplay)
			victoryDisplay.gameObject.SetActive(false);

		if(playAgain)
			playAgain.SetActive(false); 

		if(_player == null)
			_player = GameObject.FindGameObjectWithTag("Player"); 

		_spawnPos = _player.transform.position;
		_cameraPos = Camera.main.transform.position; 

		time = timeToBeatTheLevel; 
	}
	
	void Update () {
		if(!gameIsOver)
		{
			if(time < 0f)
			{
				timeDisplay.text = "0";
				_player.GetComponent<PlayerController>().KillPlayer(); 
				return; 
			}	

			time -= Time.deltaTime; 
			timeDisplay.text = time.ToString("0.00");
			fillImage.fillAmount = time / timeToBeatTheLevel; 
		}
	}

	public void EndGame()
	{
		gameIsOver = true; 	
		
		if(gameOverDisplay)
			gameOverDisplay.gameObject.SetActive(true); 

		if(playAgain)
			playAgain.SetActive(true); 

		if(backgroundMusic)
			backgroundMusic.enabled = false; 
	}

	public void Reset()
	{
		gameIsOver = false; 

		time = timeToBeatTheLevel; 

		backgroundMusic.enabled = true; 

		if(gameOverDisplay)
			gameOverDisplay.gameObject.SetActive(false); 

		if(playAgain)
			playAgain.SetActive(false); 

		if(victoryDisplay)
			victoryDisplay.gameObject.SetActive(false);

		_player.GetComponent<PlayerController>().Respawn(_spawnPos, _cameraPos); 
	}

	public void Win()
	{
		gameIsOver = true; 

		if(victoryDisplay)
			victoryDisplay.gameObject.SetActive(true); 

		if(playAgain)
			playAgain.SetActive(true); 			

		if(backgroundMusic)
			backgroundMusic.enabled = false; 
	}
}
