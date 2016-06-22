using UnityEngine;
using System.Collections;
using System.Collections.Generic; //if using lists

public class JimSkills : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();
    //save skills as a list in the Serialized script, then
    //have this script instantiate and parent those to Jim on the level load

	// Use this for initialization
	void Start () 
    {
        //Skill(string newName, bool selfInflicted, affectedStat statAffectedNew, bool percentageBased, float adjustedPercentOrPoints, int turnsLasting)
        //skills.Add(new Skill("Foul Mouth", true, Skill.affectedStat.attack, true, 10.0f, 0));
    }
}
