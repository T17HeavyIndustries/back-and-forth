using UnityEngine;
using System.Collections;

public class PuzzleMenuForward : MonoBehaviour {

    public static bool Shifting;

    private GameObject cam;

    private bool shift = false;
    private Vector3 startLoc = new Vector3();

    private bool touched = false;
    private Vector2 lastTouchPos = new Vector2();

    public Sprite defaultSprite;
    public Sprite hoverSprite;
    private SpriteRenderer rend;

    private float moveSpeed = 1000f;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        cam = Camera.main.gameObject;
    }

    void Update() {
#if UNITY_ANDROID
        if (!shift && !Shifting && !PuzzleMenuBack.Shifting) {
            if (Input.touchCount == 0) {
                if (touched && collider2D == Physics2D.OverlapPoint(lastTouchPos)) {
                    startLoc = cam.transform.position;
                    shift = true;
                    Shifting = true;
                    touched = false;
                    MenuManager.PageNumber++;
                }
                rend.sprite = defaultSprite;
            }
            else if (Input.touchCount == 1) {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                lastTouchPos = touchPos;
                if (collider2D == Physics2D.OverlapPoint(touchPos)) {
                    rend.sprite = hoverSprite;
                    touched = true;
                }
            }
        }
#endif
        if (shift) {
            cam.transform.Translate(Time.deltaTime * moveSpeed, 0f, 0f);
            if (cam.transform.position.x >= startLoc.x + 640) {
                shift = false;
                Shifting = false;
                cam.transform.position = new Vector3(startLoc.x + 640, startLoc.y, startLoc.z);
            }
        }     
    }

#if UNITY_EDITOR
    void OnMouseDown() {
        if (!shift && !Shifting && !PuzzleMenuBack.Shifting) {
            startLoc = cam.transform.position;
            shift = true;
            Shifting = true;
            MenuManager.PageNumber++;
        }
    }

#elif UNITY_STANDALONE
    void OnMouseDown(){
        if (!shift && !Shifting && !PuzzleMenuBack.Shifting) {
            startLoc = cam.transform.position;
            shift = true;
            Shifting = true;
    MenuManager.PageNumber++;
        }
    }
#endif
}
