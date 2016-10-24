using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract behaviour.
/// An abstract behaviour is 
/// </summary>
public abstract class AbstractBehaviour : MonoBehaviour {

	/// <summary>
	/// State of the AbstractBehaviour.
	/// </summary>
	public enum State {Running, Suceeded, Failed};

	public abstract State Execute ();

	/// <summary>
	/// The current state of the behaviour.
	/// </summary>
	private State behaviourState;

	// Use this for initialization
	void Start () {
		behaviourState = State.Running;
	}

	// Update is called once per frame
	void Update () {
		if (behaviourState == State.Running) {
			behaviourState = Execute ();
		}
	}

}
