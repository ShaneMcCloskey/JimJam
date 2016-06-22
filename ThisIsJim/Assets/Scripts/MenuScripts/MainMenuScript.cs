using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuScript : MonoBehaviour
{
	public bool gameStarted = false;//Game not started -> main menu
			//Game started -> hide main menu buttons, reveal extra in options menu
	//public AudioMixer masterMixer;

	public void LoadLevel (string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
	}

	public void OpenWebsite (string siteToOpen)
    {
		Application.OpenURL (siteToOpen);
	}

	public void Quit ()
    {
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}
