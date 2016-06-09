using System.Collections;

/// <summary>
/// Phase.
/// </summary>
public class Phase {
	private int active_power;
	private int reactive_power;

	public Phase (int active_power, int reactive_power)
	{
		this.active_power = active_power;
		this.reactive_power = reactive_power;
	}

	public int Active_power {
		get {
			return this.active_power;
		}
	}

	public int Reactive_power {
		get {
			return this.reactive_power;
		}
	}

	public void AddActive_power(int delta) {
		active_power += delta;
	}

	public void AddReactive_power(int delta) {
		reactive_power += delta;
	}

	public override string ToString ()
	{
		return string.Format ("[Phase: Active_power={0}, Reactive_power={1}]", active_power, reactive_power);
	}
	
	
}
