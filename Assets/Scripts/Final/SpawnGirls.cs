using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;

public class SpawnGirls : MonoBehaviour
{
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] float delayTime;
    [SerializeField] PlayableDirector director;
    [SerializeField] GameObject confettiPrefab;
    Vector3 randomPos;
    GameObject player;
    [HideInInspector] public bool beforeFinish =true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Invoke(nameof(GirlsSpawn), delayTime);
            director.Play();
            ActiveNavMesh();
            beforeFinish = false;
            player.GetComponent<CharacterMovement>().SetIsFinished(true);
            Invoke(nameof(AddCollider), 3);
            confettiPrefab.SetActive(true);
        }
    }

    private void AddCollider()
    {
       player.AddComponent<CapsuleCollider>();
    }

    private void GirlsSpawn()
    {
        int girlCount = UIController.Instance.GetGirlCount();
        for (int i = 0; i < girlCount; i++)
        {
            randomPos = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(transform.position.z - 3, transform.position.z));
            GameObject spawnGirl = Instantiate(spawnPrefab, randomPos, Quaternion.identity);
            AddNavmeshSystem(spawnGirl);
        }
    }

    IEnumerator WinMenu()
    {

        yield return new WaitForSeconds(3);
        UIController.Instance.WinMenu();
    }

    private void ActiveNavMesh()
    {
        player.GetComponent<NavMChase>().enabled = true;
        player.GetComponent<NavMeshAgent>().enabled = true;
    }

    private void AddNavmeshSystem(GameObject gameObject)
    {
        gameObject.AddComponent<NavMeshAgent>();
        gameObject.AddComponent<NavMChase>();
        gameObject.AddComponent<Animator>();
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("SpawnerWoman");
        gameObject.AddComponent<CapsuleCollider>();
    }

}
