using UnityEngine;
using System.Collections;
using System.Collections.Generic; //if using lists
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class fightManager : MonoBehaviour
{
    public string loseMessage = "RIP Jim";
    public string winMessage = "RIP Fruit Fly";
    
	public GameObject[] characters;
	public int selectedEnemy;
    public int selectedFighter;
    public float timeBetweenEnemyTurns = 2.0f;
	public List<characterStats> cStats = new List<characterStats>();			//in order of highest speed to lowest
	public List<GameObject> fighters = new List<GameObject>();					//in order of highest speed to lowest
	public List<characterStats> goodGuysStats = new List<characterStats>();
	public List<characterStats> badGuysStats = new List<characterStats>();
    public GameObject[] enemyPositions;
    public GameObject[] playerPositions;

	public enum turnState{neutral, player, enemy};
	public turnState turn;
    public int turnNumber = 0;                                                  //highest Speed to Lowest speed
    public int timesMissedAllowed = 2;

    public float sceneIntroTime = 5.5f;
    public Text narratorBox;

	public GameObject disablePlayerInput;
	bool attackIsRunning = false; //state check

    public Transform damageText;
    public cameraShake camShake;
    public float currentDamage = 0.0f;

    public bool enemyHit = false;
    //public int currentDefender;
    public fightManagerSkills fMSkills;

    characterStats attacker;
    characterStats defender;
    bool attackLuck; //last attack luck status, if true the next damage display will also display a critical hit!

    // Use this for initialization
    void Start ()
    {
        cStats.Clear();
        fighters.Clear();
        goodGuysStats.Clear();
        badGuysStats.Clear();

        fMSkills = transform.GetComponent<fightManagerSkills>();
        //characters = GameObject.FindGameObjectsWithTag ("Fighter");
		for (int i = 0; i < characters.Length; i++)
        {
			cStats.Add (characters[i].GetComponent<characterStats> ()); //gets character stats reference
			fighters.Add (characters [i]); //adds gameObjects under Fighter tag to fighters list
		}

		for (int i = 0; i < characters.Length; i++)
        {
			if (cStats [i].enemy == false)
            {
				goodGuysStats.Add (cStats [i]); //adds player characters stats to a list
			}
            else
            {
				badGuysStats.Add (cStats [i]); //adds player characters stats to a list
			}
		}

		if (fighters.Count > 0)
        {
			fighters.Sort (delegate(GameObject b, GameObject a) 
            {
				return (a.GetComponent<characterStats> ().speed).CompareTo (b.GetComponent<characterStats> ().speed);
		    });
			cStats.Sort (delegate(characterStats d, characterStats c) 
            {
				return (c.speed).CompareTo (d.speed);
			});

            StartCoroutine(FirstTurn());
		}
			
		SelectedEnemy (badGuysStats [0].gameObject); //assigns any enemy character for attack selection
    }
	
	// Update is called once per frame

		//note about the playerStats,
		//if I save them at the end of every fight scene or skills edit
		// in the options menu, they can just be loaded at the beginning of another scene,
		//no need to worry aboout bringing the Jim prefab from the apartment into the fight scenes
	

    public IEnumerator FirstTurn ()
    {
    	yield return new WaitForSeconds(sceneIntroTime);
        NextTurn();
    }

	public IEnumerator WaitToNextTurn (float timeToWait)
    {
		yield return new WaitForSeconds(timeToWait);
		NextTurn();
		attackIsRunning = false;
	}

	public void NextTurn ()
    {
        enemyHit = false;
        if (turn != turnState.neutral && turnNumber != (fighters.Count - 1) && cStats[(turnNumber + 1)].healthPoints > 0.0f)
          turnNumber++;         //go to next fighter
        else
          turnNumber = 0;        //reset to first fighter if at the last fighter in the list or on first turn

        for (int i = 0; i < fighters.Count; i++)  
        {
            if (cStats[turnNumber].healthPoints <= 0.0f) //Next fighter if current fighter is dead
            {
                if (turnNumber != fighters.Count)       //if not the last fighter in list
                    turnNumber++;                       //go next fighter
                else                                    //if so
                    turnNumber = 0;                     //reset to first fighter
            }
        }

        if (cStats[turnNumber].healthPoints <= 0.0f)
        {
            print ("turn 0");
            turnNumber = 0;
        }
         

        if (cStats[turnNumber].enemy == true)
        {
            if (fighters[turnNumber].transform.position != enemyPositions[0].transform.position)
            {
                StartCoroutine(SwapPositions(true, turnNumber));
            }
        }

        print("fighter " + turnNumber + " is ready to fight");
        narratorBox.text = cStats[turnNumber].name + " is ready to fight!";

        if (cStats[turnNumber].enemy == true)
        {
            turn = turnState.enemy;
			disablePlayerInput.SetActive (true);
			StartCoroutine (EnemyAttack ());
            // enemyStats[0].RandomOptionSelect();
        }
        else
        {
            turn = turnState.player;
			disablePlayerInput.SetActive (false);
            //playerStats[0].ShowOptions(); //shown on a timer? on-screen too?, IEnumerator
            //UI button executes action
        }
    }

    public IEnumerator SwapPositions(bool enemy, int primaryFighter)
    {
        int tempCount = 0;
        for (int i = 0; i < fighters.Count; i++)
        {
            Vector3 storedPos = fighters[i].transform.position;
            if (cStats[i].enemy == true)
            {
                
                if (i == primaryFighter)
                {
                StartCoroutine(moveObject(fighters[i].transform, enemyPositions[0].transform.position));
                    //lerp to enemyPositions[0]
                    /* while (elapsedTime < time)
                    {
                        fighters[i].transform.position = Vector3.Lerp(storedPos, enemyPositions[0].transform.position, (elapsedTime/ time));
                        elapsedTime += Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }*/
                }
                else
                {
                    print("move temp");
                    tempCount++;
                    StartCoroutine(moveObject(fighters[i].transform, enemyPositions[tempCount].transform.position));
                //lerp to enemyPositions[tempCount]
                /* while (elapsedTime < time)
                    {
                        fighters[i].transform.position = Vector3.Lerp(storedPos, enemyPositions[tempCount].transform.position, (elapsedTime / time));
                        elapsedTime += Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }*/
                }

            }
        }
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator moveObject (Transform movingObject, Vector3 to)
    {
        float time = 0.5f;
        float elapsedTime = 0.0f;
        Vector3 storedPos = movingObject.position;
        while (elapsedTime < time)
        {
            movingObject.position = Vector3.Lerp(storedPos, to, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //yield return new WaitForSeconds(0.5f);
    }

    public void SelectedEnemy()
    {
        for (int i = 0; i < badGuysStats.Count; i++)
        {
            if (badGuysStats[i].healthPoints > 0.0f)
            {
                //selectedEnemy = i;
                SelectedEnemy(badGuysStats[i].gameObject);
            }
        }
    }

    public void SelectedEnemy (GameObject enemySelection)
    {
		print ("this is " + enemySelection.name);
		for (int i = 0; i < fighters.Count; i++)
        {
			if (fighters [i] == enemySelection)
            {
				selectedEnemy = i;
				print ("RUNNNNNNING with fighter " + i);
			}
		}
	}

    //Default Options
	public void Fight ()
    { //just because I can't access the IEnumerator from the UI button OnClick()
		if (attackIsRunning == false)
        {
			//StartCoroutine (PlayerAttack ());
			PlayerAttack ();
		}
	}

    public void Skill(int skillNumber) //skill number is informed by Skill Button in fight, related to the Count in SkillsList.equippedSkills[skillNumber]
    {
        characterStats attacker = cStats[turnNumber];
        SkillsList skillList = fMSkills.skillsList;
        if (attackIsRunning == false)   //this is meant for the player skill usage
        {
            if (attacker.recollectionPoints > skillList.skillsInGame[skillList.equippedSkills[skillNumber]].rpCost && skillList.equippedSkills[skillNumber] > 0)
            {
                attackIsRunning = true;
                fMSkills.PlayerSkill(skillNumber, cStats[turnNumber], cStats[selectedEnemy]); //skillNo, attacker, defender
                StartCoroutine(WaitToNextTurn(timeBetweenEnemyTurns));
                attacker.recollectionPoints -= skillList.skillsInGame[skillList.equippedSkills[skillNumber]].rpCost;

                //attacker.transform.GetComponent<Animator>().SetInteger("skill", skillList.equippedSkills[skillNumber]); //gives skillNumber of skill to animator
                attacker.transform.GetComponent<Animator>().SetInteger("skill", 1); //gives skillNumber of skill to animator
                //print("Animator: " + attacker.transform.GetComponent<Animator>().name);
                StartCoroutine(SkillAnimTriggerReset());
            }
            else
            {
                narratorBox.text = "Skill Unavailable";
            }
            
        }
    }

    public IEnumerator SkillAnimTriggerReset()
    {
        yield return new WaitForSeconds(1.15f);
        fighters[turnNumber].GetComponent<Animator>().SetInteger("skill", 0);
    }

    public void PlayerAttack ()
    {
        SelectedEnemy();

        attackIsRunning = true;
		attacker = cStats [turnNumber];
		defender = cStats [selectedEnemy];

        attackLuck = LuckCheck(attacker.luck);
		float attackStrength = StatPlusLuck(attacker.attack, attackLuck);
		float defenseStrength = StatPlusLuck (defender.defense, LuckCheck(defender.luck));

        if (Miss(attacker.speed, defender.speed, attacker.luck) && attacker.timesMissed < timesMissedAllowed)
        {
            fighters[turnNumber].GetComponent<Animator>().SetTrigger("miss");
            attacker.timesMissed++;
        }
        else
        {
            attacker.timesMissed = 0;                                           //prevents from missing too many times in a row
            fighters[turnNumber].GetComponent<Animator>().SetTrigger("attack");
            float damageDifference = (attackStrength - defenseStrength);
            defender.healthPoints -= damageDifference;
            currentDamage = damageDifference;
            fighters[selectedEnemy].GetComponent<Animator>().SetFloat("health", defender.healthPoints);
        }
        if (defender.healthPoints <= 0.0f)               //Checks if enemy is dead
        {
            bool foundNewEnemy = false;
            for (int i = 0; i < fighters.Count; i++)    //Checks if other enemies are alive
            {
                if (cStats[i].enemy == true && cStats[i].healthPoints > 0.0f)
                {
                    foundNewEnemy = true;
                    selectedEnemy = i;
                }
            }
            if (foundNewEnemy == false)                  //If they aren't, end the game
            {
                StartCoroutine(EndGame());
            }
            else
            {
                StartCoroutine(WaitToNextTurn(timeBetweenEnemyTurns));       //If current enemy isn't dead, next turn
            }
        }
        else
        {
            StartCoroutine(WaitToNextTurn(timeBetweenEnemyTurns));       //If current enemy isn't dead, next turn
        }
	}

	IEnumerator EnemyAttack ()
    {
		yield return new WaitForSeconds (2.0f);
		attackIsRunning = true;
		attacker = cStats [turnNumber];
		defender = goodGuysStats [Random.Range (0, goodGuysStats.Count)]; //picks random defender (should only be Jim for now)

        attackLuck = LuckCheck(attacker.luck);
        float attackStrength = StatPlusLuck(attacker.attack, attackLuck);
        float defenseStrength = StatPlusLuck(defender.defense, LuckCheck(defender.luck));

        if (Miss(attacker.speed, defender.speed, attacker.luck) && attacker.timesMissed < timesMissedAllowed)
        {
            fighters[turnNumber].GetComponent<Animator>().SetTrigger("miss");
            attacker.timesMissed++;
        }
        else
        {
            attacker.timesMissed = 0;
            float damageDifference = (attackStrength - defenseStrength);
            fighters[turnNumber].GetComponent<Animator>().SetTrigger("attack");
            currentDamage = damageDifference;
            while (enemyHit == false)
                yield return null;
            defender.healthPoints -= damageDifference;

            if (defender.healthPoints <= 0.0f)  //Right now, only works if Jim is the only player character
            {
                StartCoroutine(EndGame());
            }
        }

        defender.transform.GetComponent<Animator>().SetFloat("health", defender.healthPoints);
        StartCoroutine (WaitToNextTurn (timeBetweenEnemyTurns));
    }

    bool LuckCheck (float luck)
    {
        float rolled = Random.Range(0.0f, 100.0f);
        bool lucky = false;
        if (rolled < luck)
            lucky = true; //lucky roll
        return lucky;
    }

    float StatPlusLuck (float stat, bool luck)
    {
		if (luck)
        {
			float newAttackStrength = (stat * 1.25f);
			return newAttackStrength;
		} else
        {
			return stat;
		}
	}

    bool Miss(float attackSpeed, float defendSpeed, float luck)
    {
        float speedDifference;
        speedDifference = attackSpeed - defendSpeed;
        float rolled = Random.Range(0.0f, 75.0f); // lower rolled range makes the miss less likely
        rolled -= speedDifference; //if speed dif is -10 and rolled is 20, then rolled is now 30
        if (rolled < luck)
        {
           // print("lucky roll");
            return false;
        }
        else
        {
            // print("MISSED");
            StartCoroutine(MissText());
            return true;
        }
    }

    public IEnumerator MissText()
    {
        yield return new WaitForSeconds(attacker.timeUntilMissTextAppears);
        narratorBox.text = attacker.missText;
    }

    public void DisplayDamage(Vector3 position)             //get called on from a charStats, activated by a hitbox in an anim
    {
        //print("displayText!");
        
        position = new Vector3(position.x, position.y, (position.z + 1.0f));
        Transform damageInst;
        damageInst = Instantiate(damageText, position, Quaternion.identity) as Transform;
        RisingText damageScript;
        damageScript = damageInst.GetComponent<RisingText>();
        damageScript.setup(0, "--" + Mathf.Round(currentDamage) + "HP");

        if (attackLuck) //if attack was lucky
        {
            DisplayStatus(new Vector3(position.x, position.y, position.z + 0.3f), 0, "Crit!");
            narratorBox.text = attacker.name + " let a mean one go for " + Mathf.Round(currentDamage) + " damage!";
            camShake.Shake((attacker.atkCamShake * 1.2f));
        }
        else
        {
            narratorBox.text = attacker.atkText;
            camShake.Shake(attacker.atkCamShake);
        }
    }

    public void DisplayStatus(Vector3 position, int colorRGB, string text) //get called on from a charStats
    {
        position = new Vector3(position.x, position.y, (position.z + 1.0f));
        Transform damageInst;
        damageInst = Instantiate(damageText, position, Quaternion.identity) as Transform;
        RisingText damageScript;
        damageScript = damageInst.GetComponent<RisingText>();
        damageScript.setup(colorRGB, text);
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4.0f);
        if (goodGuysStats[0].healthPoints < 1.0f)
        {
            narratorBox.text = loseMessage;
        }
        else
        {
            narratorBox.text = winMessage;
            characters[0].GetComponent<Animator>().SetTrigger("win");
        }
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("JimApartment_Main");
    }
}
