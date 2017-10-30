using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Avatar motion. This script contains all methods that manage avatar's motion.
/// </summary>
public class AvatarMotion : MonoBehaviour {

	/// <summary>
	/// The move speed of the avatar.
	/// </summary>
	[SerializeField] private float m_moveSpeed = 2;

	/// <summary>
	/// The turn speed of the avatar.
	/// </summary>
	[SerializeField] private float m_turnSpeed = 100; //200

	/// <summary>
	/// The animator of the avatar.
	/// </summary>
	private Animator m_animator;

	/// <summary>
	/// The RigidBody of the avatar.
	/// </summary>
	private Rigidbody m_rigidBody;

	/// <summary>
	/// Used to compute the direction vector.
	/// </summary>
	private float m_currentV = 0;

	/// <summary>
	/// Used to compute the direction vector.
	/// </summary>
	private float m_currentH = 0;

	/// <summary>
	/// The interpolation used to compute direction.
	/// </summary>
	private readonly float m_interpolation = 10;

	/// <summary>
	/// The walk scale used in the tank mode.
	/// </summary>
	private readonly float m_walkScale = 0.33f;

	/// <summary>
	/// The walk scale used in the tank mode.
	/// </summary>
	private readonly float m_backwardsWalkScale = 0.16f;

	/// <summary>
	/// The backward run scale used in the tank mode.
	/// </summary>
	private readonly float m_backwardRunScale = 0.66f;

	/// <summary>
	/// Indicate if the avatar was grounded.
	/// </summary>
	private bool m_wasGrounded;

	/// <summary>
	/// The current direction vector.
	/// </summary>
	private Vector3 m_currentDirection = Vector3.zero;

	/// <summary>
	/// Indicate if the avatar was grounded.
	/// </summary>
	private bool m_isGrounded;

	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator> ();
		m_rigidBody = GetComponent<Rigidbody> ();
	}

	/// <summary>
	/// The list of elements that collide with the avatar.
	/// </summary>
	private List<Collider> m_collisions = new List<Collider>();

	/// <summary>
	/// Raises the collision enter event.
	/// Adds element that collide with the avatar to the list.
	/// </summary>
	/// <param name="collision">Collision.</param>
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

	/// <summary>
	/// Raises the collision stay event.
	/// </summary>
	/// <param name="collision">Collision.</param>
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

	/// <summary>
	/// Raises the collision exit event.
	/// Remove elements that do not collide with the avatar.
	/// </summary>
	/// <param name="collision">Collision.</param>
	private void OnCollisionExit(Collision collision)
	{
		if(m_collisions.Contains(collision.collider))
		{
			m_collisions.Remove(collision.collider);
		}
		if (m_collisions.Count == 0) { m_isGrounded = false; }
	}

	/// <summary>
	/// To control avatar like a tank. He rotates on himself.
	/// </summary>
	/// <param name="verticalAxis">Vertical axis.</param>
	/// <param name="horizontalAxis">Horizontal axis.</param>
	public void TankUpdate(float verticalAxis, float horizontalAxis)
	{
		bool walk = Input.GetKey(KeyCode.LeftShift);

		if (verticalAxis < 0) {
			if (walk) { verticalAxis *= m_backwardsWalkScale; }
			else { verticalAxis *= m_backwardRunScale; }
		} else if(walk)
		{
			verticalAxis *= m_walkScale;
		}

		m_currentV = Mathf.Lerp(m_currentV, verticalAxis, Time.deltaTime * m_interpolation);
		m_currentH = Mathf.Lerp(m_currentH, horizontalAxis, Time.deltaTime * m_interpolation);

		transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
		transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

		m_animator.SetFloat("MoveSpeed", m_currentV);

	}

	/// <summary>
	/// To directly control the avatar. He does not rotate on himself. 
	/// </summary>
	/// <param name="verticalAxis">Vertical axis.</param>
	/// <param name="horizontalAxis">Horizontal axis.</param>
	public void DirectUpdate(float verticalAxis, float horizontalAxis)
	{
		Transform camera = Camera.main.transform;

		if (Input.GetKey(KeyCode.LeftShift))
		{
			verticalAxis *= m_walkScale;
			horizontalAxis *= m_walkScale;
		}

		m_currentV = Mathf.Lerp(m_currentV, verticalAxis, Time.deltaTime * m_interpolation);
		m_currentH = Mathf.Lerp(m_currentH, horizontalAxis, Time.deltaTime * m_interpolation);

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

	/// <summary>
	/// To start picks up aniamation.
	/// </summary>
	public void PickUp()
	{
		m_animator.SetTrigger ("Pickup");
	}
}
