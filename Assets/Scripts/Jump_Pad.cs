using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Pad : MonoBehaviour
{
    public Animator anim;
    
    public float jumpForce = 20;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            anim.SetTrigger("Launch");
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
        }
    }
}
