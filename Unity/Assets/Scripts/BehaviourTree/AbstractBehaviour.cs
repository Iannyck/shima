using UnityEngine;
using System.Collections;

public abstract class AbstractBehaviour : MonoBehaviour {

	public enum State {Running, Suceeded, Failed};

	public abstract State Execute ();

	private State behaviourState;

	// Use this for initialization
	void Start () {
		behaviourState = State.Running;
	}

	// Update is called once per frame
	void Update () {
		behaviourState = Execute ();
	}

}
