using UnityEngine;
using System.Collections;

public class EditorStartButton : MonoBehaviour {

    private GameObject initialSettingsParent;

    private EditorMain editorMain;

    public Sprite defaultSprite;
    public Sprite hoverSprite;
    private SpriteRenderer rend;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    

    void Start() {
        initialSettingsParent = transform.parent.gameObject;
        rend = GetComponent<SpriteRenderer>();
        editorMain = GameObject.Find("Editor Main").GetComponent<EditorMain>();
    }

#if UNITY_ANDROID
    void Update() {
        if (Input.touchCount == 0) {
            if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                EditorMain.InEditor = true;
                editorMain.EditorStart();
                touched = false;
                initialSettingsParent.SetActive(false);
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
#endif

#if UNITY_EDITOR
    void OnMouseDown() {
        rend.sprite = hoverSprite;
      
            editorMain.EditorStart();
            initialSettingsParent.SetActive(false);
        
    }
#elif UNITY_STANDALONE
    void OnMouseDown() {
        rend.sprite = hoverSprite;
       
            editorMain.EditorStart();
            initialSettingsParent.SetActive(false);
        
    }
#endif
}
