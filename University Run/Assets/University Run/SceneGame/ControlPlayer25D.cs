using mech.input;
using System.Collections;
using UnityEngine;
using System;

// velocity starting at 0 .. should be min speed

public class ControlPlayer25D : MonoBehaviour, IEventHandlerThreeButtonRunner
{
    private float speedMax = 4.5f;
    private float speedMin = 3.5f;
    private float accelSpeed = 0.2f;
    private float reduceSpeed = 0.9f;

    private float forceJump = 8;
    private float tJumpCooldown = 0.65f;

    // internal

    private bool isCooldownJump = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.A))
        {
            //if (Mathf.Abs(rb.velocity.x) < speedMin) rb.velocity = new Vector3(speedMin, rb.velocity.y, 0);
            rb.velocity = new Vector3(rb.velocity.x + accelSpeed, rb.velocity.y, 0);
            if (Mathf.Abs(rb.velocity.x) > speedMax) rb.velocity = new Vector3(speedMax, rb.velocity.y, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //if (Mathf.Abs(rb.velocity.x) < speedMin) rb.velocity = new Vector3(-speedMin, rb.velocity.y, 0);
            rb.velocity = new Vector3(rb.velocity.x - accelSpeed, rb.velocity.y, 0);
            if (Mathf.Abs(rb.velocity.x) > speedMax) rb.velocity = new Vector3(-speedMax, rb.velocity.y, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if(!isCooldownJump && !isInAir())
            {
                StartCoroutine(CoCooldownJump());
                rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            }
        }

        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            rb.velocity = new Vector3(rb.velocity.x * reduceSpeed, rb.velocity.y, 0);
            if (Mathf.Abs(rb.velocity.x) < 0.01f) rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if (isInAir())
        {
            rb.AddForce(-transform.up * 20, ForceMode.Force);
        }
    }

    private IEnumerator CoCooldownJump()
    {
        isCooldownJump = true;
        yield return new WaitForSeconds(tJumpCooldown);
        isCooldownJump = false;
    }

    private bool isInAir()
    {
        float heightPlayer = transform.localScale.y * 0.95f;
        Vector3 posGround = transform.position - new Vector3(0, heightPlayer, 0);
        return !Physics.Raycast(posGround, -transform.up, heightPlayer * 0.1f);
    }

    public void OnButtonPress(Vector3 vecMove, bool isJump)
    {
        Debug.Log("isjumping");
        //rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);

        rb.velocity += vecMove;

        if (isJump)
        {
            if (!isCooldownJump && !isInAir())
            {
                StartCoroutine(CoCooldownJump());
                rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            }
        }
    }
}