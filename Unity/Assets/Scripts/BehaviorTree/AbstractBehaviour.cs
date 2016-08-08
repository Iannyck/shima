using UnityEngine;
using System.Collections;

public abstract class AbstractBehaviour {

	public enum State {Running, Suceeded, Failed};

	public abstract State Execute ();

}
