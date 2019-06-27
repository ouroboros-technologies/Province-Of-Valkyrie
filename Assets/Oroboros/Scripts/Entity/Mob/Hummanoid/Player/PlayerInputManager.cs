using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Player))]
public class PlayerInputManager : NetworkBehaviour
{

    public string verticalLookInput = "Mouse Y";
    public string verticalMoveInput = "Vertical";
    public string horizontalLookInput = "Mouse X";
    public string horizontalMoveInput = "Horizontal";
    public string jumpInput = "Jump";
    public string rightHandInput = "Fire2";
    public string leftHandInput = "Fire1";

    //Attack Actions
    public string mainAttack = "mainAttack";
    public string secondAttack = "secondAttack";


    private Player player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //cursor not locked set visible to flase
        Cursor.visible = Cursor.lockState != CursorLockMode.Locked;
        player = GetComponent<Player>();
    }


    void Update()
    {
        if (!isLocalPlayer) return;

        player.SetVerticalMovementInput(Input.GetAxisRaw(verticalMoveInput));
        player.SetHorizontalMovementInput(Input.GetAxisRaw(horizontalMoveInput));
        player.SetVerticalLookInput(-Input.GetAxis(verticalLookInput));
        player.SetHorizontalLookInput(Input.GetAxis(horizontalLookInput));
        player.SetJumpInput(Input.GetAxisRaw(jumpInput));

        player.SetRightHandInput(Input.GetAxis(rightHandInput));
        player.SetLeftHandInput(Input.GetAxis(leftHandInput));

        //Attack Actions
        // TODO: If the attack button is held down it prevents the animation from changing back to idle after attacking.
        //moveScript.SetMainAttacking(Input.GetButton(mainAttack));
        //moveScript.SetSecondAttacking(Input.GetButton(secondAttack));
    }
}
