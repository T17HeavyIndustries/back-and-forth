using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Main : MonoBehaviour {


    //public static bool First = false;

    public static bool[] OpenPuzzles = new bool[50];
    public static bool[] BeatenPuzzles = new bool[50];

    public static bool init = false;

    public static int ExitCount = 0;

    public static int AdCounter = 5;

    public static int PortraitHeight = 0;
    public static int LandscapeHeight = 0;
    public static int PortraitWidth = 0;
    public static int LandscapeWidth = 0;

    public static Dictionary<int, float> HighScores = new Dictionary<int,float>();
    public static Dictionary<string, float> CustomHighScores = new Dictionary<string, float>();

    public GameObject hint1;
    public GameObject hint2;
    public GameObject hint3;

    void OnApplicationPause() {
//#if UNITY_ANDROID
        if(init)
            Save();
//#endif
    }

    void OnApplicationQuit() {
        print("quit");
        if (OpenPuzzles[0] == false)
            OpenPuzzles[0] = true;
        Save();
    }

    void OnLevelWasLoaded(int level) {
        ExitCount = 0;
        if (level == 1 && !File.Exists(Application.persistentDataPath + "/hint1")) {
            Instantiate(hint1);
            File.Create(Application.persistentDataPath + "/hint1");
        }
        else if ((level == 11 || level == 12) && !File.Exists(Application.persistentDataPath + "/hint2")){
            Instantiate(hint2);
            File.Create(Application.persistentDataPath + "/hint2");
        }
        else if ((level == 41 || level == 42) && !File.Exists(Application.persistentDataPath + "/hint3")){
            Instantiate(hint3);
            File.Create(Application.persistentDataPath + "/hint3");
        }
    }

    void Awake() {
        
        if (!init) {
            GoogleAnalytics.instance.LogScreen("Startup");
            init = true;

            if (Screen.orientation == ScreenOrientation.Landscape) {
                LandscapeHeight = Screen.height;
                LandscapeWidth = Screen.width;
                PortraitHeight = Screen.width;
                PortraitWidth = Screen.height;
            }
            else {
                LandscapeHeight = Screen.width;
                LandscapeWidth = Screen.height;
                PortraitHeight = Screen.height;
                PortraitWidth = Screen.width;
            }

            MenuManager.PageNumber = 1;
            
        }

        else{
            DestroyImmediate(gameObject);
            return;
        }

        /*if (AdCounter % 3 == 0)
            HZInterstitialAd.show();
        AdCounter++;*/
       

        Input.multiTouchEnabled = true;
        DontDestroyOnLoad(transform.gameObject);


        if (File.Exists(Application.persistentDataPath + "/MainSave.sav")) {
            print("loaded");
            Load();
        }
        else {
            OpenPuzzles[0] = true;
            BeatenPuzzles[0] = false;
            for (int i = 1; i < 50; i++) {
                OpenPuzzles[i] = false;
                BeatenPuzzles[i] = false;
            }
            Save();
        }
       
    }


    void Update() {
        
    }


    void Save() {
        print("save");

        SaveData saveData = new SaveData(OpenPuzzles, BeatenPuzzles);
        XmlSerializer xmls = new XmlSerializer(typeof(SaveData));
        using (var stream = File.OpenWrite(Application.persistentDataPath + "/MainSave.sav")) {
            xmls.Serialize(stream, saveData);
        }

        HighScoresData highScoreData = new HighScoresData(HighScores);
        XmlSerializer xmls2 = new XmlSerializer(typeof(HighScoresData));
        using (var stream = File.OpenWrite(Application.persistentDataPath + "/HighScores.sav")) {
            xmls2.Serialize(stream, highScoreData);
        }

        File.Delete(Application.persistentDataPath + "/CustomHighScores.sav");
        CustomHighScoresData highScoreDataCustom = new CustomHighScoresData(CustomHighScores);
        XmlSerializer xmls3 = new XmlSerializer(typeof(CustomHighScoresData));
        using (var stream = File.OpenWrite(Application.persistentDataPath + "/CustomHighScores.sav")) {
            xmls3.Serialize(stream, highScoreDataCustom);
        }

    }

    void Load() {
        SaveData saveData = new SaveData();
        XmlSerializer xmls = new XmlSerializer(typeof(SaveData));
        using (var stream = File.OpenRead(Application.persistentDataPath + "/MainSave.sav")) {
            saveData = xmls.Deserialize(stream) as SaveData;
        }

        for (int i = 0; i < 50; i++) {
            if (saveData.openLevels[i] == '1')
                OpenPuzzles[i] = true;
            else
                OpenPuzzles[i] = false;
            if (saveData.beatenLevels[i] == '1')
                BeatenPuzzles[i] = true;
            else
                BeatenPuzzles[i] = false;
        }

        HighScoresData highScoredata = new HighScoresData();
        XmlSerializer xmls2 = new XmlSerializer(typeof(HighScoresData));

        if (File.Exists(Application.persistentDataPath + "/HighScores.sav")) {
            using (var stream = File.OpenRead(Application.persistentDataPath + "/HighScores.sav")) {
                highScoredata = xmls2.Deserialize(stream) as HighScoresData;
            }

            HighScores = new Dictionary<int, float>();
            for (int i = 0; i < highScoredata.keys.Length; i++) {
                HighScores.Add(highScoredata.keys[i], highScoredata.values[i]);
            }
        }

        CustomHighScoresData highScoredataCustom = new CustomHighScoresData();
        XmlSerializer xmls3 = new XmlSerializer(typeof(CustomHighScoresData));

        if (File.Exists(Application.persistentDataPath + "/CustomHighScores.sav")) {
            using (var stream = File.OpenRead(Application.persistentDataPath + "/CustomHighScores.sav")) {
                highScoredataCustom = xmls3.Deserialize(stream) as CustomHighScoresData;
            }

            CustomHighScores = new Dictionary<string, float>();
            for (int i = 0; i < highScoredataCustom.keys.Length; i++) {
                CustomHighScores.Add(highScoredataCustom.keys[i], highScoredataCustom.values[i]);
            }
        }
    }

    public static void SendTime(int puzzleNumber, float time) {

        if (HighScores.ContainsKey(puzzleNumber)) {
            if (HighScores[puzzleNumber] < time) {
                return;
            }
            else {
                HighScores.Remove(puzzleNumber);
                HighScores.Add(puzzleNumber, time);
            }
        }
        else {
            HighScores.Add(puzzleNumber, time);
        }

    }

    public static void SendTimeCustom(string puzzleName, float time) {
        if (CustomHighScores.ContainsKey(puzzleName)) {
            if (CustomHighScores[puzzleName] < time) {
                return;
            }
            else {
                CustomHighScores.Remove(puzzleName);
                CustomHighScores.Add(puzzleName, time);
            }
        }
        else {
            CustomHighScores.Add(puzzleName, time);
        }
    }

    public static void RemoveTimeCustom(string puzzleName) {
        if (CustomHighScores.ContainsKey(puzzleName)) {
            CustomHighScores.Remove(puzzleName);
        }
        else {
            print("That custom high score entry does not exist");
        }
    }
 
}

[XmlRoot("SaveData")]
public class SaveData {

    public SaveData() { }

    public SaveData(bool[] op, bool[] btn) {
        for (int i = 0; i < 50; i++) {
            if (op[i] == true)
                openLevels += "1";
            else
                openLevels += "0";
            if (btn[i] == true)
                beatenLevels += "1";
            else
                beatenLevels += "0";
        }
    }

    [XmlElement]
    public string openLevels;
    [XmlElement]
    public string beatenLevels;
}

[XmlRoot("HighScoresData")]
public class HighScoresData {
    public HighScoresData() { 
        keys = new int[0];
        values = new float[0];
    }

    public HighScoresData(Dictionary<int, float> scrs) {
        keys = new int[scrs.Count];
        values = new float[scrs.Count];

        int i = 0;
        foreach (KeyValuePair<int, float> entry in scrs) {
            keys[i] = entry.Key;
            values[i] = entry.Value;
            i++;
        }
    }

    [XmlElement]
    public int[] keys;
    [XmlElement]
    public float[] values;
}

[XmlRoot("CustomHighScoresData")]
public class CustomHighScoresData {
    public CustomHighScoresData() {
        keys = new string[0];
        values = new float[0];
    }

    public CustomHighScoresData(Dictionary<string, float> scrs) {
        keys = new string[scrs.Count];
        values = new float[scrs.Count];

        int i = 0;
        foreach (KeyValuePair<string, float> entry in scrs) {
            keys[i] = entry.Key;
            values[i] = entry.Value;
            i++;
        }
    }

    [XmlElement]
    public string[] keys;
    [XmlElement]
    public float[] values;
}
