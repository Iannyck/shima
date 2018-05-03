using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drawer action.
/// </summary>
public class ActionOpenClose : EntityBehaviour {

	[SerializeField] private Animator anim;

	[SerializeField] private string animBaseName;

	[SerializeField] private bool isOpened = false; 

	public override BTState EBUpdate ()
	{
		if (!isOpened) {
			Debug.Log (animBaseName + "_Open");
			anim.Play (animBaseName + "_Open");
			isOpened = true;
		} else {
			Debug.Log (animBaseName + "_Close");
			anim.Play (animBaseName + "_Close");
			isOpened = false;
		}
		return BTState.STOP;
	}

	public bool IsOpened {
		get {
			return this.isOpened;
		}
	}

}
