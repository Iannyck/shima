using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGoTo : SimpleBTAction {

	private string posToGo;

	public SimpleGoTo (string posToGo) : base (0)
	{
		this.posToGo = posToGo;
	}

	public string PosToGo {
		get {
			return this.posToGo;
		}
	}



}
