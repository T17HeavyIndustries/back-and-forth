using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridItem : MonoBehaviour {

    private EditorMain editorMain;
    private EditorPallette editorPallette;
    public GameObject currentFireTile;
    public GameObject currentIceTile;
    public GameObject currentDesertTile;
    public GameObject currentCaveTile;

    public static List<GameObject> GridList;
    public static Sprite[] FireSprites;
    public static Sprite[] IceSprites;
    public static Sprite[] DesertSprites;
    public static Sprite[] CaveSprites;
    public static bool init = false;

    private GameObject iceCanvas;
    private GameObject fireCanvas;
    private GameObject desertCanvas;
    private GameObject caveCanvas;

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    public bool topLeft = false;
    public bool topRight = false;
    public bool top = false;
    public bool bottomLeft = false;
    public bool bottomRight = false;
    public bool bottom = false;
    public bool left = false;
    public bool right = false;

    public bool topLeftOut = false;
    public bool topRightOut = false;
    public bool topOut = false;
    public bool bottomLeftOut = false;
    public bool bottomRightOut = false;
    public bool bottomOut = false;
    public bool leftOut = false;
    public bool rightOut = false;
    public bool _out = false;

    public bool borderSpriteFire = false;
    public bool cornerSpriteFire = false;
    public bool borderSpriteDesert = false;
    public bool cornerSpriteDesert = false;

    void Awake() {
        init = false;
    }

    void Start() {
        fireCanvas = GameObject.Find("Fire");
        iceCanvas = GameObject.Find("Ice");
        desertCanvas = GameObject.Find("Desert");
        caveCanvas = GameObject.Find("Cave");

        if (!init) {
            GridList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Grid"));
            GridList = GridList.OrderBy(o => o.name).ToList();
            FireSprites = Resources.LoadAll<Sprite>("fire");
            IceSprites = Resources.LoadAll<Sprite>("Ice");
            DesertSprites = Resources.LoadAll<Sprite>("Desert");
            CaveSprites = Resources.LoadAll<Sprite>("Cave");
            init = true;
        }
        

        editorMain = GameObject.Find("Editor Main").GetComponent<EditorMain>();
        editorPallette = GameObject.Find("Editor Pallette").GetComponent<EditorPallette>();

        GameObject[] fireWalls = GameObject.FindGameObjectsWithTag("Fire");
        for (int i = 0; i < fireWalls.Length; i++) {
            if (fireWalls[i].transform.position == transform.position) {
                currentFireTile = fireWalls[i];
                break;
            }
        }

        GameObject[] iceWalls = GameObject.FindGameObjectsWithTag("Ice");
        for (int i = 0; i < iceWalls.Length; i++) {
            if (iceWalls[i].transform.position == transform.position) {
                currentIceTile = iceWalls[i];
                break;
            }
        }

        GameObject[] desertWalls = GameObject.FindGameObjectsWithTag("Desert");
        for (int i = 0; i < desertWalls.Length; i++) {
            if (desertWalls[i].transform.position == transform.position) {
                currentDesertTile = desertWalls[i];
                break;
            }
        }

        GameObject[] caveWalls = GameObject.FindGameObjectsWithTag("Cave");
        for (int i = 0; i < caveWalls.Length; i++) {
            if (caveWalls[i].transform.position == transform.position) {
                currentCaveTile = caveWalls[i];
                break;
            }
        }
    }

#if UNITY_ANDROID
    void Update() {
        if (Input.touchCount == 0) {
            if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                GridPressed();
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
        GridPressed();
    }

#elif UNITY_STANDALONE
    void OnMouseDown(){
        GridPressed();
    }
#endif

    private void RemovePreviousEntrance(string side) {

        if (side == "Fire" || side == "Ice") {
            foreach (GameObject node in GridList) {
                if (node.GetComponent<GridItem>().currentFireTile.name == "Fire Entrance(Clone)") {
                    Destroy(node.GetComponent<GridItem>().currentFireTile);
                    Destroy(node.GetComponent<GridItem>().currentIceTile);

                    GameObject temp1 = (GameObject)Instantiate(editorPallette.fireFloor,node.transform.position,Quaternion.identity);
                    GameObject temp2 = (GameObject)Instantiate(editorPallette.iceFloor, node.transform.position, Quaternion.identity);

                    temp1.transform.parent = fireCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;
                    temp2.transform.parent = iceCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;

                    node.GetComponent<GridItem>().currentFireTile = temp1;
                    node.GetComponent<GridItem>().currentIceTile = temp2;
                }
            }
        }
        else {
            foreach (GameObject node in GridList) {
                if (node.GetComponent<GridItem>().currentDesertTile.name == "Desert Entrance(Clone)") {
                    Destroy(node.GetComponent<GridItem>().currentDesertTile);
                    Destroy(node.GetComponent<GridItem>().currentCaveTile);                    

                    GameObject temp1 = (GameObject)Instantiate(editorPallette.desertFloor, node.transform.position, Quaternion.identity);
                    GameObject temp2 = (GameObject)Instantiate(editorPallette.caveFloor, node.transform.position, Quaternion.identity);

                    temp1.transform.parent = desertCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;
                    temp2.transform.parent = caveCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;

                    node.GetComponent<GridItem>().currentDesertTile = temp1;
                    node.GetComponent<GridItem>().currentCaveTile = temp2;
                }
            }
        }
    }

    private void RemovePreviousExit(string side) {
        if (side == "Fire" || side == "Ice") {
            foreach (GameObject node in GridList) {
                if (node.GetComponent<GridItem>().currentFireTile.name == "Fire Exit(Clone)") {
                    Destroy(node.GetComponent<GridItem>().currentFireTile);
                    Destroy(node.GetComponent<GridItem>().currentIceTile);
                    

                    GameObject temp1 = (GameObject)Instantiate(editorPallette.fireFloor, node.transform.position, Quaternion.identity);
                    GameObject temp2 = (GameObject)Instantiate(editorPallette.iceFloor, node.transform.position, Quaternion.identity);

                    temp1.transform.parent = fireCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;
                    temp2.transform.parent = iceCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;

                    node.GetComponent<GridItem>().currentFireTile = temp1;
                    node.GetComponent<GridItem>().currentIceTile =temp2;
                }
            }
        }
        else {
            foreach (GameObject node in GridList) {
                if (node.GetComponent<GridItem>().currentDesertTile.name == "Desert Exit(Clone)") {
                    Destroy(node.GetComponent<GridItem>().currentDesertTile);
                    Destroy(node.GetComponent<GridItem>().currentCaveTile);                    

                    GameObject temp1 = (GameObject)Instantiate(editorPallette.desertFloor, node.transform.position, Quaternion.identity);
                    GameObject temp2 = (GameObject)Instantiate(editorPallette.caveFloor, node.transform.position, Quaternion.identity);

                    temp1.transform.parent = desertCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;
                    temp2.transform.parent = caveCanvas.transform.FindChildIncludingDeactivated("Colliders").transform;

                    node.GetComponent<GridItem>().currentDesertTile = temp1;
                    node.GetComponent<GridItem>().currentCaveTile = temp2;
                }
            }
        }
    }

    private void CheckIfCurrentSpecial(string side) {
        if (side == "Fire" || side == "Ice") {
            if (currentFireTile.name == "Fire Entrance(Clone)") {
                EditorMain.FireIceEntrancePlaced = false;
            }
            else if (currentFireTile.name == "Fire Exit(Clone)") {
                EditorMain.FireIceExitPlaced = false;
            }
        }
        else {
            if (currentDesertTile.name == "Desert Entrance(Clone)") {
                EditorMain.DesertCaveEntrancePlaced= false;
            }
            else if (currentDesertTile.name == "Desert Exit(Clone)") {
                EditorMain.DesertCaveExitPlaced = false;
            }
        }
    }
    

    void ResolveTileImages() {

        foreach (GameObject tile in GridList) {
            int firstIndex;
            int secondIndex;
            int completeIndex;
            int.TryParse(tile.name.Substring(0, 1), out firstIndex);
            int.TryParse(tile.name.Substring(1, 1), out secondIndex);
            int.TryParse(tile.name, out completeIndex);

            GridItem curGridItem = tile.GetComponent<GridItem>();

            string fireName = curGridItem.currentFireTile.name;
            string desertName = curGridItem.currentDesertTile.name;

            fireName = RemoveClone(fireName);
            desertName = RemoveClone(desertName);

            // Fire Ice resolve-----------  
            switch (fireName) {
                case "Fire Wall": {
                        if (curGridItem.topLeftOut) { // Top Left Out
                            if (curGridItem.name == "02") {
                                if (RemoveClone(GridList[22 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[23 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[21 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                                }
                                else {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[70];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[70];
                                }
                            }
                            else if (curGridItem.name == "10") {
                                if (RemoveClone(GridList[30 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[31 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                                }
                                else {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[70];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[70];
                                }
                            }
                        }
                        else if (curGridItem.topRightOut) { // Top Right Out
                            if (RemoveClone(GridList[28 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[71];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[71];
                            }
                        }
                        else if (curGridItem.topOut) {// Top Out
                            if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[70];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[70];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[71];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[71];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[85];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[85];
                            }
                            else {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[33];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[33];
                            }
                        }

                        /*else if (curGridItem._out) { // _OUT
                            if (!IsSpecialFireWall(firstIndex,secondIndex)) {  //not a perspective wall                

                                // No edge
                                if (RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                                }

                                // Vertical Right
                                else if (RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[18];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[18];
                                }

                                // Vertical Left
                                else if (RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[16];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[16];
                                }

                                // Right L
                                else if (RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[34];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[34];
                                }

                                // Left L
                                else if (RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[32];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[32];
                                }

                                 //Flat
                                else if (RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[33];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[33];
                                }
                                // Left and Right only
                                else if (IsSpecialFireWall(firstIndex,secondIndex -1) && IsSpecialFireWall(firstIndex,secondIndex +1) && IsNormalFireWall(firstIndex + 1,secondIndex)) {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[19];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[19];
                                }
                                // U Shape
                                else if ((RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" || RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") || RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[35];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[35];
                                }
                            
                                // Add corners
                                if (curGridItem.cornerSpriteFire) {
                                    RemoveSpritesFire(curGridItem);
                                    curGridItem.cornerSpriteFire = false;
                                }
                                if (IsNormalFireWall(firstIndex + 1, secondIndex) && !IsNormalFireWall(firstIndex + 1, secondIndex - 1) && IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                    AddSpritesFire(curGridItem, 82);
                                    curGridItem.cornerSpriteFire = true;
                                }
                                if (IsNormalFireWall(firstIndex + 1, secondIndex) && !IsNormalFireWall(firstIndex + 1, secondIndex + 1) && IsNormalFireWall(firstIndex, secondIndex + 1)) {
                                    AddSpritesFire(curGridItem, 81);
                                    curGridItem.cornerSpriteFire = true;
                                }
                                if (IsNormalFireWall(firstIndex, secondIndex + 1) && IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex - 1, secondIndex + 1)) {
                                    AddSpritesFire(curGridItem, 96);
                                    curGridItem.cornerSpriteFire = true;
                                }
                                if (completeIndex != 12 && IsNormalFireWall(firstIndex, secondIndex - 1) && IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex - 1, secondIndex - 1)) {
                                    AddSpritesFire(curGridItem, 97);
                                    curGridItem.cornerSpriteFire = true;
                                }         


                            }
                            else { //perspective block
                            
                                // Open
                                if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") { 
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[65];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[65];
                                }


                                // Enclosed
                                else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") { 
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[67];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[67];
                                }

                                // Right closed
                                else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[66];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[66];
                                }
                                
                                // Left closed
                                else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[64];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[64];
                                }
                            }
                        }*/

                        else if (curGridItem.bottomLeftOut) {
                            //corner
                            if (!IsNormalFireWall(8, 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[86];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[86];
                            }
                            //open
                            else {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                        }

                        else if (curGridItem.bottomRightOut) {
                            //corner
                            if (!IsNormalFireWall(8, 8)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[87];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[87];
                            }
                            //open
                            else {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                        }

                        else if (curGridItem.bottomOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[1];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[1];
                            }
                            else if (IsSpecialFireWall(firstIndex - 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[1];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[1];
                            }

                            //Double corner
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex - 1) && !IsNormalFireWall(firstIndex - 1, secondIndex + 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[99];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[99];
                            }

                            // Left Corner
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[87];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[87];
                            }
                            // Right Corner
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[86];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[86];
                            }
                            // Empty
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                        }

                        else if (curGridItem.leftOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[18];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[18];
                            }
                            else if (IsSpecialFireWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[18];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[18];
                            }
                            //Double corner
                            else if (!IsNormalFireWall(firstIndex + 1, secondIndex + 1) && !IsNormalFireWall(firstIndex - 1, secondIndex + 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[100];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[100];
                            }
                            // Bottom Corner
                            else if (!IsNormalFireWall(firstIndex + 1, secondIndex + 1) && IsNormalFireWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[70];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[70];
                            }
                            // Top Corner
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex + 1) && IsNormalFireWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[86];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[86];
                            }
                            // Empty
                            else if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                        }

                        else if (curGridItem.rightOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[16];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[16];
                            }
                            else if (IsSpecialFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[16];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[16];
                            }

                            //Double corner
                            else if (!IsNormalFireWall(firstIndex + 1, secondIndex - 1) && !IsNormalFireWall(firstIndex - 1, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[101];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[101];
                            }
                            // Bottom Corner
                            else if (!IsNormalFireWall(firstIndex + 1, secondIndex - 1) && IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[71];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[71];
                            }
                            // Top Corner
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex - 1) && IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[87];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[87];
                            }
                            // Empty
                            else if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }

                        }

                        else if (IsSpecialFireWall(firstIndex, secondIndex)) { // Perspective Wall

                            // Open
                            if (IsSpecialFireWall(firstIndex, secondIndex + 1) && IsSpecialFireWall(firstIndex, secondIndex - 1) && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[65];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[65];
                            }


                            // Enclosed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[67];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[67];
                            }
                            else if (!IsSpecialFireWall(firstIndex, secondIndex + 1) && !IsSpecialFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[67];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[67];
                            }

                            // Right closed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[66];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[66];
                            }
                            else if (!IsSpecialFireWall(firstIndex, secondIndex + 1) && IsSpecialFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[66];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[66];
                            }

                            // Left closed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[64];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[64];
                            }
                            else if (IsSpecialFireWall(firstIndex, secondIndex + 1) && !IsSpecialFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[64];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[64];
                            }
                        }
                        else { // Non Perspective wall

                            //Enclosed
                            if (!IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex - 1) && !IsNormalFireWall(firstIndex, secondIndex + 1) && !IsNormalFireWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[51];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[51];
                            }

                            // Open
                            else if (IsNormalFireWall(firstIndex - 1, secondIndex) && IsNormalFireWall(firstIndex, secondIndex - 1) && IsNormalFireWall(firstIndex, secondIndex + 1) && IsNormalFireWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }

                            // Bottom U
                            else if (IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex - 1) && !IsNormalFireWall(firstIndex, secondIndex + 1) && !IsNormalFireWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[35];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[35];
                            }

                            // Top U
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex - 1) && !IsNormalFireWall(firstIndex, secondIndex + 1) && IsNormalFireWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[3];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[3];
                            }

                            // Sides only
                            else if (IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex - 1) && !IsNormalFireWall(firstIndex, secondIndex + 1) && IsNormalFireWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[19];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[19];
                            }
                            // Right closed
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex) && IsNormalFireWall(firstIndex, secondIndex - 1) && !IsNormalFireWall(firstIndex, secondIndex + 1) && !IsNormalFireWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[50];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[50];
                            }
                            // Left closed
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex - 1) && IsNormalFireWall(firstIndex, secondIndex + 1) && !IsNormalFireWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[48];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[48];
                            }
                            // Top and Bottom only
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex) && IsNormalFireWall(firstIndex, secondIndex - 1) && IsNormalFireWall(firstIndex, secondIndex + 1) && !IsNormalFireWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[49];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[49];
                            }
                            // Bottom Only
                            else if (!IsNormalFireWall(firstIndex + 1, secondIndex) && IsNormalFireWall(firstIndex - 1, secondIndex) && IsNormalFireWall(firstIndex, secondIndex + 1) && IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[33];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[33];
                            }
                            // Top Only
                            else if (IsNormalFireWall(firstIndex + 1, secondIndex) && !IsNormalFireWall(firstIndex - 1, secondIndex) && IsNormalFireWall(firstIndex, secondIndex + 1) && IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[1];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[1];
                            }
                            // Left Only
                            else if (IsNormalFireWall(firstIndex + 1, secondIndex) && IsNormalFireWall(firstIndex - 1, secondIndex) && IsNormalFireWall(firstIndex, secondIndex + 1) && !IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[16];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[16];
                            }
                            // Right Only
                            else if (IsNormalFireWall(firstIndex + 1, secondIndex) && IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex + 1) && IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[18];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[18];
                            }
                            // Top Left L
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[0];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[0];
                            }
                            // Top Right L
                            else if (!IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[2];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[2];
                            }
                            // Bottom Left L
                            else if (!IsNormalFireWall(firstIndex + 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[32];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[32];
                            }
                            // Bottom Right L
                            else if (!IsNormalFireWall(firstIndex + 1, secondIndex) && !IsNormalFireWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[34];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[34];
                            }

                            //---------- Add corners
                            //print(completeIndex);
                            if (curGridItem.cornerSpriteFire) {
                                print(completeIndex);
                                RemoveSpritesFire(curGridItem, completeIndex);
                                curGridItem.cornerSpriteFire = false;
                            }

                            if (IsNormalFireWall(firstIndex + 1, secondIndex) && !IsNormalFireWall(firstIndex + 1, secondIndex - 1) && IsNormalFireWall(firstIndex, secondIndex - 1)) {
                                AddSpritesFire(curGridItem, 82);
                                curGridItem.cornerSpriteFire = true;
                            }
                            if (IsNormalFireWall(firstIndex + 1, secondIndex) && !IsNormalFireWall(firstIndex + 1, secondIndex + 1) && IsNormalFireWall(firstIndex, secondIndex + 1)) {
                                AddSpritesFire(curGridItem, 81);
                                curGridItem.cornerSpriteFire = true;
                            }
                            if (IsNormalFireWall(firstIndex, secondIndex + 1) && IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex - 1, secondIndex + 1)) {
                                AddSpritesFire(curGridItem, 96);
                                curGridItem.cornerSpriteFire = true;
                            }
                            if (completeIndex != 12 && IsNormalFireWall(firstIndex, secondIndex - 1) && IsNormalFireWall(firstIndex - 1, secondIndex) && !IsNormalFireWall(firstIndex - 1, secondIndex - 1)) {
                                AddSpritesFire(curGridItem, 97);
                                curGridItem.cornerSpriteFire = true;
                            }
                        }

                        break;
                    }
                case "Fire Floor": { // ------------------------------------------------------------------

                        // Open
                        if (IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[21];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[21];
                        }

                        // Enclosed
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[55];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[55];
                        }

                         // Bottom U
                        else if (IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[39];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[39];
                        }

                        // Top U
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[7];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[7];
                        }

                        // Sides only
                        else if (IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[23];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[23];
                        }
                        // Right closed
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[54];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[54];
                        }
                        // Left closed
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[52];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[52];
                        }
                        // Top and Bottom only
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[53];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[53];
                        }
                        // Bottom Only
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[37];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[37];
                        }
                        // Top Only
                        else if (IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[5];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[5];
                        }
                        // Left Only
                        else if (IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[20];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[20];
                        }
                        // Right Only
                        else if (IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[22];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[22];
                        }
                        // Top Left L
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[4];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[4];
                        }
                        // Top Right L
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[6];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[6];
                        }
                        // Bottom Left L
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[36];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[36];
                        }
                        // Bottom Right L
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[38];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[38];
                        }

                        break;
                    }
                case "Fire Activated": {

                        // Open
                        if (IsFireActivated(firstIndex, secondIndex - 1) && IsFireActivated(firstIndex, secondIndex + 1) && IsFireActivated(firstIndex - 1, secondIndex) && IsFireActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[29];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[25];
                        }

                        // Enclosed
                        else if (!IsFireActivated(firstIndex, secondIndex - 1) && !IsFireActivated(firstIndex, secondIndex + 1) && !IsFireActivated(firstIndex - 1, secondIndex) && !IsFireActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[63];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[59];
                        }

                         // Bottom U
                        else if (IsFireActivated(firstIndex - 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex - 1) && !IsFireActivated(firstIndex, secondIndex + 1) && !IsFireActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[47];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[43];
                        }

                        // Top U
                        else if (!IsFireActivated(firstIndex - 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex - 1) && !IsFireActivated(firstIndex, secondIndex + 1) && IsFireActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[15];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[11];
                        }

                        // Sides only
                        else if (IsFireActivated(firstIndex - 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex - 1) && !IsFireActivated(firstIndex, secondIndex + 1) && IsFireActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[31];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[27];
                        }
                        // Right closed
                        else if (!IsFireActivated(firstIndex - 1, secondIndex) && IsFireActivated(firstIndex, secondIndex - 1) && !IsFireActivated(firstIndex, secondIndex + 1) && !IsFireActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[62];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[58];
                        }
                        // Left closed
                        else if (!IsFireActivated(firstIndex - 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex - 1) && IsFireActivated(firstIndex, secondIndex + 1) && !IsFireActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[60];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[56];
                        }
                        // Top and Bottom only
                        else if (!IsFireActivated(firstIndex - 1, secondIndex) && IsFireActivated(firstIndex, secondIndex - 1) && IsFireActivated(firstIndex, secondIndex + 1) && !IsFireActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[61];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[57];
                        }
                        // Bottom Only
                        else if (!IsFireActivated(firstIndex + 1, secondIndex) && IsFireActivated(firstIndex - 1, secondIndex) && IsFireActivated(firstIndex, secondIndex + 1) && IsFireActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[45];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[41];
                        }
                        // Top Only
                        else if (IsFireActivated(firstIndex + 1, secondIndex) && !IsFireActivated(firstIndex - 1, secondIndex) && IsFireActivated(firstIndex, secondIndex + 1) && IsFireActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[13];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[9];
                        }
                        // Left Only
                        else if (IsFireActivated(firstIndex + 1, secondIndex) && IsFireActivated(firstIndex - 1, secondIndex) && IsFireActivated(firstIndex, secondIndex + 1) && !IsFireActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[28];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[24];
                        }
                        // Right Only
                        else if (IsFireActivated(firstIndex + 1, secondIndex) && IsFireActivated(firstIndex - 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex + 1) && IsFireActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[30];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[26];
                        }
                        // Top Left L
                        else if (!IsFireActivated(firstIndex - 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[12];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[8];
                        }
                        // Top Right L
                        else if (!IsFireActivated(firstIndex - 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[14];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[10];
                        }
                        // Bottom Left L
                        else if (!IsFireActivated(firstIndex + 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[44];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[40];
                        }
                        // Bottom Right L
                        else if (!IsFireActivated(firstIndex + 1, secondIndex) && !IsFireActivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[46];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[42];
                        }

                        break;
                    }
                case "Fire Deactivated": {

                        // Open
                        if (IsFireDeactivated(firstIndex, secondIndex - 1) && IsFireDeactivated(firstIndex, secondIndex + 1) && IsFireDeactivated(firstIndex - 1, secondIndex) && IsFireDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[25];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[29];
                        }

                        // Enclosed
                        else if (!IsFireDeactivated(firstIndex, secondIndex - 1) && !IsFireDeactivated(firstIndex, secondIndex + 1) && !IsFireDeactivated(firstIndex - 1, secondIndex) && !IsFireDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[59];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[63];
                        }

                         // Bottom U
                        else if (IsFireDeactivated(firstIndex - 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex - 1) && !IsFireDeactivated(firstIndex, secondIndex + 1) && !IsFireDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[43];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[47];
                        }

                        // Top U
                        else if (!IsFireDeactivated(firstIndex - 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex - 1) && !IsFireDeactivated(firstIndex, secondIndex + 1) && IsFireDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[11];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[15];
                        }

                        // Sides only
                        else if (IsFireDeactivated(firstIndex - 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex - 1) && !IsFireDeactivated(firstIndex, secondIndex + 1) && IsFireDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[27];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[31];
                        }
                        // Right closed
                        else if (!IsFireDeactivated(firstIndex - 1, secondIndex) && IsFireDeactivated(firstIndex, secondIndex - 1) && !IsFireDeactivated(firstIndex, secondIndex + 1) && !IsFireDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[58];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[62];
                        }
                        // Left closed
                        else if (!IsFireDeactivated(firstIndex - 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex - 1) && IsFireDeactivated(firstIndex, secondIndex + 1) && !IsFireDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[56];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[60];
                        }
                        // Top and Bottom only
                        else if (!IsFireDeactivated(firstIndex - 1, secondIndex) && IsFireDeactivated(firstIndex, secondIndex - 1) && IsFireDeactivated(firstIndex, secondIndex + 1) && !IsFireDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[57];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[61];
                        }
                        // Bottom Only
                        else if (!IsFireDeactivated(firstIndex + 1, secondIndex) && IsFireDeactivated(firstIndex - 1, secondIndex) && IsFireDeactivated(firstIndex, secondIndex + 1) && IsFireDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[41];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[45];
                        }
                        // Top Only
                        else if (IsFireDeactivated(firstIndex + 1, secondIndex) && !IsFireDeactivated(firstIndex - 1, secondIndex) && IsFireDeactivated(firstIndex, secondIndex + 1) && IsFireDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[9];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[13];
                        }
                        // Left Only
                        else if (IsFireDeactivated(firstIndex + 1, secondIndex) && IsFireDeactivated(firstIndex - 1, secondIndex) && IsFireDeactivated(firstIndex, secondIndex + 1) && !IsFireDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[24];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[28];
                        }
                        // Right Only
                        else if (IsFireDeactivated(firstIndex + 1, secondIndex) && IsFireDeactivated(firstIndex - 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex + 1) && IsFireDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[26];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[30];
                        }
                        // Top Left L
                        else if (!IsFireDeactivated(firstIndex - 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[8];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[12];
                        }
                        // Top Right L
                        else if (!IsFireDeactivated(firstIndex - 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[10];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[14];
                        }
                        // Bottom Left L
                        else if (!IsFireDeactivated(firstIndex + 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[40];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[44];
                        }
                        // Bottom Right L
                        else if (!IsFireDeactivated(firstIndex + 1, secondIndex) && !IsFireDeactivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[42];
                            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[46];
                        }

                        //---- Add neccasary border

                        if (curGridItem.borderSpriteFire) {
                            RemoveSpritesFire(curGridItem);
                            curGridItem.borderSpriteFire = false;
                        }

                        // Enclosed
                        if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 120);
                            curGridItem.borderSpriteFire = true;
                        }

                        // Top U
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 75);
                            curGridItem.borderSpriteFire = true;
                        }

                        // Bottom E
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 105);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Right closed
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 119);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Left closed
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 117);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Sides only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 90);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top and Bottom only
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 118);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Left L
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 72);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Right L
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 74);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Left L
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 102);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Right L
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 104);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Only
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 103);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Only
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 73);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Left Only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 88);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Right Only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 89);
                            curGridItem.borderSpriteFire = true;
                        }

                        break;
                    }
                case "Fire Pit": {

                        if (!IsFirePit(firstIndex - 1, secondIndex)) {
                            int state = -1;
                            state = FirePitType(firstIndex, secondIndex);
                            print(curGridItem.name + "  " + state.ToString());

                            if (state == 0) { // Left and Right lines
                                // Top close
                                if (!IsFirePit(firstIndex - 1, secondIndex)) {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[79];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[79];
                                }
                                else {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[94];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[94];
                                }
                            }

                            else if (state == 1) { // NO side lines
                                if (!IsFirePit(firstIndex - 1, secondIndex)) {// top closed
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[77];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[77];
                                }
                                else {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[92];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[92];
                                }
                            }

                            else if (state == 2) { // Left line
                                if (!IsFirePit(firstIndex - 1, secondIndex)) {// top closed
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[76];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[76];
                                }
                                else {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[91];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[91];
                                }
                            }

                            else if (state == 3) { // Right line
                                if (!IsFirePit(firstIndex - 1, secondIndex)) {// top closed
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[78];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[78];
                                }
                                else {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[93];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[93];
                                }
                            }
                        }
                        else {
                            print(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentFireTile.GetComponent<SpriteRenderer>().sprite.name);
                            int spriteIndex = 0;
                            string str = GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentFireTile.GetComponent<SpriteRenderer>().sprite.name;
                            string subString = str.Substring(str.Length - 2);
                            int.TryParse(subString, out spriteIndex);
                            if (spriteIndex < 80) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[spriteIndex + 15];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[spriteIndex + 15];
                            }
                            else {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[spriteIndex];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[spriteIndex];
                            }
                        }

                        break;
                    }

                case "Fire Entrance": {

                        if (curGridItem.borderSpriteFire) {
                            RemoveSpritesFire(curGridItem);
                            curGridItem.borderSpriteFire = false;
                        }

                        // Enclosed
                        if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 120);
                            curGridItem.borderSpriteFire = true;
                        }

                        // Top U
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 75);
                            curGridItem.borderSpriteFire = true;
                        }

                        // Bottom E
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 105);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Right closed
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 119);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Left closed
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 117);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Sides only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 90);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top and Bottom only
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 118);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Left L
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 72);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Right L
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 74);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Left L
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 102);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Right L
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 104);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Only
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 103);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Only
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 73);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Left Only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 88);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Right Only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 89);
                            curGridItem.borderSpriteFire = true;
                        }

                        break;
                    }
                case "Fire Exit": {

                        if (curGridItem.borderSpriteFire) {
                            RemoveSpritesFire(curGridItem);
                            curGridItem.borderSpriteFire = false;
                        }

                        // Enclosed
                        if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 120);
                            curGridItem.borderSpriteFire = true;
                        }

                        // Top U
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 75);
                            curGridItem.borderSpriteFire = true;
                        }

                        // Bottom E
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 105);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Right closed
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 119);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Left closed
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 117);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Sides only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 90);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top and Bottom only
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 118);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Left L
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 72);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Right L
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 74);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Left L
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 102);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Right L
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 104);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Bottom Only
                        else if (!IsFireFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 103);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Top Only
                        else if (!IsFireFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesFire(curGridItem, 73);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Left Only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesFire(curGridItem, 88);
                            curGridItem.borderSpriteFire = true;
                        }
                        // Right Only
                        else if (!IsFireFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesFire(curGridItem, 89);
                            curGridItem.borderSpriteFire = true;
                        }

                        break;
                    }

            }

            switch (desertName) {
                case "Desert Wall": {
                        if (curGridItem.topLeftOut) { // Top Left Out
                            if (curGridItem.name == "02") {
                                if (RemoveClone(GridList[22 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[23 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[21 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                                }
                                else {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[70];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[70];
                                }
                            }
                            else if (curGridItem.name == "10") {
                                if (RemoveClone(GridList[30 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[31 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                                }
                                else {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[70];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[70];
                                }
                            }
                        }
                        else if (curGridItem.topRightOut) { // Top Right Out
                            if (RemoveClone(GridList[28 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[71];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[71];
                            }
                        }
                        else if (curGridItem.topOut) {// Top Out
                            if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[70];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[70];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[71];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[71];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[85];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[85];
                            }
                            else {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[33];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[33];
                            }
                        }

                    

                        else if (curGridItem.bottomLeftOut) {
                            //corner
                            if (!IsNormalDesertWall(8, 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[86];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[86];
                            }
                            //open
                            else {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                        }

                        else if (curGridItem.bottomRightOut) {
                            //corner
                            if (!IsNormalDesertWall(8, 8)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[87];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[87];
                            }
                            //open
                            else {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                        }

                        else if (curGridItem.bottomOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[1];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[1];
                            }
                            else if (IsSpecialDesertWall(firstIndex - 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[1];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[1];
                            }

                            //Double corner
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex - 1) && !IsNormalDesertWall(firstIndex - 1, secondIndex + 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[99];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[99];
                            }

                            // Left Corner
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[87];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[87];
                            }
                            // Right Corner
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[86];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[86];
                            }
                            // Empty
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                        }

                        else if (curGridItem.leftOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[18];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[18];
                            }
                            else if (IsSpecialDesertWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[18];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[18];
                            }
                            //Double corner
                            else if (!IsNormalDesertWall(firstIndex + 1, secondIndex + 1) && !IsNormalDesertWall(firstIndex - 1, secondIndex + 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[100];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[100];
                            }
                            // Bottom Corner
                            else if (!IsNormalDesertWall(firstIndex + 1, secondIndex + 1) && IsNormalDesertWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[70];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[70];
                            }
                            // Top Corner
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex + 1) && IsNormalDesertWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[86];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[86];
                            }
                            // Empty
                            else if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                        }

                        else if (curGridItem.rightOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[16];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[16];
                            }
                            else if (IsSpecialDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[16];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[16];
                            }

                            //Double corner
                            else if (!IsNormalDesertWall(firstIndex + 1, secondIndex - 1) && !IsNormalDesertWall(firstIndex - 1, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[101];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[101];
                            }
                            // Bottom Corner
                            else if (!IsNormalDesertWall(firstIndex + 1, secondIndex - 1) && IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[71];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[71];
                            }
                            // Top Corner
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex - 1) && IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[87];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[87];
                            }
                            // Empty
                            else if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }

                        }

                        else if (IsSpecialDesertWall(firstIndex, secondIndex)) { // Perspective Wall

                            // Open
                            if (IsSpecialDesertWall(firstIndex, secondIndex + 1) && IsSpecialDesertWall(firstIndex, secondIndex - 1) && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[65];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[65];
                            }


                            // Enclosed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[67];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[67];
                            }
                            else if (!IsSpecialDesertWall(firstIndex, secondIndex + 1) && !IsSpecialDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[67];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[67];
                            }

                            // Right closed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[66];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[66];
                            }
                            else if (!IsSpecialDesertWall(firstIndex, secondIndex + 1) && IsSpecialDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[66];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[66];
                            }

                            // Left closed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[64];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[64];
                            }
                            else if (IsSpecialDesertWall(firstIndex, secondIndex + 1) && !IsSpecialDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[64];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[64];
                            }
                        }
                        else { // Non Perspective wall

                            //Enclosed
                            if (!IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex - 1) && !IsNormalDesertWall(firstIndex, secondIndex + 1) && !IsNormalDesertWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[51];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[51];
                            }

                            // Open
                            else if (IsNormalDesertWall(firstIndex - 1, secondIndex) && IsNormalDesertWall(firstIndex, secondIndex - 1) && IsNormalDesertWall(firstIndex, secondIndex + 1) && IsNormalDesertWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }

                            // Bottom U
                            else if (IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex - 1) && !IsNormalDesertWall(firstIndex, secondIndex + 1) && !IsNormalDesertWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[35];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[35];
                            }

                            // Top U
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex - 1) && !IsNormalDesertWall(firstIndex, secondIndex + 1) && IsNormalDesertWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[3];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[3];
                            }

                            // Sides only
                            else if (IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex - 1) && !IsNormalDesertWall(firstIndex, secondIndex + 1) && IsNormalDesertWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[19];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[19];
                            }
                            // Right closed
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex) && IsNormalDesertWall(firstIndex, secondIndex - 1) && !IsNormalDesertWall(firstIndex, secondIndex + 1) && !IsNormalDesertWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[50];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[50];
                            }
                            // Left closed
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex - 1) && IsNormalDesertWall(firstIndex, secondIndex + 1) && !IsNormalDesertWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[48];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[48];
                            }
                            // Top and Bottom only
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex) && IsNormalDesertWall(firstIndex, secondIndex - 1) && IsNormalDesertWall(firstIndex, secondIndex + 1) && !IsNormalDesertWall(firstIndex + 1, secondIndex)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[49];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[49];
                            }
                            // Bottom Only
                            else if (!IsNormalDesertWall(firstIndex + 1, secondIndex) && IsNormalDesertWall(firstIndex - 1, secondIndex) && IsNormalDesertWall(firstIndex, secondIndex + 1) && IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[33];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[33];
                            }
                            // Top Only
                            else if (IsNormalDesertWall(firstIndex + 1, secondIndex) && !IsNormalDesertWall(firstIndex - 1, secondIndex) && IsNormalDesertWall(firstIndex, secondIndex + 1) && IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[1];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[1];
                            }
                            // Left Only
                            else if (IsNormalDesertWall(firstIndex + 1, secondIndex) && IsNormalDesertWall(firstIndex - 1, secondIndex) && IsNormalDesertWall(firstIndex, secondIndex + 1) && !IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[16];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[16];
                            }
                            // Right Only
                            else if (IsNormalDesertWall(firstIndex + 1, secondIndex) && IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex + 1) && IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[18];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[18];
                            }
                            // Top Left L
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[0];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[0];
                            }
                            // Top Right L
                            else if (!IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[2];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[2];
                            }
                            // Bottom Left L
                            else if (!IsNormalDesertWall(firstIndex + 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[32];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[32];
                            }
                            // Bottom Right L
                            else if (!IsNormalDesertWall(firstIndex + 1, secondIndex) && !IsNormalDesertWall(firstIndex, secondIndex + 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[34];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[34];
                            }

                            //---------- Add corners
                            //print(completeIndex);
                            if (curGridItem.cornerSpriteDesert) {
                                print(completeIndex);
                                RemoveSpritesDesert(curGridItem);
                                curGridItem.cornerSpriteDesert = false;
                            }

                            if (IsNormalDesertWall(firstIndex + 1, secondIndex) && !IsNormalDesertWall(firstIndex + 1, secondIndex - 1) && IsNormalDesertWall(firstIndex, secondIndex - 1)) {
                                AddSpritesDesert(curGridItem, 82);
                                curGridItem.cornerSpriteDesert = true;
                            }
                            if (IsNormalDesertWall(firstIndex + 1, secondIndex) && !IsNormalDesertWall(firstIndex + 1, secondIndex + 1) && IsNormalDesertWall(firstIndex, secondIndex + 1)) {
                                AddSpritesDesert(curGridItem, 81);
                                curGridItem.cornerSpriteDesert = true;
                            }
                            if (IsNormalDesertWall(firstIndex, secondIndex + 1) && IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex - 1, secondIndex + 1)) {
                                AddSpritesDesert(curGridItem, 96);
                                curGridItem.cornerSpriteDesert = true;
                            }
                            if (completeIndex != 12 && IsNormalDesertWall(firstIndex, secondIndex - 1) && IsNormalDesertWall(firstIndex - 1, secondIndex) && !IsNormalDesertWall(firstIndex - 1, secondIndex - 1)) {
                                AddSpritesDesert(curGridItem, 97);
                                curGridItem.cornerSpriteDesert = true;
                            }
                        }

                        break;
                    }
                case "Desert Floor": { // ------------------------------------------------------------------

                        // Open
                        if (IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[21];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[21];
                        }

                        // Enclosed
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[55];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[55];
                        }

                         // Bottom U
                        else if (IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[39];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[39];
                        }

                        // Top U
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[7];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[7];
                        }

                        // Sides only
                        else if (IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[23];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[23];
                        }
                        // Right closed
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[54];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[54];
                        }
                        // Left closed
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[52];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[52];
                        }
                        // Top and Bottom only
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[53];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[53];
                        }
                        // Bottom Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[37];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[37];
                        }
                        // Top Only
                        else if (IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[5];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[5];
                        }
                        // Left Only
                        else if (IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[20];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[20];
                        }
                        // Right Only
                        else if (IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[22];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[22];
                        }
                        // Top Left L
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[4];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[4];
                        }
                        // Top Right L
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[6];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[6];
                        }
                        // Bottom Left L
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[36];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[36];
                        }
                        // Bottom Right L
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[38];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[38];
                        }

                        break;
                    }
                case "Desert Activated": {

                        // Open
                        if (IsDesertActivated(firstIndex, secondIndex - 1) && IsDesertActivated(firstIndex, secondIndex + 1) && IsDesertActivated(firstIndex - 1, secondIndex) && IsDesertActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[29];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[25];
                        }

                        // Enclosed
                        else if (!IsDesertActivated(firstIndex, secondIndex - 1) && !IsDesertActivated(firstIndex, secondIndex + 1) && !IsDesertActivated(firstIndex - 1, secondIndex) && !IsDesertActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[63];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[59];
                        }

                         // Bottom U
                        else if (IsDesertActivated(firstIndex - 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex - 1) && !IsDesertActivated(firstIndex, secondIndex + 1) && !IsDesertActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[47];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[43];
                        }

                        // Top U
                        else if (!IsDesertActivated(firstIndex - 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex - 1) && !IsDesertActivated(firstIndex, secondIndex + 1) && IsDesertActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[15];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[11];
                        }

                        // Sides only
                        else if (IsDesertActivated(firstIndex - 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex - 1) && !IsDesertActivated(firstIndex, secondIndex + 1) && IsDesertActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[31];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[27];
                        }
                        // Right closed
                        else if (!IsDesertActivated(firstIndex - 1, secondIndex) && IsDesertActivated(firstIndex, secondIndex - 1) && !IsDesertActivated(firstIndex, secondIndex + 1) && !IsDesertActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[62];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[58];
                        }
                        // Left closed
                        else if (!IsDesertActivated(firstIndex - 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex - 1) && IsDesertActivated(firstIndex, secondIndex + 1) && !IsDesertActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[60];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[56];
                        }
                        // Top and Bottom only
                        else if (!IsDesertActivated(firstIndex - 1, secondIndex) && IsDesertActivated(firstIndex, secondIndex - 1) && IsDesertActivated(firstIndex, secondIndex + 1) && !IsDesertActivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[61];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[57];
                        }
                        // Bottom Only
                        else if (!IsDesertActivated(firstIndex + 1, secondIndex) && IsDesertActivated(firstIndex - 1, secondIndex) && IsDesertActivated(firstIndex, secondIndex + 1) && IsDesertActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[45];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[41];
                        }
                        // Top Only
                        else if (IsDesertActivated(firstIndex + 1, secondIndex) && !IsDesertActivated(firstIndex - 1, secondIndex) && IsDesertActivated(firstIndex, secondIndex + 1) && IsDesertActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[13];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[9];
                        }
                        // Left Only
                        else if (IsDesertActivated(firstIndex + 1, secondIndex) && IsDesertActivated(firstIndex - 1, secondIndex) && IsDesertActivated(firstIndex, secondIndex + 1) && !IsDesertActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[28];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[24];
                        }
                        // Right Only
                        else if (IsDesertActivated(firstIndex + 1, secondIndex) && IsDesertActivated(firstIndex - 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex + 1) && IsDesertActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[30];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[26];
                        }
                        // Top Left L
                        else if (!IsDesertActivated(firstIndex - 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[12];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[8];
                        }
                        // Top Right L
                        else if (!IsDesertActivated(firstIndex - 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[14];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[10];
                        }
                        // Bottom Left L
                        else if (!IsDesertActivated(firstIndex + 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[44];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[40];
                        }
                        // Bottom Right L
                        else if (!IsDesertActivated(firstIndex + 1, secondIndex) && !IsDesertActivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[46];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[42];
                        }

                        break;
                    }
                case "Desert Deactivated": {

                        // Open
                        if (IsDesertDeactivated(firstIndex, secondIndex - 1) && IsDesertDeactivated(firstIndex, secondIndex + 1) && IsDesertDeactivated(firstIndex - 1, secondIndex) && IsDesertDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[25];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[29];
                        }

                        // Enclosed
                        else if (!IsDesertDeactivated(firstIndex, secondIndex - 1) && !IsDesertDeactivated(firstIndex, secondIndex + 1) && !IsDesertDeactivated(firstIndex - 1, secondIndex) && !IsDesertDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[59];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[63];
                        }

                         // Bottom U
                        else if (IsDesertDeactivated(firstIndex - 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex - 1) && !IsDesertDeactivated(firstIndex, secondIndex + 1) && !IsDesertDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[43];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[47];
                        }

                        // Top U
                        else if (!IsDesertDeactivated(firstIndex - 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex - 1) && !IsDesertDeactivated(firstIndex, secondIndex + 1) && IsDesertDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[11];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[15];
                        }

                        // Sides only
                        else if (IsDesertDeactivated(firstIndex - 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex - 1) && !IsDesertDeactivated(firstIndex, secondIndex + 1) && IsDesertDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[27];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[31];
                        }
                        // Right closed
                        else if (!IsDesertDeactivated(firstIndex - 1, secondIndex) && IsDesertDeactivated(firstIndex, secondIndex - 1) && !IsDesertDeactivated(firstIndex, secondIndex + 1) && !IsDesertDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[58];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[62];
                        }
                        // Left closed
                        else if (!IsDesertDeactivated(firstIndex - 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex - 1) && IsDesertDeactivated(firstIndex, secondIndex + 1) && !IsDesertDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[56];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[60];
                        }
                        // Top and Bottom only
                        else if (!IsDesertDeactivated(firstIndex - 1, secondIndex) && IsDesertDeactivated(firstIndex, secondIndex - 1) && IsDesertDeactivated(firstIndex, secondIndex + 1) && !IsDesertDeactivated(firstIndex + 1, secondIndex)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[57];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[61];
                        }
                        // Bottom Only
                        else if (!IsDesertDeactivated(firstIndex + 1, secondIndex) && IsDesertDeactivated(firstIndex - 1, secondIndex) && IsDesertDeactivated(firstIndex, secondIndex + 1) && IsDesertDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[41];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[45];
                        }
                        // Top Only
                        else if (IsDesertDeactivated(firstIndex + 1, secondIndex) && !IsDesertDeactivated(firstIndex - 1, secondIndex) && IsDesertDeactivated(firstIndex, secondIndex + 1) && IsDesertDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[9];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[13];
                        }
                        // Left Only
                        else if (IsDesertDeactivated(firstIndex + 1, secondIndex) && IsDesertDeactivated(firstIndex - 1, secondIndex) && IsDesertDeactivated(firstIndex, secondIndex + 1) && !IsDesertDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[24];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[28];
                        }
                        // Right Only
                        else if (IsDesertDeactivated(firstIndex + 1, secondIndex) && IsDesertDeactivated(firstIndex - 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex + 1) && IsDesertDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[26];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[30];
                        }
                        // Top Left L
                        else if (!IsDesertDeactivated(firstIndex - 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[8];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[12];
                        }
                        // Top Right L
                        else if (!IsDesertDeactivated(firstIndex - 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[10];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[14];
                        }
                        // Bottom Left L
                        else if (!IsDesertDeactivated(firstIndex + 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex - 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[40];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[44];
                        }
                        // Bottom Right L
                        else if (!IsDesertDeactivated(firstIndex + 1, secondIndex) && !IsDesertDeactivated(firstIndex, secondIndex + 1)) {
                            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[42];
                            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[46];
                        }

                        //---- Add neccasary border

                        if (curGridItem.borderSpriteDesert) {
                            RemoveSpritesDesert(curGridItem);
                            curGridItem.borderSpriteDesert = false;
                        }

                        // Enclosed
                        if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 120);
                            curGridItem.borderSpriteDesert = true;
                        }

                        // Top U
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 75);
                            curGridItem.borderSpriteDesert = true;
                        }

                        // Bottom E
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 105);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Right closed
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 119);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Left closed
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 117);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Sides only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 90);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top and Bottom only
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 118);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Left L
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 72);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Right L
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 74);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Left L
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 102);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Right L
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 104);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 103);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 73);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Left Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 88);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Right Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 89);
                            curGridItem.borderSpriteDesert = true;
                        }

                        break;
                    }
                case "Desert Pit": {

                        if (!IsDesertPit(firstIndex - 1, secondIndex)) {
                            int state = -1;
                            state = DesertPitType(firstIndex, secondIndex);
                            print(curGridItem.name + "  " + state.ToString());

                            if (state == 0) { // Left and Right lines
                                // Top close
                                if (!IsDesertPit(firstIndex - 1, secondIndex)) {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[79];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[79];
                                }
                                else {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[94];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[94];
                                }
                            }

                            else if (state == 1) { // NO side lines
                                if (!IsDesertPit(firstIndex - 1, secondIndex)) {// top closed
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[77];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[77];
                                }
                                else {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[92];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[92];
                                }
                            }

                            else if (state == 2) { // Left line
                                if (!IsDesertPit(firstIndex - 1, secondIndex)) {// top closed
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[76];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[76];
                                }
                                else {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[91];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[91];
                                }
                            }

                            else if (state == 3) { // Right line
                                if (!IsDesertPit(firstIndex - 1, secondIndex)) {// top closed
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[78];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[78];
                                }
                                else {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[93];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[93];
                                }
                            }
                        }
                        else {
                            print(GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentDesertTile.GetComponent<SpriteRenderer>().sprite.name);
                            int spriteIndex = 0;
                            string str = GridList[completeIndex - 10 - 2].GetComponent<GridItem>().currentDesertTile.GetComponent<SpriteRenderer>().sprite.name;
                            string subString = str.Substring(str.Length - 2);
                            int.TryParse(subString, out spriteIndex);
                            if (spriteIndex < 80) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[spriteIndex + 15];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[spriteIndex + 15];
                            }
                            else {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[spriteIndex];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[spriteIndex];
                            }
                        }

                        break;
                    }

                case "Desert Entrance": {

                        if (curGridItem.borderSpriteDesert) {
                            RemoveSpritesDesert(curGridItem);
                            curGridItem.borderSpriteDesert = false;
                        }

                        // Enclosed
                        if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 120);
                            curGridItem.borderSpriteDesert = true;
                        }

                        // Top U
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 75);
                            curGridItem.borderSpriteDesert = true;
                        }

                        // Bottom E
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 105);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Right closed
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 119);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Left closed
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 117);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Sides only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 90);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top and Bottom only
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 118);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Left L
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 72);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Right L
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 74);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Left L
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 102);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Right L
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 104);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 103);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 73);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Left Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 88);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Right Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 89);
                            curGridItem.borderSpriteDesert = true;
                        }

                        break;
                    }
                case "Desert Exit": {

                        if (curGridItem.borderSpriteDesert) {
                            RemoveSpritesDesert(curGridItem);
                            curGridItem.borderSpriteDesert = false;
                        }

                        // Enclosed
                        if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 120);
                            curGridItem.borderSpriteDesert = true;
                        }

                        // Top U
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 75);
                            curGridItem.borderSpriteDesert = true;
                        }

                        // Bottom E
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 105);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Right closed
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 119);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Left closed
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 117);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Sides only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 90);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top and Bottom only
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 118);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Left L
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 72);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Right L
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 74);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Left L
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 102);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Right L
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex) && !IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 104);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Bottom Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex + 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 103);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Top Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex - 1, secondIndex)) {
                            AddSpritesDesert(curGridItem, 73);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Left Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex - 1)) {
                            AddSpritesDesert(curGridItem, 88);
                            curGridItem.borderSpriteDesert = true;
                        }
                        // Right Only
                        else if (!IsDesertFloorOrDeactivated(firstIndex, secondIndex + 1)) {
                            AddSpritesDesert(curGridItem, 89);
                            curGridItem.borderSpriteDesert = true;
                        }

                        break;
                    }

            }

        }
    }

    private static string RemoveClone(string str) {
        try {
            if (str.Substring(str.Length - 7, 7) == "(Clone)") {
                return str.Substring(0, str.Length - 7);
            }
            else {
                return str;
            }
        }
        catch {
            print("String of" + str);
            return str;
        }
    }

    private bool IsSpecialFireWall(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
            if (indexOne ==2 && indexTwo == 2) {
                print("here");
            }
            return false;
        }
        try {
            if (RemoveClone(GridList[((indexOne - 1) * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[((indexOne + 1) * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) != "Fire Wall") {
                return true;
            }
            else {
                if (indexOne == 2 && indexTwo == 2) {
                    print("here1");
                }
                return false;
                
            }
        }
        catch { print("ERROR " + indexOne.ToString() + indexTwo.ToString()); return false; }

    }

    private bool IsNormalFireWall(int indexOne, int indexTwo) {
        try {
            if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Wall") {
                if (indexTwo != 0 && indexTwo != 9 && indexOne != 9 && (indexOne * 10 + indexTwo > 11)) {
                    if (!IsSpecialFireWall(indexOne, indexTwo)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else
                    return true;
            }
            else {
                return false;
            }
        }
        catch { print(indexOne.ToString() + indexTwo.ToString()); return false; }
    }


    private bool IsFireFloor(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Floor") {
            return true;
        }
        else
            return false;
    }

    private bool IsFireDeactivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Deactivated") {
            return true;
        }
        else
            return false;
    }

    private bool IsFireActivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Activated") {
            return true;
        }
        else
            return false;
    }

    private bool IsFireFloorOrDeactivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Floor" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Deactivated" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Exit" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Entrance") {
            return true;
        }
        else
            return false;
    }

    private bool IsFirePit(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentFireTile.name) == "Fire Pit") {
            return true;
        }
        else
            return false;
    }

    private int FirePitType(int indexOne, int indexTwo) {
        int leftColumnEnd = -1;
        int rightColumnEnd = -1;
        int thisColumnEnd = -1;

        for (int i = indexOne; i > 1; i--){
            if (!IsFirePit(i - 1, indexTwo)) {
                thisColumnEnd = i;
                break;
            }
        }
        
        if (IsFirePit(indexOne, indexTwo - 1)) {
            for (int i = indexOne; i > 1; i--) {
                if (!IsFirePit(i - 1, indexTwo - 1)) {
                    leftColumnEnd = i;
                    break;
                }
            }
        }

        if (IsFirePit(indexOne, indexTwo + 1)) {
            for (int i = indexOne; i > 1; i--) {
                if (!IsFirePit(i - 1, indexTwo + 1)) {
                    rightColumnEnd = i;
                    break;
                }
            }
        }

        bool leftWall = false;
        bool rightWall = false;

        if (thisColumnEnd != leftColumnEnd)
            leftWall = true;
        if (thisColumnEnd != rightColumnEnd)
            rightWall = true;

        if (leftWall && rightWall) {
            return 0;
        }
        else if (!leftWall && !rightWall) {
            return 1;
        }
        else if (leftWall) {
            return 2;
        }
        else if(rightWall)
            return 3;
        else {
            print(".peoeee");
            return -1;
        }
 
    }

    private void AddSpritesFire(GridItem gridItem, int spriteIndex) {
        GameObject spriteParent1 = new GameObject();
        SpriteRenderer rend1 = spriteParent1.AddComponent<SpriteRenderer>();
        rend1.sprite = FireSprites[spriteIndex];
        rend1.sortingLayerName = "Level";
        spriteParent1.transform.parent = gridItem.currentFireTile.transform;
        spriteParent1.transform.localPosition = Vector3.zero;
        rend1.sortingLayerName = "Level2";
        spriteParent1.tag = "Overlay Sprite";
        spriteParent1.name = "Fire" + spriteIndex.ToString();

        GameObject spriteParent2 = new GameObject();
        SpriteRenderer rend2 = spriteParent2.AddComponent<SpriteRenderer>();
        rend2.sprite = IceSprites[spriteIndex];
        rend2.sortingLayerName = "Level";
        spriteParent2.transform.parent = gridItem.currentIceTile.transform;
        spriteParent2.transform.localPosition = Vector3.zero;
        rend2.sortingLayerName = "Level2";
        spriteParent2.tag = "Overlay Sprite";
        spriteParent2.name = "Ice" + spriteIndex.ToString();
    }

    private void AddSpritesFire(GridItem gridItem, int spriteIndex, int spriteIndex2) {
        GameObject spriteParent1 = new GameObject();
        SpriteRenderer rend1 = spriteParent1.AddComponent<SpriteRenderer>();
        rend1.sprite = FireSprites[spriteIndex];
        rend1.sortingLayerName = "Level";
        spriteParent1.transform.parent = gridItem.currentFireTile.transform;
        spriteParent1.transform.localPosition = Vector3.zero;

        GameObject spriteParent2 = new GameObject();
        SpriteRenderer rend2 = spriteParent2.AddComponent<SpriteRenderer>();
        rend2.sprite = IceSprites[spriteIndex];
        rend2.sortingLayerName = "Level";
        spriteParent2.transform.parent = gridItem.currentIceTile.transform;
        spriteParent2.transform.localPosition = Vector3.zero;

        GameObject spriteParent3 = new GameObject();
        SpriteRenderer rend3 = spriteParent3.AddComponent<SpriteRenderer>();
        rend3.sprite = FireSprites[spriteIndex2];
        rend3.sortingLayerName = "Level";
        spriteParent3.transform.parent = gridItem.currentFireTile.transform;
        spriteParent3.transform.localPosition = Vector3.zero;

        GameObject spriteParent4 = new GameObject();
        SpriteRenderer rend4 = spriteParent4.AddComponent<SpriteRenderer>();
        rend4.sprite = IceSprites[spriteIndex2];
        rend4.sortingLayerName = "Level";
        spriteParent4.transform.parent = gridItem.currentIceTile.transform;
        spriteParent4.transform.localPosition = Vector3.zero;
    }

    private void RemoveSpritesFire(GridItem gridItem, int tracker = -1) {
        if (tracker == 12) {
            print("here");
        }
        try {
            for (int i = 0; i < gridItem.currentFireTile.transform.childCount; i++) {
                Destroy(gridItem.currentFireTile.transform.GetChild(i).gameObject);
                Destroy(gridItem.currentIceTile.transform.GetChild(i).gameObject);
            }
        }
        catch {
            print("ERROR: Could not destroy sprite child object from grid item " + gridItem.name);
        }
    }



    private bool IsSpecialDesertWall(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
            return false;
        }
        try {
            if (RemoveClone(GridList[((indexOne - 1) * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[((indexOne + 1) * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) != "Desert Wall") {
                return true;
            }
            else {
                return false;
            }
        }
        catch { print("ERROR " + indexOne.ToString() + indexTwo.ToString()); return false; }

    }

    private bool IsNormalDesertWall(int indexOne, int indexTwo) {
        try {
            if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Wall") {
                if (indexTwo != 0 && indexTwo != 9 && indexOne != 9 && (indexOne * 10 + indexTwo > 11)) {
                    if (!IsSpecialDesertWall(indexOne, indexTwo)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else
                    return true;
            }
            else {
                return false;
            }
        }
        catch { print(indexOne.ToString() + indexTwo.ToString()); return false; }
    }

    private bool IsDesertFloor(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Floor") {
            return true;
        }
        else
            return false;
    }

    private bool IsDesertDeactivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Deactivated") {
            return true;
        }
        else
            return false;
    }

    private bool IsDesertActivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Activated") {
            return true;
        }
        else
            return false;
    }

    private bool IsDesertFloorOrDeactivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Floor" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Deactivated" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Exit" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Entrance") {
            return true;
        }
        else
            return false;
    }

    private bool IsDesertPit(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<GridItem>().currentDesertTile.name) == "Desert Pit") {
            return true;
        }
        else
            return false;
    }

    private int DesertPitType(int indexOne, int indexTwo) {
        int leftColumnEnd = -1;
        int rightColumnEnd = -1;
        int thisColumnEnd = -1;

        for (int i = indexOne; i > 1; i--) {
            if (!IsDesertPit(i - 1, indexTwo)) {
                thisColumnEnd = i;
                break;
            }
        }

        if (IsDesertPit(indexOne, indexTwo - 1)) {
            for (int i = indexOne; i > 1; i--) {
                if (!IsDesertPit(i - 1, indexTwo - 1)) {
                    leftColumnEnd = i;
                    break;
                }
            }
        }

        if (IsDesertPit(indexOne, indexTwo + 1)) {
            for (int i = indexOne; i > 1; i--) {
                if (!IsDesertPit(i - 1, indexTwo + 1)) {
                    rightColumnEnd = i;
                    break;
                }
            }
        }

        bool leftWall = false;
        bool rightWall = false;

        if (thisColumnEnd != leftColumnEnd)
            leftWall = true;
        if (thisColumnEnd != rightColumnEnd)
            rightWall = true;

        if (leftWall && rightWall) {
            return 0;
        }
        else if (!leftWall && !rightWall) {
            return 1;
        }
        else if (leftWall) {
            return 2;
        }
        else if (rightWall)
            return 3;
        else {
            print(".peoeee");
            return -1;
        }

    }

    private void AddSpritesDesert(GridItem gridItem, int spriteIndex) {
        GameObject spriteParent1 = new GameObject();
        SpriteRenderer rend1 = spriteParent1.AddComponent<SpriteRenderer>();
        rend1.sprite = DesertSprites[spriteIndex];
        rend1.sortingLayerName = "Level";
        spriteParent1.transform.parent = gridItem.currentDesertTile.transform;
        spriteParent1.transform.localPosition = Vector3.zero;
        rend1.sortingLayerName = "Level2";
        spriteParent1.tag = "Overlay Sprite";
        spriteParent1.name = "Desert" + spriteIndex.ToString();

        GameObject spriteParent2 = new GameObject();
        SpriteRenderer rend2 = spriteParent2.AddComponent<SpriteRenderer>();
        rend2.sprite = CaveSprites[spriteIndex];
        rend2.sortingLayerName = "Level";
        spriteParent2.transform.parent = gridItem.currentCaveTile.transform;
        spriteParent2.transform.localPosition = Vector3.zero;
        rend2.sortingLayerName = "Level2";
        spriteParent2.tag = "Overlay Sprite";
        spriteParent2.name = "Cave" + spriteIndex.ToString();
    }

    private void AddSpritesDesert(GridItem gridItem, int spriteIndex, int spriteIndex2) {
        GameObject spriteParent1 = new GameObject();
        SpriteRenderer rend1 = spriteParent1.AddComponent<SpriteRenderer>();
        rend1.sprite = DesertSprites[spriteIndex];
        rend1.sortingLayerName = "Level";
        spriteParent1.transform.parent = gridItem.currentDesertTile.transform;
        spriteParent1.transform.localPosition = Vector3.zero;

        GameObject spriteParent2 = new GameObject();
        SpriteRenderer rend2 = spriteParent2.AddComponent<SpriteRenderer>();
        rend2.sprite = CaveSprites[spriteIndex];
        rend2.sortingLayerName = "Level";
        spriteParent2.transform.parent = gridItem.currentCaveTile.transform;
        spriteParent2.transform.localPosition = Vector3.zero;

        GameObject spriteParent3 = new GameObject();
        SpriteRenderer rend3 = spriteParent3.AddComponent<SpriteRenderer>();
        rend3.sprite = DesertSprites[spriteIndex2];
        rend3.sortingLayerName = "Level";
        spriteParent3.transform.parent = gridItem.currentDesertTile.transform;
        spriteParent3.transform.localPosition = Vector3.zero;

        GameObject spriteParent4 = new GameObject();
        SpriteRenderer rend4 = spriteParent4.AddComponent<SpriteRenderer>();
        rend4.sprite = CaveSprites[spriteIndex2];
        rend4.sortingLayerName = "Level";
        spriteParent4.transform.parent = gridItem.currentCaveTile.transform;
        spriteParent4.transform.localPosition = Vector3.zero;
    }

    private void RemoveSpritesDesert(GridItem gridItem) {

        try {
            for (int i = 0; i < gridItem.currentDesertTile.transform.childCount; i++) {
                Destroy(gridItem.currentDesertTile.transform.GetChild(i).gameObject);
                Destroy(gridItem.currentCaveTile.transform.GetChild(i).gameObject);
            }
        }
        catch {
            print("ERROR: Could not destroy sprite child object from grid item " + gridItem.name);
        }
    }

    

    void GridPressed() {
        if(_out || rightOut || leftOut || topOut || bottomOut){
            return;
        }
        print("grid clicked");
        if (EditorPallette.PrimaryTile == null) {
            print("tile sent was null");
            return;
        }


        GameObject[] temp = new GameObject[2];
        if (EditorMain.CurrentEditing == "Fire") {

            CheckIfCurrentSpecial("Fire");

            GameObject.Destroy(currentFireTile);
            GameObject.Destroy(currentIceTile);

            temp = editorMain.GridTouched(gameObject);

            if (temp[0].name == "Fire Entrance(Clone)") {
                if (EditorMain.FireIceEntrancePlaced)
                    RemovePreviousEntrance("Fire");

                EditorMain.FireIceEntrancePlaced = true;
                EditorMain.FireIceStartPos = temp[0].transform.localPosition;
            }
            else if (temp[0].name == "Fire Exit(Clone)") {
                if (EditorMain.FireIceExitPlaced)
                    RemovePreviousExit("Fire");

                EditorMain.FireIceExitPlaced = true;
                EditorMain.FireIceExitPos = temp[0].transform.localPosition;
            }

            currentFireTile = temp[0];
            currentIceTile = temp[1];


        }

        if (EditorMain.CurrentEditing == "Ice") {

            CheckIfCurrentSpecial("Ice");

            GameObject.Destroy(currentFireTile);
            GameObject.Destroy(currentIceTile);

            temp = editorMain.GridTouched(gameObject);

            if (temp[0].name == "Ice Entrance(Clone)") {
                if (EditorMain.FireIceEntrancePlaced)
                    RemovePreviousEntrance("Ice");

                EditorMain.FireIceEntrancePlaced = true;
                EditorMain.FireIceStartPos = temp[0].transform.localPosition;
            }
            else if (temp[0].name == "Ice Exit(Clone)") {
                if (EditorMain.FireIceExitPlaced)
                    RemovePreviousExit("Ice");

                EditorMain.FireIceExitPlaced = true;
                EditorMain.FireIceExitPos = temp[0].transform.localPosition;
            }

            currentIceTile = temp[0];
            currentFireTile = temp[1];


        }

        if (EditorMain.CurrentEditing == "Desert") {

            CheckIfCurrentSpecial("Desert");

            GameObject.Destroy(currentDesertTile);
            GameObject.Destroy(currentCaveTile);

            temp = editorMain.GridTouched(gameObject);

            if (temp[0].name == "Desert Entrance(Clone)") {
                if (EditorMain.DesertCaveEntrancePlaced)
                    RemovePreviousEntrance("Desert");

                EditorMain.DesertCaveEntrancePlaced = true;
                EditorMain.DesertCaveStartPos = temp[0].transform.localPosition;
            }
            else if (temp[0].name == "Desert Exit(Clone)") {
                if (EditorMain.DesertCaveExitPlaced)
                    RemovePreviousExit("Desert");

                EditorMain.DesertCaveExitPlaced = true;
                EditorMain.DesertCaveExitPos = temp[0].transform.localPosition;
            }

            currentDesertTile = temp[0];
            currentCaveTile = temp[1];

        }

        if (EditorMain.CurrentEditing == "Cave") {

            CheckIfCurrentSpecial("Cave");

            GameObject.Destroy(currentCaveTile);
            GameObject.Destroy(currentDesertTile);

            temp = editorMain.GridTouched(gameObject);

            if (temp[0].name == "Cave Entrance(Clone)") {
                if (EditorMain.DesertCaveEntrancePlaced)
                    RemovePreviousEntrance("Cave");

                EditorMain.DesertCaveEntrancePlaced = true;
                EditorMain.DesertCaveStartPos = temp[0].transform.localPosition;
            }
            else if (temp[0].name == "Cave Exit(Clone)") {
                if (EditorMain.DesertCaveExitPlaced)
                    RemovePreviousExit("Cave");

                EditorMain.DesertCaveExitPlaced = true;
                EditorMain.DesertCaveExitPos = temp[0].transform.localPosition;
            }

            currentCaveTile = temp[0];
            currentDesertTile = temp[1];

        }

        ResolveTileImages();
    }

}
