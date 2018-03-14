using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : MonoBehaviour {

	[Header("Doors")]
	public AudioClip DoorOpen;
	public AudioClip DoorCreak;
	public AudioClip DoorClose;
	public AudioClip DoorCloseFail;

	[Header("Ambient")]
	public AudioClip StaticTV;
	public AudioClip AlarmClock;
	public AudioClip BrokenLight;
	public AudioClip RunningOutside;

	[Header("Monster Specific")]
	public AudioClip RunningAttic;
	public AudioClip PlayingInWater;
	public AudioClip SinkningInWater;
	public AudioClip UnderTheBed;
	public AudioClip SlidingTile;

	[Header("Interactive")]
	public AudioClip CurtainsOpen;
	public AudioClip CurtainsClose;
	public AudioClip SwitchOnLight;
	public AudioClip SwitchOffLight;

	[Header("UI Buttons")]
	public AudioClip ButtonClicks;
	public AudioClip StartGame;

}
