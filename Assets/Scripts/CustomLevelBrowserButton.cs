using UnityEngine;
using System.Collections;

public class CustomLevelBrowserButton : MonoBehaviour {

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

    public bool selected = false;

    SpriteRenderer rend;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    void Start() {
        transform.FindChild("Name").GetComponent<TextMesh>().text =  levelInfo.name;
        float highScore = 0;
        if (Main.CustomHighScores.TryGetValue(levelInfo.name, out highScore)) {
            transform.FindChild("Score").GetComponent<TextMesh>().text = highScore.ToString("0.0");
        }
        else {
            transform.FindChild("Score").GetComponent<TextMesh>().text = "0.0";
        }

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
#if UNITY_ANDROID
        if (Input.touchCount == 0) {
            if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                Select();
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
    }

    void Select() {
        UnSelectAll();

        CustomLevelMain.CurLevelInfo = levelInfo;
        selected = true;
        rend.sprite = defaultSelected;

    }
    public void UnSelect() {
        selected = false;
        rend.sprite = defaultSprite;
    }

    public static void UnSelectAll(){
        CustomLevelMain.CurLevelInfo = null;
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Level Button");
        foreach (GameObject button in buttons) {
            if(button.activeInHierarchy)
                button.GetComponent<CustomLevelBrowserButton>().UnSelect();
        };
    }

#if UNITY_EDITOR
    void OnMouseDown() {
        Select();
    }
#elif UNITY_STANDALONE
    void OnMouseDown() {
        Select();
    }
#endif


}
