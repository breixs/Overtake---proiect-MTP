using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oncomingBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject npcCar;

    void Update()
    {
        //StartCoroutine(ExampleCoroutine());
    
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (npcCar.transform.position.y < 0 || npcCar.transform.position.z < -10)
                Destroy(npcCar);
      
    }

  
}
