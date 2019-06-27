using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum HumanoidState
{
    Idle, Combat, None
}

[RequireComponent(typeof(Stats))]
public class Humanoid : Mob
{

    //hand items
    public Hand leftHand;
    public Hand rightHand;

    //hand inputs
    protected float leftHandInput;
    protected float rightHandInput;

    private Stats stats;
    private HumanoidState state = HumanoidState.Idle;

    private List<Item> equipedItems;

    new protected void Start()
    {
        base.Start();
        stats = GetComponent<Stats>();

        if(leftHand.GetHeldItem()) leftHand.GetHeldItem().SetOwner(rb);
        if(rightHand.GetHeldItem()) rightHand.GetHeldItem().SetOwner(rb);
    }

    new protected void Update()
    {
        if(!isLocalPlayer) return;
        base.Update();
        UseHands();
        anim.SetTrigger("HorSwing");
    }

    private void UseHands()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            leftHand.StopUsingHand();
            rightHand.StopUsingHand();
        }

        if(leftHandInput > 0)
        {
            if(leftHand.GetItemType() == ItemType.MELEE && state == HumanoidState.Combat)
            {
                anim.SetBool("IsPerformingAction", true);
            }
            else if(leftHand.GetItemType() == ItemType.MELEE && state == HumanoidState.Idle)
            {
                state = HumanoidState.Combat;
            }
            leftHand.UseHand();
        }
        else
        {
            anim.SetBool("IsPerformingAction", false);
        }

        if(rightHandInput > 0)
        {
            if(rightHand.GetItemType() == ItemType.MELEE && state == HumanoidState.Combat)
            {
                anim.SetBool("IsPerformingAction", true);
            }
            else if(rightHand.GetItemType() == ItemType.MELEE && state == HumanoidState.Idle)
            {
                state = HumanoidState.Combat;
            }

            rightHand.UseHand();
        }
        else
        {
            anim.SetBool("IsPerformingAction", false);
        }
    }

    public Stats GetStats()
    {
        return stats;
    }

    public void SetLeftHandInput(float input)
    {
        leftHandInput = input;
    }

    public void SetRightHandInput(float input)
    {
        rightHandInput = input;
    }

}
