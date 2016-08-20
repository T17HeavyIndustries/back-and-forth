using UnityEngine;
using System.Collections;

public class MenuPopUpExit : MonoBehaviour {

    public Sprite fireDefault;
    public Sprite fireHover;
    public Sprite iceDefault;
    public Sprite iceHover;
    private Sprite defaultSprite;
    private Sprite defaultHover;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    public Sprite fireBack;
    public Sprite iceBack;

    private Camera cam;

    private SpriteRenderer rend;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Left Camera").camera;
        touched = false;
        GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = false;
        if (GameObject.Find("FireBackground").GetComponent<SpriteRenderer>().enabled) {
            GetComponent<SpriteRenderer>().sprite = fireDefault;
            defaultSprite = fireDefault;
            defaultHover = fireHover;
            transform.parent.GetComponent<SpriteRenderer>().sprite = fireBack;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = iceDefault;
            defaultSprite = iceDefault;
            defaultHover = iceHover;
            transform.parent.GetComponent<SpriteRenderer>().sprite = iceBack;
        }
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
        if (Application.loadedLevel > 0 && Application.loadedLevel < 51) {
            Application.LoadLevel("PuzzleMenu");
        }
        else if (Application.loadedLevelName == "LevelEditor") {
            GameObject.Find("Editor Main").GetComponent<EditorMain>().BackToEditor();
            Destroy(MenuButton.curPop);
            MenuButton.PopUpInScene = false;
        }
        else {
            Application.LoadLevel("CustomLevelBrowser");
        }
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
