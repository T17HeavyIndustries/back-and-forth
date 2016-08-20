using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

    // Character gameobjects
    private GameObject playerRight;
    private GameObject playerLeft;

    //input values
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;


    //move slide stuff
    private bool movingUp = false;
    private bool movingDown = false;
    private bool movingLeft = false;
    private bool movingRight = false;

    public float moveTime;
    private float moveTimeCounter = 0;

    private Vector3 leftStartLocation;
    private Vector3 rightStartLocation;

    //
    public bool mobile = true;
    public bool moving = false;

    //
    public int movementSpeed;
    private Vector3 rightPrevLoc;
    private Vector3 leftPrevLoc;

    //
    public bool rLeftContact = false;
    public bool rRightContact = false;
    public bool rUpContact = false;
    public bool rDownContact = false;

    public bool lLeftContact = false;
    public bool lRightContact = false;
    public bool lUpContact = false;
    public bool lDownContact = false;

    public bool pitLeft = false;
    public bool pitRight = false;
    public bool pitUp = false;
    public bool pitDown = false;

    private bool rLc = false;
    private bool rRc = false;
    private bool rUc = false;
    private bool rDc = false;

    private bool lLc = false;
    private bool lRc = false;
    private bool lUc = false;
    private bool lDc = false;

    private bool pL = false;
    private bool pR = false;
    private bool pU = false;
    private bool pD = false;

    public static bool CanShift = true;

    private bool mobileNextFrame = false;

    //Swip variables
    //private float startTime;
    private Vector2 startPos;
    private bool couldBeSwipe;
    private float minSwipeDistance = 30;


    void Start() {
        playerRight = GameObject.Find("Player Right");
        playerLeft = GameObject.Find("Player Left");
        rightPrevLoc = playerRight.transform.position;
        leftPrevLoc = playerLeft.transform.position;
    }


    void Update() {
        

#if UNITY_ANDROID
        // Get movement values
        if (Input.touchCount > 0) {
            //down = true;
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    couldBeSwipe = true;
                    startPos = touch.position;
                    //startTime = Time.time;
                    break;


                case TouchPhase.Ended:

                    float swipeDist = (touch.position - startPos).magnitude;
                    if (swipeDist < minSwipeDistance) {
                        couldBeSwipe = false;
                    }
                    if (couldBeSwipe) {
                        float swipeX = touch.position.x - startPos.x;
                        float swipeY = touch.position.y - startPos.y;

                        if (Mathf.Abs(swipeX) > Mathf.Abs(swipeY)) {
                            if (swipeX > 0) {
                                right = true;
                            }
                            else {
                                left = true;
                            }

                        }

                        else {
                            if (swipeY > 0) {
                                up = true;
                            }
                            else {
                                down = true;
                            }
                        }
                    }

                    break;

            }
        }
#endif

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            print("left down");
            left = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            right = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            up = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            down = true;
        }
#endif
#if UNITY_STANDALONE
    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            print("left down");
            left = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            right = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            up = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            down = true;
        }
#endif

        if (mobile) {

            rLc = rLeftContact;
            rRc = rRightContact;
            rUc = rUpContact;
            rDc = rDownContact;
            lLc = lLeftContact;
            lRc = lRightContact;
            lUc = lUpContact;
            lDc = lDownContact;
            pR = pitRight;
            pL = pitLeft;
            pU = pitUp;
            pD = pitDown;


            if (up) {
                movingUp = true;
                mobile = false;
                moving = true;

                leftStartLocation = playerLeft.transform.position;
                rightStartLocation = playerRight.transform.position;
            }
            else if (down) {
                movingDown = true;
                mobile = false;
                moving = true;
                leftStartLocation = playerLeft.transform.position;
                rightStartLocation = playerRight.transform.position;
            }

            if (left) {
                movingLeft = true;
                mobile = false;
                moving = true;
                leftStartLocation = playerLeft.transform.position;
                rightStartLocation = playerRight.transform.position;
            }
            else if (right) {
                movingRight = true;
                mobile = false;
                moving = true;
                leftStartLocation = playerLeft.transform.position;
                rightStartLocation = playerRight.transform.position;
            }

        }

        if (mobileNextFrame) {
            mobile = true;
            mobileNextFrame = false;
        }
    }

    void FixedUpdate() {

        //Apply Movement


        if (moving) {
            if (movingUp) {
                if ((rUc && lUc) || pU) {
                    moveTimeCounter = 0;
                    print("here");
                    //Debug.Break();
                    moving = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = false;
                    movingUp = false;
                    mobileNextFrame = true;
                    return;
                }
                
                if (!rUc)
                    playerRight.transform.Translate(new Vector2(0, 1) * Time.deltaTime * movementSpeed);
                if (!lUc)
                    playerLeft.transform.Translate(-new Vector2(0, 1) * Time.deltaTime * movementSpeed);

                if (playerRight.transform.position.y >= rightStartLocation.y + 16 || playerLeft.transform.position.y <= leftStartLocation.y - 16) {
                    moveTimeCounter = 0;
                    moving = false;
                    movingUp = false;
                    mobile = true;
                    moveTimeCounter = 0;
                }
            }

            else if (movingDown) {
                if ((rDc && lDc) || pD) {
                    moveTimeCounter = 0;
                    moving = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = false;
                    movingUp = false;
                    mobileNextFrame = true;
                    return;
                }
                if (!rDc)
                    playerRight.transform.Translate(-new Vector2(0, 1) * Time.deltaTime * movementSpeed);
                if (!lDc)
                    playerLeft.transform.Translate(new Vector2(0, 1) * Time.deltaTime * movementSpeed);

                if (playerRight.transform.position.y <= rightStartLocation.y - 16 || playerLeft.transform.position.y >= leftStartLocation.y + 16) {
                    moveTimeCounter = 0;
                    moving = false;
                    movingDown = false;
                    mobile = true;
                    moveTimeCounter = 0;
                }
            }

            else if (movingLeft) {
                if ((rLc && lLc) || pL) {
                    moveTimeCounter = 0;
                    moving = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = false;
                    movingUp = false;
                    mobileNextFrame = true;
                    return;
                }
                if (!rLc)
                    playerRight.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * movementSpeed);
                if (!lLc)
                    playerLeft.transform.Translate(-new Vector2(-1, 0) * Time.deltaTime * movementSpeed);

                if (playerRight.transform.position.x <= rightStartLocation.x - 16 || playerLeft.transform.position.x >= leftStartLocation.x + 16) {
                    moveTimeCounter = 0;
                    moving = false;
                    movingLeft = false;
                    mobile = true;
                    moveTimeCounter = 0;
                }
            }

            else if (movingRight) {
                if ((rRc && lRc) || pR) {
                    moveTimeCounter = 0;
                    moving = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = false;
                    movingUp = false;
                    mobileNextFrame = true;
                    return;
                }
                if (!rRc)
                    playerRight.transform.Translate(-new Vector2(-1, 0) * Time.deltaTime * movementSpeed);
                if (!lRc) {
                    playerLeft.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * movementSpeed);
                }

                if (playerRight.transform.position.x >= rightStartLocation.x + 16 || playerLeft.transform.position.x <= leftStartLocation.x - 16) {
                    moveTimeCounter = 0;
                    moving = false;
                    movingRight = false;
                    mobile = true;
                    moveTimeCounter = 0;
                }
            }

            moveTimeCounter += Time.deltaTime;
            if (moveTimeCounter >= moveTime) {
                moveTimeCounter = 0;
                moving = false;
                movingDown = false;
                movingLeft = false;
                movingRight = false;
                movingUp = false;
                mobileNextFrame = true;
            }
        }


        if (Mathf.Abs(rightPrevLoc.x - playerRight.transform.position.x) < 0.05 && Mathf.Abs(rightPrevLoc.y - playerRight.transform.position.y) < 0.05) {

            playerRight.transform.position = new Vector3(Mathf.FloorToInt(playerRight.transform.position.x), Mathf.FloorToInt(playerRight.transform.position.y), 0);
        }
        if (Mathf.Abs(leftPrevLoc.x - playerLeft.transform.position.x) < 0.05 && Mathf.Abs(leftPrevLoc.y - playerLeft.transform.position.y) < 0.05) {

            playerLeft.transform.position = new Vector3(Mathf.FloorToInt(playerLeft.transform.position.x), Mathf.FloorToInt(playerLeft.transform.position.y), 0);
        }

        rightPrevLoc = playerRight.transform.position;
        leftPrevLoc = playerLeft.transform.position;

        up = false;
        down = false;
        left = false;
        right = false;
    }

    public bool Moving {
        get { return moving; }
    }
}
