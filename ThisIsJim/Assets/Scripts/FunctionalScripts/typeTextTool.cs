using UnityEngine;
using System.Collections;

public class typeTextTool : MonoBehaviour {

    public string lineToType = "Type Single Line here";
    public float secondsFromStart = 0.5f;               // how long it'll take the typing to kick in
    public float secondsPerCharacter = 0.1f;            // how fast it'll type each character
    public float secondsToDisappear = 0.5f;
    public AudioClip typeSound;                         // frequency of sound is every other character for now
    public GameObject narratorBox;                      // gameObject with the text tool on it

    // Use this for initialization
    void Start () {
        StartCoroutine(TypeText());
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator TypeText()
    {
        yield return new WaitForSeconds(secondsFromStart);
        textTyper.TypeText(narratorBox, lineToType, secondsPerCharacter, typeSound);
        yield return new WaitForSeconds(secondsToDisappear);
        textTyper.TypeText(narratorBox, " ", secondsPerCharacter, typeSound);
    }
}
