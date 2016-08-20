using UnityEngine;
using System.Collections;

public class PlayerContact : MonoBehaviour {

    private PlayerController playerController;

    public bool rightPlayer = false;
    public bool leftPlyer = false;
    public bool left = false;
    public bool right = false;
    public bool up = false;
    public bool down = false;

    void Start() {
        playerController = GameObject.Find("Player Parent").GetComponent<PlayerController>();
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == 8 && !col.isTrigger) {
            if(rightPlayer){
                if (left)
                    playerController.rLeftContact = true;
                else if (right)
                    playerController.rRightContact = true;
                else if (up)
                    playerController.rUpContact = true;
                else if (down)
                    playerController.rDownContact = true;
            }
            else if (leftPlyer) {
                if (left)
                    playerController.lLeftContact = true;
                else if (right)
                    playerController.lRightContact = true;
                else if (up)
                    playerController.lUpContact = true;
                else if (down)
                    playerController.lDownContact = true;
            }
        }
        else if (col.gameObject.layer == 9 && !col.isTrigger) {
            if (rightPlayer) {
                if (left) {
                    playerController.pitLeft = true;
                }
                else if (right) {
                    playerController.pitRight =  true;
                }
                else if (up) {
                    playerController.pitUp = true;
                }
                else if (down) {
                    playerController.pitDown = true;
                }
            }
            else if (leftPlyer) {
                if (left) {
                    playerController.pitLeft = true;
                }
                else if (right) {
                    playerController.pitRight = true;
                }
                else if (up) {
                    playerController.pitUp = true;
                }
                else if (down) {
                    playerController.pitDown = true;
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col ) {
        if (col.gameObject.layer == 8 && !col.isTrigger) {
            if (rightPlayer) {
                if (left)
                    playerController.rLeftContact = true;
                else if (right)
                    playerController.rRightContact = true;
                else if (up)
                    playerController.rUpContact = true;
                else if (down)
                    playerController.rDownContact = true;
            }
            else if (leftPlyer) {
                if (left)
                    playerController.lLeftContact = true;
                else if (right)
                    playerController.lRightContact = true;
                else if (up)
                    playerController.lUpContact = true;
                else if (down)
                    playerController.lDownContact = true;
            }
        }
        else if (col.gameObject.layer == 9 && !col.isTrigger) {
            if (rightPlayer) {
                if (left) {
                    playerController.pitLeft = true;
                }
                else if (right) {
                    playerController.pitRight = true;
                }
                else if (up) {
                    playerController.pitUp = true;
                }
                else if (down) {
                    playerController.pitDown = true;
                }
            }
            else if (leftPlyer) {
                if (left) {
                    playerController.pitLeft = true;
                }
                else if (right) {
                    playerController.pitRight = true;
                }
                else if (up) {
                    playerController.pitUp = true;
                }
                else if (down) {
                    playerController.pitDown = true;
                }
            }
        }
    }

    void OnTriggerExit2D (Collider2D col){
        if (col.gameObject.layer == 8 && !col.isTrigger) {
            if (rightPlayer) {
                if (left)
                    playerController.rLeftContact = false;
                else if (right)
                    playerController.rRightContact = false;
                else if (up)
                    playerController.rUpContact = false;
                else if (down)
                    playerController.rDownContact = false;
            }
            else if (leftPlyer) {
                if (left)
                    playerController.lLeftContact = false;
                else if (right)
                    playerController.lRightContact = false;
                else if (up)
                    playerController.lUpContact = false;
                else if (down)
                    playerController.lDownContact = false;
            }
        }
        else if (col.gameObject.layer == 9 && !col.isTrigger) {
            if (rightPlayer) {
                if (left) {
                    playerController.pitLeft = false;
                }
                else if (right) {
                    playerController.pitRight = false;
                }
                else if (up) {
                    playerController.pitUp = false;
                }
                else if (down) {
                    playerController.pitDown = false;
                }
            }
            else if (leftPlyer) {
                if (left) {
                    playerController.pitLeft = false;
                }
                else if (right) {
                    playerController.pitRight = false;
                }
                else if (up) {
                    playerController.pitUp = false;
                }
                else if (down) {
                    playerController.pitDown = false;
                }
            }
        }
    }

    public void Clear() {
        if (rightPlayer) {
                if (left)
                    playerController.rLeftContact = false;
                else if (right)
                    playerController.rRightContact = false;
                else if (up)
                    playerController.rUpContact = false;
                else if (down)
                    playerController.rDownContact = false;

                if (left) {
                    playerController.pitLeft = false;
                }
                else if (right) {
                    playerController.pitRight = false;
                }
                else if (up) {
                    playerController.pitUp = false;
                }
                else if (down) {
                    playerController.pitDown = false;
                }

            }
            else if (leftPlyer) {
                if (left)
                    playerController.lLeftContact = false;
                else if (right)
                    playerController.lRightContact = false;
                else if (up)
                    playerController.lUpContact = false;
                else if (down)
                    playerController.lDownContact = false;

                if (left) {
                    playerController.pitLeft = false;
                }
                else if (right) {
                    playerController.pitRight = false;
                }
                else if (up) {
                    playerController.pitUp = false;
                }
                else if (down) {
                    playerController.pitDown = false;
                }
            }
    }
}
