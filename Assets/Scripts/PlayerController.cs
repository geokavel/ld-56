using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject spawnPoint; 
    public InputAction MoveAction;
    public InputAction ShootAction;
    public int maxHealth;
    int health;
    public float jumpForce;
    public float speed;
    public float maxSpeed;
    public float shootForceX;
    public float shootForceY;
    public float shootTorque;
    Rigidbody2D rigidbody2d;
    float inputX;
    float inputY;
    bool jumping = false;
    SpriteRenderer sprite;
    int nextScene = -1;
    int curScene = 1;
    public int creatureCount = 0;
    int dirInputX;
    Transform creatures;
    // Start is called before the first frame update
    Animator anim;
    void Awake() {
        instance = this;
    }
    void Start()
    {
      health = maxHealth;
      rigidbody2d = GetComponent<Rigidbody2D>();
      sprite = GetComponent<SpriteRenderer>();
      anim = GetComponent<Animator>();
      MoveAction.Enable();
      ShootAction.Enable();
      ShootAction.performed += Shoot;
      creatures = transform.Find("Creatures");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = MoveAction.ReadValue<Vector2>();
        inputX = input.x;
        inputY = input.y;
        if(Keyboard.current.digit7Key.isPressed) nextScene = 1;
        else if(Keyboard.current.digit8Key.isPressed) nextScene = 2;
        else if(Keyboard.current.digit9Key.isPressed) nextScene = 3;
        else if(Keyboard.current.digit0Key.isPressed) nextScene = 4; 
    }

    void FixedUpdate() {
        Vector2 pos = rigidbody2d.position;
        if(nextScene != -1) {
            float y = float.NaN;
            if(nextScene==1) {
                y = -1;
                curScene = 1;
            } 
            else if(nextScene==2) {
                y = 13;
                curScene = 2;
            }
            else if(nextScene==3) {
                y = 26;
                curScene = 3;
            }
            else if(nextScene==4) {
                y = 39;
                curScene = 4;
            } 
            if(!float.IsNaN(y)) {
                transform.position = new Vector2(pos.x,y);
                //rigidbody2d.MovePosition(new Vector2(pos.x,y));
            }
            
        }
        nextScene = -1;
        
        Vector2 vel = rigidbody2d.velocity;
        anim.SetFloat("Velocity",Mathf.Sign(vel.x)*vel.magnitude);
        if(!Mathf.Approximately(inputX,0)) {
            dirInputX = Math.Sign(inputX);
            //sprite.flipX = dirInputX == 1;
            if(dirInputX == 1) {
                //transform.Rotate(0,180,0);
                //transform.rotation = Quaternion.Euler(0,180,0); 
            }
            anim.SetBool("Flip",dirInputX == 1);
            if(creatures != null) {
                float creaturesX = creatures.transform.localPosition.x;
                creatures.transform.localPosition = new Vector2(-dirInputX*Mathf.Abs(creaturesX),0);
                creatures.transform.rotation = Quaternion.Euler(new Vector3(0,dirInputX==-1 ?180 : 0,0)); 
            }
            //int dirVelX = Math.Sign(vel.x);
            //if(Mathf.Approximately(vel.x,0) || dirInputX != dirVelX) {
            if(Math.Abs(vel.x) < maxSpeed) {
                rigidbody2d.AddForce( new Vector2(speed * dirInputX,0));
            }
            else{
                //anim.Play("Idle");
            }
               
            //}
            //pos.x += inputX * 0.1f;
            //rigidbody2d.MovePosition(pos);
            //rigidbody2d.velocity = new Vector2(speed*,rigidbody2d.velocity.y);
        }
        
        
        if(!jumping && !Mathf.Approximately(inputY,0) && inputY > 0) {
            rigidbody2d.AddForce(new Vector2(0,jumpForce));
            jumping = true;
        }
    }

    public void ChangeHealth(int amount) {
        health += amount;
        Debug.Log("Health= "+health);
    }

    void OnCollisionEnter2D(Collision2D other) {
        //if(other.gameObject.GetComponent<Tilemap>() != null) {
            jumping = false;
        //}
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject);
        if(other.CompareTag("Creature")) {
            creatureCount++;
            GameObject creature = other.gameObject;
            Debug.Log($"Collected {creature.name} ({creatureCount}/4)");
            creature.GetComponent<Collider2D>().isTrigger = false;
            creature.GetComponent<Rigidbody2D>().simulated = false;
            creature.transform.SetParent(creatures);
            float creaturesX = creatures.transform.localPosition.x;
            creature.transform.localPosition = new Vector2(Mathf.Abs(creaturesX)-creatureCount,0);
            creature.transform.rotation = Quaternion.Euler(0,dirInputX==-1 ? 180: 0,0);
        }
    }

    void Shoot(InputAction.CallbackContext c) {
        if(creatureCount > 0) {
            Transform firstCreature = creatures.GetChild(0);
            firstCreature.GetComponent<CreatureController>()
            .Shoot(new Vector2(dirInputX*shootForceX,shootForceY),
            -shootTorque*dirInputX);
            Vector2 pos = creatures.transform.position;
            pos.x += 1*dirInputX;
            creatures.transform.position = pos;
            creatureCount--;
        }
       
    }
}
