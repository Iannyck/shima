using System.Collections;

/// <summary>
/// Request.
/// </summary>
public class Request {

	public enum RequestState : byte {NotStarted, DeltaGiven}

	private Action actionOnPhase1;
	private Action actionOnPhase2;
	private Action actionOnPhase3;

	private RequestState state;

	public Request (int delta_active_power_phase1, int delta_reactive_power_phase1, int delta_active_power_phase2, 
		int delta_reactive_power_phase2, int delta_active_power_phase3, int delta_reactive_power_phase3)
	{
		actionOnPhase1 = new Action(delta_active_power_phase1, delta_reactive_power_phase1);
		actionOnPhase2 = new Action(delta_active_power_phase2, delta_reactive_power_phase2);
		actionOnPhase3 = new Action(delta_active_power_phase3, delta_reactive_power_phase3);
		state = RequestState.NotStarted;
	}

	public void Execute(Phase phase1, Phase phase2, Phase phase3) {
		actionOnPhase1.Execute (phase1);
		actionOnPhase2.Execute (phase2);
		actionOnPhase3.Execute (phase3);
	}

	public RequestState State {
		get {
			return this.state;
		}
		set {
			state = value;
		}
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


	}
}
