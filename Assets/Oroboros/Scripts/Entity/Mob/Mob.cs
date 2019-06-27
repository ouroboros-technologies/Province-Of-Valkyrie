using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(HeadLookController))]
public class Mob : NetworkBehaviour
{

    public bool mobCanJump;

    //movement variables
    public float speed;
    protected float verticalLookInput;
    protected float horizontalLookInput;
    protected float verticalMovementInput;
    protected float horizontalMovementInput;

    //jump variables
    protected bool isJumping;
    protected float jumpInput;
    public float jumpStrength = 1;

    //look stuff
    public GameObject head;
    public Transform lookTarget;
    public float lookSensitivity;
    protected HeadLookController hlc;

    //Camera
    public Camera cam;

    //look direction variables
    Quaternion lookDirection;
    Vector3 lookAxis;

    public float xClampAngle = 90.0f;
    protected float xAxisClamp;

    //components
    protected Rigidbody rb;
    protected Animator anim;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hlc = GetComponent<HeadLookController>();

        lookAxis = new Vector3(horizontalLookInput, verticalLookInput, 0);
    }

    public void Update()
    {
        if(!isLocalPlayer) return;
        Move();
        Look();
    }

    //move the mob based on the move inputs
    private void Move()
    {
        float horizInput = horizontalMovementInput * speed * Time.deltaTime;
        float vertInput = verticalMovementInput * speed * Time.deltaTime;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        rb.MovePosition(transform.position + forwardMovement + rightMovement);
        anim.SetFloat("Speed", vertInput);
        anim.SetFloat("Direction", horizInput);
        Jump();
    }

    //if the mob can jump, jump if input is greater then 0
    private void Jump()
    {
        if(jumpInput > 0 && !isJumping)
        {
            isJumping = true;
            rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        }
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            isJumping = false;
        }
    }

    //rotate the head based on the look inputs
    private void Look()
    {
        float inputX = horizontalLookInput * lookSensitivity * Time.deltaTime;
        float inputY = verticalLookInput * lookSensitivity * Time.deltaTime;

        lookAxis = new Vector3(horizontalLookInput, verticalLookInput, 0);
        lookAxis.Normalize();

        xAxisClamp += inputY;

        if(xAxisClamp > xClampAngle)
        {
            xAxisClamp = xClampAngle;
            inputY = 0.0f;
        }
        else if (xAxisClamp < -xClampAngle)
        {
            xAxisClamp = -xClampAngle;
            inputY = 0.0f;
        }

        head.transform.Rotate(Vector3.right * inputY);
        transform.Rotate(Vector3.up * inputX);
        hlc.target = lookTarget.TransformPoint(0, 0, 0);

        if (float.IsNaN(Mathf.Atan(lookAxis.y / lookAxis.x)) || lookAxis.magnitude < 0.9f)
        {
            lookDirection = Quaternion.identity;
        }
        else
        {
            Vector3 rotation = new Vector3(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(lookAxis.y, lookAxis.x)) - 270);
            lookDirection = Quaternion.Euler(rotation);
        }
    }

    public void SetVerticalLookInput(float input)
    {
        verticalLookInput = input;
    }

    public void SetHorizontalLookInput(float input)
    {
        horizontalLookInput = input;
    }

    public void SetVerticalMovementInput(float input)
    {
        verticalMovementInput = input;
    }

    public void SetHorizontalMovementInput(float input)
    {
        horizontalMovementInput = input;
    }

    public void SetJumpInput(float input)
    {
        jumpInput = input;
    }

}
