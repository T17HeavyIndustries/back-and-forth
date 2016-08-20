using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomFireGrid : MonoBehaviour {

    public GameObject currentFireTile;
    public GameObject currentIceTile;

    public static List<GameObject> GridList;
    public static Sprite[] FireSprites;
    public static Sprite[] IceSprites;

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

    public bool borderSpriteFire = false;
    public bool cornerSpriteFire = false;


    void Awake() {

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


        if (!init) {
            GridList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Grid Left"));
            GridList = GridList.OrderBy(o => o.name).ToList();
            FireSprites = Resources.LoadAll<Sprite>("fire");
            IceSprites = Resources.LoadAll<Sprite>("Ice");
            init = true;
            count = 2;

            GameObject.Find("FireBackground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("FireBackground");
            GameObject.Find("IceBackground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("IceBackground");
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

            CustomFireGrid curGridItem = tile.GetComponent<CustomFireGrid>();
            //
            curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sortingLayerName = "Level";
            curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sortingLayerName = "Level";
            

            string fireName = curGridItem.currentFireTile.name;

            fireName = RemoveClone(fireName);

            // Fire Ice resolve-----------  
            switch (fireName) {
                case "Fire Wall": {
                        if (curGridItem.topLeftOut) { // Top Left Out
                            if (curGridItem.name == "02") {
                                if (RemoveClone(GridList[22 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[23 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[21 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                                }
                                else {
                                    curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[70];
                                    curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[70];
                                }
                            }
                            else if (curGridItem.name == "10") {
                                if (RemoveClone(GridList[30 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[31 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
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
                            if (RemoveClone(GridList[28 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[71];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[71];
                            }
                        }
                        else if (curGridItem.topOut) {// Top Out
                            if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[70];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[70];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[71];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[71];
                            }
                            else if (RemoveClone(GridList[completeIndex + 19 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 20 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 21 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[85];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[85];
                            }
                            else {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[33];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[33];
                            }
                        }                      

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
                            if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
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
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[87];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[87];
                            }
                            // Right Corner
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[86];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[86];
                            }
                            // Empty
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex - 10 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                        }

                        else if (curGridItem.leftOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
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
                            else if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex + 1 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex - 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                        }

                        else if (curGridItem.rightOut) {

                            // Flat
                            if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
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
                            else if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }
                            else if (RemoveClone(GridList[completeIndex - 1 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex - 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[17];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[17];
                            }

                        }

                        else if (IsSpecialFireWall(firstIndex, secondIndex)) { // Perspective Wall

                            // Open
                            if (IsSpecialFireWall(firstIndex, secondIndex + 1) && IsSpecialFireWall(firstIndex, secondIndex - 1) && RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[65];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[65];
                            }


                            // Enclosed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[67];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[67];
                            }
                            else if (!IsSpecialFireWall(firstIndex, secondIndex + 1) && !IsSpecialFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[67];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[67];
                            }

                            // Right closed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[66];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[66];
                            }
                            else if (!IsSpecialFireWall(firstIndex, secondIndex + 1) && IsSpecialFireWall(firstIndex, secondIndex - 1)) {
                                curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[66];
                                curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[66];
                            }

                            // Left closed
                            else if (RemoveClone(GridList[completeIndex + 11 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall" && RemoveClone(GridList[completeIndex + 9 - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
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
                            print(GridList[completeIndex - 10 - 2].GetComponent<CustomFireGrid>().currentFireTile.GetComponent<SpriteRenderer>().sprite.name);
                            int spriteIndex = 0;
                            string str = GridList[completeIndex - 10 - 2].GetComponent<CustomFireGrid>().currentFireTile.GetComponent<SpriteRenderer>().sprite.name;
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
                        curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[69];
                        curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[69];

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

                        curGridItem.currentFireTile.GetComponent<SpriteRenderer>().sprite = FireSprites[68];
                        curGridItem.currentIceTile.GetComponent<SpriteRenderer>().sprite = IceSprites[68];

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
            }
        }

    void ResolveOverlayImages() {
        GameObject[] overImages = GameObject.FindGameObjectsWithTag("Overlay Sprite");

        foreach (GameObject image in overImages) {
            SpriteRenderer renderer = image.GetComponent<SpriteRenderer>();

            renderer.sortingLayerName = "Level2";

            switch (image.name){
                case "Fire72":{
                    renderer.sprite = FireSprites[72];
                    break;
                }
                case "Fire73": {
                        renderer.sprite = FireSprites[73];
                        break;
                    }
                case "Fire74": {
                        renderer.sprite = FireSprites[74];
                        break;
                    }
                case "Fire75": {
                        renderer.sprite = FireSprites[75];
                        break;
                    }
                case "Fire81": {
                        renderer.sprite = FireSprites[81];
                        break;
                    }
                case "Fire82": {
                        renderer.sprite = FireSprites[82];
                        break;
                    }
                case "Fire88": {
                        renderer.sprite = FireSprites[88];
                        break;
                    }
                case "Fire89": {
                        renderer.sprite = FireSprites[89];
                        break;
                    }
                case "Fire90": {
                        renderer.sprite = FireSprites[90];
                        break;
                    }
                case "Fire95": {
                        renderer.sprite = FireSprites[95];
                        break;
                    }
                case "Fire96": {
                        renderer.sprite = FireSprites[96];
                        break;
                    }
                case "Fire97": {
                        renderer.sprite = FireSprites[97];
                        break;
                    }
                case "Fire102": {
                        renderer.sprite = FireSprites[102];
                        break;

                    }
                case "Fire103": {
                        renderer.sprite = FireSprites[103];
                        break;
                    }
                case "Fire104": {
                        renderer.sprite = FireSprites[104];
                        break;
                    }
                case "Fire105": {
                        renderer.sprite = FireSprites[105];
                        break;
                    }
                case "Fire110": {
                        renderer.sprite = FireSprites[110];
                        break;
                    }
                case "Fire111": {
                        renderer.sprite = FireSprites[111];
                        break;
                    }
                case "Fire112": {
                        renderer.sprite = FireSprites[112];
                        break;
                    }
                case "Fire117": {
                        renderer.sprite = FireSprites[117];
                        break;
                    }
                case "Fire118": {
                        renderer.sprite = FireSprites[118];
                        break;
                    }
                case "Fire119": {
                        renderer.sprite = FireSprites[119];
                        break;
                    }
                case "Fire120": {
                        renderer.sprite = FireSprites[120];
                        break;
                    }

                case "Ice72": {
                        renderer.sprite = IceSprites[72];
                        break;
                    }
                case "Ice73": {
                        renderer.sprite = IceSprites[73];
                        break;
                    }
                case "Ice74": {
                        renderer.sprite = IceSprites[74];
                        break;
                    }
                case "Ice75": {
                        renderer.sprite = IceSprites[75];
                        break;
                    }
                case "Ice81": {
                        renderer.sprite = IceSprites[81];
                        break;
                    }
                case "Ice82": {
                        renderer.sprite = IceSprites[82];
                        break;
                    }
                case "Ice88": {
                        renderer.sprite = IceSprites[88];
                        break;
                    }
                case "Ice89": {
                        renderer.sprite = IceSprites[89];
                        break;
                    }
                case "Ice90": {
                        renderer.sprite = IceSprites[90];
                        break;
                    }
                case "Ice95": {
                        renderer.sprite = IceSprites[95];
                        break;
                    }
                case "Ice96": {
                        renderer.sprite = IceSprites[96];
                        break;
                    }
                case "Ice97": {
                        renderer.sprite = IceSprites[97];
                        break;
                    }
                case "Ice102": {
                        renderer.sprite = IceSprites[102];
                        break;

                    }
                case "Ice103": {
                        renderer.sprite = IceSprites[103];
                        break;
                    }
                case "Ice104": {
                        renderer.sprite = IceSprites[104];
                        break;
                    }
                case "Ice105": {
                        renderer.sprite = IceSprites[105];
                        break;
                    }
                case "Ice110": {
                        renderer.sprite = IceSprites[110];
                        break;
                    }
                case "Ice111": {
                        renderer.sprite = IceSprites[111];
                        break;
                    }
                case "Ice112": {
                        renderer.sprite = IceSprites[112];
                        break;
                    }
                case "Ice117": {
                        renderer.sprite = IceSprites[117];
                        break;
                    }
                case "Ice118": {
                        renderer.sprite = IceSprites[118];
                        break;
                    }
                case "Ice119": {
                        renderer.sprite = IceSprites[119];
                        break;
                    }
                case "Ice120": {
                        renderer.sprite = IceSprites[120];
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
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
            if (indexOne == 2 && indexTwo == 2) {
                print("here");
            }
            return false;
        }
        try {
            if (RemoveClone(GridList[((indexOne - 1) * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall" && RemoveClone(GridList[((indexOne + 1) * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) != "Fire Wall") {
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
            if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Wall") {
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
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Floor") {
            return true;
        }
        else
            return false;
    }

    private bool IsFireDeactivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Deactivated") {
            return true;
        }
        else
            return false;
    }

    private bool IsFireActivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Activated") {
            return true;
        }
        else
            return false;
    }

    private bool IsFireFloorOrDeactivated(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Floor" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Deactivated" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Exit" || RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Entrance") {
            return true;
        }
        else
            return false;
    }

    private bool IsFirePit(int indexOne, int indexTwo) {
        if (RemoveClone(GridList[(indexOne * 10) + indexTwo - 2].GetComponent<CustomFireGrid>().currentFireTile.name) == "Fire Pit") {
            return true;
        }
        else
            return false;
    }

    private int FirePitType(int indexOne, int indexTwo) {
        int leftColumnEnd = -1;
        int rightColumnEnd = -1;
        int thisColumnEnd = -1;

        for (int i = indexOne; i > 1; i--) {
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
        else if (rightWall)
            return 3;
        else {
            print(".peoeee");
            return -1;
        }

    }

    private void AddSpritesFire(CustomFireGrid gridItem, int spriteIndex) {
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
    }

    private void AddSpritesFire(CustomFireGrid gridItem, int spriteIndex, int spriteIndex2) {
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

    private void RemoveSpritesFire(CustomFireGrid gridItem, int tracker = -1) {
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
}
