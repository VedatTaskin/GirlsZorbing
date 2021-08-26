using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeZorb : MonoBehaviour
{
    public float minForce;
    public float maxForce;
    public float radius;
    public float destroyDelay;

    private void Start()
    {
        Explode();
    }

    public void Explode()
    {
        foreach(Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(UnityEngine.Random.Range(minForce, maxForce), transform.position, radius);
            }
            Destroy(t.gameObject, destroyDelay);
        }
    }
}
