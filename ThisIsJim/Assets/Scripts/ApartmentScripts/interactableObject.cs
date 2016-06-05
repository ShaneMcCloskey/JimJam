using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic; //if using lists
using UnityEngine.Audio;

//these will need to cue up memories (scenes), later

public class interactableObject : MonoBehaviour {

    public memoryManager mmryManager;
	public Animator anim;
	public AudioClip hoverSound;
    public float notHoveringVolume = 0.35f;
    public float hoveringVolume = 1.0f;
    public Transform pointOfInterest; //point jim walks to upon being clicked
    public bool jimFacesLeft = false;

    public string[] foundMoneySentences = new string[] { "Jim found money between his cheeks!" };
    public string[] sentientSentences;
    public string[] noMemoryAvailableSentences = new string[] { "Jim stares blankly" };
	public float typeSpeed = 0.1f;
	public AudioClip typeSound; //frequency is every other character for now
	public GameObject narratorBox;
    Text narratorBoxText;

    public string[] memoryScenes;
    public List<int> memorySceneList = new List<int>();

    public string playerPrompt = "Jim wants to interact?";
	public GameObject promptObject;
	jimControl2D player;

	AudioSource audioSrc;

	Camera camM;
	public cameraMain camScript;
    bool typingObject = false;

	// Use this for initialization
	void Awake() {
        mmryManager = GameObject.FindGameObjectWithTag("MemoryManager").GetComponent<memoryManager>();
        anim = transform.GetComponent <Animator>();
		//narratorBox = GameObject.FindGameObjectWithTag ("TextBox");
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //print("AWAKE" + playerObj.name);
        player = playerObj.GetComponent<jimControl2D>();
		if (gameObject.GetComponent<AudioSource> () != null) {
			audioSrc = gameObject.GetComponent<AudioSource> ();
        }
		camM = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		camScript = camM.gameObject.GetComponent<cameraMain> ();
        narratorBoxText = narratorBox.GetComponent<Text>();
        audioSrc.volume = notHoveringVolume;

        
      

    }

    void Start()
    {
        StartCoroutine(StartDelayed()); //waiting for memoryManager start fcn
    }

    public IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(0.15f);
        //bool removed = false; //this will restart the scene, since the count is changing
        for (int i = 0; i < (memorySceneList.Count); i++)
        {
            print("allllllmost");
            if (mmryManager.memoryExperienced[memorySceneList[i]]) //if memory experienced is equal to true, REMOVE memory from possible memories
            {
                print("should remove from list");
                memorySceneList.RemoveAt(i);
                //memorySceneList.Remove(i);
                StartCoroutine(StartDelayed());
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public bool fadingUp = false;
	public void OnMouseOver(){
		//print (gameObject.name);
		if (anim != null) {
			anim.SetBool ("Hover", true);
			if (hoverSound != null) {
				audioSrc.clip = hoverSound;
                if (!audioSrc.isPlaying)
                {
                    //audio.Play();
             
                }
                if (audioSrc.volume < 1.0f && fadingUp == false) { //FADE UP
                    StartCoroutine(FadeValue(audioSrc.volume, hoveringVolume, 0.3f));
                    fadingUp = true;
                }
                //audio.loop = true;
            }
		}
	}

	public  void OnMouseExit(){
		if (anim != null) {
			anim.SetBool ("Hover", false);
            //audio.loop = false;
            //audio.volume = 0.4f;
            if (audioSrc.volume <= hoveringVolume) //FADE DOWN
            {
                StartCoroutine(FadeValue(audioSrc.volume, notHoveringVolume, 0.5f));
            }
        }
	}

    public IEnumerator FadeValue(float start, float end, float time)
    {
        float startOriginal = start;
        
        float elapsedT = 0.0f;
        while (elapsedT < time)
        {
           // songAudioSource.volume = Mathf.Lerp(0.0f, 1.0f, (elapsedT / t));
            elapsedT += Time.deltaTime;
            audioSrc.volume = Mathf.Lerp(start, end, (elapsedT / time));
            yield return new WaitForEndOfFrame();
        }
        fadingUp = false;
        yield return new WaitForSeconds(0.1f);
    }

	public void typePrompt (){
        if (!typingObject)
        {
            player.graphicHolder.flipX = jimFacesLeft; //Orients jim 

            typingObject = true;
            if (memorySceneList.Count >= 1)    //if (memoryScenes.Length > 0)
            {
                print("type prompt " + gameObject.name);
                promptObject.SetActive(true);
                textTyper.TypeText(narratorBox, playerPrompt, typeSpeed, typeSound);
                promptObject.GetComponent<memoryPrompt>().iObject = gameObject.GetComponent<interactableObject>();

                camScript.FramePlayerAndObject(gameObject);
            }
            else
            {
                textTyper.TypeText(narratorBox, noMemoryAvailableSentences[Random.Range(0, noMemoryAvailableSentences.Length)], typeSpeed, typeSound);
                camScript.FramePlayerAndObject(gameObject);
                StartCoroutine(noMemory());
            }
            StartCoroutine(resetType());
        }
	}

    public IEnumerator resetType() //This prevents the typePrompt from being run more than once at the same time
    {
        yield return new WaitForSeconds(0.5f);
        typingObject = false;
    }

	public void Answer(bool answer){
        typingObject = false;
        if (answer == true)
        { //memoryScenes[0] != null
          // SceneManager.LoadScene(memoryScenes[0], LoadSceneMode.Additive);
            mmryManager.LoadMemory(memorySceneList[Random.Range(0, memorySceneList.Count)]);
        }
        else{
            player.clickToMoveEnabled = true;
            camScript.UnFramePlayerAndObject();
            narratorBoxText.text = ""; //BUG FIX?
        }
        promptObject.SetActive(false);
        narratorBox.SetActive(false);
        anim.SetBool("HoverLock", false);
        player.anim.SetBool("interacting", false);

    }

    public IEnumerator noMemory() //answers NO when no prompt is available
    {
        yield return new WaitForSeconds(1.5f);
        camScript.UnFramePlayerAndObject();
        Answer(false);
    }

	public void sentientInteraction (){
        //sentientSentences [Random.Range (0, sentientSentences.Length)];
        if (!typingObject)
        {
            player.graphicHolder.flipX = jimFacesLeft; //Orients jim 
            typingObject = true;
            textTyper.TypeText(narratorBox, sentientSentences[Random.Range(0, sentientSentences.Length)], typeSpeed, typeSound);
            StartCoroutine(resetType());
        }
        //put Jim found money check here
	}
}
