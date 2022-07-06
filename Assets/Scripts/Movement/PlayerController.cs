using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoMovingWithAnimation{
    [SerializeField] GameObject interaction = default;
    [SerializeField] Transform player = default;

    [SerializeField] float timeToIdle;
    private float timeNotMoving;

    private bool waitAction = false;

    private void FixedUpdate(){
        if(waitAction) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        float speedX = 0, speedY = 0;

        if(moveY != 0){
            //Vertical
            DoAnimation("Horizontal", false);
            DoAnimation("Vertical", true);

            if(moveY < 0){
                player.eulerAngles = new Vector3(180, 0, 0);
            }else{
                player.eulerAngles = new Vector3(0, 0, 0);
            }

            speedY = GetMoveSpeed() * moveY;
        }

        if(moveX != 0){
            //Horizontal
            if(speedY == 0){
                DoAnimation("Vertical", false);
                DoAnimation("Horizontal", true);

                //Probably could be changed
                if(moveX < 0){
                    player.eulerAngles = new Vector3(0, 180, 0);
                }else{
                    player.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            speedX = GetMoveSpeed() * moveX;
        }

        ChangeAnimationIdle(moveX, moveY);
        base.Move(new Vector2(speedX, speedY));
    }

    private void ChangeAnimationIdle(float first, float second){
        DoAnimation("NotMoving", timeNotMoving);

        int notMovement = Convert.ToInt32(!((first != 0) || (second != 0)));

        if(timeNotMoving >= timeToIdle && notMovement == 1){
            return;
        }

        timeNotMoving = (timeNotMoving + Time.fixedDeltaTime) * notMovement;
    }

    public void ShowInteraction(){
        interaction.SetActive(true);
    }

    public void UnShowInteraction(){
        interaction.SetActive(false);
    }
    
    public void ChangeStateWait(bool state){
        waitAction = state;
    }
}
