using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum ItemType
{
  MELEE, NONE
}

public abstract class Item : NetworkBehaviour
{
    protected ItemType type;
    protected Rigidbody owner;

    public abstract void Use();
    public abstract void StopUsing();
    public abstract void PickUpItem();
    public abstract void DropItem();

    public ItemType GetItemType()
    {
      return type;
    }
<<<<<<< HEAD

    public Rigidbody GetOwner()
    {
        return owner;
    }

    public void SetOwner(Rigidbody rb)
    {
        owner = rb;
    }
=======
    
    public void SetOwner(Rigidbody rb)
    {
        this.owner = rb;
    }
    
>>>>>>> origin/Fixing_Branch
}
