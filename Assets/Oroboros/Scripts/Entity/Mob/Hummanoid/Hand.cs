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
        if(input == 0 && isUsingItem)
        {
            StopUsingHand();
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
        Destroy(heldItem.gameObject);
        heldItem = null;
    }

    public void PickUpItem(Item item)
    {
        heldItem = item;
    }

    public ItemType GetType()
    {
        if(heldItem)
          return heldItem.GetItemType();
        else
          return ItemType.NONE;
    }

}
