using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed = 5f; 
	public float jumpForce = 500f; 

	public bool playerCanMove = true; 

	public Transform groundCheck;
	public LayerMask Ground; 

	public AudioClip playerJump; 
	public AudioClip playerDeath; 
	public AudioClip playerVictory; 

	public GameObject starsEffect; 
	
	float _velocityX; 
	float _velocityY; 

	bool _facingRight = true; 
	bool isGrounded = false; 
	bool isRunning = false;
	bool _canDoubleJump = false; 

	Transform _transform; 
	Animator _animator; 
	AudioSource _audio; 
	Rigidbody2D _rigidbody; 

	void Awake() {
		
		_transform = GetComponent<Transform>();
		_animator = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_audio = GetComponent<AudioSource>();
	}

	void Update () {

		if(!playerCanMove)	
			return;

		_velocityX = Input.GetAxisRaw("Horizontal"); 

		if(_velocityX != 0)
			isRunning = true; 
		else 
			isRunning = false; 

		_animator.SetBool("isRunning", isRunning); 

		_velocityY = _rigidbody.velocity.y; 
		
		isGrounded = Physics2D.Linecast(_transform.position, groundCheck.position, Ground);

		if(isGrounded)
		{
			_canDoubleJump = true; 
		}

		_animator.SetBool("isGrounded", isGrounded); 

		if(isGrounded && Input.GetButtonDown("Jump"))
		{
			doJump(); 
		}else if(_canDoubleJump && Input.GetButtonDown("Jump"))
		{
			doJump();
			Instantiate(starsEffect,transform.position,transform.rotation);
			_canDoubleJump = false; 
		}


		if(Input.GetButtonUp("Jump") && _velocityY > 0f)
		{
			_velocityY = 0f; 
		}

		_rigidbody.velocity = new Vector2(_velocityX * moveSpeed, _velocityY);

	}

	void LateUpdate()
	{
		Vector3 localScale = _transform.localScale; 
		if(_velocityX > 0)
		{
			_facingRight = true; 
		}else if(_velocityX < 0)
		{
			_facingRight = false; 
		}

		if(((_facingRight) && (localScale.x < 0) || (!_facingRight) && (localScale.x > 0)))
		{
			localScale.x *= -1; 
		}

		_transform.localScale = localScale; 
	}

	void doJump()

	{
		_velocityY = 0f;
		_rigidbody.AddForce(new Vector2(0, jumpForce));  

		_audio.PlayOneShot(playerJump); 
	}

	public void FreezePlayer()
	{
		playerCanMove = false; 
		_rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
	}

	public void UnfreezePlayer()
	{
		playerCanMove = true; 
		_rigidbody.constraints = RigidbodyConstraints2D.None; 
		_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation; 

	}

	public void KillPlayer()
	{
		playerCanMove = false; 

		_audio.PlayOneShot(playerDeath); 
		
		if(GameManager.Instance)
			GameManager.Instance.EndGame(); 
	}

	public void Respawn(Vector3 respawnPos, Vector3 cameraPos)
	{
		UnfreezePlayer();

		transform.position = respawnPos; 
		Camera.main.transform.position = cameraPos;
	}

	public void Victory()
	{
		FreezePlayer(); 
		
		_animator.SetTrigger("Victory"); 
		_audio.PlayOneShot(playerVictory); 

		if(GameManager.Instance)
			GameManager.Instance.Win(); 
	}

}
