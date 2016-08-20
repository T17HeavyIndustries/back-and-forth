using UnityEngine;
using System.Collections;

public class CompletedSaveAndLeave : MonoBehaviour {

    public Sprite defaultSprite;
    public Sprite defaultHover;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    private Camera cam;

    private SpriteRenderer rend;

    public GameObject nameEnter;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Left Camera").camera;
        touched = false;
    }


    void Update() {
#if UNITY_ANDROID
        //being pressed
        if (Input.touchCount == 0) {
            ExitHover();
            if (touched) {
                Down();
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
#endif
    }

    void Hover() {

        rend.sprite = defaultHover;
    }

    void ExitHover() {
        rend.sprite = defaultSprite;
    }

    void Down() {
        nameEnter.SetActive(true);
        GameObject.Find("Test Completed").SetActive(false);
    }

#if UNITY_EDITOR
    void OnMouseDown() {
        Down();
    }

#elif UNITY_STANDALONE
    void OnMouseDown(){
        Down();
    }
#endif
}
