using UnityEngine;
using System.Collections;

public class MenuPopUpReset : MonoBehaviour {

    public Sprite fireDefault;
    public Sprite fireHover;
    public Sprite iceDefault;
    public Sprite iceHover;
    private Sprite defaultSprite;
    private Sprite defaultHover;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    private Camera cam;

    private SpriteRenderer rend;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Left Camera").camera;
        if (GameObject.Find("FireBackground").GetComponent<SpriteRenderer>().enabled) {
            GetComponent<SpriteRenderer>().sprite = fireDefault;
            defaultSprite = fireDefault;
            defaultHover = fireHover;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = iceDefault;
            defaultSprite = iceDefault;
            defaultHover = iceHover;
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
        if (!GameObject.Find("Level Parent").GetComponent<LevelController>().custom) {
            Application.LoadLevel("Puzzle" + GameObject.Find("Level Parent").GetComponent<LevelController>().puzzleNumber.ToString());
        }
        else if(Application.loadedLevelName == "LevelEditor") {
            StartCoroutine("LevelEditorTestReset");
        }
        else {
            Application.LoadLevel("CustomLevel");
        }
    }

    IEnumerator LevelEditorTestReset() {
        GameObject.Find("Editor Main").GetComponent<EditorMain>().BackToEditor();
        yield return null;
        GameObject.Find("Editor Main").GetComponent<EditorMain>().PackagePuzzle();
        Destroy(MenuButton.curPop);
        MenuButton.PopUpInScene = false;
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
