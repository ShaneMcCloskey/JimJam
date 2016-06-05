using UnityEngine;
using System.Collections;

public class characterStats : MonoBehaviour {

	//The stats will be ATK (Attack), DEF (Defense), SPD (Speed), MOD (Mood),
	//MRY (Memory), LCK (Luck), HP (Health Points), and MP (Memory Points).

	public string name = "Jim";
	public bool enemy = false;

    public string missText = "Jim slipped and missed";
    public float timeUntilMissTextAppears = 1.0f;

    public string atkText = "Jim found an opening and went for it!";
    public float atkCamShake = 1.0f;

    public float attack = 25.0f; //Standard attack strength
	float baseAttack;
	public float defense = 10.0f; //Affects how much damage Jim takes when hurt
	float baseDefense;
	public float speed = 10.0f; //Speed affects dodge capability and if Jim attacks first, 
	float baseSpeed;
	public float obedience = 100.0f; //always out of 100
	public float memory = 25.0f; //Memory affects the strength/effectiveness of Jim's skills
    float baseMemory;
	public float luck = 10.0f; //Affects almost everything a slight degree, including how many pennies Jim can find //should be based on a percentage out of 100.0f
	float baseLuck;
	public float healthPoints = 100.0f; //affects how much health Jim has
	float baseHP;
    public int recollectionPoints = 25; //Affects how many points Jim can expend on skill moves in combat.
    int baseRP;

    moodState mood = moodState.Nuetral;
	enum moodState{Nuetral, Anger, Sadness, Overjoy, Fear};

    //not serialized at all
    public int timesMissed = 0;
	 

	// Use this for initialization
	void Start () {
		//if no save data, set all these base stats
		baseAttack = attack;
		baseDefense = defense;
		baseSpeed = speed;
		baseRP = recollectionPoints;
		baseLuck = luck;
		baseHP = healthPoints;

		moodUpdate (mood);

	}
	
	// Update is called once per frame
	void Update () {
	 
	}

	void moodUpdate(moodState newMood){
		mood = newMood;
		if (newMood == moodState.Nuetral) {

		}
		if (newMood == moodState.Anger) { // Anger - 50-60% chance to ignore, increased attack strength
			obedience = (100.0f - 55.0f);
			attack = (baseAttack + (baseAttack *0.1f));
		}
		if (newMood == moodState.Sadness) { // Sadness - 40% chance to ignore, decreased attack, increased defense
			obedience = (100.0f - 40.0f);
			attack = (baseAttack - (baseAttack *0.1f));
			defense = (baseDefense + (baseDefense *0.1f));
		}
		if (newMood == moodState.Overjoy) { // Overjoy - 40% chance to ignore, increased chance to miss, 40% chance to hit twice
			obedience = (100.0f - 40.0f);
			//going to come back for the hit twice
		}
		if (newMood == moodState.Fear) { // Fear - 70% chance to ignore, defense way up
			obedience = (100.0f - 70.0f);
			defense = (baseDefense + (baseDefense *0.25f));
		}
	}

    
 
}
