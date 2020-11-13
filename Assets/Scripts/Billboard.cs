using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform OtherPlayercamera;

    void LateUpdate()
    {
        transform.LookAt(transform.position + OtherPlayercamera.forward);
    }
}
