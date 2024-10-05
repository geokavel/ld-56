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
    public float jumpForce;
    public float speed;
    Rigidbody2D rigidbody2d;
    float inputX;
    float inputY;
    bool jumping = false;
    // Start is called before the first frame update
    void Start()
    {
      rigidbody2d = GetComponent<Rigidbody2D>();
      MoveAction.Enable();  
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = MoveAction.ReadValue<Vector2>();
        inputX = input.x;
        inputY = input.y;
    }

    void FixedUpdate() {
        Vector2 pos = rigidbody2d.position;
        if(!Mathf.Approximately(inputX,0)) {
            //pos.x += inputX * 0.1f;
            //rigidbody2d.MovePosition(pos);
            rigidbody2d.velocity = new Vector2(speed*Math.Sign(inputX),rigidbody2d.velocity.y);
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
