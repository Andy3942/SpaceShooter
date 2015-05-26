using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Rigidbody rigidbody = GetComponent<Rigidbody> ();
		rigidbody.velocity = movement * speed;
		if (rigidbody.velocity.x > 0) {
			rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, -rigidbody.velocity.x * tilt);		
		} else if (rigidbody.velocity.x < 0) {
			rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, -rigidbody.velocity.x * tilt);
		} else 
		{
			rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
		}
		rigidbody.position = new Vector3 (Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), rigidbody.position.y, Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax));
	}
}
