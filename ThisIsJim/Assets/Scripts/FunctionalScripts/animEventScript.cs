using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class animEventScript : MonoBehaviour {

    public AudioSource audio1;
    public cameraShake camShake;
    //public AudioSource[] audioClips;

    // Use this for initialization
    void Start () {
        audio1 = transform.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PrintEvent (int num)
    {
        print("Hello Event: " + num);
    }

    public void PlayAudio (AudioClip aud)
    {
        audio1.clip = aud;
        audio1.Play();
    }

   public void RandomAudio (GameObject prefab)
    {
        audioAnimEvent audioObject = prefab.GetComponent<audioAnimEvent>();
        audio1.clip = audioObject.DetermineClip();
        audio1.Play();
    }

    public void CameraShake(float shakeMultiplier)
    {
        camShake.Shake(shakeMultiplier);
    }
}
