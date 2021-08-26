using UnityEngine;

public class ManControl : MonoBehaviour
{

    public GameObject elmasPatlamaEffecti;
    public GameObject elmasToplamaObjesi;
    public int scaleChangeCount = -1;

    bool isHit = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball" && !isHit)
        {
            isHit = true;
            elmasToplamaObjesi.GetComponent<ElmasControl>().ElmasTopla(Camera.main.WorldToScreenPoint(transform.position),1);            
            Destroy(Instantiate(elmasPatlamaEffecti,
                new Vector3(transform.position.x, transform.position.y + 6, transform.position.z), Quaternion.identity), 0.3f);
            GetComponent<EnemyAnimator>().DoRagdoll(true);
            other.gameObject.GetComponent<BallMove>().ChangeScale(scaleChangeCount);
            Destroy(this.gameObject, 1);
        }
    }

}
