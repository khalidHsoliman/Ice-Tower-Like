using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed; 
	public float jumpForce; 

	public Transform groundCheck;
	public LayerMask Ground; 
	
	float _velocityX; 
	float _velocityY; 

	bool _facingRight = true; 
	bool isGrounded = false; 
	bool isRunning = false;
	bool isJumping = false; 
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

		moveSpeed = 5f;   
		jumpForce = 50f; 
	}

	void Update () {

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
			isJumping = false; 
		}

		_animator.SetBool("isGrounded", isGrounded); 

		if(isGrounded && Input.GetButtonDown("Jump"))
		{
			doJump(); 
		}else if(_canDoubleJump && Input.GetButtonDown("Jump"))
		{
			isJumping = true; 
			doJump();
			_canDoubleJump = false; 
		}

		_rigidbody.velocity = new Vector2(_velocityX * moveSpeed, _velocityY);

	}

	void doJump()
	{

	}
}
