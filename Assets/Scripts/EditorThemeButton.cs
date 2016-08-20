using UnityEngine;
using System.Collections;

public class EditorThemeButton : MonoBehaviour {

    public bool fire = false;
    public bool ice = false;
    public bool desert = false;
    public bool cave = false;

    private EditorMain editorMain;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    void Start() {
        editorMain = GameObject.Find("Editor Main").GetComponent<EditorMain>();
    }


#if UNITY_ANDROID
    void Update() {
        if (Input.touchCount == 0) {
            if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                ThemeSelected();
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
    }
#endif

#if UNITY_EDITOR
    void OnMouseDown() {
        ThemeSelected();
    }
#elif UNITY_STANDALONE
    void OnMouseDown() {
        ThemeSelected();
    }
#endif

    void ThemeSelected() {
        if (fire) {
            if (EditorMain.CurrentEditing != "Fire") {
                editorMain.ChangeTheme("Fire");
            }
        }
        else if (ice) {
            if (EditorMain.CurrentEditing != "Ice" ) {
                editorMain.ChangeTheme("Ice");
            }
        }
        else if (desert) {
            if (EditorMain.CurrentEditing != "Desert") {
                editorMain.ChangeTheme("Desert");
            }
        }
        else {//Cave
            if (EditorMain.CurrentEditing != "Cave" ) {
                editorMain.ChangeTheme("Cave");
            }
        }
    }
}
