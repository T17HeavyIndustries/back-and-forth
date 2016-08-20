using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {

    public bool isFire = false;

    public Sprite fire;
    public Sprite fireHover;
    public Sprite ice;
    public Sprite iceHover;

    public GameObject fireObject;
    //public GameObject iceObject;

    private SpriteRenderer rend;

    private bool lastState;

    private bool touched = false;
    private bool touchedRelease = false;

    private float releaseDelayTime = 0.15f;
    private float releaseDealyCounter = 0;

    private Camera cam;

    void Start() {
        fireObject = GameObject.Find("Fire");
        rend = GetComponent<SpriteRenderer>();
        lastState = isFire;
        cam = GameObject.Find("Left Camera").camera;
    }

    void Update() {
        if (fireObject.activeInHierarchy) {
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
            }
        }

        lastState = isFire;
    }

    void Down() {

        Main.ExitCount = 0;
        int number = -1;
        int.TryParse(GameObject.FindGameObjectWithTag("Puzzle Number").name, out number);
        Application.LoadLevel(number);
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
}
