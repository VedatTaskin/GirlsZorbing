using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlPickUp : MonoBehaviour
{
    public GameObject heartPrefab;

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            UIController.Instance.SetGirlCount(1);
            GameObject hearts= Instantiate(heartPrefab,new Vector3(transform.position.x, transform.position.y+2,transform.position.z), Quaternion.identity) as GameObject;
            Destroy(hearts,2);
            Destroy(this.gameObject);            
        }
    }
}
