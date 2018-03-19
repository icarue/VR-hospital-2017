using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TYPE {
	UI,
	MONSTER,
	AMBIENT
}

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    public static AudioController instance = null;     //Allows other scripts to call functions from SoundManager.             
	private AudioSource[] _audioSource;
	public AudioData AUDIO { get; private set; }

	#region Mono
    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
		_audioSource = GetComponents<AudioSource> ();
		AUDIO = GetComponent<AudioData> ();
    }
	#endregion

	#region Controls
	public void PLAY(AudioClip clip, TYPE type, float volume = 1.0f) {
		_audioSource[(int)type].PlayOneShot (clip, volume);
	}

	public void STOP(TYPE type){
		AudioSource source = _audioSource [(int)type];
		if (source.isPlaying) {
			_audioSource [(int)type].Stop ();
		}
	}

	public void STOPALL(){
		for (int i = 0; i < _audioSource.Length; i++) {
			_audioSource [i].Stop ();
		}
	}

	public bool isPlaying(TYPE type){
		return _audioSource [(int)type].isPlaying;
	}

	#endregion

	#region Buttons
	//For On Click buttons
	public void MenuClick(){
		PLAY (AUDIO.ButtonClicks,TYPE.UI, 0.5f);
	}

	#endregion



}
