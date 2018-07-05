using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Chat : NetworkBehaviour
{
    public static Chat instance;
    private string lastMessage = "";

    void Start()
    {
        instance = this;
    }
    
    [ClientRpc]
    public void RpcDisplayChat(string message)
    {
        Debug.Log("Chat: "+message);
        lastMessage = message;
    }

    void OnGUI()
    {
        GUILayout.Label(lastMessage);
    }
}
