using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTOClose : SimpleBTAction {

	private GameObject itemToOpen;

	public SimpleBTOClose (GameObject itemToOpen) : base (0)
	{
		this.itemToOpen = itemToOpen;
	}

	public GameObject ItemToOpen {
		get {
			return this.itemToOpen;
		}
		set {
			itemToOpen = value;
		}
	}
	
}
