using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorMain : MonoBehaviour {
    //LevelSerializer.SaveObjectTreeToFile("test.lev", levelParent);
    //LevelSerializer.LoadObjectTreeFromFile("test.lev");

    public static bool StartFireDesert;
    public static bool StartIceCave;

    public static bool SwitchLeftSide;
    public static bool SwitchRightSide;

    public static bool IncludeFire;
    public static bool IncludeIce;
    public static bool IncludeDesert;
    public static bool IncludeCave;

    public static bool InEditor = true;

    private GameObject levelParent;

    private GameObject fireCanvas;
    private GameObject iceCanvas;
    private GameObject desertCanvas;
    private GameObject caveCanvas;

    private GameObject firePallette;
    private GameObject icePallette;
    private GameObject desertPallette;
    private GameObject cavePallette;

    private GameObject editorPallette;
    private GameObject grid;

    public static bool FireIceEntrancePlaced = false;
    public static bool FireIceExitPlaced = false;
    public static bool DesertCaveEntrancePlaced = false;
    public static bool DesertCaveExitPlaced = false;
    public static string CurrentEditing; // "Fire" "Ice" "Desert" "Cave"
    public static Vector3 FireIceExitPos;
    public static Vector3 DesertCaveExitPos;
    public static Vector3 FireIceStartPos;
    public static Vector3 DesertCaveStartPos;

    private bool firstFrame = true;

    public static int CustomLevelCount;

    //For packaging the level
    private GameObject mainCam;
    private GameObject packagedLevelParent;
    private GameObject playerParent;

    public GameObject leftPlayer;
    public GameObject rightPlayer;
    public GameObject leftCamera;
    public GameObject rightCamera;
    public GameObject gui;
    public GameObject exit;
    
    //for testing level
    public GameObject completedPrompt;
    private List<GameObject> testObjects = new List<GameObject>();

    void Awake() {
        StartFireDesert = false;
        StartIceCave = false;
        SwitchLeftSide = false;
        SwitchRightSide = false;
        IncludeFire = false;
        IncludeDesert = false;
        IncludeIce = false;
        IncludeCave = false;
    }

	void Start () {
        GoogleAnalytics.instance.LogScreen("Editor Initial Settings");
        fireCanvas = GameObject.Find("Fire");
        iceCanvas = GameObject.Find("Ice");
        desertCanvas = GameObject.Find("Desert");
        caveCanvas = GameObject.Find("Cave");

        firePallette = GameObject.Find("Fire Pallette");
        icePallette = GameObject.Find("Ice Pallette");
        desertPallette = GameObject.Find("Desert Pallette");
        cavePallette = GameObject.Find("Cave Pallette");

        levelParent = GameObject.Find("Level Parent Editor");
        editorPallette = GameObject.Find("Editor Pallette");
        grid = GameObject.Find("Grid");

        mainCam = Camera.main.gameObject;
	}

    void Update() {

        if (firstFrame) {
            fireCanvas.SetActive(false);
            iceCanvas.SetActive(false);
            caveCanvas.SetActive(false);
            desertCanvas.SetActive(false);
            editorPallette.SetActive(false);     
            grid.SetActive(false);

            firstFrame = false;
        }

       
    }

    public void EditorStart() {
        InEditor = true;
        GoogleAnalytics.instance.LogScreen("Editor");
        grid.SetActive(true);
        editorPallette.SetActive(true);
        EditorPallette.PrimaryTile = null;
        EditorPallette.SecondaryTile = null;
        levelParent.SetActive(true);

        if (IncludeFire && IncludeIce) {
            SwitchLeftSide = true;
        }
        else{
            SwitchLeftSide = false;
        }
        if (IncludeDesert && IncludeCave) {
            SwitchRightSide = true;
        }
        else {
            SwitchRightSide = false;
        }

        if (!IncludeFire) {
            GameObject.Find("Toggle Fire").SetActive(false);
        }
        if (!IncludeIce) {
            GameObject.Find("Toggle Ice").SetActive(false);
        }
        if (!IncludeDesert) {
            GameObject.Find("Toggle Desert").SetActive(false);
        }
        if (!IncludeCave) {
            GameObject.Find("Toggle Cave").SetActive(false);
        }

        if (!SwitchLeftSide) {
            GameObject[] fireSpecial = GameObject.FindGameObjectsWithTag("Special Tile Fire");
            foreach (GameObject item in fireSpecial) {
                item.SetActive(false);
            }
        }
        if (!SwitchRightSide) {
            GameObject[] desertSpecial = GameObject.FindGameObjectsWithTag("Special Desert");
            print(desertSpecial.Length);
            foreach (GameObject item in desertSpecial) {
                item.SetActive(false);
            }
        }

        if (StartFireDesert && IncludeFire) {
            fireCanvas.SetActive(true);
            iceCanvas.SetActive(false);
            caveCanvas.SetActive(false);
            desertCanvas.SetActive(false);
            firePallette.SetActive(true);
            icePallette.SetActive(false);
            desertPallette.SetActive(false);
            cavePallette.SetActive(false);
            CurrentEditing = "Fire";
        }
        else if (StartFireDesert && IncludeDesert) {
            fireCanvas.SetActive(false);
            iceCanvas.SetActive(false);
            caveCanvas.SetActive(false);
            desertCanvas.SetActive(true);
            firePallette.SetActive(false);
            icePallette.SetActive(false);
            desertPallette.SetActive(true);
            cavePallette.SetActive(false);
            CurrentEditing = "Fire";
        }

        else if (StartIceCave && IncludeIce) {
            fireCanvas.SetActive(false);
            iceCanvas.SetActive(true);
            caveCanvas.SetActive(false);
            desertCanvas.SetActive(false);
            firePallette.SetActive(false);
            icePallette.SetActive(true);
            desertPallette.SetActive(false);
            cavePallette.SetActive(false);
            CurrentEditing = "Ice";
        }
        else if (StartIceCave && IncludeCave) {
            fireCanvas.SetActive(false);
            iceCanvas.SetActive(false);
            caveCanvas.SetActive(true);
            desertCanvas.SetActive(false);
            firePallette.SetActive(false);
            icePallette.SetActive(true);
            desertPallette.SetActive(false);
            cavePallette.SetActive(false);
            CurrentEditing = "Ice";
        }
    }

    public GameObject[] GridTouched(GameObject gridObject) {

        GameObject[] returnVal = new GameObject[2];

        if (EditorPallette.PrimaryTile != null) {

            returnVal[0] = (GameObject)Instantiate(EditorPallette.PrimaryTile, gridObject.transform.position, Quaternion.identity);
            returnVal[1] = (GameObject)Instantiate(EditorPallette.SecondaryTile, gridObject.transform.position, Quaternion.identity);

            if (CurrentEditing == "Fire") {
                if (EditorPallette.PrimaryTile.name != "Fire Pit") {
                    returnVal[0].transform.parent = fireCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;
                    returnVal[1].transform.parent = iceCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;
                }
                else {
                    returnVal[0].transform.parent = fireCanvas.transform.FindChildIncludingDeactivated("Colliders").transform.FindChildIncludingDeactivated("Pit").transform;
                    returnVal[1].transform.parent = iceCanvas.transform.FindChildIncludingDeactivated("Colliders").transform.FindChildIncludingDeactivated("Pit").transform;
                }
            }
            else if (CurrentEditing == "Ice") {
                if (EditorPallette.PrimaryTile.name != "Ice Pit") {
                    returnVal[0].transform.parent = iceCanvas.transform.FindChildIncludingDeactivated("Colliders").transform; ;
                    returnVal[1].transform.parent = fireCanvas.transform.FindChildIncludingDeactivated("Colliders").transform; 
                }
                else {
                    returnVal[0].transform.parent = iceCanvas.transform.FindChildIncludingDeactivated("Colliders").transform.FindChildIncludingDeactivated("Pit").transform;
                    returnVal[1].transform.parent = fireCanvas.transform.FindChildIncludingDeactivated("Colliders").transform.FindChildIncludingDeactivated("Pit").transform;
                }
            }
            else if (CurrentEditing == "Desert") {
                if (EditorPallette.PrimaryTile.name != "Desert Pit") {
                    returnVal[0].transform.parent = desertCanvas.transform.FindChildIncludingDeactivated("Colliders").transform; ;
                    returnVal[1].transform.parent = caveCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;
                }
                else {
                    returnVal[0].transform.parent = desertCanvas.transform.FindChildIncludingDeactivated("Colliders").transform.FindChildIncludingDeactivated("Pit").transform;
                    returnVal[1].transform.parent = caveCanvas.transform.FindChildIncludingDeactivated("Colliders").transform.FindChildIncludingDeactivated("Pit").transform;
                }
            }
            else if (CurrentEditing == "Cave") {
                if (EditorPallette.PrimaryTile.name != "Cave Pit") {
                    returnVal[0].transform.parent = caveCanvas.transform.FindChildIncludingDeactivated("Colliders").transform; ;
                    returnVal[1].transform.parent = desertCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;
                }
                else {
                    returnVal[0].transform.parent = caveCanvas.transform.FindChildIncludingDeactivated("Colliders").transform.FindChildIncludingDeactivated("Pit").transform;
                    returnVal[1].transform.parent = desertCanvas.transform.FindChildIncludingDeactivated("Colliders").transform.FindChildIncludingDeactivated("Pit").transform;
                }
            }

            return returnVal;
        }
        else return null;
    }

    public void ChangeTheme(string str) {

        EditorPallette.PrimaryTile = null;
        EditorPallette.SecondaryTile = null;

        fireCanvas.SetActive(false);
        iceCanvas.SetActive(false);
        caveCanvas.SetActive(false);
        desertCanvas.SetActive(false);
        firePallette.SetActive(false);
        icePallette.SetActive(false);
        desertPallette.SetActive(false);
        cavePallette.SetActive(false);

        if (str == "Fire") {
            fireCanvas.SetActive(true);
            firePallette.SetActive(true);
            CurrentEditing = "Fire";
        }
        else if (str == "Ice") {
            iceCanvas.SetActive(true);
            icePallette.SetActive(true);
            CurrentEditing = "Ice";
        }
        else if (str == "Desert") {
            desertCanvas.SetActive(true);
            desertPallette.SetActive(true);
            CurrentEditing = "Desert";
        }
        else if (str == "Cave") {
            caveCanvas.SetActive(true);
            cavePallette.SetActive(true);
            CurrentEditing = "Cave";
        }
    }

    public void PackagePuzzle() {
        if (FireIceEntrancePlaced && FireIceExitPlaced && DesertCaveEntrancePlaced && DesertCaveExitPlaced) {

            InEditor = false;

            packagedLevelParent = (GameObject)Instantiate(levelParent);
            testObjects.Add(packagedLevelParent);
            packagedLevelParent.name = "Level Parent";

                     

            //Move themes correct location
            packagedLevelParent.transform.position = Vector3.zero;
            packagedLevelParent.transform.FindChild("Fire").transform.position = new Vector3(800, 1000, 0);
            packagedLevelParent.transform.FindChild("Ice").transform.position = new Vector3(800, 1000, 0);
            packagedLevelParent.transform.FindChild("Desert").transform.position = new Vector3(1600, 1000, 0);
            packagedLevelParent.transform.FindChild("Cave").transform.position = new Vector3(1600, 1000, 0);

            //Add GUI
            /*GameObject _gui = (GameObject)Instantiate(gui);
            testObjects.Add(_gui);
            _gui.name = "GUI";*/

            //Add level controller script
            LevelController levelController = packagedLevelParent.AddComponent<LevelController>();
            levelController.leftSwitchable = SwitchLeftSide;
            levelController.rightSwitchable = SwitchRightSide;          
            levelController.startFireDesert = StartFireDesert;
            levelController.startIceCave = StartIceCave;
            levelController.includeFire = IncludeFire;
            levelController.includeIce = IncludeIce;
            levelController.includeDesert = IncludeDesert;
            levelController.includeCave = IncludeCave;

            //Activate all of the themes
            packagedLevelParent.transform.FindChild("Fire").gameObject.SetActive(true);
            packagedLevelParent.transform.FindChild("Ice").gameObject.SetActive(true);
            packagedLevelParent.transform.FindChild("Desert").gameObject.SetActive(true);
            packagedLevelParent.transform.FindChild("Cave").gameObject.SetActive(true);

            //Activate all of the colliders
            GameObject fireCol = packagedLevelParent.transform.FindChild("Fire").FindChild("Colliders").gameObject;
            GameObject firePit = packagedLevelParent.transform.FindChild("Fire").FindChild("Colliders").FindChild("Pit").gameObject;

            for(int i = 0; i < fireCol.transform.childCount; i++){
                try { fireCol.transform.GetChild(i).collider2D.enabled = true; }
                catch{}
            }
            for (int i = 0; i < firePit.transform.childCount; i++) {
                try { firePit.transform.GetChild(i).collider2D.enabled = true; }
                catch { }
            }

            GameObject desertCol = packagedLevelParent.transform.FindChild("Desert").FindChild("Colliders").gameObject;
            GameObject desertPit = packagedLevelParent.transform.FindChild("Desert").FindChild("Colliders").FindChild("Pit").gameObject;

            for (int i = 0; i < desertCol.transform.childCount; i++) {
                try { desertCol.transform.GetChild(i).collider2D.enabled = true; }
                catch { }
            }
            for (int i = 0; i < desertPit.transform.childCount; i++) {
                try { desertPit.transform.GetChild(i).collider2D.enabled = true; }
                catch { }
            }

            GameObject iceCol = packagedLevelParent.transform.FindChild("Ice").FindChild("Colliders").gameObject;
            GameObject icePit = packagedLevelParent.transform.FindChild("Ice").FindChild("Colliders").FindChild("Pit").gameObject;

            for (int i = 0; i < iceCol.transform.childCount; i++) {
                try { iceCol.transform.GetChild(i).collider2D.enabled = true; }
                catch { }
            }
            for (int i = 0; i < icePit.transform.childCount; i++) {
                try { icePit.transform.GetChild(i).collider2D.enabled = true; }
                catch { }
            }

            GameObject caveCol = packagedLevelParent.transform.FindChild("Cave").FindChild("Colliders").gameObject;
            GameObject cavePit = packagedLevelParent.transform.FindChild("Cave").FindChild("Colliders").FindChild("Pit").gameObject;

            for (int i = 0; i < caveCol.transform.childCount; i++) {
                try { caveCol.transform.GetChild(i).collider2D.enabled = true; }
                catch { }
            }
            for (int i = 0; i < cavePit.transform.childCount; i++) {
                try { cavePit.transform.GetChild(i).collider2D.enabled = true; }
                catch { }
            }
            

            if (StartFireDesert) {
                if (IncludeFire) {
                    packagedLevelParent.transform.FindChild("Fire").transform.FindChild("FireBackground").GetComponent<SpriteRenderer>().enabled = true;
                    packagedLevelParent.transform.FindChild("Ice").transform.FindChild("IceBackground").GetComponent<SpriteRenderer>().enabled = false;
                    packagedLevelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(true);
                    packagedLevelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(false);
                }
                else {
                    packagedLevelParent.transform.FindChild("Fire").transform.FindChild("FireBackground").GetComponent<SpriteRenderer>().enabled = false;
                    packagedLevelParent.transform.FindChild("Ice").transform.FindChild("IceBackground").GetComponent<SpriteRenderer>().enabled = true;
                    packagedLevelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(false);
                    packagedLevelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(true);
                }
                if (IncludeDesert) {
                    packagedLevelParent.transform.FindChild("Desert").transform.FindChild("DesertBackground").GetComponent<SpriteRenderer>().enabled = true;
                    packagedLevelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(true);
                    packagedLevelParent.transform.FindChild("Cave").transform.FindChild("CaveBackground").GetComponent<SpriteRenderer>().enabled = false;
                    packagedLevelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(false);
                }
                else {
                    packagedLevelParent.transform.FindChild("Desert").transform.FindChild("DesertBackground").GetComponent<SpriteRenderer>().enabled = false;
                    packagedLevelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(false);
                    packagedLevelParent.transform.FindChild("Cave").transform.FindChild("CaveBackground").GetComponent<SpriteRenderer>().enabled = true;
                    packagedLevelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(true);
                }
              
                  
            }
            else {
                if (IncludeIce) {
                    packagedLevelParent.transform.FindChild("Fire").transform.FindChild("FireBackground").GetComponent<SpriteRenderer>().enabled = false;
                    packagedLevelParent.transform.FindChild("Ice").transform.FindChild("IceBackground").GetComponent<SpriteRenderer>().enabled = true;
                    packagedLevelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(false);
                    packagedLevelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(true);
                }
                else {
                    packagedLevelParent.transform.FindChild("Fire").transform.FindChild("FireBackground").GetComponent<SpriteRenderer>().enabled = true;
                    packagedLevelParent.transform.FindChild("Ice").transform.FindChild("IceBackground").GetComponent<SpriteRenderer>().enabled = false;
                    packagedLevelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(true);
                    packagedLevelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(false);
                }

                if (IncludeCave) {
                    packagedLevelParent.transform.FindChild("Desert").transform.FindChild("DesertBackground").GetComponent<SpriteRenderer>().enabled = false;
                    packagedLevelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(false);
                    packagedLevelParent.transform.FindChild("Cave").transform.FindChild("CaveBackground").GetComponent<SpriteRenderer>().enabled = true;
                    packagedLevelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(true);
                }

                else {
                    packagedLevelParent.transform.FindChild("Desert").transform.FindChild("DesertBackground").GetComponent<SpriteRenderer>().enabled = true;
                    packagedLevelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(true);
                    packagedLevelParent.transform.FindChild("Cave").transform.FindChild("CaveBackground").GetComponent<SpriteRenderer>().enabled = false;
                    packagedLevelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(false);
                }
            }

            //Add cameras
            GameObject rightCam = (GameObject)Instantiate(rightCamera);
            GameObject leftCam = (GameObject)Instantiate(leftCamera);
            testObjects.Add(rightCam);
            testObjects.Add(leftCam);
            rightCam.name = "Right Camera";
            leftCam.name = "Left Camera";
            mainCam.SetActive(false);

            GameObject.Find("Main").GetComponent<RotateHandler>().ForcePuzzle();

            //Add Player
            playerParent = new GameObject();
            testObjects.Add(playerParent);
            playerParent.name = "Player Parent";
            GameObject playerL = (GameObject)Instantiate(leftPlayer);
            GameObject playerR = (GameObject)Instantiate(rightPlayer);
            testObjects.Add(playerL);
            testObjects.Add(playerR);
            playerL.name = "Player Left";
            playerR.name = "Player Right";
            playerL.transform.parent = playerParent.transform;
            playerR.transform.parent = playerParent.transform;

            playerL.transform.position = new Vector3(FireIceStartPos.x + 800, FireIceStartPos.y + 1000, 0);
            playerR.transform.position = new Vector3(DesertCaveStartPos.x + 1600, DesertCaveStartPos.y + 1000, 0);

            //Add player controller
            PlayerController playerController = playerParent.AddComponent<PlayerController>();
            playerController.movementSpeed = 50;
            playerController.moveTime = 0.5f;

            //Add player start positions
            GameObject playerStartLeft = new GameObject();
            playerStartLeft.name = "Player Start Left";
            playerStartLeft.transform.position = new Vector3(FireIceStartPos.x + 800, FireIceStartPos.y + 1000, 0);
            playerStartLeft.transform.parent = packagedLevelParent.transform;

            GameObject playerStartRight = new GameObject();
            playerStartRight.name = "Player Start Right";
            playerStartRight.transform.position = new Vector3(DesertCaveStartPos.x + 1600, DesertCaveStartPos.y + 1000);
            playerStartRight.transform.parent = packagedLevelParent.transform;

            //Add exits
            GameObject exitLeft = (GameObject)Instantiate(exit);
            exitLeft.name = "Exit Left";
            exitLeft.transform.position = new Vector3(FireIceExitPos.x + 800, FireIceExitPos.y + 1000, 0);
            exitLeft.transform.parent = packagedLevelParent.transform;

            GameObject exitRight = (GameObject)Instantiate(exit);
            exitRight.name = "Exit Right";
            exitRight.transform.position = new Vector3(DesertCaveExitPos.x + 1600, DesertCaveExitPos.y + 1000);
            exitRight.transform.parent = packagedLevelParent.transform;

        }       
    }

    public void BackToEditor() {
        foreach (GameObject obj in testObjects) {
            print(obj.name);
            Destroy(obj);
        }
        testObjects = new List<GameObject>();
        Main.ExitCount = 0;
        completedPrompt.SetActive(false);
        mainCam.SetActive(true);
        GameObject.Find("Main").GetComponent<RotateHandler>().ForceEditor();
        try {
            GameObject.Find("Name Enter").SetActive(false);
        }
        catch { }
        InEditor = true;
    }

    public void SaveLevel() {
        Main.ExitCount = 0;
        packagedLevelParent.transform.FindChild("Desert").transform.FindChild("Colliders").gameObject.SetActive(true);
        packagedLevelParent.transform.FindChild("Fire").transform.FindChild("Colliders").gameObject.SetActive(true);
        packagedLevelParent.transform.FindChild("Ice").transform.FindChild("Colliders").gameObject.SetActive(true);
        packagedLevelParent.transform.FindChild("Cave").transform.FindChild("Colliders").gameObject.SetActive(true);
        LevelSerializer.SaveObjectTreeToFile(NameEnter.CurrentNameText + ".lev", packagedLevelParent);
        //CustomLevelCount++;
    }

    public static void ResetStatics(){
        StartFireDesert = false;
        StartIceCave = false;
        SwitchLeftSide = false;
        SwitchRightSide = false;
        IncludeFire = false;
        IncludeDesert = false;
        IncludeIce = false;
        IncludeCave = false;
    }
}

