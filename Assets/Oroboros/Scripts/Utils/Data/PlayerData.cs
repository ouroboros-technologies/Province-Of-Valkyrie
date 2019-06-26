using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    private float x_pos;
    private float y_pos;
    private float z_pos;

    private float x_rot;
    private float y_rot;
    private float z_rot;

    private uint cur_mana;
    private uint max_mana;

    private uint cur_health;
    private uint max_health;

    private uint cur_stamina;
    private uint max_stamina;

    private ulong wisdom;
    private ulong vitality;
    private ulong strength;
    private ulong endurance;
    private ulong inteligence;

    public void SetData(Player player)
    {
        Stats stats = player.GetStats();

        x_pos = player.transform.position.x;
        y_pos = player.transform.position.y;
        z_pos = player.transform.position.z;

        x_rot = player.transform.rotation.x;
        y_rot = player.transform.rotation.y;
        z_rot = player.transform.rotation.z;

        cur_mana = stats.cur_mana;
        max_mana = stats.max_mana;
        cur_health = stats.cur_health;
        max_health = stats.max_health;
        cur_stamina = stats.cur_stamina;
        max_stamina = stats.max_stamina;

        wisdom = stats.wisdom;
        vitality = stats.vitality;
        strength = stats.strength;
        endurance = stats.endurance;
        inteligence = stats.inteligence;
    }
}
