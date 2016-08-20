using UnityEngine;
using System.Collections;

public class ExitPopUpYes : MonoBehaviour {

    private SpriteRenderer rend;
    private Sprite defaultSprite;
    private Sprite defaultHover;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    public Sprite fireFull;
    public Sprite desert;
    public Sprite cave;

    public Sprite caveDefault;
    public Sprite caveHover;
    public Sprite desertDefault;
    public Sprite desertHover;
    public Sprite fireDefault;
    public Sprite fireHover;

    public ExitPopUpNo brother;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
       
        touched = false;
        Input.simulateMouseWithTouches = false;

        try { transform.GetChild(0).GetComponent<MeshRenderer>().sortingLayerName = "Text"; }
        catch { }
        try { GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = false; }
        catch { }


        if (Application.loadedLevelName == "LevelEditor") {
            if (!EditorMain.InEditor) {
                GameObject.Find("Level Parent");
                transform.parent.transform.position = new Vector3(1600, 1000, -1);
                if (GameObject.Find("DesertBackground").GetComponent<SpriteRenderer>().enabled) {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = desert;
                    GetComponent<SpriteRenderer>().sprite = desertDefault;
                    brother.gameObject.GetComponent<SpriteRenderer>().sprite = desertDefault;
                    defaultSprite = desertDefault;
                    defaultHover = desertHover;
                    brother.defaultSprite = desertDefault;
                    brother.defaultHover = desertHover;
                }

                else {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = cave;
                    GetComponent<SpriteRenderer>().sprite = caveDefault;
                    brother.gameObject.GetComponent<SpriteRenderer>().sprite = caveDefault;
                    defaultSprite = caveDefault;
                    defaultHover = caveHover;
                    brother.defaultSprite = caveDefault;
                    brother.defaultHover = caveHover;
                }
            }
            else {
                transform.parent.FindChild("Text").GetComponent<TextMesh>().text = "Exit Level Editor?";
                transform.parent.transform.position = new Vector3(80, 0, -1);
                transform.parent.GetComponent<SpriteRenderer>().sprite = fireFull;
                defaultSprite = fireDefault;
                defaultHover = fireHover;
            }
        }
                   
        else {
            transform.parent.transform.position = new Vector3(1600, 1000, -1);
            if (GameObject.Find("DesertBackground").GetComponent<SpriteRenderer>().enabled) {
                transform.parent.GetComponent<SpriteRenderer>().sprite = desert;
                GetComponent<SpriteRenderer>().sprite = desertDefault;
                brother.gameObject.GetComponent<SpriteRenderer>().sprite = desertDefault;
                defaultSprite = desertDefault;
                defaultHover = desertHover;
                brother.defaultSprite = desertDefault;
                brother.defaultHover = desertHover;
                
            }
            else {
                transform.parent.GetComponent<SpriteRenderer>().sprite = cave;
                GetComponent<SpriteRenderer>().sprite = caveDefault;
                brother.gameObject.GetComponent<SpriteRenderer>().sprite = caveDefault;
                defaultSprite = caveDefault;
                defaultHover = caveHover;
                brother.defaultSprite = caveDefault;
                brother.defaultHover = caveHover;
            }
            
        }
    }

    void Update() {
#if UNITY_ANDROID
        //being pressed
        if (Input.touchCount == 0) {
            ExitHover();
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
                Hover();
                touched = true;
            }
            else {
                ExitHover();
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
        else if (Application.loadedLevelName == "CustomLevel") {
            Application.LoadLevel("CustomLevelBrowser");
        }
        else if (Application.loadedLevelName == "LevelEditor") {
            if (!EditorMain.InEditor) {
                GameObject.Find("Editor Main").GetComponent<EditorMain>().BackToEditor();
                Destroy(BackPhoneButton.curPop);
                BackPhoneButton.PopUpInScene = false;
            }
            else {
                Application.LoadLevel("EditorInitial");
            }
            
        }
        BackPhoneButton.PopUpInScene = false;
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
