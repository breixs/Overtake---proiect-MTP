using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerMove : MonoBehaviour
{
    public CarController carController;
    // Update is called once per frame
    void Update()
    {
        if (!carController.hit)
        {
            transform.position += new Vector3(0, 0, 4) * Time.deltaTime;
        }
        //else
            //Debug.Log("destroyer stop");
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.CompareTag("Road"))
            Destroy(other.gameObject);
    }
}
