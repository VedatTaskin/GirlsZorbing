using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;

public class HeliMove : MonoBehaviour
{
    public List<Collider> inHeli;
    [SerializeField] PlayableDirector director;
    private void OnTriggerEnter(Collider other)
    {
        inHeli.Add(other);
        other.transform.parent = transform;
        other.GetComponent<NavMChase>().enabled = false;
        other.GetComponent<NavMeshAgent>().enabled = false;
        other.transform.position = transform.position;
    }

    private void Update()
    {
        if(inHeli.Count == UIController.Instance.GetGirlCount()+1)
        {
            director.Play();
            UIController.Instance.WinMenu();
        }
    }
}
