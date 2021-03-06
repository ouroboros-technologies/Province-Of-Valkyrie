﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Make a better way to assign player Object here var: OwnerRigidbody currently set by serialized field right now

// make sure volume can be calculated
[RequireComponent(typeof(MeshFilter), typeof(Calculate_Weight))]
public class MeleeWeapon : Item
{
    private List<RayCastMelee> hitPoints;
    private Calculate_Weight weight;

    void Start()
    {
        type = ItemType.MELEE;
        hitPoints = new List<RayCastMelee>();
        weight = GetComponent<Calculate_Weight>();
        GetRCMs();
    }

    void Update()
    {
        foreach (RayCastMelee rcm in hitPoints)
        {
            Transform hit = rcm.GetObjectHit();
            if (hit != null)
            {
                OnRCMHit(hit);
            }
        }
    }

    public override void Use()
    {
        foreach (RayCastMelee rcm in hitPoints)
        {
            rcm.SetEnabled(true);
        }
    }

    public override void StopUsing()
    {
        foreach (RayCastMelee rcm in hitPoints)
        {
            rcm.SetEnabled(false);
        }
    }

    public override void PickUpItem()
    {

    }

    public override void DropItem()
    {

    }

    public void OnRCMHit(Transform hit)
    {

    }

    //rcm rayCastMelee
    private void GetRCMs()
    {
        foreach (Transform t in transform)
        {
            if (t.GetComponent<RayCastMelee>())
            {
                RayCastMelee rcm = t.GetComponent<RayCastMelee>();
                rcm.SetWeight(weight.getWeight());

                if (rcm.OwnerRigidbody == null)
                {
                    rcm.OwnerRigidbody = this.owner;
                    Debug.Log(GetOwner().name);
                }

                hitPoints.Add(rcm);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 lastPosition = Vector3.zero;
        foreach (Transform t in transform)
        {
            if (t.GetComponent<RayCastMelee>())
            {
                Vector3 thisPosition = t.position;
                if (lastPosition != Vector3.zero)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(lastPosition, thisPosition);
                }
                lastPosition = thisPosition;
            }
        }
    }

}
