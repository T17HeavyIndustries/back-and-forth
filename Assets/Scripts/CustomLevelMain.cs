using UnityEngine;
using System.Collections;

public class CustomLevelMain : MonoBehaviour {

    public GameObject playerLeft;
    public GameObject playerRight;
    public static CustomLevelEntry CurLevelInfo;

    private GameObject levelParent;

    private int frameCountStart;
    LevelController levelController;

    private bool firstFrame = true;

    void Awake() {
        LevelSerializer.LoadObjectTreeFromFile(CurLevelInfo.filePath);
        levelParent = GameObject.Find("Level Parent");
        levelController = levelParent.GetComponent<LevelController>();
        levelController.test = false;
        levelController.custom = true;

        GameObject.Find("Player Left").transform.position = GameObject.Find("Player Start Left").transform.position;
        GameObject.Find("Player Right").transform.position = GameObject.Find("Player Start Right").transform.position;

        frameCountStart = Time.frameCount;     
    }

    void Update() {
        if (Time.frameCount - frameCountStart == 2) {
            if (levelController.startFireDesert) {
                if (levelController.includeFire) {
                    levelParent.transform.FindChild("Fire").transform.FindChild("FireBackground").GetComponent<SpriteRenderer>().enabled = true;
                    levelParent.transform.FindChild("Ice").transform.FindChild("IceBackground").GetComponent<SpriteRenderer>().enabled = false;
                    levelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(true);
                    levelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(false);
                }
                else {
                    levelParent.transform.FindChild("Fire").transform.FindChild("FireBackground").GetComponent<SpriteRenderer>().enabled = false;
                    levelParent.transform.FindChild("Ice").transform.FindChild("IceBackground").GetComponent<SpriteRenderer>().enabled = true;
                    levelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(false);
                    levelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(true);
                }
                if (levelController.includeDesert) {
                    levelParent.transform.FindChild("Desert").transform.FindChild("DesertBackground").GetComponent<SpriteRenderer>().enabled = true;
                    levelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(true);
                    levelParent.transform.FindChild("Cave").transform.FindChild("CaveBackground").GetComponent<SpriteRenderer>().enabled = false;
                    levelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(false);
                }
                else {
                    levelParent.transform.FindChild("Desert").transform.FindChild("DesertBackground").GetComponent<SpriteRenderer>().enabled = false;
                    levelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(false);
                    levelParent.transform.FindChild("Cave").transform.FindChild("CaveBackground").GetComponent<SpriteRenderer>().enabled = true;
                    levelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(true);
                }


            }
            else {
                if (levelController.includeIce) {
                    levelParent.transform.FindChild("Fire").transform.FindChild("FireBackground").GetComponent<SpriteRenderer>().enabled = false;
                    levelParent.transform.FindChild("Ice").transform.FindChild("IceBackground").GetComponent<SpriteRenderer>().enabled = true;
                    levelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(false);
                    levelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(true);
                }
                else {
                    levelParent.transform.FindChild("Fire").transform.FindChild("FireBackground").GetComponent<SpriteRenderer>().enabled = true;
                    levelParent.transform.FindChild("Ice").transform.FindChild("IceBackground").GetComponent<SpriteRenderer>().enabled = false;
                    levelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(true);
                    levelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(false);
                }

                if (levelController.includeCave) {
                    levelParent.transform.FindChild("Desert").transform.FindChild("DesertBackground").GetComponent<SpriteRenderer>().enabled = false;
                    levelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(false);
                    levelParent.transform.FindChild("Cave").transform.FindChild("CaveBackground").GetComponent<SpriteRenderer>().enabled = true;
                    levelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(true);
                }

                else {
                    levelParent.transform.FindChild("Desert").transform.FindChild("DesertBackground").GetComponent<SpriteRenderer>().enabled = true;
                    levelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(true);
                    levelParent.transform.FindChild("Cave").transform.FindChild("CaveBackground").GetComponent<SpriteRenderer>().enabled = false;
                    levelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(false);
                }
            }
        }
    }
}
