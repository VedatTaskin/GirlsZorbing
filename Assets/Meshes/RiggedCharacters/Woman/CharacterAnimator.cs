using System.Collections;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    //Collider MainCollider;
    Collider[] AllColliders;

    bool isWorkingAboutRagdoll;

    // Use this for initialization
    void Awake()
    {
        isWorkingAboutRagdoll = false;
        //MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        DoRagdollOnce(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DoRagdoll(true);
    }

    public void DoRagdoll(bool isRagdoll)
    {
        if (!isWorkingAboutRagdoll)
        {
            isWorkingAboutRagdoll = true;
            GetComponent<PlayerChase>().enabled = !isRagdoll;
            StartCoroutine(DoRagdollAsync(isRagdoll));
        }
    }

    private void DoRagdollOnce(bool isRagdoll)
    {
        foreach (var col in AllColliders)
            col.enabled = isRagdoll;
        isWorkingAboutRagdoll = false;
        //MainCollider.enabled = !isRagdoll;
        //GetComponent<Rigidbody>().useGravity = !isRagdoll;
        //GetComponent<Animator>().enabled = !isRagdoll;
    }

    IEnumerator DoRagdollAsync(bool isRagdoll)
    {
        foreach (var col in AllColliders)
        {
            yield return new WaitForSeconds(0.01f);
            col.enabled = isRagdoll;
        }
        isWorkingAboutRagdoll = false;
        //MainCollider.enabled = !isRagdoll;
        //GetComponent<Rigidbody>().useGravity = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
    }
}
