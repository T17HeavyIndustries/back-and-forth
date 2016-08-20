using UnityEngine;
using System.Collections;

public class IncludeTheme : MonoBehaviour {

	private EditorMain editorMain;

    public bool fire = false;
    public bool ice = false;
    public bool desert = false;
    public bool cave = false;

    public Sprite defaultSprite;
    public Sprite hoverSprite;
    private SpriteRenderer rend;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    private bool selected = false;

    void Awake() {
        rend = GetComponent<SpriteRenderer>();
    }

    void Start() {
        editorMain = GameObject.Find("Editor Main").GetComponent<EditorMain>();
        if (fire || desert) {
            Down();
        }
    }

    void OnEnable() {
        if (fire) {
            if (EditorMain.IncludeFire) {
                Selected();
            }
            else {
                UnSelected();
            }
        }
        else if(ice){
            if (EditorMain.IncludeIce) {
                Selected();
            }
            else {
                UnSelected();
            }
        }
        else if (desert) {
            if (EditorMain.IncludeDesert) {
                Selected();
            }
            else {
                UnSelected();
            }
        }
        else if (cave) {
            if (EditorMain.IncludeCave) {
                Selected();
            }
            else {
                UnSelected();
            }
        }
    }

//#if UNITY_ANDROID
    void Update() {
        //being pressed
        if (Input.touchCount == 0) {
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
                rend.sprite = hoverSprite;
                touched = true;
            }
        }
    }
//#endif

    void Down() {
        if (!selected) {
            Selected();
        }
        else if (fire && selected && EditorMain.IncludeIce) {
            UnSelected();
        }
        else if (ice && selected && EditorMain.IncludeFire) {
            UnSelected();
        }
        else if (desert && selected && EditorMain.IncludeCave) {
            UnSelected();
        }
        else if (cave && selected && EditorMain.IncludeDesert) {
            UnSelected();
        }
    }

    void Selected() {
        try { rend.sprite = hoverSprite; }
        catch { print(transform.parent.name); }
        if (fire) {
            EditorMain.IncludeFire = true;
        }
        else if (ice) {
            EditorMain.IncludeIce = true;
        }
        else if (desert) {
            EditorMain.IncludeDesert = true;
        }
        else if (cave) {
            EditorMain.IncludeCave = true;
        }
        selected = true;
    }

    void UnSelected() {
        rend.sprite = defaultSprite;
        if (fire) {
            EditorMain.IncludeFire = false;
        }
        else if (ice) {
            EditorMain.IncludeIce = false;
        }
        else if (desert) {
            EditorMain.IncludeDesert = false;
        }
        else if (cave) {
            EditorMain.IncludeCave = false;
        }
        selected = false;
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

