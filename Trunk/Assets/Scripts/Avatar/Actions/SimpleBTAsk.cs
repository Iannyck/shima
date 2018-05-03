using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTAsk : SimpleBTAction {

	private int time;

	public SimpleBTAsk (int time) : base (0)
	{
		this.time = time;
	}

	public int Time {
		get {
			return this.time;
		}
	}
	
}
