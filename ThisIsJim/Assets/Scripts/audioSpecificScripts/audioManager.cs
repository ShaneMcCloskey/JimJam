using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public AudioClip currentSong;
    public AudioSource[] SyncedClips;
    public AudioSource songAudioSource;
    public bool startSongAtRandomPoint = true;
    public float[] startingPoints = new float[] { 0.0f, 15.0f, 40.0f };
    public float songLengthInSeconds = 60.0f;
    public float fadeInTime = 2.0f;

    public AudioMixerSnapshot memorySnap;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
    public bool memoryLoaded = false;

    float randomStartingTime;

    public bool fadeDown = false;

    // Use this for initialization
    void Start ()
    {
        songAudioSource.clip = currentSong;
        if (startSongAtRandomPoint == true)
        {
            randomStartingTime = startingPoints[Random.Range(0, startingPoints.Length)];
            if (randomStartingTime > songLengthInSeconds)
            {
                randomStartingTime = 0.0f;
            }
            PlaySong();

            for (int i = 0; i < SyncedClips.Length; i++)
            {
                float tempStartTime;
                if (randomStartingTime > SyncedClips[i].clip.length)
                {
                    //150, 60,  get 30 from these
                    //150/60 = 2.5. 150 -(60 * 0.5) = 30
                    //155/60 = 2.583. 155 - (60 * 0.583) = 35
                    //randomStartingTime/ SyncedClips[i].clip.length
                    //Mathf.Floor //round down
                    float tempNumber;
                    tempNumber = (randomStartingTime / SyncedClips[i].clip.length);
                    tempStartTime = ((tempNumber - Mathf.Floor(tempNumber))) * randomStartingTime;
                    SyncedClips[i].time = tempStartTime;
                }
                else
                {
                    SyncedClips[i].time = randomStartingTime;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (memoryLoaded == false)
        {
            if (Time.timeScale <= 0.1f)
            {
                paused.TransitionTo(.001f);
            }
            else {
                unpaused.TransitionTo(.01f);
            }
        }
        else
        {
            memorySnap.TransitionTo(0.5f);
        }
    }

    public void PlaySong()
    {
        //print("PlaySong");
        // masterMixer.SetFloat("musicVol", 0.0f);
        //songAudioSource.volume = 0.0f;
        songAudioSource.time = randomStartingTime;
        songAudioSource.Play();
         StartCoroutine(FadeUp());
    }

    public IEnumerator FadeUp()
    {
       //print("FadeUp");
        float t = fadeInTime;
        float elapsedT = 0.0f;
        while (elapsedT < t && !fadeDown)
        {
           songAudioSource.volume = Mathf.Lerp(0.0f, 1.0f, (elapsedT/t));
            elapsedT += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            // masterMixer.SetFloat("musicVol", t);
            print("fadingUp");
        }

        if (fadeDown == true)
        {
            StartCoroutine(FadeDown());
        }
        
        yield return new WaitForSeconds(0.1f);
    }

 

    public IEnumerator FadeDown()   //made really just for the eventTrigger for the demo
    {
        //print("fadingDown??");
        //print("FadeUp");
        float t = fadeInTime;
        float elapsedT = 0.0f;
        while (elapsedT < t)
        {
            songAudioSource.volume = Mathf.Lerp(0.8f, 0.01f, (elapsedT / t));
            elapsedT += Time.deltaTime * 1.25f;
            yield return new WaitForEndOfFrame();
            // masterMixer.SetFloat("musicVol", t);
        }

        yield return new WaitForSeconds(0.1f);
    }


    /*  var elapsedTime : float = 0;
       var startingPos : Vector3 = transform.position;
       while (elapsedTime<time)
       {
           transform.position = Vector3.Lerp(startingPos, newPosition, (elapsedTime / time));
           elapsedTime += Time.deltaTime;
           yield;
       }*/
}
