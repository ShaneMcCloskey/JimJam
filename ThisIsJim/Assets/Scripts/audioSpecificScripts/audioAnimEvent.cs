using UnityEngine;
using System.Collections;

public class audioAnimEvent : MonoBehaviour {

    public AudioClip[] audioClips;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public AudioClip DetermineClip()
    {
        int chooseClip;
        AudioClip clip;
        chooseClip = Random.Range(0, audioClips.Length);
        clip = audioClips[chooseClip];
        return clip; 
    }
}
