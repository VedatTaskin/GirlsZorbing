using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    float acceleration, deceleration, maximumRunVel, maximumSideVel;

    float velX, velZ;

    int velXHash, velZHash;

    bool isFinished;


    void Start()
    {
        velXHash = Animator.StringToHash("VelX");
        velZHash = Animator.StringToHash("VelZ");

        velX = 0;
        velZ = 0;

        anim = GetComponent<Animator>();

        isFinished = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || isFinished)
            BlendMovement(maximumRunVel);
        else
            BlendMovement(0);
    }

    void BlendMovement(float speed)
    {
        /*bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);

        VerticalMovement(forwardPressed);
        HorizontalMovement(rightPressed, leftPressed);*/

        anim.SetFloat(velZHash, speed);
        anim.SetFloat(velXHash, 0);

        //anim.SetFloat(velZHash, velZ);
        //anim.SetFloat(velXHash, velX);
        /*
        Vector3 locVel = Vector3.zero;
        locVel.x = velX;
        locVel.z = velZ;
        locVel.y = -9.8f;
        locVel *= Time.deltaTime * speed;
        controller.Move(transform.TransformDirection(locVel));*/
    }

    private void VerticalMovement(bool forwardPressed)
    {
        if (forwardPressed && velZ < maximumRunVel)
            velZ += Time.deltaTime * acceleration;
        if (!forwardPressed)
        {
            if (velZ < 0.1f)
                velZ = 0;
            else
                velZ -= Time.deltaTime * deceleration;
        }

        if (velZ > maximumRunVel)
            velZ = maximumRunVel;
    }
    private void HorizontalMovement(bool rightPressed, bool leftPressed)
    {
        if (rightPressed && velX < maximumSideVel)
            velX += Time.deltaTime * acceleration;
        else if (leftPressed && velX > -maximumSideVel)
            velX -= Time.deltaTime * acceleration;
        if (!rightPressed && !leftPressed)
        {
            if (velX > -0.1f && velX < 0.1f)
                velX = 0;
            else if (velX < 0)
                velX += Time.deltaTime * deceleration;
            else
                velX -= Time.deltaTime * deceleration;
        }

        if (velX > maximumSideVel)
            velX = maximumSideVel;
        else if (velX < -maximumSideVel)
            velX = -maximumSideVel;
    }

    internal void SetIsFinished(bool isFinished)
    {
        this.isFinished = isFinished;
    }
}
