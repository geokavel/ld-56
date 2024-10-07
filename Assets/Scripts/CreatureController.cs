using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{   
    public GameObject prefab;
    bool flying = false;
    float flyTime = 5.0f;
    float timer=0;
    Rigidbody2D creatureRB;
    TrailRenderer trailRenderer;
    // Start is called before the first frame update

    void Start()
    {
        creatureRB = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flying) {
            timer += Time.deltaTime;
        
            if(timer >= flyTime) {
                flying = false;
                timer = 0;
                trailRenderer.emitting = false;
                //creatureRB.isKinematic = false;
                creatureRB.angularVelocity = 0;
                creatureRB.velocity = new Vector2(0,0);
                creatureRB.rotation = 0;
                creatureRB.gravityScale = 0.0f;
                //transform.position = PlayerController.instance.spawnPoint.transform.position;
                GetComponent<BoxCollider2D>().isTrigger = true;
                Debug.Log($"{transform.gameObject.name} Respawn");
                Instantiate(prefab,PlayerController.instance.spawnPoint.transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
        }
        
    }
    public void Shoot(Vector2 force, float torque) {
        transform.gameObject.layer = 8;
        transform.localPosition = new Vector2(0,0.5f);
        transform.SetParent(transform.parent.parent.parent);
        creatureRB.simulated = true;
        //creatureRB.bodyType=RigidbodyType2D.Dynamic;
        //creatureRB.isKinematic = true;
        creatureRB.gravityScale=1.0f;
        creatureRB.AddForce(force);
        creatureRB.AddTorque(torque);
        trailRenderer.emitting = true;
        flying = true;
    }
}
