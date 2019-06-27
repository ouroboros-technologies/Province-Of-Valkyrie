using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Humanoid
{
    PlayerData data;
    private Stats stats;

    new protected void Start()
    {
        base.Start();

        data = new PlayerData();
        data.SetData(this);
        cam.gameObject.SetActive(isLocalPlayer);
        //Debug.Log(Helper.SerializeObject<PlayerData>(data));
    }

    new protected void Update()
    {
        base.Update();
        if(!isLocalPlayer) return;
        data.SetData(this);
    }

}
