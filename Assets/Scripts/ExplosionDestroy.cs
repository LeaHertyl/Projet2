using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    [SerializeField] private float Timer;

    private void Update()
    {
        if(Timer > 0f)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
