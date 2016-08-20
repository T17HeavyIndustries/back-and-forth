using UnityEngine;
using System.Collections;

public class MenuClick : MonoBehaviour {

    private string puzzleName;
    private SpriteRenderer rend;
    public Sprite unlocked;
    public Sprite unlockedHover;
    public Sprite locked;
    public Sprite lockedHover;
    public Sprite complete;
    public Sprite completeHover;

    public bool open = false; 
    public bool beaten = false; 

    private bool touched = false;
    private bool touchedRelease = false;
    

    private float releaseDelayTime = 0.15f;
    private float releaseDealyCounter = 0;

    void Start() {
        puzzleName = gameObject.name;
        rend = GetComponent<SpriteRenderer>();
        if (beaten) {
            rend.sprite = complete;
        }
        else if (open) {
            rend.sprite = unlocked;
        }
        else {
            rend.sprite = locked;
        }
        touched = false;
        Input.simulateMouseWithTouches = false;
    }

    void Update() {

        //being pressed
        if (Input.touchCount == 0) {
            ExitHover();
            if (touched) {
                Down();
            }
        }
        else if (Input.touchCount == 1) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos)) {
                Hover();
                touched = true;
            }
            else {
                ExitHover();
            }
        }

        if (touchedRelease) {
            releaseDealyCounter += Time.deltaTime;
            if (releaseDealyCounter >= releaseDelayTime) {
                releaseDealyCounter = 0;
                Down();
            }
        }

    }

    void Hover() {
        if (beaten) {
            rend.sprite = completeHover;
        }
        else if (open) {
            rend.sprite = unlockedHover;
        }
        else {
            rend.sprite = lockedHover;
        }
    }

    void ExitHover() {
        if (beaten) {
            rend.sprite = complete;
        }
        else if (open) {
            rend.sprite = unlocked;
        }
        else {
            rend.sprite = locked;
        }
    }

    void Down() {
        if (open) {
            Application.LoadLevel(puzzleName);
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
