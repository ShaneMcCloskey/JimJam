using UnityEngine;
using System.Collections;

public class characterInFight : MonoBehaviour
{
    public fightManager fManager;
    public bool offsetAnimation = false;
    public float animOffset = 0.5f;
    Animator anim; 

	// Use this for initialization
	void Start ()
    {
        anim = transform.GetComponent<Animator>();
        if (offsetAnimation)
        {
            StartCoroutine(offsetAnim());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox")
        {
            Animator anim;
            anim = transform.GetComponent<Animator>();
            anim.SetTrigger("hit");
            fManager.DisplayDamage(transform.position);
            fManager.enemyHit = true;
        }
        if (other.tag == "StatusEffectBox")
        {
            Animator anim;
            anim = transform.GetComponent<Animator>();
            anim.SetTrigger("hit");
            //fManager.DisplayDamage(transform.position);
            fManager.enemyHit = true;
        }
    }

    public IEnumerator offsetAnim()
    {
        anim.speed = 0.2f;
        //anim.StopPlayback();
        yield return new WaitForSeconds(animOffset);
        //anim.StartPlayback();
        anim.speed = 1.0f;
    }
}
