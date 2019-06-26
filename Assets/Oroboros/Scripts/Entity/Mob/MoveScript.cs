using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public abstract class MoveScript : NetworkBehaviour
{
    public float height = 1;

    public float speed = 1;
    public float jumpStrength = 1;
    public float lookSensitivity = 1;

    public float xClampAngle = 90.0f;

    //Used for all actions to prevent overlapping/double actions
    private bool isPerformingAction = false;

    //Attacking Melee
    private bool isMainAttacking;
    private bool isSecondAttacking;
    private Vector3 attackPosStart;
    private Vector3 attackPosEnd;

    //hand anims
    public string default_left_hand_anim;
    public string default_right_hand_anim;

    private bool isUsingLHand = false;
    private bool isUsingRHand = false;

    private Camera cam;
    private Rigidbody rb;
    private Animator anim;
    
    //heads look target
    private HeadLookController hlc;
    public Transform lookTarget;

    public AnimationCurve jumpFallOff;

    private float xAxisClamp;

    private bool isJumping = false;

    //movement intputs
    private float verticalMovementInput;
    private float horizontalMovementInput;

    private float jumpInput;

    //look inputs
    private float verticalLookInput;
    private float horizontalLookInput;

    //hand inputs
    private float leftHandInput;
    private float rightHandInput;

    //body parts
    protected GameObject head;
    protected Transform LHand;
    protected Transform RHand;

    public Item RHandItem;
    public Item LHandItem;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hlc = GetComponent<HeadLookController>();
        head = transform.Find("Head").gameObject;
        LHand = Helper.FindComponentInChildWithTag<Transform>(gameObject, "Left Hand");
        RHand = Helper.FindComponentInChildWithTag<Transform>(gameObject, "Right Hand");
        xAxisClamp = 0.0f;

    }

    protected void Update()
    {
        if (!isLocalPlayer) return;
        movement();
        cameraRotation();
        handUpdate();

        if (isMainAttacking && !isPerformingAction && !anim.GetBool("IsPerformingAction"))
        {
            //Debug.Log("is main attacking");
            AnimationEventHandler_ActionChanging(1);
            anim.SetTrigger("HorSwing");
        }

        else if (isSecondAttacking && !isPerformingAction && !anim.GetBool("IsPerformingAction"))
        {
            //Debug.Log("is second attacking");
            AnimationEventHandler_ActionChanging(1);
            anim.SetTrigger("VertSwing");
        }
    }

    private void handUpdate()
    {
        anim.SetFloat("LeftHand", leftHandInput);
        anim.SetFloat("RightHand", rightHandInput);

        if(RHandItem as MeleeWeapon)
        {
            // trying to add in a command that will send the melee weapon the players rigidbody to ignore any interaction with it. 
            // this variable in question is OwnerRigidbody in MeleeWeapon
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            // using placeholder 'IsName("attacktypes") for the attacks
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("horizontal") && !isUsingRHand 
                    || anim.GetCurrentAnimatorStateInfo(0).IsName("vertical") && !isUsingRHand)
            {
                isUsingRHand = true;
                useRightHand();
            }
            else
            {
                isUsingRHand = false;
                stopUsingRightHand();
            }
        }
    }

    private void jump()
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

    private void movement()
    {
        float horizInput = horizontalMovementInput * speed * Time.deltaTime;
        float vertInput = verticalMovementInput * speed * Time.deltaTime;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        rb.MovePosition(transform.position + forwardMovement + rightMovement);
        anim.SetFloat("Speed", vertInput);
        anim.SetFloat("Direction", horizInput);
        jump();
    }

    private void cameraRotation()
    {
        float inputX = horizontalLookInput * lookSensitivity * Time.deltaTime;
        float inputY = verticalLookInput * lookSensitivity * Time.deltaTime;

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
    }

    public abstract void useRightHand();
    public abstract void useLeftHand();

    public abstract void stopUsingRightHand();
    public abstract void stopUsingLeftHand();

    public void setHorizontalMovementInput(float horizontalMovementInput)
    {
        this.horizontalMovementInput = horizontalMovementInput;
    }

    public void setVerticalMovementInput(float verticalMovementInput)
    {
        this.verticalMovementInput = verticalMovementInput;
    }

    public void setHorizontalLookInput(float horizontalLookInput)
    {
        this.horizontalLookInput = horizontalLookInput;
    }

    public void setVerticalLookInput(float verticalLookInput)
    {
        this.verticalLookInput = verticalLookInput;
    }

    public void setJumpingInput(float jumpInput)
    {
        this.jumpInput = jumpInput;
    }

    public void setLeftHandInput(float leftHandInput)
    {
        this.leftHandInput = leftHandInput;
    }

    public void setRightHandInput(float rightHandInput)
    {
        this.rightHandInput = rightHandInput;
    }

    public void AnimationEventHandler_ActionChanging(float value)
    {
        if (value == 0) // false
        {
            this.isPerformingAction = false;
            anim.SetBool("IsPerformingAction", false);
        }

        else if (value == 1) // true
        {
            this.isPerformingAction = true;
            anim.SetBool("IsPerformingAction", true);
        }
    }

    //Attacking
    public void setMainAttacking(bool value)
    {
        this.isMainAttacking = value;
    }

    //Attacking
    public void setSecondAttacking(bool value)
    {
        this.isSecondAttacking = value;
    }


}
