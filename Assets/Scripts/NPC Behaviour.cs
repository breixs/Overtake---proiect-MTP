using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public float speed;
    public bool hit;
    public static NPCBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!hit)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (transform.position.y < 0 || transform.position.z < -50)
                Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Npc"))
        {
            if (other.gameObject.CompareTag("Npc"))
            {
                if (!hit)
                {
                    //Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }

        if(gameObject.CompareTag("NpcIncoming"))
        {
            if (other.gameObject.CompareTag("NpcIncoming"))
            {
                if (!hit)
                {
                    //Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }

        var stop = new Vector3(0, 0, 0);

        if (gameObject.CompareTag("NpcBody") && other.gameObject.CompareTag("Player"))
        {
            transform.Translate(stop * 0 * Time.deltaTime);
            Debug.Log("Collision");
            hit = true;
        }

        if(other.gameObject.CompareTag("DistanceTrigger"))
        {
            Destroy(gameObject);
        }
     

    }

    public void Purge()
    {
        Destroy(gameObject);
    }
}
