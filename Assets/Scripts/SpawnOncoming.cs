using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOncoming : MonoBehaviour
{
    public List<GameObject> npcs;

    private int GetRandom()
    {
        int rnd = Random.Range(0, npcs.Count);
        return rnd;
    }

    void Start()
    {
        var rot = new Vector3(0, 180, 0);
        npcs = new List<GameObject>(Resources.LoadAll<GameObject>("CarPrefabs/OncomingPrefabs"));
        int rndIndex = GetRandom();
        Instantiate(npcs[rndIndex], transform.position, Quaternion.Euler(rot));
        //Instantiate(npc, transform.position, Quaternion.identity);
    }
}
