using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTActivate : SimpleBTAction {

	private GameObject itemToActivate;

	public SimpleBTActivate (int forgetErrorRate, GameObject itemToActivate) : base (forgetErrorRate)
	{
		this.itemToActivate = itemToActivate;
	}

	public GameObject ItemToActivate {
		get {
			return this.itemToActivate;
		}
		set {
			itemToActivate = value;
		}
	}
}
