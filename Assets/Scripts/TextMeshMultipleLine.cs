using UnityEngine;
using System.Collections;

public class TextMeshMultipleLine : MonoBehaviour {

	// Use this for initialization
    public string txt;
	void Start () {
        GetComponent<TextMesh>().text ="Swipe the screen to \n \n move the player blocks";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
