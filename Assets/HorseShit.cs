using UnityEngine;
using System.Collections;

public class HorseShit : MonoBehaviour {

    GameObject levelParent;

    private int frameCountStart;

	// Use this for initialization
	void Start () {

        frameCountStart = Time.frameCount;

        LevelController levelController = GameObject.Find("Level Parent").GetComponent<LevelController>();

        
	}
	
	// Update is called once per frame
	/*void Update () {
        if (Time.frameCount - frameCountStart == 2) {
            if (levelController.startFire) {
                GameObject.Find("Fire").transform.FindChild("Colliders").gameObject.SetActive(true);
                GameObject.Find("Ice").transform.FindChild("Colliders").gameObject.SetActive(false);
            }
            else {
                GameObject.Find("Fire").transform.FindChild("Colliders").gameObject.SetActive(false);
                GameObject.Find("Ice").transform.FindChild("Colliders").gameObject.SetActive(true);
            }
            if (levelController.startDesert) {
                GameObject.Find("Desert").transform.FindChild("Colliders").gameObject.SetActive(true);
                GameObject.Find("Cave").transform.FindChild("Colliders").gameObject.SetActive(false);
            }
            else {
                GameObject.Find("Desert").transform.FindChild("Colliders").gameObject.SetActive(false);
                GameObject.Find("Cave").transform.FindChild("Colliders").gameObject.SetActive(true);
            }
        }
       
	}*/
}
