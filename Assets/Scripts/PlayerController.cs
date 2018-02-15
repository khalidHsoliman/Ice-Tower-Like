using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed = 5f; 
	public float jumpForce = 500f; 

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
	}
}
