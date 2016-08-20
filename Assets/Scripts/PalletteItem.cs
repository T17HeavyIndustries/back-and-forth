using UnityEngine;
using System.Collections;

public class PalletteItem : MonoBehaviour {

    public string tileType;

    private EditorPallette editorPallette;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    void Start() {
        editorPallette = GameObject.Find("Editor Pallette").GetComponent<EditorPallette>();
    }

#if UNITY_ANDROID
    void Update() {
        if (Input.touchCount == 0) {
            if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                editorPallette.SetCurrentTile(tileType);
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
	void OnMouseDown(){
        editorPallette.SetCurrentTile(tileType);
    }

#elif UNITY_STANDALONE
    void OnMouseDown(){
        editorPallette.SetCurrentTile(tileType);
    }
#endif
}
