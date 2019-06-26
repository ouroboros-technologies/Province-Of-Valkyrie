using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SinglePlayer : MonoBehaviour
{
    NetworkManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
        manager.StartHost();
    }
}
