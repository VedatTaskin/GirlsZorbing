using UnityEngine;

public class EngelControl : MonoBehaviour
{
    [SerializeField]
    int scaleChanger =-1;
    [SerializeField]
    float kickForce = 5;

    [SerializeField]
    GameObject ballPref;

    bool isHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !isHit)
        {
            isHit = true;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            Vector3 way = (other.transform.position - transform.position + Vector3.up).normalized;
            rb.AddForce(way * kickForce * rb.mass, ForceMode.Impulse);

            CreateSomeBall(other.transform, way);

            other.GetComponent<BallMove>().ChangeScale(scaleChanger);
            CinemachineShake.instance.ShakeCamera(1.5f, 0.4f);
        }
    }

    private void CreateSomeBall(Transform tr, Vector3 way)
    {
        GameObject particleBall = Instantiate(ballPref, tr.position + Vector3.up * 5, tr.rotation);
        particleBall.transform.localScale *= 0.4f;

        way = (way + Vector3.down * 2).normalized;

        //particleBall.GetComponent<Rigidbody>().AddForce(way * kickForce * 2, ForceMode.Impulse);
        //particleBall.GetComponent<Collider>().enabled = false;
        //particleBall.GetComponent<SmallBall>().enabled = false;
        Destroy(particleBall, 1f);
    }
}
