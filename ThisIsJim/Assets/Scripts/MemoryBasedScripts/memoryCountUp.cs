using UnityEngine;
using System.Collections;

public class memoryCountUp : MonoBehaviour {

    //Placed on an object in a memory scene. Looks for the tag "SerializedMain", grabs savedInfo, adds 1 to the memories experienced count
    //and saves. The detection for the apartment count to the end should only be in the start function so the additive loading
    //doesn't confuse the end-game count.

    public savedInfo savedGame;
    public int skillToUnlock = 2;

    // Use this for initialization
    void Start () {
        savedGame = GameObject.FindWithTag("SerializedMain").GetComponent<savedInfo>();
        savedGame.memoriesExperienced++;
        savedGame.Save();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
