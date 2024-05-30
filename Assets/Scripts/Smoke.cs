using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    private ParticleSystem ps;

    public void Start()
    {
        ps=GetComponent<ParticleSystem>();
    }

    public void Setup()
    {
        gameObject.SetActive(true);
     
    }

    public void ExhaustStart()
    {
        var emission = ps.emission;
        emission.enabled = true;
    }

    public void ExhaustStop()
    {
        //gameObject.GetComponent<ParticleSystem>().enableEmission = true;
        //gameObject.SetActive(false);
        var emission = ps.emission;
        emission.enabled = false;
    }
}
