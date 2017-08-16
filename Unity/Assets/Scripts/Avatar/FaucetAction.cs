using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetAction : EntityBehaviour {

	public override BTState EBUpdate ()
	{
		return BTState.STOP;
	}

}
