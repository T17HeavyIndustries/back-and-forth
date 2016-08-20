using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CustomLevelBrowser : MonoBehaviour {

    public static List<CustomLevelEntry> LevelEntries = new List<CustomLevelEntry>();

    public GameObject browserButton;
    public GameObject pagePortrait;
    public GameObject pageLandscape;

    public Sprite fireBackPortrait;
    public Sprite iceBackPortrait;
    public Sprite desertBackPortrait;
    public Sprite caveBackPortrait;
    public Sprite fireBackLandscape;
    public Sprite iceBackLandscape;
    public Sprite desertBackLandscape;
    public Sprite caveBackLandscape;
    public Sprite fireBackFull;
    public Sprite iceBackFull;
    public Sprite desertBackFull;
    public Sprite caveBackFull;

    private int pageCount = 1;

    void Awake() {
        LevelEntries = new List<CustomLevelEntry>();
        LoadCustomLevelList();
        CustomLevelMain.CurLevelInfo = null;
    }

    private void LoadCustomLevelList(){

        DirectoryInfo dirInfo = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] fileInfos = dirInfo.GetFiles();

        foreach (FileInfo file in fileInfos) {
            //print(file.Name);
            if (file.Name.Substring(file.Name.Length - 3) == "lev") {
                print("found level " + file.Name);
                LevelEntries.Add(new CustomLevelEntry(file.Name.Remove(file.Name.Length - 4), file.Name));
            }
        }

        LevelEntries = LevelEntries.OrderBy(o => o.name).ToList();

        int page = 0;
        int item = 0;
        foreach (CustomLevelEntry levelEntry in LevelEntries) {
                if (GameObject.Find("Page" + page.ToString()) == null) {
                    GameObject pageObjP = (GameObject)Instantiate(pagePortrait);
                    GameObject pageObjL = (GameObject)Instantiate(pageLandscape);
                    pageObjP.name = "Page" + page.ToString();
                    pageObjL.name = "Page" + page.ToString();
                    pageObjP.transform.position = new Vector3(640 * page, 0, 0);
                    pageObjL.transform.position = new Vector3(640 * page, 0, 0);
                    pageObjP.transform.parent = GameObject.Find("Portrait Menu").transform;
                    pageObjL.transform.parent = GameObject.Find("Landscape Menu").transform;

                    if (page % 4 == 0) {
                        pageObjP.transform.FindChild("background1").GetComponent<SpriteRenderer>().sprite = fireBackPortrait;
                        pageObjL.transform.FindChild("background1").GetComponent<SpriteRenderer>().sprite = fireBackLandscape;
                        pageObjP.transform.FindChild("background2").GetComponent<SpriteRenderer>().sprite = fireBackFull;
                        pageObjL.transform.FindChild("background2").GetComponent<SpriteRenderer>().sprite = fireBackFull;

                    }
                    else if (page % 4 == 1) {
                        pageObjP.transform.FindChild("background1").GetComponent<SpriteRenderer>().sprite = iceBackPortrait;
                        pageObjL.transform.FindChild("background1").GetComponent<SpriteRenderer>().sprite = iceBackLandscape;
                        pageObjP.transform.FindChild("background2").GetComponent<SpriteRenderer>().sprite = iceBackFull;
                        pageObjL.transform.FindChild("background2").GetComponent<SpriteRenderer>().sprite = iceBackFull;
                    }
                    else if (page % 4 == 2) {
                        pageObjP.transform.FindChild("background1").GetComponent<SpriteRenderer>().sprite = desertBackPortrait;
                        pageObjL.transform.FindChild("background1").GetComponent<SpriteRenderer>().sprite = desertBackLandscape;
                        pageObjP.transform.FindChild("background2").GetComponent<SpriteRenderer>().sprite = desertBackFull;
                        pageObjL.transform.FindChild("background2").GetComponent<SpriteRenderer>().sprite = desertBackFull;
                    }
                    else if (page % 4 == 3) {
                        pageObjP.transform.FindChild("background1").GetComponent<SpriteRenderer>().sprite = caveBackPortrait;
                        pageObjL.transform.FindChild("background1").GetComponent<SpriteRenderer>().sprite = caveBackLandscape;
                        pageObjP.transform.FindChild("background2").GetComponent<SpriteRenderer>().sprite = caveBackFull;
                        pageObjL.transform.FindChild("background2").GetComponent<SpriteRenderer>().sprite = caveBackFull;
                    }
                }
                GameObject curButtonP = (GameObject)Instantiate(browserButton);
                GameObject curButtonL = (GameObject)Instantiate(browserButton);
                curButtonP.transform.parent = GameObject.Find("Portrait Menu").transform.FindChild("Page" + page.ToString()).transform;
                curButtonL.transform.parent = GameObject.Find("Landscape Menu").transform.FindChild("Page" + page.ToString()).transform;

                curButtonP.transform.localPosition = new Vector3(0, 88 - (item % 10) * (16), 0);
                if(item % 10 < 5)
                    curButtonL.transform.localPosition = new Vector3(-78, 54 - (item % 10) * (16), 0);
                else
                    curButtonL.transform.localPosition = new Vector3(78, 54 - (item % 10) * (16), 0);

                curButtonP.GetComponent<CustomLevelBrowserButton>().levelInfo = levelEntry;
                curButtonL.GetComponent<CustomLevelBrowserButton>().levelInfo = levelEntry;

                item++;
                page = (item / 10);     
        }

    }
}

public class CustomLevelEntry {
    public CustomLevelEntry(string nam, string path) {
        name = nam;
        filePath = path;
    }
    public string name;
    public string filePath;
}
