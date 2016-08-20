using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomDesertGrid : MonoBehaviour {

    public GameObject currentDesertTile;
    public GameObject currentCaveTile;

    public static List<GameObject> GridList;
    public static Sprite[] DesertSprites;
    public static Sprite[] CaveSprites;

    public static bool init = false;
    public static int count;

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

    public bool borderSpriteDesert = false;
    public bool cornerSpriteDesert = false;


    void Awake() {

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


        if (!init) {
            GridList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Grid Right"));
            GridList = GridList.OrderBy(o => o.name).ToList();
            DesertSprites = Resources.LoadAll<Sprite>("desert");
            CaveSprites = Resources.LoadAll<Sprite>("Cave");
            init = true;
            count = 2;

            GameObject.Find("DesertBackground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DesertBackground");
            GameObject.Find("CaveBackground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CaveBackground");

        }
        else {
            count++;
        }
        if (count == 99) {
            ResolveTileImages();
            ResolveOverlayImages();
            init = false;
        }
    }

    void ResolveTileImages() {
        print(GridList.Count);
        foreach (GameObject tile in GridList) {
            int firstIndex;
            int secondIndex;
            int completeIndex;
            int.TryParse(tile.name.Substring(0, 1), out firstIndex);
            int.TryParse(tile.name.Substring(1, 1), out secondIndex);
            int.TryParse(tile.name, out completeIndex);

            CustomDesertGrid curGridItem = tile.GetComponent<CustomDesertGrid>();
            //
            curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sortingLayerName = "Level";
            curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sortingLayerName = "Level";


            string desertName = curGridItem.currentDesertTile.name;

            desertName = RemoveClone(desertName);

            // Desert Cave resolve-----------  
            switch (desertName) {
                case "Desert Wall": {
                        if (curGridItem.topLeftOut) { // Top Left Out
                            if (curGridItem.name == "02") {
                                if (RemoveClone(GridList[22 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[23 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[21 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                                }
                                else {
                                    curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[70];
                                    curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[70];
                                }
                            }
                            else if (curGridItem.name == "10") {
                                if (RemoveClone(GridList[30 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[31 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
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
                            if (RemoveClone(GridList[28 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[71];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[71];
                            }
                        }
                        else if (curGridItem.topOut) {// Top Out
                            if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[70];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[70];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[71];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[71];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
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
                            if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
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
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[87];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[87];
                            }
                            // Right Corner
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[86];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[86];
                            }
                            // Empty
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                        }

                        else if (curGridItem.leftOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
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
                            else if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                        }

                        else if (curGridItem.rightOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
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
                            else if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[17];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[17];
                            }

                        }

                        else if (IsSpecialDesertWall(firstIndex, secondIndex)) { // Perspective Wall

                            // Open
                            if (IsSpecialDesertWall(firstIndex, secondIndex + 1) && IsSpecialDesertWall(firstIndex, secondIndex - 1) && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[65];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[65];
                            }


                            // Enclosed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[67];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[67];
                            }
                            else if (!IsSpecialDesertWall(firstIndex, secondIndex + 1) && !IsSpecialDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[67];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[67];
                            }

                            // Right closed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[66];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[66];
                            }
                            else if (!IsSpecialDesertWall(firstIndex, secondIndex + 1) && IsSpecialDesertWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[66];
                                curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[66];
                            }

                            // Left closed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
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
                                RemoveSpritesDesert(curGridItem, completeIndex);
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
                            print(GridList[completeIndex - 10 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.GetComponent<SpriteRenderer>().sprite.name);
                            int spriteIndex = 0;
                            string str = GridList[completeIndex - 10 - 2].GetComponent<CustomDesertGrid>().currentDesertTile.GetComponent<SpriteRenderer>().sprite.name;
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
                        curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[69];
                        curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[69];

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

                        curGridItem.currentDesertTile.GetComponent<SpriteRenderer>().sprite = DesertSprites[68];
                        curGridItem.currentCaveTile.GetComponent<SpriteRenderer>().sprite = CaveSprites[68];

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

    void ResolveOverlayImages() {
        GameObject[] overImages = GameObject.FindGameObjectsWithTag("Overlay Sprite");

        foreach (GameObject image in overImages) {
            SpriteRenderer renderer = image.GetComponent<SpriteRenderer>();

            renderer.sortingLayerName = "Level2";

            switch (image.name) {
                case "Desert72": {
                        renderer.sprite = DesertSprites[72];
                        break;
                    }
                case "Desert73": {
                        renderer.sprite = DesertSprites[73];
                        break;
                    }
                case "Desert74": {
                        renderer.sprite = DesertSprites[74];
                        break;
                    }
                case "Desert75": {
                        renderer.sprite = DesertSprites[75];
                        break;
                    }
                case "Desert81": {
                        renderer.sprite = DesertSprites[81];
                        break;
                    }
                case "Desert82": {
                        renderer.sprite = DesertSprites[82];
                        break;
                    }
                case "Desert88": {
                        renderer.sprite = DesertSprites[88];
                        break;
                    }
                case "Desert89": {
                        renderer.sprite = DesertSprites[89];
                        break;
                    }
                case "Desert90": {
                        renderer.sprite = DesertSprites[90];
                        break;
                    }
                case "Desert95": {
                        renderer.sprite = DesertSprites[95];
                        break;
                    }
                case "Desert96": {
                        renderer.sprite = DesertSprites[96];
                        break;
                    }
                case "Desert97": {
                        renderer.sprite = DesertSprites[97];
                        break;
                    }
                case "Desert102": {
                        renderer.sprite = DesertSprites[102];
                        break;

                    }
                case "Desert103": {
                        renderer.sprite = DesertSprites[103];
                        break;
                    }
                case "Desert104": {
                        renderer.sprite = DesertSprites[104];
                        break;
                    }
                case "Desert105": {
                        renderer.sprite = DesertSprites[105];
                        break;
                    }
                case "Desert110": {
                        renderer.sprite = DesertSprites[110];
                        break;
                    }
                case "Desert111": {
                        renderer.sprite = DesertSprites[111];
                        break;
                    }
                case "Desert112": {
                        renderer.sprite = DesertSprites[112];
                        break;
                    }
                case "Desert117": {
                        renderer.sprite = DesertSprites[117];
                        break;
                    }
                case "Desert118": {
                        renderer.sprite = DesertSprites[118];
                        break;
                    }
                case "Desert119": {
                        renderer.sprite = DesertSprites[119];
                        break;
                    }
                case "Desert120": {
                        renderer.sprite = DesertSprites[120];
                        break;
                    }

                case "Cave72": {
                        renderer.sprite = CaveSprites[72];
                        break;
                    }
                case "Cave73": {
                        renderer.sprite = CaveSprites[73];
                        break;
                    }
                case "Cave74": {
                        renderer.sprite = CaveSprites[74];
                        break;
                    }
                case "Cave75": {
                        renderer.sprite = CaveSprites[75];
                        break;
                    }
                case "Cave81": {
                        renderer.sprite = CaveSprites[81];
                        break;
                    }
                case "Cave82": {
                        renderer.sprite = CaveSprites[82];
                        break;
                    }
                case "Cave88": {
                        renderer.sprite = CaveSprites[88];
                        break;
                    }
                case "Cave89": {
                        renderer.sprite = CaveSprites[89];
                        break;
                    }
                case "Cave90": {
                        renderer.sprite = CaveSprites[90];
                        break;
                    }
                case "Cave95": {
                        renderer.sprite = CaveSprites[95];
                        break;
                    }
                case "Cave96": {
                        renderer.sprite = CaveSprites[96];
                        break;
                    }
                case "Cave97": {
                        renderer.sprite = CaveSprites[97];
                        break;
                    }
                case "Cave102": {
                        renderer.sprite = CaveSprites[102];
                        break;

                    }
                case "Cave103": {
                        renderer.sprite = CaveSprites[103];
                        break;
                    }
                case "Cave104": {
                        renderer.sprite = CaveSprites[104];
                        break;
                    }
                case "Cave105": {
                        renderer.sprite = CaveSprites[105];
                        break;
                    }
                case "Cave110": {
                        renderer.sprite = CaveSprites[110];
                        break;
                    }
                case "Cave111": {
                        renderer.sprite = CaveSprites[111];
                        break;
                    }
                case "Cave112": {
                        renderer.sprite = CaveSprites[112];
                        break;
                    }
                case "Cave117": {
                        renderer.sprite = CaveSprites[117];
                        break;
                    }
                case "Cave118": {
                        renderer.sprite = CaveSprites[118];
                        break;
                    }
                case "Cave119": {
                        renderer.sprite = CaveSprites[119];
                        break;
                    }
                case "Cave120": {
                        renderer.sprite = CaveSprites[120];
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

    private bool IsSpecialDesertWall(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
            if (indexOne == 2 && indexTwo == 2) {
                print("here");
            }
            return false;
        }
        try {
            if (RemoveClone(GridList[((indexOne - 1) * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall" && RemoveClone(GridList[((indexOne + 1) * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) != "Desert Wall") {
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

    private bool IsNormalDesertWall(int indexOne, int indexTwo) {
        try {
            if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Wall") {
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
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Floor") {
            return true;
        }
        else
            return false;
    }

    private bool IsDesertDeactivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Deactivated") {
            return true;
        }
        else
            return false;
    }

    private bool IsDesertActivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Activated") {
            return true;
        }
        else
            return false;
    }

    private bool IsDesertFloorOrDeactivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Floor" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Deactivated" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Exit" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Entrance") {
            return true;
        }
        else
            return false;
    }

    private bool IsDesertPit(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomDesertGrid>().currentDesertTile.name) == "Desert Pit") {
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

    private void AddSpritesDesert(CustomDesertGrid gridItem, int spriteIndex) {
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
    }

    private void AddSpritesDesert(CustomDesertGrid gridItem, int spriteIndex, int spriteIndex2) {
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

    private void RemoveSpritesDesert(CustomDesertGrid gridItem, int tracker = -1) {
        if (tracker == 12) {
            print("here");
        }
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
}
