using UnityEngine;
using System.Collections;

public class memoryPrompt : MonoBehaviour
{
	//yes or no box to initiate the memory

	public interactableObject iObject; //object giving the prompt

	public void Answer(bool answer)
    {
		print ("ANSWERED");
		iObject.Answer (answer);
	}
}
