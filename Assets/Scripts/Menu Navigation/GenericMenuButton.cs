using UnityEngine;
using System.Collections;

public class GenericMenuButton : MonoBehaviour {

    private SpriteRenderer rend;
    public Sprite defaultSprite;
    public Sprite defaultHover;

    public Sprite locked; // only for puzzle
    public Sprite lockedHover; // only for puzz/e
    public Sprite complete; // ditto 
    public Sprite completeHover; // ditto
    public string puzzleName; // only for puzzle

    public bool open = false; // only for puzzle
    public bool beaten = false; // only for puzzle

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();


    // Button definiton bools
    public bool MainMenuPlay = false;
    public bool MainMenuEditor = false;
    public bool MainMenuMoreGames = false;
    public bool MainMenuPremium = false;
    public bool MenuBackToMainMenu = false;
    public bool PuzzleMenuSelection = false;
    public bool EditorInitialCreateNew = false;
    public bool EditorInitialPlayCustom = false;
    public bool EditorTestStart = false;
    public bool EditorTestFinish = false;
    public bool EditorTestBack = false;
    public bool PuzzleMenuHighScores = false;
    public bool EditorBack = false;
    public bool ScoresBack = false;

    void Start() {

        rend = GetComponent<SpriteRenderer>();

        if (PuzzleMenuSelection) {// only for puzzle selection
            if (beaten) {
                rend.sprite = complete;
            }
            else if (open) {
                rend.sprite = defaultSprite;
            }
            else {
                rend.sprite = locked;
            }
        }
        touched = false;
        Input.simulateMouseWithTouches = false;

        try { transform.GetChild(0).GetComponent<MeshRenderer>().sortingLayerName = "Text"; }
        catch { }
    }

    void Update(){
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
        if (PuzzleMenuSelection) { // only for puzzle selection
            if (beaten) {
                rend.sprite = completeHover;
            }
            else if (open) {
                rend.sprite = defaultHover;
            }
            else {
                rend.sprite = lockedHover;
            }
        }
        else {
            rend.sprite = defaultHover;
        }
    }

    void ExitHover() {
        if (PuzzleMenuSelection) { // only for puzzle selection
            if (beaten) {
                rend.sprite = complete;
            }
            else if (open) {
                rend.sprite = defaultHover;
            }
            else {
                rend.sprite = locked;
            }
        }
        else {
            rend.sprite = defaultSprite;
        }
    }

    void Down() {
        if (MainMenuPlay) {
            MenuManager.PageNumber = 1;
            Application.LoadLevel("PuzzleMenu");
        }
        else if (MainMenuEditor) {
            Application.LoadLevel("EditorInitial");
        }
        else if (MainMenuMoreGames) {
            GoogleAnalytics.instance.LogScreen("More Games Selected");
            Application.OpenURL("http://www.t17heavyindustries.com/games/");
            touched = false;
            ExitHover();
        }
        else if (MainMenuPremium){
            GoogleAnalytics.instance.LogScreen("Premium Selected");
            Application.OpenURL("market://details?id=com.t17heavyindustries.back_and_forth");
            touched = false;
            ExitHover();
        }
        else if (PuzzleMenuSelection) {
            if (open) {
                Application.LoadLevel(puzzleName);
            }
        }
        else if (MenuBackToMainMenu) {
            Application.LoadLevel(0);
        }
        else if (EditorInitialCreateNew) {
            Application.LoadLevel("LevelEditor");
        }
        else if (EditorInitialCreateNew) {
            //Application.LoadLevel("Custom Level Loader");

            //temp
            touched = false;
            ExitHover();
        }
        else if (EditorTestStart) {
            GameObject.Find("Editor Main").GetComponent<EditorMain>().PackagePuzzle();
        }
        else if (EditorTestBack) {
            GameObject.Find("Editor Main").GetComponent<EditorMain>().BackToEditor();
        }
        else if (EditorTestFinish) {
            //GameObject.Find("Editor Main").GetComponent<EditorMain>().SaveLevel();
            //nameEnter.SetActive(true);
           // Destroy(GameObject.Find("Test Completed"));
        }
        else if (PuzzleMenuHighScores) {
            if(!PuzzleMenuForward.Shifting && !PuzzleMenuBack.Shifting)
                Application.LoadLevel("ScoresPage");
        }
        else if (EditorInitialPlayCustom) {
            Application.LoadLevel("CustomLevelBrowser");
        }
        else if (EditorBack) {
            GameObject.Find("Main").GetComponent<BackPhoneButton>().CreateExitPopUp();
        }
        else if (ScoresBack) {
            Application.LoadLevel("PuzzleMenu");
        }

    }

#if UNITY_EDITOR
    void OnMouseDown(){
        Down();
    }

#elif UNITY_STANDALONE
    void OnMouseDown(){
        Down();
    }
#endif
}


