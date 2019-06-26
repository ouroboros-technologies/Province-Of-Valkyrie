using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Stats : NetworkBehaviour
{
    [SyncVar]
    public uint cur_mana;
    [SyncVar]
    public uint max_mana;

    [SyncVar]
    public uint cur_health;
    [SyncVar]
    public uint max_health;

    [SyncVar]
    public uint cur_stamina;
    [SyncVar]
    public uint max_stamina;

    [SyncVar]
    public ulong wisdom;
    [SyncVar]
    public ulong vitality;
    [SyncVar]
    public ulong strength;
    [SyncVar]
    public ulong endurance;
    [SyncVar]
    public ulong inteligence;

    public void TakeDamage(uint amount)
    {
        if (!isServer) return;
        //Debug.Log("Damage is being passed to a RigidBody");
        if (cur_health - amount <= 0) cur_health = 0; //also die eventually
        else cur_health -= amount;
    }

    public void HealDamage(uint amount)
    {
        if (!isServer) return;
        cur_health += amount;
    }

    public void SetPosition(Vector3 position)
    {
        if (!isServer) return;
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        if (!isServer) return;
        transform.rotation = rotation;
    }

    public void SetCurrentMana(uint mana)
    {
        if (!isServer) return;
        this.cur_mana = mana;
    }

    public void SetMaximumMana(uint mana)
    {
        if (!isServer) return;
        this.max_mana = mana;
    }

    public void SetCurrentHealth(uint health)
    {
        if (!isServer) return;
        this.cur_health = health;
    }

    public void SetMaximumHealth(uint health)
    {
        if (!isServer) return;
        this.max_health = health;
    }

    public void SetCurrentStamina(uint stamina)
    {
        if (!isServer) return;
        this.cur_stamina = stamina;
    }

    public void SetMaximumStamina(uint stamina)
    {
        if (!isServer) return;
        this.max_stamina = stamina;
    }

    public void SetEndurance(ulong endurance)
    {
        if (!isServer) return;
        this.endurance = endurance;
    }

    public void SetStrength(ulong strength)
    {
        if (!isServer) return;
        this.strength = strength;
    }

    public void SetVitality(ulong vitality)
    {
        if (!isServer) return;
        this.vitality = vitality;
    }

    public void SetWisdom(ulong wisdom)
    {
        if (!isServer) return;
        this.wisdom = wisdom;
    }

    public void SetInteligence(ulong inteligence)
    {
        if (!isServer) return;
        this.inteligence = inteligence;
    }
}
