using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Walking : NetworkBehaviour {
    public AudioClip oof;
    public Transform target;
    [SyncVar]
    public string nick = "";
    [SyncVar(hook = "OnSongChanged")]
    public int song;
    [SyncVar]
    public float hp = 666;
    public AudioClip[] songs;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            transform.Find("[CameraRig]").gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 joystick = SteamVR_Controller.Input(1).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis2) + SteamVR_Controller.Input(2).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis2);
        Vector2 touchpad = SteamVR_Controller.Input(1).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0) + SteamVR_Controller.Input(2).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        if (joystick.magnitude < 0.25f)
            joystick = Vector3.zero;
        if (touchpad.magnitude < 0.25f)
            touchpad = Vector3.zero;
        float forward = joystick.y;
        float turn = joystick.x;
        float roll = touchpad.x;
        if (Input.GetKey(KeyCode.W))
            forward += 1.0f;
        if (Input.GetKey(KeyCode.S))
            forward -= 1.0f;
        if (Input.GetKey(KeyCode.A))
            turn -= 1.0f;
        if (Input.GetKey(KeyCode.D))
            turn += 1.0f;
        if (Input.GetKey(KeyCode.Q))
            roll += 1.0f;
        if (Input.GetKey(KeyCode.E))
            roll -= 1.0f;
        if (Input.GetKey(KeyCode.LeftShift))
            forward *= 3.0f;
        if (isLocalPlayer)
        {
            //Debug.Log(forward + " " + turn + " " + roll);
            GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 50 * forward) * Time.deltaTime, ForceMode.VelocityChange);
            GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, 0, 15 * roll) * Time.deltaTime, ForceMode.VelocityChange);
            GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, 25 * turn, 0) * Time.deltaTime, ForceMode.VelocityChange);
            if (Input.GetKeyDown(KeyCode.Space))
                CmdSendChat("test");
        }
        GetComponentInChildren<TextMesh>().text = nick+" [HP: "+hp.ToString("N2")+"]";

        /*RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.EulerAngles(hit.normal), Time.deltaTime * 100);
            transform.up = Vector3.MoveTowards(transform.up, hit.normal, Time.deltaTime);
        }*/

        //GetComponent<AudioSource>().pitch = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude / 15.0f, 0.6f, 1.0f);

        //GetComponent<NavMeshAgent>().SetDestination(target.position);
        /*GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * Time.deltaTime * 1000.0f, ForceMode.Impulse);
        transform.LookAt(target);*/
    }

    void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(oof, transform.position);
        if (collision.gameObject.GetComponent<Walking>() != null)
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > this.GetComponent<Rigidbody>().velocity.magnitude)
                hp -= Mathf.Pow(Mathf.Abs(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude - this.GetComponent<Rigidbody>().velocity.magnitude), 0.6f) * 25.0f;
        }
    }

    [Command]
    public void CmdSendChat(string message)
    {
        Chat.instance.RpcDisplayChat(message);
    }

    private string editName;

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (nick == "")
            {
                editName = GUILayout.TextField(editName);
                if (GUILayout.Button("Set name"))
                {
                    CmdSetNick(editName);
                }
            }

            GUILayout.BeginHorizontal();
            for(int i = 0; i < songs.Length; i++)
            {
                if (GUILayout.Button("Song"+i))
                {
                    CmdSetSong(i);
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    [Command]
    void CmdSetNick(string newName)
    {
        nick = newName;
    }

    [Command]
    void CmdSetSong(int newSong)
    {
        song = newSong;
    }

    void OnSongChanged(int newSong)
    {
        song = newSong;
        GetComponent<AudioSource>().clip = songs[newSong];
        GetComponent<AudioSource>().Play();
    }
}
