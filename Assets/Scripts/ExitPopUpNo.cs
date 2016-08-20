using UnityEngine;
using System.Collections;

public class ExitPopUpNo : MonoBehaviour {

    private SpriteRenderer rend;
    public Sprite defaultSprite;
    public Sprite defaultHover;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();


    void Start() {
        LevelController.TimePause = true;
        rend = GetComponent<SpriteRenderer>();

        touched = false;
        Input.simulateMouseWithTouches = false;

        try { transform.GetChild(0).GetComponent<MeshRenderer>().sortingLayerName = "Text"; }
        catch { }
    }

    void Update() {
#if UNITY_ANDROID
        //being pressed
        if (Input.touchCount == 0) {
            ExitHover();
            if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                Down();
                touched = false;
            }
        }
        else if (Input.touchCount == 1) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            lastTouchPos = touchPos;
            if (collider2D == Physics2D.OverlapPoint(touchPos)) {
                Hover();
                touched = true;
            }
            else {
                ExitHover();
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
        try { GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = true; }
        catch { }
        BackPhoneButton.PopUpInScene = false;
        Destroy(transform.parent.gameObject);
        LevelController.TimePause = false;
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
