using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public Boundary boundary;
	public float horizontalTilt;
	public float verticalTilt;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire = 0.0f;

	void Update()
	{
		AudioSource audio = GetComponent<AudioSource> ();
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Rigidbody body = GetComponent<Rigidbody>();
		body.velocity = movement * speed;

		body.position = new Vector3 (
			Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);

		body.rotation = Quaternion.Euler(body.velocity.z * verticalTilt, 0.0f, body.velocity.x * -horizontalTilt);
	}
}
