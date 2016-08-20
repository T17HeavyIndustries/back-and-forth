using UnityEngine;
using System.Collections;

public class HighScoresPage : MonoBehaviour {

    public bool portrait = false;
    public bool landscape = false;

    public Sprite fireBack;
    public Sprite iceBack;
    public Sprite desertBack;
    public Sprite caveBack;

    public Sprite fireMain;
    public Sprite iceMain;
    public Sprite desertMain;
    public Sprite caveMain;

    private Sprite[] fireButtons;
    private Sprite[] iceButtons;
    private Sprite[] desertButtons;
    private Sprite[] caveButtons;

    private GameObject menuParent;

    void Awake() {

        fireButtons = Resources.LoadAll<Sprite>("button_fire");
        iceButtons = Resources.LoadAll<Sprite>("button_ice");
        desertButtons = Resources.LoadAll<Sprite>("button_desert");
        caveButtons = Resources.LoadAll<Sprite>("button_cave");


        if (portrait) {
            menuParent = GameObject.Find("Portrait Menu");
        }
        else if (landscape) {
            menuParent = GameObject.Find("Landscape Menu");
        }

        if (MenuManager.PageNumber == 1 || MenuManager.PageNumber == 5) {
            menuParent.transform.FindChildIncludingDeactivated("Background1").GetComponent<SpriteRenderer>().sprite = fireMain;
            menuParent.transform.FindChildIncludingDeactivated("Background2").GetComponent<SpriteRenderer>().sprite = fireBack;
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<SpriteRenderer>().sprite = fireButtons[0];
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<GenericMenuButton>().defaultSprite = fireButtons[0];
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<GenericMenuButton>().defaultHover = fireButtons[1];
        }
        else if (MenuManager.PageNumber == 2) {
            menuParent.transform.FindChildIncludingDeactivated("Background1").GetComponent<SpriteRenderer>().sprite = iceMain;
            menuParent.transform.FindChildIncludingDeactivated("Background2").GetComponent<SpriteRenderer>().sprite = iceBack;
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<SpriteRenderer>().sprite = iceButtons[0];
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<GenericMenuButton>().defaultSprite = iceButtons[0];
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<GenericMenuButton>().defaultHover = iceButtons[1];
        }
        else if (MenuManager.PageNumber == 3) {
            menuParent.transform.FindChildIncludingDeactivated("Background1").GetComponent<SpriteRenderer>().sprite = desertMain;
            menuParent.transform.FindChildIncludingDeactivated("Background2").GetComponent<SpriteRenderer>().sprite = desertBack;
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<SpriteRenderer>().sprite = desertButtons[0];
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<GenericMenuButton>().defaultSprite = desertButtons[0];
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<GenericMenuButton>().defaultHover = desertButtons[1];
        }
        else if (MenuManager.PageNumber == 4) {
            menuParent.transform.FindChildIncludingDeactivated("Background1").GetComponent<SpriteRenderer>().sprite = caveMain;
            menuParent.transform.FindChildIncludingDeactivated("Background2").GetComponent<SpriteRenderer>().sprite = caveBack;
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<SpriteRenderer>().sprite = caveButtons[0];
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<GenericMenuButton>().defaultSprite = caveButtons[0];
            menuParent.transform.FindChildIncludingDeactivated("Back").GetComponent<GenericMenuButton>().defaultHover = caveButtons[1];
        }

        for (int i = 1; i < 11; i++) {
            menuParent.transform.FindChild("name" + i.ToString()).GetComponent<TextMesh>().text = MenuManager.PageNumber.ToString() + "  -" + i.ToString();

            if (Main.HighScores.ContainsKey((MenuManager.PageNumber - 1) * (10) + i)) {
                menuParent.transform.FindChild("time" + i.ToString()).GetComponent<TextMesh>().text = Main.HighScores[(MenuManager.PageNumber - 1) * (10) + i].ToString("0.00");
            }
        }


    }
}
