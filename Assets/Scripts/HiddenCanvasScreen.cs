using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenCanvasScreen : MonoBehaviour
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
