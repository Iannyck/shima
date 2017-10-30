using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPhase {

	private bool isActive;
	private int activePower;
	private int reactivePower;

	public ElectricPhase (bool isActive)
	{
		this.isActive = isActive;
		this.activePower = 0;
		this.reactivePower = 0;
	}

	public bool IsActive {
		get {
			return this.isActive;
		}
		set {
			isActive = value;
		}
	}

	public int ActivePower {
		get {
			return this.activePower;
		}
	}

	public int ReactivePower {
		get {
			return this.reactivePower;
		}
	}

	public void AddActivePower(int activePower) {
		this.activePower += activePower;
	}

	public void AddReactivePower(int reactivePower) {
		this.reactivePower += reactivePower;
	}

	public override string ToString ()
	{
		return string.Format ("[Phase: Active_power={0}, Reactive_power={1}]", activePower, reactivePower);
	}
}
