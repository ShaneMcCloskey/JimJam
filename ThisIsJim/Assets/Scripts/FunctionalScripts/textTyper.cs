using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textTyper : MonoBehaviour
{
	/*public static void TypeText(GameObject textElementObject, string text, float time){
		//print ("typing without sound");
		//float characterDelay = time / text.Length;	//time to type whole sentence
		float characterDelay = time; 					//characters per second
		Text textElement = textElementObject.GetComponent<Text> ();
		textElement.text = " "; //wipe text
		textElement.StartCoroutine (SetText (textElementObject, text, characterDelay));
	}*/

	public static void TypeText(GameObject textElementObject, string text, float time, AudioClip typeSound)
    {
		//print ("typing WITH sound");
		float characterDelay = time;
		Text textElement = textElementObject.GetComponent<Text> ();
		textElementObject.SetActive (true);
        textElement.text = " "; //wipe text
        textElement.StartCoroutine (SetText (textElementObject, text, characterDelay, typeSound));
	}

	/*static IEnumerator SetText(GameObject textElementObject, string text, float characterDelay){
		Text textElement = textElementObject.GetComponent<Text> ();
        textElement.text = " ";
		for (int i = 0; i < text.Length; i++) {
			textElement.text += text [i];
			yield return new WaitForSeconds (characterDelay);
		}
	}*/

	static IEnumerator SetText(GameObject textElementObject, string text, float characterDelay,  AudioClip typeSound)
    {
		Text textElement = textElementObject.GetComponent<Text> ();
		AudioSource audioMain = textElementObject.GetComponent<AudioSource>();
		audioMain.clip = typeSound;
        textElement.text = " ";
        yield return new WaitForSeconds(0.15f);
        textElement.text = " ";
        for (int i = 0; i < text.Length; i++)
        {
			textElement.text += text [i];
            if (i % 2 == 0)//is even
            {
                audioMain.Play();
            }
			yield return new WaitForSeconds (characterDelay);
		}
	}
}
