using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChase : MonoBehaviour
{
    GameObject ball;
    Vector3 starter;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        starter = ball.transform.position - transform.position;
    }

    private void Update()
    {        
        if (ball.activeInHierarchy )
        {
            ChaseBall();
        }      
    }

    private void ChaseBall()
    {
            transform.position = ball.transform.position - starter;
    }
}
