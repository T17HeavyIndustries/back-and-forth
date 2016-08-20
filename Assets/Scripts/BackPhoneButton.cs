using UnityEngine;
using System.Collections;

public class BackPhoneButton : MonoBehaviour {

    private bool mainMenu = false;//
    private bool editorMenu = false;//
    private bool editorCustomBrowser = false;//
    private bool editorLevelEditor = false;//
    private bool puzzleMenu = false;//
    private bool puzzle = false;
    private bool customPuzzle = false;

    public GameObject exitPopUp;

    public static bool PopUpInScene = false;

    public static GameObject curPop;

    void Start() {
        mainMenu = true; // assumes that we are starting on the main menu
    }

    void OnLevelWasLoaded(int level) {
        PopUpInScene = false;
        if (level == 0) {
            mainMenu = true;
            editorMenu = false;
            editorCustomBrowser = false;
            editorLevelEditor = false;
            puzzleMenu = false;
            puzzle = false;
            customPuzzle = false;
        }
        else if (Application.loadedLevelName == "EditorInitial") {
            mainMenu = false;
            editorMenu = true;
            editorCustomBrowser = false;
            editorLevelEditor = false;
            puzzleMenu = false;
            puzzle = false;
            customPuzzle = false;
        }
        else if (Application.loadedLevelName == "CustomLevelBrowser") {
            mainMenu = false;
            editorMenu = false;
            editorCustomBrowser = true;
            editorLevelEditor = false;
            puzzleMenu = false;
            puzzle = false;
            customPuzzle = false;
        }
        else if (Application.loadedLevelName == "LevelEditor") {
            mainMenu = false;
            editorMenu = false;
            editorCustomBrowser = false;
            editorLevelEditor = true;
            puzzleMenu = false;
            puzzle = false;
            customPuzzle = false;
        }
        else if (Application.loadedLevelName == "PuzzleMenu"){
            mainMenu = false;
            editorMenu = false;
            editorCustomBrowser = false;
            editorLevelEditor = false;
            puzzleMenu = true;
            puzzle = false;
            customPuzzle = false;
        }
        else if (level > 0 && level < 51){
            mainMenu = false;
            editorMenu = false;
            editorCustomBrowser = false;
            editorLevelEditor = false;
            puzzleMenu = false;
            puzzle = true;
            customPuzzle = false;
        }
        else if (Application.loadedLevelName == "CustomLevel"){
            mainMenu = false;
            editorMenu = false;
            editorCustomBrowser = false;
            editorLevelEditor = false;
            puzzleMenu = false;
            puzzle = false;
            customPuzzle = true;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (mainMenu) {
                Application.Quit();
            }
            else if (editorMenu) {

                Application.LoadLevel(0);
            }
            else if (editorCustomBrowser) {
                Application.LoadLevel("EditorInitial");
            }
            else if (puzzleMenu) {
                Application.LoadLevel(0);
            }
            else if (editorLevelEditor) {
                if (!MenuButton.PopUpInScene && !PopUpInScene) {
                    curPop = (GameObject)Instantiate(exitPopUp);
                    PopUpInScene = true;
                    return;
                }
                if (MenuButton.PopUpInScene) {
                    Destroy(MenuButton.curPop);
                    MenuButton.PopUpInScene = false;
                    try {
                        GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = true;
                    }
                    catch { }
                }
                if (PopUpInScene) {
                    Destroy(curPop);
                    PopUpInScene = false;
                    try {
                        GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = true;
                    }
                    catch { }
                }
                try {GameObject.Find("Test Completed").SetActive(false);}
                catch { }
                try { GameObject.Find("Enter Name").SetActive(false); }
                catch{}
            }
            else if ((puzzle || customPuzzle)) {
                if(!MenuButton.PopUpInScene && !PopUpInScene) {
                    curPop = (GameObject)Instantiate(exitPopUp);
                    PopUpInScene = true;
                    return;
                }
                if (MenuButton.PopUpInScene) {
                    Destroy(MenuButton.curPop);
                    MenuButton.PopUpInScene = false;
                    LevelController.TimePause = false;
                    try {
                        GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = true;
                    }
                    catch { }
                }
                if (PopUpInScene) {
                    Destroy(curPop);
                    PopUpInScene = false;
                    LevelController.TimePause = false;
                    try {
                        GameObject.Find("Player Parent").GetComponent<PlayerController>().enabled = true;
                    }
                    catch { }
                }
                
            }
        }
    }

    public void CreateExitPopUp() {
        if (!PopUpInScene) {
            curPop = (GameObject)Instantiate(exitPopUp);
            PopUpInScene = true;
        }
    }
}


