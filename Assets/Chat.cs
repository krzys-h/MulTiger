using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Chat : NetworkBehaviour
{
    public static Chat instance;
    private string lastMessage = "";
    [SyncVar]
    public int numPlayers = -1;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (isServer)
        {
            numPlayers = NetworkManager.singleton.numPlayers;
        }
    }

    [ClientRpc]
    public void RpcDisplayChat(string message)
    {
        Debug.Log("Chat: "+message);
        lastMessage = message;
    }

    void OnGUI()
    {
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label(" ");
        GUILayout.Label("Connected players: " + numPlayers);
        GUILayout.Label(lastMessage);
    }
}
