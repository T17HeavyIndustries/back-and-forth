using UnityEngine;
using System.Collections;

public class EditorPallette : MonoBehaviour {

    public static GameObject PrimaryTile;
    public static GameObject SecondaryTile;

    public GameObject fireFloor;
    public GameObject fireWall;
    public GameObject fireActivated;
    public GameObject fireDeactivated;
    public GameObject fireExit;
    public GameObject fireEntrance;
    public GameObject firePit;

    public GameObject iceFloor;
    public GameObject iceWall;
    public GameObject iceActivated;
    public GameObject iceDeactivated;
    public GameObject iceExit;
    public GameObject iceEntrance;
    public GameObject icePit;

    public GameObject caveFloor;
    public GameObject caveWall;
    public GameObject caveActivated;
    public GameObject caveDeactivated;
    public GameObject caveExit;
    public GameObject caveEntrance;
    public GameObject cavePit;

    public GameObject desertFloor;
    public GameObject desertWall;
    public GameObject desertActivated;
    public GameObject desertDeactivated;
    public GameObject desertExit;
    public GameObject desertEntrance;
    public GameObject desertPit;


    public void SetCurrentTile(string name) {
        print(name);
        switch(name){
            case "Fire Floor": {
                    PrimaryTile = fireFloor;
                    SecondaryTile = iceFloor;
                    break;
                }
            case "Fire Wall": {
                    PrimaryTile = fireWall;
                    SecondaryTile = iceWall;
                    break;
                }
            case "Fire Activated": {
                    PrimaryTile = fireActivated;
                    SecondaryTile = iceDeactivated;
                    break;
                }
            case "Fire Deactivated": {
                    PrimaryTile = fireDeactivated;
                    SecondaryTile = iceActivated;
                    break;
                }
            case "Fire Entrance": {
                    PrimaryTile = fireEntrance;
                    SecondaryTile = iceEntrance;
                    break;
                }
            case "Fire Exit": {
                    PrimaryTile = fireExit;
                    SecondaryTile = iceExit;
                    break;
                }
            case "Fire Pit": {
                    PrimaryTile = firePit;
                    SecondaryTile = icePit;
                    break;
                }


            case "Ice Floor": {
                    PrimaryTile = iceFloor;
                    SecondaryTile = fireFloor;
                    break;
                }
            case "Ice Wall": {
                    PrimaryTile = iceWall;
                    SecondaryTile = fireWall;
                    break;
                }
            case "Ice Activated": {
                    PrimaryTile = iceActivated;
                    SecondaryTile = fireDeactivated;
                    break;
                }
            case "Ice Deactivated": {
                    PrimaryTile = iceDeactivated;
                    SecondaryTile = fireActivated;
                    break;
                }
            case "Ice Entrance": {
                    PrimaryTile = iceEntrance;
                    SecondaryTile = fireEntrance;
                    break;
                }
            case "Ice Exit": {
                    PrimaryTile = iceExit;
                    SecondaryTile = fireExit;
                    break;
                }
            case "Ice Pit": {
                    PrimaryTile = icePit;
                    SecondaryTile = firePit;
                    break;
                }


            case "Desert Floor": {
                    PrimaryTile = desertFloor;
                    SecondaryTile = caveFloor;
                    break;
                }
            case "Desert Wall": {
                    PrimaryTile = desertWall;
                    SecondaryTile = caveWall;
                    break;
                }
            case "Desert Activated": {
                    PrimaryTile = desertActivated;
                    SecondaryTile = caveDeactivated;
                    break;
                }
            case "Desert Deactivated": {
                    PrimaryTile = desertDeactivated;
                    SecondaryTile = caveActivated;
                    break;
                }
            case "Desert Entrance": {
                    PrimaryTile = desertEntrance;
                    SecondaryTile = caveEntrance;
                    break;
                }
            case "Desert Exit": {
                    PrimaryTile = desertExit;
                    SecondaryTile = caveExit;
                    break;
                }
            case "Desert Pit": {
                    PrimaryTile = desertPit;
                    SecondaryTile = cavePit;
                    break;
                }


            case "Cave Floor": {
                    PrimaryTile = caveFloor;
                    SecondaryTile = desertFloor;
                    break;
                }
            case "Cave Wall": {
                    PrimaryTile = caveWall;
                    SecondaryTile = desertWall;
                    break;
                }
            case "Cave Activated": {
                    PrimaryTile = caveActivated;
                    SecondaryTile = desertDeactivated;
                    break;
                }
            case "Cave Deactivated": {
                    PrimaryTile = caveDeactivated;
                    SecondaryTile = desertActivated;
                    break;
                }
            case "Cave Entrance": {
                    PrimaryTile = caveEntrance;
                    SecondaryTile = desertEntrance;
                    break;
                }
            case "Cave Exit": {
                    PrimaryTile = caveExit;
                    SecondaryTile = desertExit;
                    break;
                }
            case "Cave Pit": {
                    PrimaryTile = cavePit;
                    SecondaryTile = desertPit;
                    break;
                }
        }
           
    }

    
}
