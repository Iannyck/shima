using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract behaviour.
/// An abstract behaviour is 
/// </summary>
public abstract class AbstractBehaviour : MonoBehaviour {


	public bool AutoRun = false;

	/// <summary>
	/// State of the AbstractBehaviour.
	/// </summary>
	public enum State {Stopped, Running, Suceeded, Failed};

	/// <summary>
	/// The name of the AbstractBehaviour.
	/// </summary>
	public string BName;

	public abstract void Init ();

	public abstract State Execute ();

	/// <summary>
	/// The current state of the behaviour.
	/// </summary>
	private State behaviourState;

	public State BehaviourState {
		get {
			return this.behaviourState;
		}
	}

	/// <summary>
	/// Run this instance.
	/// </summary>
	public void Run() {
		behaviourState = State.Running;
	}

	/// <summary>
	/// Pause this instance.
	/// </summary>
	public void Pause() {
		behaviourState = State.Stopped;
	}

	// Use this for initialization
	void Start () {
		behaviourState = State.Stopped;
		Init ();
		if (AutoRun)
			Run ();
	}

	// Update is called once per frame
	void Update () {
		if (behaviourState == State.Running) {
			behaviourState = Execute ();
		}
	}

}
