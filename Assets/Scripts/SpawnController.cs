using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      PlayerController.instance.spawnPoint = transform.gameObject;  
    }

  
}
