using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public List<GameObject> roadList;
    private ulong nr = 300;
    private int i = 0;

    private void Start()
    {
        roadList = new List<GameObject>(Resources.LoadAll<GameObject>("RoadPrefabs"));
        int rnd = GetRandom();
        Instantiate(roadList[rnd], new Vector3(0,0.4f, 100), Quaternion.Euler(new Vector3(0,90,0)));
    }

    private int GetRandom()
    {
        int rnd = Random.Range(0, roadList.Count - 1);
        return rnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        var rot=new Vector3 (0, 90, 0);
       
        if (other.gameObject.CompareTag("Trigger"))
        {
            var pos = new Vector3(0, 0.4f, nr);
            //Debug.Log(roadList[0] + " " + roadList[1]);
            int rndIndex = Random.Range(0,roadList.Count-1);
            //Debug.Log(rndIndex);
            if(i%2!=0)
                Instantiate(roadList[rndIndex], pos, Quaternion.Euler(rot));
            else
                Instantiate(roadList[2], pos, Quaternion.Euler(rot));
            nr = nr+100;
            i++;
        }

       
    }
}
