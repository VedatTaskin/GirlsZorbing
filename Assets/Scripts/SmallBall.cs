using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallBall : MonoBehaviour
{
    [SerializeField]
    int scaleChanger = 1;
    public GameObject effect;  
    public MeshRenderer meshRenderer;

    private void Start()
    {
        InvokeRepeating(nameof(Shake), 1, 1.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<BallMove>().ChangeScale(scaleChanger);
            meshRenderer.enabled = false;

            GameObject effectObject = Instantiate(effect,
                new Vector3(other.transform.position.x, other.transform.position.y + 4, other.transform.position.z), Quaternion.identity) as GameObject;

            UIController.Instance.ShowPlusOne(new Vector3(other.transform.position.x+2, other.transform.position.y + 8, other.transform.position.z));
            Destroy(effectObject, 1);
            Destroy(this.gameObject);
        }
    }

    private void Shake()
    {
        transform.DOShakeScale(1.5f, 1, 1, 10, true);
    }
}
