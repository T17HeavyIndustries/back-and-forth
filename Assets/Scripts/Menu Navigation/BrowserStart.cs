using UnityEngine;
using System.Collections;

public class BrowserStart : MonoBehaviour {

    public CustomLevelEntry levelInfo;

    public Sprite fireDefault;
    public Sprite fireSelected;
    public Sprite iceDefault;
    public Sprite iceSelected;
    public Sprite desertDefault;
    public Sprite desertSelected;
    public Sprite caveDefault;
    public Sprite caveSelected;

    private Sprite defaultSprite;
    private Sprite defaultSelected;

    SpriteRenderer rend;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    void Start() {

        int num = 0;
        int.TryParse(transform.parent.name.Substring(4), out num);

        rend = GetComponent<SpriteRenderer>();

        if (num % 4 == 0) {
            defaultSprite = fireDefault;
            defaultSelected = fireSelected;
            rend.sprite = fireDefault;
        }
        else if (num % 4 == 1) {
            defaultSprite = iceDefault;
            defaultSelected = iceSelected;
            rend.sprite = iceDefault;
        }
        else if (num % 4 == 2) {
            defaultSprite = desertDefault;
            defaultSelected = desertSelected;
            rend.sprite = desertDefault;
        }
        else if (num % 4 == 3) {
            defaultSprite = caveDefault;
            defaultSelected = caveSelected;
            rend.sprite = caveDefault;
        }
    }

    void Update() {
//#if UNITY_ANDROID
        if (Input.touchCount == 0) {
            if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                Down();
                touched = false;
            }
            else {
                rend.sprite = defaultSprite;
            }
        }
        else if (Input.touchCount == 1) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            lastTouchPos = touchPos;
            if (collider2D == Physics2D.OverlapPoint(touchPos)) {
                touched = true;
                rend.sprite = defaultSelected;
            }
        }
//#endif
    }

    void Down() {
        if (CustomLevelMain.CurLevelInfo != null) {
            Application.LoadLevel("CustomLevel");
        }
    }   

#if UNITY_EDITOR
    void OnMouseDown() {
        Down();
    }
#elif UNITY_STANDALONE
    void OnMouseDown() {
        Down();
    }
#endif

}
