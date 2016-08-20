using UnityEngine;
using System.Collections;

public class NameEnter : MonoBehaviour {

    private TextMesh textMesh;
#if UNITY_ANDROID
    private TouchScreenKeyboard keyboard;
#endif

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();
    private Camera cam;

    public static string CurrentNameText = "";

	void Start () {
#if UNITY_ANDROID
        keyboard = TouchScreenKeyboard.Open(transform.FindChild("Text").GetComponent<TextMesh>().text,TouchScreenKeyboardType.Default,true,false);
#endif
        textMesh = transform.FindChild("Text").GetComponent<TextMesh>();
        cam = GameObject.Find("Left Camera").camera;
        touched = false;

#if UNITY_EDITOR
        textMesh.text = Time.frameCount.ToString("0");
#elif UNITY_STANDALONE
        textMesh.text = Time.frameCount.ToString("0");
#endif
    }
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID        
        if(keyboard.active){
                textMesh.text = keyboard.text.ToLower();
        }     
#endif
        if (textMesh.text.Length > 10) {
            textMesh.text = textMesh.text.Substring(0, 11);
        }

        CurrentNameText = textMesh.text.ToLower();

#if UNITY_ANDROID
        //being pressed
        if (Input.touchCount == 0) {
            if (touched) {
                if(!keyboard.active){
                   keyboard = TouchScreenKeyboard.Open(transform.FindChild("Text").GetComponent<TextMesh>().text,TouchScreenKeyboardType.Default,true,false);
                }
                touched = false;
            }
        }
        else if (Input.touchCount == 1) {
            Vector3 wp = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos)) {
                touched = true;
            }

        }
#endif
	}
}
