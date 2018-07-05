using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walking : MonoBehaviour {
    public AudioClip oof;
    public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float turbo = 1.0f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            turbo = 1.5f;
        }

		if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 30) * Time.deltaTime * turbo, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, -30) * Time.deltaTime * turbo, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.E))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, 0, -15) * Time.deltaTime * turbo, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, 0, 15) * Time.deltaTime * turbo, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, -25, 0) * Time.deltaTime * turbo, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, 25, 0) * Time.deltaTime * turbo, ForceMode.VelocityChange);
        }

        /*RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.EulerAngles(hit.normal), Time.deltaTime * 100);
            transform.up = Vector3.MoveTowards(transform.up, hit.normal, Time.deltaTime);
        }*/

        GetComponent<AudioSource>().pitch = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude / 15.0f, 0.6f, turbo > 1.0f ? 99.0f : 1.0f);

        //GetComponent<NavMeshAgent>().SetDestination(target.position);
        /*GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * Time.deltaTime * 1000.0f, ForceMode.Impulse);
        transform.LookAt(target);*/
    }

    void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(oof, transform.position);
    }
}
