using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
	//Generic Save script
	//Assign in inspector:
	public int 							saveSlot = 0;
	public UnityEngine.UI.Text 			saveNameInput; //only used on a new game
	public UnityEngine.UI.Text[] 		slotName;
	public UnityEngine.UI.Image[]		screenShot;
	public UnityEngine.UI.Button		loadButton; //active if there is a game to load
	public GameObject					clearButton; //active if there is a game to load

	public bool 						loaded = false; //true = selected in main menu
	public bool 						gameStarted = false; //true = exited main menu

	//Saved variables
	public int 							level = 1;
	public string 						saveName;

	public void Awake()
	{
		Start ();
	}

	// Use this for initialization
	void Start ()
    {
		//loadButton.interactable = false;
		clearButton.SetActive (false);
		DontDestroyOnLoad (this);
		if (File.Exists (Application.persistentDataPath + "/playerInfo" + saveSlot + ".dat"))
        {
			loadButton.interactable = true;
			clearButton.SetActive (true);
		}

		Load ();
		for (int i = 0; i < slotName.Length; i++)
        {
			slotName[i].text = saveName;
		}
	}

	public void Save ()
    {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo" + saveSlot + ".dat");

		PlayerData data = new PlayerData ();
		data.saveName = saveName;
		data.level = level;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load ()
    {
		if (File.Exists (Application.persistentDataPath + "/playerInfo" + saveSlot + ".dat"))
        {
			print ("load file exists");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo" + saveSlot + ".dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			saveName = data.saveName;
			level = data.level;
			//slotName.text = data.saveName;
			for (int i = 0; i < slotName.Length; i++)
            {
				slotName [i].text = saveName;
			}
			clearButton.SetActive (true);
			loadButton.interactable = true;
		}
        else
        {
			print ("no load file");
			clearButton.SetActive (false);
			for (int j = 0; j < slotName.Length; j++)
            {
				slotName [j].text = "Slot " + saveSlot;
				//loadButton.interactable = false;
			}
		}
	}

	public void Clear()
    {
		print ("clear");
		if (File.Exists (Application.persistentDataPath + "/playerInfo" + saveSlot + ".dat"))
        {
			File.Delete (Application.persistentDataPath + "/playerInfo" + saveSlot + ".dat");
			clearButton.SetActive (false);
		}
	}

	public void LoadAndStart ()
    {
		if (File.Exists (Application.persistentDataPath + "/playerInfo" + saveSlot + ".dat"))
        {
			loaded = true;
            SceneManager.LoadScene(level);
		}
        else
        {
			//empty file
		}
	}

	public void Select(bool selected)
    {
		if (selected)
        {
			loaded = true;
			saveName = saveNameInput.text;
		}
        else
        { 
			loaded = false;
		}
	}

	void OnLevelWasLoaded(int levelNumber)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "MainMenu" && loaded == false)
        {
			Destroy (gameObject);
		}
		if (currentScene.name != "MainMenu")
        {
			gameStarted = true;
		}
		if (currentScene.name == "MainMenu" && gameStarted)
        { 
			Destroy (gameObject);
			Awake();
		}
		level = levelNumber;
	}
}

[Serializable]
class PlayerData
{
	public string 						saveName;
	public int 							level = 0;
}
