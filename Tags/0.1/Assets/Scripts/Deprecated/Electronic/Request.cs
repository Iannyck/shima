using System.Collections;

/// <summary>
/// Request.
/// </summary>
public class Request {

	private Action actionOnPhase1;
	private Action actionOnPhase2;
	private Action actionOnPhase3;

	private bool isRequestDone;

	public Request (int delta_active_power_phase1, int delta_reactive_power_phase1, int delta_active_power_phase2, 
		int delta_reactive_power_phase2, int delta_active_power_phase3, int delta_reactive_power_phase3)
	{
		actionOnPhase1 = new Action(delta_active_power_phase1, delta_reactive_power_phase1);
		actionOnPhase2 = new Action(delta_active_power_phase2, delta_reactive_power_phase2);
		actionOnPhase3 = new Action(delta_active_power_phase3, delta_reactive_power_phase3);
		isRequestDone = false;
	}

	public void Execute(Phase phase1, Phase phase2, Phase phase3) {
		actionOnPhase1.Execute (phase1);
		actionOnPhase2.Execute (phase2);
		actionOnPhase3.Execute (phase3);
	}

	public bool IsRequestDone {
		get {
			return this.isRequestDone;
		}
		set {
			isRequestDone = value;
		}
	}

	public void Revert() {
		actionOnPhase1.Revert ();
		actionOnPhase2.Revert ();
		actionOnPhase3.Revert ();
		isRequestDone = false;
	}

	/// <summary>
	/// Action.
	/// </summary>
	class Action{
		private int delta_active_power;
		private int delta_reactive_power;
		private Phase phase;

		public Action (int delta_active_power, int delta_reactive_power)
		{
			this.delta_active_power = delta_active_power;
			this.delta_reactive_power = delta_reactive_power;
		}
		public int Delta_active_power {
			get {
				return this.delta_active_power;
			}
		}

		public int Delta_reactive_power {
			get {
				return this.delta_reactive_power;
			}
		}

		public void Execute(Phase phase) {
			phase.AddActive_power (delta_active_power);
			phase.AddReactive_power (delta_reactive_power);
		}

		public void Revert() {
			delta_active_power = delta_active_power * (-1);
			delta_reactive_power = delta_reactive_power * (-1);
		}
	}
}
