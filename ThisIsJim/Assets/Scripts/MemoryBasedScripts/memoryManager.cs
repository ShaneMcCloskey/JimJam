using UnityEngine;
using System.Collections;
using System.Collections.Generic; //if using lists
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class memoryManager : MonoBehaviour {

    public savedInfo saveInfo;

    public Skill[] skillToScene = new Skill[14];
    public Sprite[] skillGainImages = new Sprite[14];
    public Image skillGainSlot;
    public int lastMemoryExperienced = 1000; //if not 0, will display sprite & text

    public bool[] memoryExperienced = new bool[14];
    public List<string> memoryScenes = new List<string>();
    public List<string> initialMemoryScenes = new List<string>();
    

    public float typeSpeed = 0.1f;
    public AudioClip typeSound; //frequency is every other character for now
    public GameObject narratorBox;
    public audioManager audioManagement;

    // Use this for initialization
    void Start () {
        skillGainSlot.gameObject.SetActive(false);
        StartCoroutine(LoadInfo());
        
	}

   public IEnumerator LoadInfo() //only run by start function
    {
        yield return new WaitForSeconds(0.05f);
        memoryExperienced = saveInfo.memoryExperienced;
        lastMemoryExperienced = saveInfo.lastMemoryExperienced;

        //Always initiated at the start of the apartment
        for (int i = 0; i < memoryExperienced.Length; i++)
        {
            if (memoryExperienced[i]) //is true
            {
                for (int j = 0; j < memoryScenes.Count; j++)  //find memory scene and remove
                {
                        if (initialMemoryScenes[i] == memoryScenes[j])
                        {
                            print("Matched scene deleted " + initialMemoryScenes[i]);
                            // memoryScenes.Remove(memoryScenes[j]);
                            memoryScenes.RemoveAt(j);
                            //ALSO, now play the skill obtained
                        }
                }
            }
        }

        print("last memory??");
        if (lastMemoryExperienced != 1000)
        {
            //display unlocked skill
            skillGainSlot.sprite = skillGainImages[lastMemoryExperienced];
            skillGainSlot.gameObject.SetActive(true);
            skillGainSlot.enabled = true;

            textTyper.TypeText(narratorBox, "Jim has remembered '" + skillToScene[lastMemoryExperienced].skillName + "'!", typeSpeed, typeSound);
            yield return new WaitForSeconds(4.5f);
            skillGainSlot.enabled = false;
            narratorBox.SetActive(false);
            skillGainSlot.gameObject.SetActive(false);
            lastMemoryExperienced = 1000;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadMemory (int memoryToLoad)
    {
        SceneManager.LoadScene(initialMemoryScenes[memoryToLoad], LoadSceneMode.Additive);
        memoryExperienced[memoryToLoad] = true;
        saveInfo.lastMemoryExperienced = memoryToLoad;
        saveInfo.memoryExperienced[memoryToLoad] = true;                        //Memory experienced
        saveInfo.unlockedSkills[skillToScene[memoryToLoad].skillNumber] = true; //Unlock skill here and save
        saveInfo.Save();
        audioManagement.memoryLoaded = true;
    }
}
