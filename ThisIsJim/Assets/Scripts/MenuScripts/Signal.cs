using UnityEngine;
using System.Collections;

public class Signal : MonoBehaviour {
	//Will find object with tag SaveSlot and send "Save" message to that object

	public string tagToFind = "SaveSlot";
	public string messageToSend = "Save";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SignalObject (){
		GameObject.FindGameObjectWithTag (tagToFind).SendMessage(messageToSend);
	}

	public void SignalObjects (){
		GameObject[] objectArray = GameObject.FindGameObjectsWithTag (tagToFind);
		for (int i = 0; i < objectArray.Length; i++) {
			objectArray[i].SendMessage(messageToSend);
		}
	}
}
