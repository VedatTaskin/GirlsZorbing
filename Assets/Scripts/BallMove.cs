using UnityEngine;
using Cinemachine;
using System.Collections;

public class BallMove : MonoBehaviour
{
   
    public bool beforeFinishLine = true;       
    public float forwardSpeed = 10f;
    [SerializeField]private float _limit = 3;
    private float _lastFrameFingerPositionX;//, kickCounter;
    private float _moveFactorX;
    public bool onClick = false;  // ekrana basýlýp basýlmadýðý kontrol ediliyor.
    public float swerveSpeed = 0.5f;
    public float maxSwerveAmount = 1f;
    private float etapUzunlugu;
    private float topBaslangicPozisyonu;

    [SerializeField]
    public float ballScale = 1;
    [SerializeField] CinemachineVirtualCamera camera1;
    [SerializeField]
    float ballBelowLimit = 1, ballUpLimit = 2;//, kickTime = 0.5f;
    [SerializeField]
    int ballUpCount = 40;

    float tempScale;

    [SerializeField]
    CharacterMovement chMovement;
    [SerializeField]
    CharacterAnimator chAnimator;

    [SerializeField]
    GameObject fracturedObject;

    private void Start()
    {
        tempScale = Mathf.Pow(ballScale, 3);
        topBaslangicPozisyonu = transform.position.z;
            
        //kickCounter = kickTime;
    }

    // Update is called once per frame
    void Update()
    {
        beforeFinishLine = GameObject.FindGameObjectWithTag("Finish").GetComponent<SpawnGirls>().beforeFinish;
        CalculateSwerveAmount();

        if (onClick /*&& beforeFinishLine*/)
        {
            //if (kickCounter > kickTime)
                MovePlayer();
           /* else
                kickCounter += Time.deltaTime;*/
        }
    }

    
    private void CalculateSwerveAmount()
    {

        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
            onClick = true;
        }

        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0;
            onClick = false;
        }

        if (beforeFinishLine)
        {
            UIController.Instance.TouchWarning(onClick);
            
        }        
        
    }

    void MovePlayer()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * _moveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        if (transform.position.x < -_limit)
        {
            transform.position = new Vector3(-_limit, transform.position.y, transform.position.z);
        }
        if (transform.position.x > _limit)
        {
            transform.position = new Vector3(_limit, transform.position.y, transform.position.z);
        }
        Vector3 vec = new Vector3(swerveAmount, 0, forwardSpeed * Time.deltaTime);
        transform.Translate(vec);
        SliderDegerGuncelle();        
    }


    void SliderDegerGuncelle()
    {
        //etaba negatif z deðerinden baþlandýðý için hesaplama yapýlýyor.
        float sifidanHelikoptereUzunluk = 180;
        etapUzunlugu = Mathf.Abs(topBaslangicPozisyonu) + sifidanHelikoptereUzunluk;
        float temp = Mathf.Abs(transform.position.z+ Mathf.Abs(topBaslangicPozisyonu)) / etapUzunlugu;
        UIController.Instance.SetSliderValue(temp);
    }

    internal void ChangeScale(int scale)
    {
        
        UIController.Instance.SetBallPickedUp(scale);

        float variable = scale * (Mathf.Pow(ballUpLimit, 3) - Mathf.Pow(ballBelowLimit, 3)) / ballUpCount;
        StartCoroutine(ChangeScaleAsync(variable));

        /*if(scale < 0)
        {
            kickCounter = 0;
        }*/

    }

    IEnumerator ChangeScaleAsync(float scale)
    {
        for(float i = 0; i < 10; i++)
        {
            tempScale += scale / 10f;
            ballScale = Mathf.Pow(tempScale, 1.0f / 3);

            if (ballScale < ballBelowLimit)
            {
                DestroyBall();
            }

            else if (ballScale > ballUpLimit)
                ballScale = ballUpLimit;

            transform.localScale = Vector3.one * ballScale;

            yield return new WaitForSeconds(0.02f);
        }
        //if (ballScale >= 1.6f) ChangeCameraDistance(scale);
    }

    private void ChangeCameraDistance(float multiplier)
    {
        var transposer = camera1.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset.y += (15 * multiplier);       
    }
    
    private void DestroyBall()
    {
        UIController.Instance.LooseMenu();        
        SpawnFracturedObject();
        chAnimator.DoRagdoll(true);
    }

    

    private void SpawnFracturedObject()
    {
        Vector3 rotationVector = new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        Quaternion rotation = Quaternion.Euler(rotationVector); // vedat ekledi topu farklý açýlarda instantiate etmek için
        Destroy(gameObject);
        Instantiate(fracturedObject, transform.position, rotation).GetComponent<ExplodeZorb>().Explode();
    }
}
