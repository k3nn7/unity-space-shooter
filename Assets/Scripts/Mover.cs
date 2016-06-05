using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	public float speed;

	void Start ()
	{
		Rigidbody body = GetComponent<Rigidbody> ();
		body.velocity = transform.forward * speed;
	}
}
