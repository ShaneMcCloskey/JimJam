using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic; //if using lists

public class SkillsList_PauseDisplay : MonoBehaviour
{ 
    //Variables that must be assigned
    public SkillsList skillsList;
    public List<Text> skillTitleSlots = new List<Text>(4);
    public List<Toggle> skillTitleToggles = new List<Toggle>(); //public for debugging
    public Text[] equippedDisplay = new Text[4];
    public Text toolTipDisplay;

    //Doesn't really need to be assigned
    public List<string> toolTips = new List<string>();

    // Use this for initialization
    void Start()
    {
        //RefreshSkillList();
        skillTitleToggles = new List<Toggle>();
        StartCoroutine(WaitToRefresh()); //Making sure the unlocked skills list syncs up with the save list
    }

    public IEnumerator WaitToRefresh()
    {
        yield return new WaitForSeconds(0.1f);
        RefreshSkillList();
    }

    public void RefreshSkillList()
    {
        toolTips = new List<string>();
        
            for (int i = 0; i < skillsList.skillsInGame.Count; i++)                     //assigning equipped names to UI components in pause screen
            {

                if (skillTitleSlots[0] != null) //if there's no title slots, don't do this
                {
                    skillTitleToggles.Add(skillTitleSlots[i].transform.GetChild(0).GetComponent<Toggle>()); //assigns skill
                }


                if (skillsList.unlockedSkills[i] == true)                               //checks if skill is unlocked
                {
                    //print("ran!");
                    toolTips.Add(skillsList.skillsInGame[i].toolTip);                   //toolTip
                    if (skillTitleSlots[0] != null) //if there's no title slots, don't do this
                    {
                        skillTitleSlots[i].text = skillsList.skillsInGame[i].skillName;
                        skillTitleToggles[i].interactable = true; //shows unequipped, default symbol
                        skillTitleToggles[i].isOn = false; //hides locked symbol
                    }
                }
                else
                {
                    toolTips.Add(skillsList.skillsInGame[0].toolTip);
                    if (skillTitleSlots[0] != null) //if there's no title slots, don't do this
                    {
                        skillTitleSlots[i].text = "?????";
                        skillTitleToggles[i].interactable = false;
                        skillTitleToggles[i].isOn = true; //shows locked symbol
                    }
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < equippedDisplay.Length; i++)                            //assigning equipped names to UI components in pause screen
        {
            equippedDisplay[i].text = skillsList.skillsInGame[skillsList.equippedSkills[i]].skillName;

        }

        if (skillTitleSlots[0] != null) //if there's no title slots, don't do this
        {
            for (int j = 0; j < skillsList.skillsInGame.Count; j++)
            {
                //equippedDisplay[i].text = skillsList.skillsInGame[skillsList.equippedSkills[i]].skillName;
                for (int k = 0; k < skillsList.equippedSkills.Count; k++) //check if j is the same as any of the equipped skills
                {
                    if (j == skillsList.equippedSkills[k]) //skill j is equipped
                    {
                        skillTitleToggles[j].interactable = true;
                        skillTitleToggles[j].isOn = true; //shows equipped symbol
                    }
                    if (j != skillsList.equippedSkills[k] && skillsList.unlockedSkills[j] == true) //&& skillsList.unlockedSkills[k] == true
                    {
                        skillTitleToggles[j].interactable = true;
                        skillTitleToggles[j].isOn = false; //hides locked symbol
                    }
                }

            }
        }
    }

    public void Hover(int skillNumber)
    {
        //skillsInGame
        //toolTipDisplay.text = skillsList.skillsInGame[skillsList.equippedSkills[skillNumber]].skillName + " : " + toolTips[skillsList.equippedSkills[skillNumber]];
        toolTipDisplay.text = skillsList.skillsInGame[skillNumber].skillName + " : " + toolTips[skillNumber];
        //toolTipDisplay.text = skillsList.skillsInGame[skillNumber].toolTip;
    }

    public void HoverEquipped(int skillNumber)
    {
        //skillsInGame
        toolTipDisplay.text = skillsList.skillsInGame[skillsList.equippedSkills[skillNumber]].skillName + " : " + toolTips[skillsList.equippedSkills[skillNumber]];
        //toolTipDisplay.text = skillsList.skillsInGame[skillNumber].skillName + " : " + toolTips[skillNumber];
        //toolTipDisplay.text = skillsList.skillsInGame[skillNumber].toolTip;
    }
}
