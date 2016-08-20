using UnityEngine;
using System.Collections;

public class EditorBackButton : MonoBehaviour {
    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    private Sprite defaultSprite;
    private Sprite hoverSprite;

    public Sprite fireDefault;
    public Sprite fireSelected;
    public Sprite iceDefault;
    public Sprite iceSelected;
    public Sprite desertDefault;
    public Sprite desertSelected;
    public Sprite caveDefault;
    public Sprite caveSelected;

    private SpriteRenderer rend;

    void Start() {
        rend = GetComponent<SpriteRenderer>();

        if (EditorMain.CurrentEditing == "Fire") {
            defaultSprite = fireDefault;
            hoverSprite = fireSelected;
            rend.sprite = fireDefault;
        }
        else if (EditorMain.CurrentEditing == "Ice") {
            defaultSprite = iceDefault;
            hoverSprite = iceSelected;
            rend.sprite = iceDefault;
        }
        else if (EditorMain.CurrentEditing == "Desert") {
            defaultSprite = desertDefault;
            hoverSprite = desertSelected;
            rend.sprite = desertDefault;
        }
        else if (EditorMain.CurrentEditing == "Cave") {
            defaultSprite = caveDefault;
            hoverSprite = caveSelected;
            rend.sprite = caveDefault;
        }
    }

    void Update() {
#if UNITY_ANDROID
        if (Input.touchCount == 0) {
            if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                GameObject.Find("Main").GetComponent<BackPhoneButton>().CreateExitPopUp();
                touched = false;
            }
            rend.sprite = defaultSprite;
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
#endif

        if (EditorMain.CurrentEditing == "Fire") {
            defaultSprite = fireDefault;
            hoverSprite = fireSelected;
        }
        else if (EditorMain.CurrentEditing == "Ice") {
            defaultSprite = iceDefault;
            hoverSprite = iceSelected;
        }
        else if (EditorMain.CurrentEditing == "Desert") {
            defaultSprite = desertDefault;
            hoverSprite = desertSelected;
        }
        else if (EditorMain.CurrentEditing == "Cave") {
            defaultSprite = caveDefault;
            hoverSprite = caveSelected;
        }
    }
#if UNITY_EDITOR
    void OnMouseDown() {
        GameObject.Find("Main").GetComponent<BackPhoneButton>().CreateExitPopUp();

    }
#elif UNITY_STANDALONE
    void OnMouseDown(){
            GameObject.Find("Main").GetComponent<BackPhoneButton>().CreateExitPopUp();

    }

#endif
}
