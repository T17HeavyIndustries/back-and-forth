using UnityEngine;
using System.Collections;

public class RotateHandler : MonoBehaviour {

    private bool inPuzzle = false;
    private bool inEditor = false;
    private bool inMenu = false;
    private int portrait = -1;

    private bool init = false;

    private GameObject menuLandscape;
    private GameObject menuPortrait;

    Camera leftCam;
    Camera rightCam;
    Camera mainCamera;

    private GameObject pallette;
    private GameObject grid;
    private GameObject levelParent;

    private bool dontSetMenus = false;

	void Awake () {
        DontDestroyOnLoad(transform.gameObject);
    
        if (!init) {
            mainCamera = Camera.main;
            init = true;
            ScreenPrep(Application.loadedLevel);
        }

	}

    void OnLevelWasLoaded(int level) {
            ScreenPrep(level);
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) {
                Portrait();
            }
            else {
                Landscape();
            }
    }

    void ScreenPrep(int level) {
        dontSetMenus = false;
        print("here rotate" + level.ToString());
        if (level > 0 && level < 51 || Application.loadedLevelName == "CustomLevel") {// in a puzzle
            inPuzzle = true;
            inMenu = false;
            inEditor = false;
            leftCam = GameObject.Find("Left Camera").camera;
            rightCam = GameObject.Find("Right Camera").camera;
        }
        else if (level == 0 || Application.loadedLevelName == "EditorInitial" || Application.loadedLevelName == "CustomLevelBrowser" || Application.loadedLevelName == "PuzzleMenu" || Application.loadedLevelName == "ScoresPage") { // Menus with portrait and landscape
            mainCamera = Camera.main;
            inPuzzle = false;
            inEditor = false;
            inMenu = true;

            menuLandscape = GameObject.Find("Landscape Menu");
            menuPortrait = GameObject.Find("Portrait Menu");
            menuLandscape.SetActive(false);
            menuPortrait.SetActive(false);

            CustomLevelBrowserButton.UnSelectAll();
            CustomLevelMain.CurLevelInfo = null;
        }
        else if (Application.loadedLevelName == "LevelEditor") {
            if (level == -1) {// testing
                inPuzzle = true;
                inMenu = false;
                inEditor = false;
                leftCam = GameObject.Find("Left Camera").camera;
                rightCam = GameObject.Find("Right Camera").camera;
                pallette = GameObject.Find("Editor Pallette");
                grid = GameObject.Find("Grid");
                levelParent = GameObject.Find("Level Parent Editor");
            }

            else {
                mainCamera = Camera.main;
                inPuzzle = false;
                inEditor = true;
                inMenu = false;
                if (menuLandscape == null && menuPortrait == null) {
                    menuLandscape = GameObject.Find("Initial Settings Landscape");
                    menuPortrait = GameObject.Find("Initial Settings Portrait");
                }
                else {
                    dontSetMenus = true;
                }
                pallette = GameObject.Find("Editor Pallette");
                grid = GameObject.Find("Grid");
                levelParent = GameObject.Find("Level Parent Editor");
            }
        }
        

        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) {
            Portrait();
        }
        else {
            Landscape();
        }
    }

	void Update () {

        if (Application.loadedLevelName == "PuzzleMenu") {
            if (!PuzzleMenuForward.Shifting && !PuzzleMenuBack.Shifting) {
                if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) {
                    if (portrait != 1)
                        Portrait();
                }
                else {
                    if (portrait != 0) {
                        //Landscape();
                        StartCoroutine("WaitAndGoLandscape");
                    }

                }
            }
        }


        else {
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) {
                if (portrait != 1)
                    Portrait();
            }
            else {
                if (portrait != 0)
                    Landscape();
            }
        }
        

        
	}


    private void Portrait() {
        portrait = 1;

        if (inPuzzle) {
            leftCam.rect = new Rect(0, 0.5f, 1f, 0.5f);
            rightCam.rect = new Rect(0, 0, 1f, 0.5f);

            SetCameraSizePuzzle();
        }
        else if(inMenu) {
            try {
                
                menuPortrait.SetActive(true);
                menuLandscape.SetActive(false);
            }
            catch {
                print("catch");
            }
            SetCameraSizeMenu();
        }
        else if (inEditor) {
            SetCameraSizeMenu();
            if (!dontSetMenus && menuLandscape.activeInHierarchy) {
                menuPortrait.SetActive(true);
                menuLandscape.SetActive(false);
            }
            pallette.transform.position = new Vector3(-80, -95, 0);
            grid.transform.position = new Vector3(80, 75, 0);
            levelParent.transform.position = new Vector3(80, 75, 0);

        }
    }

    private void Landscape() {
        portrait = 0;

        if (inPuzzle) {
            leftCam.rect = new Rect(0, 0, 0.5f, 1f);
            rightCam.rect = new Rect(0.5f, 0, .5f, 1f);

            SetCameraSizePuzzle();
        }
        else if(inMenu){
            menuLandscape.SetActive(true);
            menuPortrait.SetActive(false);
            SetCameraSizeMenu();
        }
        else if (inEditor) {
            SetCameraSizeMenu();
            if (!dontSetMenus && menuPortrait.activeInHierarchy) {
                menuLandscape.SetActive(true);
                menuPortrait.SetActive(false);
            }
            pallette.transform.position = new Vector3(0, -10, 0);
            grid.transform.position = Vector3.zero;
            levelParent.transform.position = Vector3.zero;
        }

    }

    private void SetCameraSizePuzzle() {
        int height;
        int width;

        if (portrait == 1) {
            height = Main.PortraitHeight / 2;
            width = Main.PortraitWidth;
        }
        else {
            height = Main.LandscapeHeight;
            width = Main.LandscapeWidth / 2;
        }

        if (height < 320 || width < 320) { // 1x
            leftCam.orthographicSize = height / 2;
            rightCam.orthographicSize = height / 2;
        }

        else if (height < 480 || width < 480) { // 2x
            leftCam.orthographicSize = height / 4;
            rightCam.orthographicSize = height / 4;
        }

        else if (height < 640 || width < 640) { // 3x
            leftCam.orthographicSize = height / 6;
            rightCam.orthographicSize = height / 6;
        }

        else if (height < 800 || width < 800) { // 4x
            leftCam.orthographicSize = height / 8;
            rightCam.orthographicSize = height / 8;
        }
        else if (height < 960 || width < 960) { // 5x
            leftCam.orthographicSize = height / 10;
            rightCam.orthographicSize = height / 10;
        }
        else if (height < 1120 || width < 1120) { // 56
            leftCam.orthographicSize = height / 12;
            rightCam.orthographicSize = height / 12;
        }

    }

    private void SetCameraSizeMenu() {
        int height;
        int width;

        if (portrait == 1) {
            height = Main.PortraitHeight;
            width = Main.PortraitWidth;

            if (width < 320 || height < 640 ) {
                mainCamera.orthographicSize = height / 2;
            }
            else if (width < 480 || height < 960) {
                mainCamera.orthographicSize = height / 4;
            }
            else if (width < 640 || height < 1280) {
                mainCamera.orthographicSize = height / 6;
            }
            else if (width < 800 || height < 1600) {
                mainCamera.orthographicSize = height / 8;
            }
            else if (width < 960 || height < 1920) {
                mainCamera.orthographicSize = height / 10;
            }
            else if (width < 1120 || height < 2240) {
                mainCamera.orthographicSize = height / 12;
            }

        }
        else {
            height = Main.LandscapeHeight;
            width = Main.LandscapeWidth;

            if (width < 640 || height < 320) {
                mainCamera.orthographicSize = height / 2;
            }
            else if (width < 960 || height < 480) {
                mainCamera.orthographicSize = height / 4;
            }
            else if (width < 1280 || height < 640) {
                mainCamera.orthographicSize = height / 6;
            }
            else if (width < 1600 || height < 800) {
                mainCamera.orthographicSize = height / 8;
            }
            else if (width < 1920 || height < 960) {
                mainCamera.orthographicSize = height / 10;
            }
            else if (width < 2240 || height < 1120) {
                mainCamera.orthographicSize = height / 12;
            }
        }

    }

    public void ForcePuzzle() {
        ScreenPrep(-1);
    }
    public void ForceEditor() {
        ScreenPrep(-2);
    }

    IEnumerator WaitAndGoLandscape() {
        yield return new WaitForSeconds(.25f);
        Landscape();
    }

}
