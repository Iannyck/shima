using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorOpenDoor : EntityBehaviour {

	public override void EBUpdate ()
	{
		Refrigerator refrigerator = GetComponent<Refrigerator> ();
		refrigerator.Act ();
		IsStarted = false;
	}
}
