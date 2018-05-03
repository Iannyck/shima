using UnityEngine;
using System.Collections;

public class PhoneDevice : UsableDevice {

	public AudioClip PhoneRing;
	private AudioSource audio;

	protected override bool Init ()
	{
		if (audio == null) {
			audio = gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
			audio.clip = PhoneRing;
		}
		return true;
	}

	protected override State OnOn ()
	{
		if (audio.isPlaying) {
			audio.Stop ();
		}
		return State.Opening;
	}

	protected override State OnClose ()
	{
		return State.Closing;
	}

	protected override State OnOpening ()
	{
		return State.On;
	}

	protected override State OnClosing ()
	{
		return State.Off;
	}

	public void Ring() {
		if (!IsInit) {
			this.Init ();
		}
		if (!audio.isPlaying)
			audio.Play ();
	}

	public void StopRing() {
		if (!IsInit) {
			this.Init ();
		}
		if (audio.isPlaying)
			audio.Stop ();
	}

}
