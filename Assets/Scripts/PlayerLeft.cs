using UnityEngine;
using System.Collections;

public class PlayerLeft : MonoBehaviour {

    public bool Right;
    public bool Left;

    public Sprite fire;
    public Sprite ice;
    public Sprite desert;
    public Sprite cave;

    private SpriteRenderer rend;

    private bool curFire;
    private bool curDesert;

	void Start () {
        rend = GetComponent<SpriteRenderer>();
        if (Left) {
            if (GameObject.Find("Fire").transform.FindChildIncludingDeactivated("FireBackground").GetComponent<SpriteRenderer>().enabled) {
                curFire = true;
                rend.sprite = fire;
            }
            else {
                curFire = false;
                rend.sprite = ice;
            }
        }
        else if(Right){
            if (GameObject.Find("Desert").transform.FindChildIncludingDeactivated("DesertBackground").GetComponent<SpriteRenderer>().enabled) {
                curDesert = true;
                rend.sprite = desert;
            }
            else {
                rend.sprite = cave;
                curDesert = false;
            }
        }
	}
	
	void Update () {
	
	}

    public void Switch() {
        if (Left) {
            if (curFire) {
                curFire = false;
                rend.sprite = ice;
            }
            else {
                curFire = true;
                rend.sprite = fire;
            }
        }
        else {
            if (curDesert) {
                curDesert = false;
                rend.sprite = cave;
            }
            else {
                curDesert = true;
                rend.sprite = desert;
            }
        }
    }

}
