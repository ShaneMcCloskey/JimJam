using UnityEngine;
using System.Collections;
using System.Collections.Generic; //if using lists
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


//This should work as a skill for an enemy or a player character right now
//Put on a prefab so it can be assigned to

[Serializable]
public class Skill : MonoBehaviour
{
    [Tooltip("Don't adjust this boioi.")]
    public int skillNumber = 0; //might not be needed
    [Tooltip("How many recollection points this will cost in combat.")]
    public int rpCost = 5;
    [Tooltip("Don't adjust this boioi.")]
    public Skill doubledSkill;
    [Tooltip("Don't adjust this boioi.")]
    public bool useDoubledSkill = false;
    [Tooltip("Timed with the skill animation. How long it takes for the floating text to appear.")]
    public float secondsToDisplayStatus;
    [Tooltip("Name that'll appear in menu selections.")]
    public string skillName = "Foul Mouth";
    [Tooltip("What appears in the Skills menu and in the fight. We can separate those later.")]
    public string toolTip = "Verbally abuse Jim's opponent, lowering their ATK power";
    [Tooltip("What will show up in the floating text in combat.")]
    public string notifyText = "ATK-";
    [Tooltip("Top text that appears during the fight.")]
    public string skillText = "Jim does a skill! Replace this text.";
    [Tooltip("Which stat is affected.")]
    public affectedStat statAffected = affectedStat.attack;
    [Tooltip("Self-explanatory.")]
    public bool selfInfliction = false;
    [Tooltip("Whether the statAffected is adjusted by percentage or points.")]
    public bool percentageBased = true;
    [Tooltip("How much percentage or how many points to adjust the statAffected by.")]
    public float adjustedPercentageOrPoints = -10.0f; //As is, will lower attack power by 10%
    [Tooltip("This does not work yet.")]
    public int turnLength = 0;

    [Tooltip("Don't adjust this boioi.")]
    public bool enemySkill = false;

    [Tooltip("Don't adjust this boioi.")]
    public characterStats adjustedStats;
    [Tooltip("Don't adjust this boioi.")]
    public fightManager fightManagement;

    public enum affectedStat { attack, defense, speed, obedience, memory, luck, healthPoints, memoryPoints};

    //Serialized bits   //The way things are, none of these skills should be serialized, only separate lists about them
    //public bool unlocked = false; //entails that it has been bought/found/recieved.
    //public bool equipped = false; //equipped to Jim, mostly for tying to a UI element, real equip will be in Jim stats

    void Awake()
    {
        //print("test: " + statAffected.ToString());
        //search for fight manager by tag, if not there, you're in the store!
    }

    /*
    public Skill(string newName, bool selfInflicted, affectedStat statAffectedNew, bool percentageBased, float adjustedPercentOrPoints, int turnsLasting)
    {
        name = newName;
        selfInfliction = selfInflicted;
        statAffected = statAffectedNew;
        percentageOrPointBased = percentageBased;
        adjustedPercentageOrPoints = adjustedPercentOrPoints;
        turnLength = turnsLasting;
    }

    //for Hamlet's Ghost, we might need to make 3 skills all named Hamlet's Ghost and pick from those
    */
}
