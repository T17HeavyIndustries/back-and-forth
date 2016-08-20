using UnityEngine;
using System.Collections;

public class NoShift : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "Player Right" || col.gameObject.name == "Player Left")
            PlayerController.CanShift = false;
    }
    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.name == "Player Right" || col.gameObject.name == "Player Left")
            PlayerController.CanShift = false;
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Player Right" || col.gameObject.name == "Player Left")
            PlayerController.CanShift = true;
    }

}
