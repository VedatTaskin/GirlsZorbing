using System.Collections;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Collider[] AllColliders;

    bool isWorkingAboutRagdoll;

    Collider mainCollider;

    void Start()
    {
        isWorkingAboutRagdoll = false;
        mainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        DoRagdollOnce(false);
    }

    private void DoRagdollOnce(bool isRagdoll)
    {
        foreach (var col in AllColliders)
            col.enabled = isRagdoll;
        isWorkingAboutRagdoll = false;
        mainCollider.enabled = true;
    }

    public void DoRagdoll(bool isRagdoll)
    {
        if (!isWorkingAboutRagdoll)
        {
            isWorkingAboutRagdoll = true;
            StartCoroutine(DoRagdollAsync(isRagdoll));
        }
    }

    IEnumerator DoRagdollAsync(bool isRagdoll)
    {
        foreach (var col in AllColliders)
        {
            yield return new WaitForSeconds(0.01f);
            col.enabled = isRagdoll;
        }
        isWorkingAboutRagdoll = false;
        GetComponent<Animator>().enabled = !isRagdoll;
    }
}
