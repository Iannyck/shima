using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOpenClose : MonoBehaviour {

	[SerializeField] private Animator anim;

	[SerializeField] private string animBaseName;

	[SerializeField] private bool isOpened = false; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Act() {
		if (!isOpened) {
			Debug.Log (animBaseName + "_Open");
			anim.Play (animBaseName + "_Open");
			isOpened = true;
		} else {
			Debug.Log (animBaseName + "_Close");
			anim.Play (animBaseName + "_Close");
			isOpened = false;
		}
	}

	public bool IsOpened {
		get {
			return this.isOpened;
		}
	}
}
