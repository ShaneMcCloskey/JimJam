using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioLevels : MonoBehaviour {

	public AudioMixer masterMixer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSfxLvl(float sfxLvl){
		masterMixer.SetFloat ("sfxVol", sfxLvl);
	}
	
	public void SetMusicLvl(float musicLvl){
		masterMixer.SetFloat ("musicVol", musicLvl);
	}
}
