using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drawer action.
/// </summary>
public class DrawerAction : EntityBehaviour {

	private GameObject pivot;

	[SerializeField] private float xRotation;
	[SerializeField] private float yRotation;
	[SerializeField] private float zRotation;

	[SerializeField] private float xTranslate;
	[SerializeField] private float yTranslate;
	[SerializeField] private float zTranslate;

	public override BTState EBUpdate ()
	{
		return BTState.STOP;
	}


}
