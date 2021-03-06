using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hand : MonoBehaviour
{
    private float input;
    [SerializeField]
    private Item heldItem;

    private bool isUsingItem;

    public void Update()
    {
        if(input > 0 && !isUsingItem)
        {
           UseHand();
        }
    }

    public void UseHand()
    {
        isUsingItem = true;
        if(heldItem)
          heldItem.Use();
    }

    public void StopUsingHand()
    {
        isUsingItem = false;
        if(heldItem)
          heldItem.StopUsing();
    }

    public void DropItem()
    {
        heldItem.DropItem();
        Destroy(heldItem.gameObject);
        heldItem = null;
    }

    public void PickUpItem(Item item)
    {
        heldItem = item;
        heldItem.PickUpItem();
    }

    public ItemType GetItemType()
    {
        if(heldItem)
          return heldItem.GetItemType();
        else
          return ItemType.NONE;
    }

    public Item GetHeldItem()
    {
        return heldItem;
    }

    public bool isUsingHand()
    {
        return isUsingItem;
    }
}
