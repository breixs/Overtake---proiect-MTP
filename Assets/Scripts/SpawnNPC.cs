using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    //public GameObject npc;
    public List<GameObject> npcs;

    private int GetRandom()
    {
        int rnd = Random.Range(0, npcs.Count);
        return rnd;
    }

    void Start()
    {
        npcs= new List<GameObject>(Resources.LoadAll<GameObject>("CarPrefabs/NormalPrefabs"));
        int rndIndex=GetRandom();
        Instantiate(npcs[rndIndex], transform.position, Quaternion.identity);
        //Instantiate(npc, transform.position, Quaternion.identity);
    }
}
