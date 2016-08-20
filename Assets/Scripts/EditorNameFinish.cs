using UnityEngine;
using System.Collections;
using System.IO;

public class EditorNameFinish : MonoBehaviour {

    public Sprite defaultSprite;
    public Sprite defaultHover;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    private Camera cam;

    private SpriteRenderer rend;

    public GameObject warning;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Left Camera").camera;
    }


    void Update() {
#if UNITY_ANDROID
        //being pressed
        if (Input.touchCount == 0) {
            ExitHover();
            if (touched) {
                Down();
            }
            touched = false;
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
        if (NameEnter.CurrentNameText.Length > 0) {
            print(Application.persistentDataPath  + "/" + NameEnter.CurrentNameText + ".lev");
            if (!File.Exists(Application.persistentDataPath + "/" + NameEnter.CurrentNameText + ".lev")) {
                GameObject.Find("Editor Main").GetComponent<EditorMain>().SaveLevel();
                Application.LoadLevel("EditorInitial");
                print("does not exist");
            }
            else {
                warning.SetActive(true);
            }
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
