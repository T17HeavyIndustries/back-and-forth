using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

    private bool isFire = false;

    public Sprite fire;
    public Sprite fireHover;
    public Sprite ice;
    public Sprite iceHover;

    private GameObject fireObject;

    private SpriteRenderer rend;

    private bool lastState;

    private bool touched = false;
    private bool touchedRelease = false;

    private float releaseDelayTime = 0.15f;
    private float releaseDealyCounter = 0;

    private Camera cam;

    public static bool PopUpInScene;

    public GameObject popUp;

    public static GameObject curPop;
    private bool searchForCam = false;

    void Start() {
        transform.position = new Vector3(744, 1064, 0);
        PopUpInScene = false;
        fireObject = GameObject.Find("FireBackground");
        rend = GetComponent<SpriteRenderer>();
        lastState = isFire;
        try { cam = GameObject.Find("Left Camera").camera; }
        catch { searchForCam = true; }
    }

    void Update() {
        if (searchForCam) {
            try { cam = GameObject.Find("Left Camera").camera; }
            catch { }
        }
        if (fireObject.renderer.enabled) {
            isFire = true;
        }
        else {
            isFire = false;
        }

        if (isFire && !lastState) {
            rend.sprite = fire;
        }
        if (!isFire && lastState) {
            rend.sprite = ice;
        }
#if UNITY_ANDROID
        if (Input.touchCount == 0) {
            ExitHover();
            if (touched) {
                touchedRelease = true;
            }
        }
        else if (Input.touchCount == 1) {
            Vector3 wp = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos)) {
                Hover();
                touched = true;
            }

        }

        if (touchedRelease) {
            releaseDealyCounter += Time.deltaTime;
            if (releaseDealyCounter >= releaseDelayTime) {
                releaseDealyCounter = 0;
                Down();
                touched = false;
                touchedRelease = false;
            }
        }


        lastState = isFire;
#endif
    }

    void Down() {
        if (!PopUpInScene && GameObject.Find("Hint1(Clone)") == null && GameObject.Find("Hint2(Clone)") == null && GameObject.Find("Hint3(Clone)") == null) {
            curPop = (GameObject)Instantiate(popUp);
            PopUpInScene = true;
        }
    }

    void Hover() {
        if (isFire) {
            rend.sprite = fireHover;
        }
        else {
            rend.sprite = iceHover;
        }
    }
    void ExitHover() {
        if (isFire) {
            rend.sprite = fire;
        }
        else {
            rend.sprite = ice;
        }
    }

#if UNITY_EDITOR
    void OnMouseDown(){
        Down();
    }
#elif UNITY_STANDALONE
    void OnMouseDown(){
        Down();
    }
#endif
}
