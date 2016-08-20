using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    private GameObject ice;
    private GameObject fire;
    private GameObject cave;
    private GameObject desert;

    public bool leftSwitchable = false;
    public bool rightSwitchable = false;
    public bool custom;
    public bool test;

    private float switchTime = .5f;
    private float switchCounter = 0;

    private bool switching = false;

    private GameObject playerLeft;
    private GameObject playerRight;
    private PlayerController playerCon;

    private float currentTime = 0;

    private GameObject timer;
    private GameObject highScore;

    public int puzzleNumber = -1;

    public static GameObject debug;

    public bool startFireDesert;
    public bool startIceCave;
    public bool includeFire;
    public bool includeDesert;
    public bool includeIce;
    public bool includeCave;

    private bool firstFrame = true;

    public static bool TimePause = false;

    private bool shaking = false;
    

    void Start() {
     
        if (Application.loadedLevelName == "LevelEditor") {
            test = true;
            custom = true;
        }
        else if(Application.loadedLevelName == "CustomLevel"){
            test = false;
            custom = true;
            StartCoroutine("CustomLevelLateStart");
        }
        else{
            test = false;
            custom = false;
        }

        TimePause = false;
        if (!custom) {
            int.TryParse(Application.loadedLevelName.Substring(6), out puzzleNumber);
            MenuManager.PageNumber = (puzzleNumber - 1) / 10 + 1;
        
        }

            playerLeft = GameObject.Find("Player Left");
            playerRight = GameObject.Find("Player Right");
            playerCon = GameObject.Find("Player Parent").GetComponent<PlayerController>();

        

        fire = transform.FindChild("Fire").gameObject;
        ice = transform.FindChild("Ice").gameObject;
        desert = transform.FindChild("Desert").gameObject;
        cave = transform.FindChild("Cave").gameObject;

        GoogleAnalytics.instance.LogScreen(Application.loadedLevelName);

        GameObject.Find("Left Camera").transform.FindChild("Fade").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        GameObject.Find("Right Camera").transform.FindChild("Fade").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);

        playerCon.mobile = false;
        LevelController.TimePause = true;
        StartCoroutine("FadeIn");

        print(test);
        if (!test) {
            debug = GameObject.Find("Debug");
            timer = GameObject.Find("Timer");
            highScore = GameObject.Find("High Score");
            timer.GetComponent<TextMesh>().text = "0.0";

            if (!custom) {
                try {
                    highScore.GetComponent<TextMesh>().text = "Best:" + Main.HighScores[puzzleNumber].ToString("0.0");
                }
                catch {
                    highScore.GetComponent<TextMesh>().text = "Best:NA";
                }
            }
            else {
                try {
                    highScore.GetComponent<TextMesh>().text = "Best:" + Main.CustomHighScores[CustomLevelMain.CurLevelInfo.name].ToString("0.0");
                }
                catch {
                    highScore.GetComponent<TextMesh>().text = "Best:NA";
                }
            }
        }
        
    }

    void Update() {

        print(Main.ExitCount);

        if (!playerCon.moving && Main.ExitCount == 2) {
            TimePause = true;
            if (!custom) {
                int number = -1;
                int.TryParse(Application.loadedLevelName.Substring(6), out number);
                Main.BeatenPuzzles[number - 1] = true;
                try {
                    Main.OpenPuzzles[number] = true;
                }
                catch { }
                try {
                    Main.OpenPuzzles[number + 1] = true;
                }
                catch { }
                
                Main.ExitCount = 0;

                if (Application.loadedLevelName != "Puzzle50") {
                    playerCon.mobile = false;
                    StartCoroutine("IEFadeScreenAndLoadScene", "Puzzle" + (puzzleNumber + 1).ToString());
                }
                else {
                    playerCon.mobile = false;
                    StartCoroutine("IEFadeScreenAndLoadScene", "PuzzleMenu");
                }
                
                Main.SendTime(number, currentTime);
            }

            else if (Application.loadedLevelName == "LevelEditor") {
                Main.ExitCount = 0;
                playerCon.enabled = false;
                GameObject.Find("Editor Main").GetComponent<EditorMain>().completedPrompt.SetActive(true);
            }  

            else if (Application.loadedLevelName == "CustomLevel") {
                Main.ExitCount = 0;
                playerCon.mobile = false;
                Main.SendTimeCustom(CustomLevelMain.CurLevelInfo.name, currentTime);
                StartCoroutine("IEFadeScreenAndLoadScene", "CustomLevelBrowser");
            }
                 
        }

        //print(TimePause.ToString() + test.ToString());
        if(!TimePause && !test){
            timer.GetComponent<TextMesh>().text = currentTime.ToString("0.0");
            currentTime += Time.deltaTime;
        }
        
        foreach(Touch touch in Input.touches) {
            if(touch.tapCount == 2)
                Switch();
        }

#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.J)) {
            Switch();
        }
#endif
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.J)) {
            Switch();
        }
#endif


        if (switching) {
            switchCounter += Time.deltaTime;
            if (switchCounter >= switchTime) {
                switchCounter = 0;
                switching = false;
            }
        }
        

       
    }

    public void Switch() {
        if (!switching) {

            if ((!leftSwitchable && !rightSwitchable) || !PlayerController.CanShift) {
                if (!shaking) {
                    StartCoroutine("ShakeLeft");
                    StartCoroutine("ShakeRight");
                }
            }          

            GameObject[] con = GameObject.FindGameObjectsWithTag("Player Contact");
            for (int i = 0; i < con.Length; i++) {
              con[i].GetComponent<PlayerContact>().Clear();
            }
            switching = true;
            if (leftSwitchable && PlayerController.CanShift) {
                    if (ice.transform.FindChildIncludingDeactivated("IceBackground").GetComponent<SpriteRenderer>().enabled) {

                        ice.transform.FindChildIncludingDeactivated("IceBackground").GetComponent<SpriteRenderer>().enabled = false;
                        fire.transform.FindChildIncludingDeactivated("FireBackground").GetComponent<SpriteRenderer>().enabled = true;

       

                        ice.transform.FindChildIncludingDeactivated("Colliders").gameObject.SetActive(false);
                        fire.transform.FindChildIncludingDeactivated("Colliders").gameObject.SetActive(true);

                        if (!custom) {
                            ice.GetComponent<SpriteRenderer>().enabled = false;
                            fire.GetComponent<SpriteRenderer>().enabled = true;
                        }
                    
                    
                }
                else {
                        ice.transform.FindChildIncludingDeactivated("IceBackground").GetComponent<SpriteRenderer>().enabled = true;
                        fire.transform.FindChildIncludingDeactivated("FireBackground").GetComponent<SpriteRenderer>().enabled = false;

        
                        ice.transform.FindChild("Colliders").gameObject.SetActive(true);
                        fire.transform.FindChild("Colliders").gameObject.SetActive(false);

                        if (!custom) {
                            ice.GetComponent<SpriteRenderer>().enabled = true;
                            fire.GetComponent<SpriteRenderer>().enabled = false;
                        }
                    
                 
                }
                playerLeft.GetComponent<PlayerLeft>().Switch();
            }



            if (rightSwitchable && PlayerController.CanShift) {
                if (cave.transform.FindChildIncludingDeactivated("CaveBackground").GetComponent<SpriteRenderer>().enabled) {

                    cave.transform.FindChildIncludingDeactivated("CaveBackground").GetComponent<SpriteRenderer>().enabled = false;
                    desert.transform.FindChildIncludingDeactivated("DesertBackground").GetComponent<SpriteRenderer>().enabled = true;

          
                        cave.transform.FindChildIncludingDeactivated("Colliders").gameObject.SetActive(false);
                        desert.transform.FindChildIncludingDeactivated("Colliders").gameObject.SetActive(true);

                        if (!custom) {
                            cave.GetComponent<SpriteRenderer>().enabled = false;
                            desert.GetComponent<SpriteRenderer>().enabled = true;
                        }
                    
                  
                }
                else {
                    cave.transform.FindChildIncludingDeactivated("CaveBackground").GetComponent<SpriteRenderer>().enabled = true;
                    desert.transform.FindChildIncludingDeactivated("DesertBackground").GetComponent<SpriteRenderer>().enabled = false;

                        cave.transform.FindChild("Colliders").gameObject.SetActive(true);
                        desert.transform.FindChild("Colliders").gameObject.SetActive(false);

                        if (!custom) {
                            cave.GetComponent<SpriteRenderer>().enabled = true;
                            desert.GetComponent<SpriteRenderer>().enabled = false;
                        }
                    
                 
                }
                playerRight.GetComponent<PlayerLeft>().Switch();
            }

            if (!rightSwitchable && !leftSwitchable) {
                GameObject.Find("Left Camera").audio.Play();
            }
        }
    }

    IEnumerator IEFadeScreenAndLoadScene(string leveName) {

        for (float i = 0f; i <= 1f; i = i + .2f) {
            if (leveName.Substring(0, 6) == "Puzzle" || leveName == "CustomLevelBrowser") {
                GameObject.Find("Left Camera").transform.FindChild("Fade").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i);
                GameObject.Find("Right Camera").transform.FindChild("Fade").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i);
            }

            yield return new WaitForSeconds(.05f);
        }
        print(leveName);

 


        Application.LoadLevel(leveName);
    }

    IEnumerator FadeIn() {
        for (float i = 1f; i > 0f; i = i - .2f) {           
                GameObject.Find("Left Camera").transform.FindChild("Fade").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i);
                GameObject.Find("Right Camera").transform.FindChild("Fade").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i);
             yield return new WaitForSeconds(.05f);

        }
        playerCon.mobile = true;
        LevelController.TimePause = false;

    }

    IEnumerator ShakeLeft() {
        GameObject leftCam = GameObject.Find("Left Camera");        
        Vector3 leftStart = leftCam.transform.position;

        shaking = true;

            for (int i = 0; i < 5; i++) {
                leftCam.transform.Translate(100 * Time.deltaTime, 0, 0);
                yield return null;
            }
            for (int i = 0; i < 10; i++) {
                leftCam.transform.Translate(-100 * Time.deltaTime, 0, 0);
                yield return null;
            }
            for (int i = 0; i < 5; i++) {
                leftCam.transform.Translate(100 * Time.deltaTime, 0, 0);
                yield return null;
            }
            leftCam.transform.position = leftStart;

            shaking = false;
       
    }

    IEnumerator ShakeRight() {
        GameObject rightCam = GameObject.Find("Right Camera");
        Vector3 rightStart = rightCam.transform.position;

        shaking = true;

        for (int i = 0; i < 5; i++) {
            rightCam.transform.Translate(-100 * Time.deltaTime, 0, 0);
            yield return null;
        }
        for (int i = 0; i < 10; i++) {
            rightCam.transform.Translate(100 * Time.deltaTime, 0, 0);
            yield return null;
        }
        for (int i = 0; i < 5; i++) {
            rightCam.transform.Translate(-100 * Time.deltaTime, 0, 0);
            yield return null;
        }
        rightCam.transform.position = rightStart;


        shaking = false;
    }

    IEnumerator CustomLevelLateStart() {
        yield return null;
        yield return null;
        if (Application.loadedLevelName == "LevelEditor") {
            test = true;
            custom = true;
        }
        else if (Application.loadedLevelName == "CustomLevel") {
            test = false;
            custom = true;
        }
        else {
            test = false;
            custom = false;
        }

        TimePause = false;
    }
}
