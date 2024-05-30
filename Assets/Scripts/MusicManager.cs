using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void Stop()
    {
        gameObject.SetActive(false);
    }
}
