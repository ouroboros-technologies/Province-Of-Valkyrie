using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
  MELEE, NONE
}

public abstract class Item : MonoBehaviour
{
    protected ItemType type;
    public abstract void Use();
    public abstract void StopUsing();

    public ItemType GetItemType()
    {
      return type;
    }
}
