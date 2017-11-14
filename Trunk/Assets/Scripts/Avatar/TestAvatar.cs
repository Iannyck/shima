using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAvatar : MonoBehaviour {


	public GameObject freezer;
	public GameObject shelf1;
	public GameObject shelf2;

	public string toGo;

	public GameObject mug;

	private bool init;

	private AvatarGoTo avatarGoTo;

	private enum Action
	{
		GoTo,
		PickUpRelease,
		OpenClose,
	};
		
	// Use this for initialization
	void Start () {
		init = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!init) {
			avatarGoTo = GetComponent<AvatarGoTo> ();
			if (avatarGoTo != null) {
				avatarGoTo.GoTo (toGo);
				init = true;
			}
		}
	}


	private void PutInShelf1() {
	}

	private void PutInShelf2() {
	}

	private void TakeFromFeezer() {
	}

	private void PutInFeezer() {
	}

	private void Open() {
	}
}
