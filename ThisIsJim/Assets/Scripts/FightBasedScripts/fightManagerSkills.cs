using UnityEngine;
using System.Collections;

public class fightManagerSkills : MonoBehaviour
{
    public SkillsList skillsList;
    fightManager fManager;

    // Use this for initialization
    void Start ()
    {
        fManager = transform.GetComponent<fightManager>();
	}

    public void PlayerSkill(int skillNumber, characterStats attacker, characterStats defender)
    {
        Skill skill = skillsList.skillsInGame[skillsList.equippedSkills[skillNumber]];

        PlayerSkillExecute(skill, attacker, defender);
    }

    public void PlayerSkillExecute(Skill skill, characterStats attacker, characterStats defender)
    {
        characterStats adjustedStats;

        if (skill.selfInfliction)
            adjustedStats = attacker;       //SELF INFLICTED
        else
            adjustedStats = defender;       //ENEMY INFLICTED

        //ADJUSTING STATS
        if (skill.statAffected == Skill.affectedStat.attack)        //Attack
            adjustedStats.attack = SkillAdjustFloat(adjustedStats.attack, skill.adjustedPercentageOrPoints, skill.percentageBased);

        //adjustedStats.attack = (adjustedStats.attack + (adjustedStats.attack * (skill.adjustedPercentageOrPoints / 100.0f)));
        //                      stat                + ( stat * (percentage/100))
        if (skill.statAffected == Skill.affectedStat.defense)       //Defense
            adjustedStats.defense = SkillAdjustFloat(adjustedStats.defense, skill.adjustedPercentageOrPoints, skill.percentageBased);

        if (skill.statAffected == Skill.affectedStat.speed)         //Speed
            adjustedStats.speed = SkillAdjustFloat(adjustedStats.speed, skill.adjustedPercentageOrPoints, skill.percentageBased);

        if (skill.statAffected == Skill.affectedStat.luck)          //Luck
            adjustedStats.luck = SkillAdjustFloat(adjustedStats.luck, skill.adjustedPercentageOrPoints, skill.percentageBased);

        if (skill.statAffected == Skill.affectedStat.healthPoints)  //Health Points
            adjustedStats.healthPoints = SkillAdjustFloat(adjustedStats.healthPoints, skill.adjustedPercentageOrPoints, skill.percentageBased);


        //fManager.damageScript.setup(0, "--" + Mathf.Round(currentDamage) + "HP");

        if (skill.adjustedPercentageOrPoints < 0)
        {
            //play the DOWNgrade animation with the floating text   //secondsToDisplayStatus
            //fManager.DisplayStatus(adjustedStats.transform.position, 0, skill.notifyText);
            StartCoroutine(displayEffect(skill, 0, adjustedStats));
        }
        else
        {
            //play the UPgrade animation with the floating text
            //fManager.DisplayStatus(adjustedStats.transform.position, 2, skill.notifyText);
            StartCoroutine(displayEffect(skill, 2, adjustedStats));
        }

        print("skill used");
        fManager.narratorBox.text = skill.skillText;

        if (skill.useDoubledSkill == true)
        {
            print("double skill used");
            PlayerSkillExecute(skill.doubledSkill, attacker, defender);
        }
    }

    public IEnumerator displayEffect (Skill skill, int color, characterStats adjustedStats)
    {
        yield return new WaitForSeconds(skill.secondsToDisplayStatus);
        fManager.DisplayStatus(adjustedStats.transform.position, color, skill.notifyText);
    }

    float SkillAdjustFloat(float stat, float adjustedPercentageOrPoints, bool percentageBased)
    {
        float newStat;
        if (percentageBased) //percentage based
        {
            newStat = (stat + (stat * (adjustedPercentageOrPoints / 100.0f)));
        }
        else { //point based
            newStat = (stat + adjustedPercentageOrPoints);
        }
        return newStat;
    }
}
