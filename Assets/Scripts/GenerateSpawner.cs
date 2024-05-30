using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpawner : MonoBehaviour
{
    public GameObject going;
    public GameObject oncoming;
    public GameObject player;

    private float sizeX=1.1f;
    private float sizeZ=4000f;
    private float offsetX=3f;
    private float offsetZ;
    void Start()
    {
        Vector3 pos;
        //var rotation = new Vector3(-180, 0, -180);
        for(int x=0;x<sizeX;x++)
            for(int z=1;z<sizeZ;z++)
            {
                offsetZ = Random.Range(45, 70);
                if (x==0)
                {
                    if (z == 1)
                    {
                        pos = new Vector3(471f + ((x * offsetX)), 1, z * 20);
                        GameObject s = Instantiate(oncoming, pos, Quaternion.identity) as GameObject;
                        s.transform.SetParent(this.transform);
                    }
                    else
                    {
                        pos = new Vector3(471f + ((x * offsetX)), 1, z * offsetZ*2);
                        GameObject s = Instantiate(oncoming, pos, Quaternion.identity) as GameObject;
                        s.transform.SetParent(this.transform);
                    }
                }
                else
                {
                    if (z == 1)
                    {
                        pos = new Vector3(471f + ((x * offsetX)), 1, z * 10);
                        GameObject s = Instantiate(going, pos, Quaternion.identity) as GameObject;
                        s.transform.SetParent(this.transform);
                    }
                    else
                    {
                        pos = new Vector3(471f + ((x * offsetX)), 1, z * offsetZ);
                        GameObject s = Instantiate(going, pos, Quaternion.identity) as GameObject;
                        s.transform.SetParent(this.transform);
                    }
                }
            }
    }

}
