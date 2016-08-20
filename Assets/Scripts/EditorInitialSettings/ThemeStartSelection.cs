using UnityEngine;
using System.Collections;

public class ThemeStartSelection : MonoBehaviour {
    private EditorMain editorMain;
    public GameObject otherButton;

    public bool fireDesert = false;
    public bool iceCave = false;

    public Sprite defaultSprite;
    public Sprite hoverSprite;
    private SpriteRenderer rend;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    private bool selected;

    void Awake() {
        rend = GetComponent<SpriteRenderer>();
    }

    void Start() {

       
        editorMain = GameObject.Find("Editor Main").GetComponent<EditorMain>();
        if (fireDesert) {
            Down();
        }
    }

    void OnEnable() {
        if (fireDesert) {
            if (EditorMain.StartFireDesert) {
                Select();
            }
        }
        else {
            if (EditorMain.StartIceCave) {
                Select();
            }
        }
    }

    void Update() {
#if UNITY_ANDROID
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
               
                touched = true;
            }
        }
#endif
        if (fireDesert && selected && !EditorMain.IncludeFire && !EditorMain.IncludeDesert) {
            otherButton.GetComponent<ThemeStartSelection>().Select();
        }
        else if (iceCave && selected && !EditorMain.IncludeIce && !EditorMain.IncludeCave) {
            otherButton.GetComponent<ThemeStartSelection>().Select();
        }
    }


    void Down() {
        if (fireDesert && (EditorMain.IncludeFire || EditorMain.IncludeDesert)) {
            Select();
        }
        else if (iceCave && (EditorMain.IncludeIce || EditorMain.IncludeCave)) {
                Select();
            }
    }

     public void Select(){
        rend.sprite = hoverSprite;
        if (fireDesert) {
            EditorMain.StartFireDesert = true;
        }
        else {
            EditorMain.StartIceCave = true;
        }
        otherButton.GetComponent<ThemeStartSelection>().UnSelect();
        selected = true;
     }

     public void UnSelect() {
         rend.sprite = defaultSprite;
         if (fireDesert) {
             EditorMain.StartFireDesert = false;
         }
         else {
             EditorMain.StartIceCave = false;
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
