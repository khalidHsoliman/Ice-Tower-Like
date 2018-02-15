using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

	public Transform target; 

	float _offset; 

	Vector3 m_CurrentVelocity;
	Vector3 m_LastTargetPosition;
	Vector3 m_LookAheadPos; 
	
	void Awake () {
		if(target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player").transform; 
		}
	}

	void Start() {
		_offset = (transform.position - target.position).z; 

		m_LastTargetPosition = transform.position; 
	}
	
	// Update is called once per frame
	void Update () {
			if (target == null)
				return;

            float xMoveDelta = (target.position - m_LastTargetPosition).x;

			m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime);

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*_offset;
			
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, 0f);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
	}
}
