using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public InputAction SwitchAction;
    public float jumpForce;
    public float speed;
    public float maxSpeed;
    Rigidbody2D rigidbody2d;
    float inputX;
    float inputY;
    bool jumping = false;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
      rigidbody2d = GetComponent<Rigidbody2D>();
      sprite = GetComponent<SpriteRenderer>();
      MoveAction.Enable();  
      //SwitchAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = MoveAction.ReadValue<Vector2>();
        inputX = input.x;
        inputY = input.y;
        Debug.Log(SwitchAction.ReadValue<char>());
    }

    void FixedUpdate() {
        Vector2 pos = rigidbody2d.position;
        Vector2 vel = rigidbody2d.velocity;
        if(!Mathf.Approximately(inputX,0)) {
            int dirInputX = Math.Sign(inputX);
            sprite.flipX = dirInputX == 1;
            //int dirVelX = Math.Sign(vel.x);
            //if(Mathf.Approximately(vel.x,0) || dirInputX != dirVelX) {
            if(Math.Abs(vel.x) < maxSpeed) {
                rigidbody2d.AddForce( new Vector2(speed * dirInputX,0));
            }
               
            //}
            //pos.x += inputX * 0.1f;
            //rigidbody2d.MovePosition(pos);
            //rigidbody2d.velocity = new Vector2(speed*,rigidbody2d.velocity.y);
        }
        
        
        if(!jumping && !Mathf.Approximately(inputY,0)) {
            rigidbody2d.AddForce(new Vector2(0,jumpForce));
            jumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<Tilemap>() != null) {
            jumping = false;
        }
    }
}
