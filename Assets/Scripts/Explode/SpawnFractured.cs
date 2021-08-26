using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFractured : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnFracturedObject();
        }
    }

    private void SpawnFracturedObject()
    {
        Destroy(originalObject);
        GameObject fractObj = Instantiate(fracturedObject) as GameObject;
        fractObj.GetComponent<ExplodeZorb>().Explode();
    }
}
