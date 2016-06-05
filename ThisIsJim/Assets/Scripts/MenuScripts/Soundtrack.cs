using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Soundtrack : MonoBehaviour {
	//Soundtrack on a level by level basis
	public AudioSource mainMusic;
	public AudioClip[] clips = new AudioClip[2];
	public string[] levels = new string[2];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded(int level) {
		//if (level == 13)
			//print("Woohoo");
		for (int i = 0; i < clips.Length; i++){
            Scene loadedScene = SceneManager.GetActiveScene();
			if (loadedScene.name == levels[i]){
				mainMusic.clip = clips[i];
				mainMusic.Play ();
			}
		}
	}
}
