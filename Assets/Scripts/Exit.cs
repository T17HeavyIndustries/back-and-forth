using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

    //private Main main;

    void Start() {
        //main = GameObject.Find("Main").GetComponent<Main>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        Main.ExitCount++;
    }
    void OnTriggerExit2D(Collider2D col) {
        Main.ExitCount--;
    }
}
