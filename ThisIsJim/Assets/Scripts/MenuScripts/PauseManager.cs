using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	public float pauseTimeScale = 0.001f;
	public bool inGame = true; 
	//Canvas canvas;
	public GameObject pauseDisplay;

	// Use this for initialization
	void Start () {
		//canvas = GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			Pause ();
		}
	}

	public void Pause (){
		if (inGame) {
			//canvas.enabled = !canvas.enabled;
			pauseDisplay.SetActive(!pauseDisplay.activeSelf);
			Time.timeScale = Time.timeScale == pauseTimeScale ? 1 : pauseTimeScale;
			Lowpass ();
		}
	}

	void Lowpass(){
		if (Time.timeScale == pauseTimeScale) {
			paused.TransitionTo (.01f);
		} else {
			unpaused.TransitionTo (.01f);
		}
	}

	void OnLevelWasLoaded(int level) {
        Scene currentScene = SceneManager.GetActiveScene();
		if (currentScene.name != "MainMenu") {
			inGame = true;
		} else {
			inGame = false;
			Time.timeScale = 1;
			pauseDisplay.SetActive(false);
			//canvas.enabled = false;
			Lowpass ();
		}
	}

	public void LoadLevel (string levelToLoad){
		Time.timeScale = 1;
		//Application.LoadLevel (levelToLoad);
        SceneManager.LoadScene(levelToLoad);
	}

	public void Quit (){
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}

	public void FullScreen (){
		Screen.fullScreen = !Screen.fullScreen;
	}
}
