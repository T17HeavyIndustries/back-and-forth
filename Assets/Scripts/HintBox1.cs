using UnityEngine;
using System.Collections;

public class HintBox1 : MonoBehaviour {

    private bool canExit = false;
    private Camera cam;
    private bool touched = false;
    Vector3 lastTouchPos = Vector3.zero;


    void Start() {
        LevelController.TimePause = true;
        GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = false;
        cam = GameObject.Find("Left Camera").camera;
        transform.GetChild(0).GetComponent<TextMesh>().text = " Swipe the screen to \n \n move the player blocks \n \n \n \n \n The fire and ice themes \n \n have mirrored movement \n \n \n \n \n Move both your blocks on \n \n the exits to win";
        StartCoroutine("TimeToClose");
    }


    void Update() {
        LevelController.TimePause = true;
        if (Input.touchCount == 1 && canExit) {
            Destroy(gameObject);
            GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = true;
            LevelController.TimePause = false;
        }
    }

    IEnumerator TimeToClose() {
        yield return new WaitForSeconds(3f);
        canExit = true;
    }

#if UNITY_EDITOR
    void OnMouseDown() {
        if (canExit) {
            Destroy(gameObject);
            GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = true;
            LevelController.TimePause = false;
        }
    }
#elif UNITY_STANDALONE
    void OnMouseDown(){
        if(canExit){
            Destroy(gameObject);
            GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = true;
            LevelController.TimePause = false;
        }
    }

#endif
}
