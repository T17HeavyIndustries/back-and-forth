using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public static int PageNumber;

    void Awake() {
        print("here menu");
        GameObject landscape = GameObject.Find("Landscape Menu");
        GameObject portrait = GameObject.Find("Portrait Menu");
        for (int i = 0; i < 10; i++) {
            if (Main.OpenPuzzles[i] == true) {
                portrait.transform.Find("Page One/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
                landscape.transform.Find("Page One/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
            }
            if (Main.BeatenPuzzles[i] == true) {
                portrait.transform.Find("Page One/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
                landscape.transform.Find("Page One/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
            }
        }
        for (int i = 10; i < 20; i++) {
            if (Main.OpenPuzzles[i] == true) {
                portrait.transform.Find("Page Two/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
                landscape.transform.Find("Page Two/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
            }
            if (Main.BeatenPuzzles[i] == true) {
                portrait.transform.Find("Page Two/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
                landscape.transform.Find("Page Two/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
            }
        }
        for (int i = 20; i < 30; i++) {
            if (Main.OpenPuzzles[i] == true) {
                portrait.transform.Find("Page Three/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
                landscape.transform.Find("Page Three/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
            }
            if (Main.BeatenPuzzles[i] == true) {
                portrait.transform.Find("Page Three/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
                landscape.transform.Find("Page Three/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
            }
        }
        for (int i = 30; i < 40; i++) {
            if (Main.OpenPuzzles[i] == true) {
                portrait.transform.Find("Page Four/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
                landscape.transform.Find("Page Four/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
            }
            if (Main.BeatenPuzzles[i] == true) {
                portrait.transform.Find("Page Four/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
                landscape.transform.Find("Page Four/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
            }
        }
        for (int i = 40; i < 50; i++) {
            if (Main.OpenPuzzles[i] == true) {
                portrait.transform.Find("Page Five/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
                landscape.transform.Find("Page Five/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().open = true;
            }
            if (Main.BeatenPuzzles[i] == true) {
                portrait.transform.Find("Page Five/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
                landscape.transform.Find("Page Five/Puzzle" + (i + 1).ToString()).GetComponent<MenuClick>().beaten = true;
            }
        }
    }

    void Start() {
        print(PageNumber);
        if (PageNumber == 1) {
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }
        else if (PageNumber == 2) {
            Camera.main.transform.position = new Vector3(640, 0, -10);
        }
        else if (PageNumber == 3) {
            Camera.main.transform.position = new Vector3(1280, 0, -10);
        }
        else if (PageNumber == 4) {
            Camera.main.transform.position = new Vector3(1920, 0, -10);
        }
        else if (PageNumber == 5) {
            Camera.main.transform.position = new Vector3(2560, 0, -10);
        }
    }
	
}
