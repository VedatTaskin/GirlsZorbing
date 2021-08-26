using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMChase : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target");
    }

    private void Chase()
    {
        agent.SetDestination(target.transform.position);
        agent.speed = 20;
    }
    private void Update()
    {
        Chase();
    }

}
