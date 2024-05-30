using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private float duration;

    public void LookAt(Transform target)
    {
        if (target == null)
        {
            return;
        }
        else
        {
            transform.DOLookAt(target.position, duration);
            Camera.main.fieldOfView = 25;
        }
    }

    public void GoBack(Transform target)
    {
        if (target == null)
        {
            return;
        }
        else
        {
            transform.DOLookAt(target.position, duration);
            Camera.main.fieldOfView = 50;
        }
    }
}
