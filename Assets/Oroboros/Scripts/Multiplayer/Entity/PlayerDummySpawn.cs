using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerDummySpawn : NetworkBehaviour
{

    public Transform playerPref;

    private void Start()
    {
        GameObject Dummy = Instantiate(playerPref.gameObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
