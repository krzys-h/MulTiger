using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EasterEgg : NetworkBehaviour
{
    [ClientRpc]
    public void RpcEasterEgg()
    {
        GetComponent<AudioSource>().Play();
    }
}
