using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoMovingWithAnimation : MonoBehaviour, MovingObject{
    [SerializeField] float moveSpeed = default;

    private Animator anim;
    private Rigidbody2D rb;

    void Start(){
        rb = this.transform.GetComponent<Rigidbody2D>();
        anim = this.transform.GetComponent<Animator>();
    }

    public void Move(Vector2 velocity){
        //rb.AddForce(velocity);
        rb.velocity = velocity;
    }

    public void DoAnimation(string animationName, bool state){
        anim.SetBool(animationName, state);
    }

    public void DoAnimation(string animationName, float state){
        anim.SetFloat(animationName, state);
    }

    public float GetMoveSpeed(){
        return this.moveSpeed;
    }
}