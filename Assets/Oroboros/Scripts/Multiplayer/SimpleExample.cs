using System;
using UnityEngine;

public class SimpleExample : MonoBehaviour
{
    Telepathy.Client client = new Telepathy.Client();
    Telepathy.Server server = new Telepathy.Server();

    GameObject SquarePrefab;
    GameObject SpherePrefab;
    GameObject PlayerPrefab;
    void Awake()
    {
        // update even if window isn't focused, otherwise we don't receive.
        Application.runInBackground = true;

        // use Debug.Log functions for Telepathy so we can see it in the console
        Telepathy.Logger.Log = Debug.Log;
        Telepathy.Logger.LogWarning = Debug.LogWarning;
        Telepathy.Logger.LogError = Debug.LogError;
        print(Resources.Load("Prefabs/CubeObject").name);
        SquarePrefab = (GameObject)Resources.Load("Prefabs/CubeObject");
        SpherePrefab = (GameObject)Resources.Load("Prefabs/SphereObject");
        PlayerPrefab = (GameObject)Resources.Load("Prefabs/PlayerObject");
    }

    void Update()
    {
        // client
        if (client.Connected)
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                client.Send(new byte[] { 0x1 });
               
                //client.Send(Telepathy.Utils.StringToBytes(GameObject.Find("PlayerObject(Clone)").name)); // sends the players name as a byte[]
                //byte[] bytesvalue = Telepathy.Utils.StringToBytes(GameObject.Find("PlayerObject").name);
            }
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                GameObject.Instantiate(SquarePrefab, Vector3.zero, Quaternion.identity);
            }
            // show all new messages
            Telepathy.Message msg;
            while (client.GetNextMessage(out msg))
            {
                switch (msg.eventType)
                {
                    case Telepathy.EventType.Connected:
                        Debug.Log("Connected");
                        break;
                    case Telepathy.EventType.Data:
                        Debug.Log("Data: " + BitConverter.ToString(msg.data));
                        break;
                    case Telepathy.EventType.Disconnected:
                        Debug.Log("Disconnected");
                        break;
                }
            }
        }

        // server
        if (server.Active)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                server.Send(0, new byte[] { 0x2 });

            // show all new messages
            Telepathy.Message msg;
            while (server.GetNextMessage(out msg))
            {
                switch (msg.eventType)
                {
                    case Telepathy.EventType.Connected:
                        Debug.Log(msg.connectionId + " Connected");
                        break;
                    case Telepathy.EventType.Data:
                        Debug.Log(msg.connectionId + " Data: " + BitConverter.ToString(msg.data));
                        break;
                    case Telepathy.EventType.Disconnected:
                        Debug.Log(msg.connectionId + " Disconnected");
                        break;
                }
            }
        }
    }

    void OnGUI()
    {
        // client
        GUI.enabled = !client.Connected;
        if (GUI.Button(new Rect(0, 0, 120, 20), "Connect Client")) {
            client.Connect("localhost", 1337);
            GameObject.Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        }

        GUI.enabled = client.Connected;
        if (GUI.Button(new Rect(130, 0, 120, 20), "Disconnect Client"))
        {
            client.Disconnect();

        }

        // server
        GUI.enabled = !server.Active;
        if (GUI.Button(new Rect(0, 25, 120, 20), "Start Server"))
            server.Start(1337);

        GUI.enabled = server.Active;
        if (GUI.Button(new Rect(130, 25, 120, 20), "Stop Server"))
            server.Stop();

        GUI.enabled = true;
    }

    void OnApplicationQuit()
    {
        // the client/server threads won't receive the OnQuit info if we are
        // running them in the Editor. they would only quit when we press Play
        // again later. this is fine, but let's shut them down here for consistency
        client.Disconnect();
        server.Stop();
    }
}