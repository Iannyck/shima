using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTWait : SimpleBTAction {

	private int time;

	public SimpleBTWait (int time) : base (0)
	{
		this.time = time;
	}

	public int Time {
		get {
			return this.time;
		}
	}
}
