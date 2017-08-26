using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarControl : MonoBehaviour {

	private enum ControlMode
	{
		Tank,
		Direct
	}

	[SerializeField] private float m_moveSpeed = 2;
	[SerializeField] private float m_turnSpeed = 100; //200

	[SerializeField] private Animator m_animator;
	[SerializeField] private Rigidbody m_rigidBody;

	[SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

	private float m_currentV = 0;
	private float m_currentH = 0;

	private readonly float m_interpolation = 10;
	private readonly float m_walkScale = 0.33f;
	private readonly float m_backwardsWalkScale = 0.16f;
	private readonly float m_backwardRunScale = 0.66f;

	private bool m_wasGrounded;
	private Vector3 m_currentDirection = Vector3.zero;

	private bool m_isGrounded;

	// Use this for initialization
	void Start () {
		
	}

	private List<Collider> m_collisions = new List<Collider>();

	private void OnCollisionEnter(Collision collision)
	{
		ContactPoint[] contactPoints = collision.contacts;
		for(int i = 0; i < contactPoints.Length; i++)
		{
			if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
			{
				if (!m_collisions.Contains(collision.collider)) {
					m_collisions.Add(collision.collider);
				}
				m_isGrounded = true;
			}
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		ContactPoint[] contactPoints = collision.contacts;
		bool validSurfaceNormal = false;
		for (int i = 0; i < contactPoints.Length; i++)
		{
			if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
			{
				validSurfaceNormal = true; break;
			}
		}

		if(validSurfaceNormal)
		{
			m_isGrounded = true;
			if (!m_collisions.Contains(collision.collider))
			{
				m_collisions.Add(collision.collider);
			}
		} else
		{
			if (m_collisions.Contains(collision.collider))
			{
				m_collisions.Remove(collision.collider);
			}
			if (m_collisions.Count == 0) { m_isGrounded = false; }
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if(m_collisions.Contains(collision.collider))
		{
			m_collisions.Remove(collision.collider);
		}
		if (m_collisions.Count == 0) { m_isGrounded = false; }
	}

	// Update is called once per frame
	void Update () {
		m_animator.SetBool("Grounded", m_isGrounded);

		switch(m_controlMode)
		{
		case ControlMode.Direct:
			DirectUpdate();
			break;

		case ControlMode.Tank:
			TankUpdate();
			break;

		default:
			Debug.LogError("Unsupported state");
			break;
		}

		m_wasGrounded = m_isGrounded;
	}

	private void TankUpdate()
	{
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");

		bool walk = Input.GetKey(KeyCode.LeftShift);

		if (v < 0) {
			if (walk) { v *= m_backwardsWalkScale; }
			else { v *= m_backwardRunScale; }
		} else if(walk)
		{
			v *= m_walkScale;
		}

		m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
		m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

		transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
		transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

		m_animator.SetFloat("MoveSpeed", m_currentV);

	}

	private void DirectUpdate()
	{
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");

		Transform camera = Camera.main.transform;

		if (Input.GetKey(KeyCode.LeftShift))
		{
			v *= m_walkScale;
			h *= m_walkScale;
		}

		m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
		m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

		Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

		float directionLength = direction.magnitude;
		direction.y = 0;
		direction = direction.normalized * directionLength;

		if(direction != Vector3.zero)
		{
			m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

			transform.rotation = Quaternion.LookRotation(m_currentDirection);
			transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

			m_animator.SetFloat("MoveSpeed", direction.magnitude);
		}

	}

	public void PickUp()
	{
		m_animator.SetTrigger ("Pickup");
	}

}
