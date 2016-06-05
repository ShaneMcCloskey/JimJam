using UnityEngine;
using System.Collections;

public class roomTrigger : MonoBehaviour {
	//Manages room fading for Jim's apartment

	public Transform lRSpriteParent;
	public Transform bRSpriteParent;
	public Transform kSpriteParent;

	public Color transparentColor;
	public bool inLivingRoom = true;

	public SpriteRenderer[] lRSprites;
	SpriteRenderer[] bRSprites;
	SpriteRenderer[] kSprites;
	public float timer = 2.0f;
	public float speed = 0.1f;
	// Use this for initialization
	void Start () {
		lRSprites = lRSpriteParent.GetComponentsInChildren<SpriteRenderer> ();
		bRSprites = bRSpriteParent.GetComponentsInChildren<SpriteRenderer> ();
		kSprites = kSpriteParent.GetComponentsInChildren<SpriteRenderer> ();

		for (int i = 0; i < kSprites.Length; i++) {
			kSprites[i].color = transparentColor;
		}
		for (int i = 0; i < bRSprites.Length; i++) {
			bRSprites[i].color = transparentColor;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (timer < 1.0f) {
			timer += 1.0f * Time.deltaTime * speed;
			if (inLivingRoom == true) {
				for (int i = 0; i < lRSprites.Length; i++) {
					lRSprites[i].color = Color.Lerp (transparentColor, Color.white, timer);
				}
				for (int i = 0; i < bRSprites.Length; i++) {
					bRSprites[i].color = Color.Lerp ( Color.white, transparentColor, timer);
				}
				for (int i = 0; i < kSprites.Length; i++) {
					kSprites[i].color = Color.Lerp (Color.white, transparentColor, timer);
				}
			} else {
				for (int i = 0; i < lRSprites.Length; i++) {
					lRSprites[i].color = Color.Lerp (Color.white, transparentColor, timer);
				}
				for (int i = 0; i < bRSprites.Length; i++) {
					bRSprites[i].color = Color.Lerp (transparentColor, Color.white, timer);
				}
				for (int i = 0; i < kSprites.Length; i++) {
					kSprites[i].color = Color.Lerp (transparentColor, Color.white, timer);
				}
			}
		}
	}

	void enterNewRoom (){ //livingRoom, bedroom, kitchen
		inLivingRoom = !inLivingRoom;
		timer = 0.0f;
	}


	void OnTriggerEnter (Collider other){
		SpriteRenderer[] allChildren = lRSpriteParent.GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer child in allChildren) {
			child.color = Color.white;
		}
	}

	void OnTriggerExit  (Collider other){
		SpriteRenderer[] allChildren = lRSpriteParent.GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer child in allChildren) {
			child.color = transparentColor;
		}
	}
}
