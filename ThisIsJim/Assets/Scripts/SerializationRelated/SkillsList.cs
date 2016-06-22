using UnityEngine;
using System.Collections;
using System.Collections.Generic; //if using lists

public class SkillsList : MonoBehaviour
{
    public savedInfo savedInformation;
    public List<int> equippedSkills = new List<int>(4);       //equippedSkills
    public List<Skill> skillsInGame = new List<Skill>();      //list of every skill
    public List<bool> unlockedSkills = new List<bool>();      //skills, and if they're unlocked
    
    // Use this for initialization
    void Start ()
    {
        //StartCoroutine(LoadSavedInfo());
        equippedSkills = savedInformation.equippedSkills;
        if (savedInformation.unlockedSkills.Count < 2)
        {
            savedInformation.unlockedSkills = unlockedSkills;
            savedInformation.Save();
        }
        else
        {
            unlockedSkills = savedInformation.unlockedSkills; //we'll get to it
        }
        //StartCoroutine(LoadSavedInfo());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //equippedSkills.Sort(delegate (int a, int b) { return a.CompareTo(b); });
        //UpdateSavedInfo();
    }

    public void UpdateSavedInfo()
    {
        savedInformation.equippedSkills = equippedSkills;
        savedInformation.unlockedSkills = unlockedSkills;
    }

    public IEnumerator LoadSavedInfo() //pointless fcn?
    {
        yield return new WaitForSeconds(1.05f);
        equippedSkills = savedInformation.equippedSkills;
        unlockedSkills = savedInformation.unlockedSkills;
        yield return new WaitForSeconds(0.05f);
    }

    public void EquipSkill(int skillNumber)
    {
        bool skillEquipped = false;
        if (unlockedSkills[skillNumber] == true) // check if skill is still locked
        {
            //check if skill is already equipped
            for (int j = 0; j < skillsInGame.Count; j++)
            {
                for (int k = 0; k < equippedSkills.Count; k++) //check if j is the same as any of the equipped skills
                {
                    if (equippedSkills[k] == skillNumber && equippedSkills[k] != 0 && skillEquipped == false) //skill is equipped
                    {
                        skillEquipped = true;                       //skill was already equipped
                        equippedSkills[k] = 0;    //Unequip
                        //equippedSkills.Sort();                      //and sort
                        equippedSkills.Sort(delegate (int b, int a) {
                            return (a).CompareTo(b);
                        });
                    }
                }
            }
            for (int i = 0; i < equippedSkills.Count; i++) {

                if (skillEquipped == false) //skill wasn't already equipped
                {
                    if (equippedSkills[i] == null || equippedSkills[i] == 0)
                    {
                        skillEquipped = true;
                        equippedSkills[i] = skillNumber; //
                    }
                }
            }
        }
    }
}
