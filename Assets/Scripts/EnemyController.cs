using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool backward;
    public float moveTime;
    public float speed;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += (backward ? -1 : 1)*speed*Time.deltaTime;
        transform.position = pos;
        timer += Time.deltaTime;
        if(timer > moveTime) {
            backward = !backward;
            timer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        GameObject gameObj = other.gameObject;
        if(gameObj.CompareTag("Player")) {
            gameObj.GetComponent<PlayerController>().ChangeHealth(-1);
        }
        else if(gameObj.CompareTag("Creature")) {
            Destroy(transform.gameObject);
        }
        
    }
}
