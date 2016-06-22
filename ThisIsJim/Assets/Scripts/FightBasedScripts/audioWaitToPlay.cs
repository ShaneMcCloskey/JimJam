using UnityEngine;
using System.Collections;

public class audioWaitToPlay : MonoBehaviour
{
    public float secondsFromStart = 0.5f;               // how long it'll take the typing to kick in

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio()
    {
        yield return new WaitForSeconds(secondsFromStart);
        transform.GetComponent<AudioSource>().Play();
    }
}
