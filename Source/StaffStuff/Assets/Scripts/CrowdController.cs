using UnityEngine;

public class CrowdController : MonoBehaviour
{
	public float minSpeed = 0.1f;
	public float maxSpeed = 1.5f;
	public float range = 3.0f;
	public float startX = 0.0f;

	private float speed = 0.0f;

	private void Start()
	{
		speed = Random.Range(minSpeed, maxSpeed);
		startX = transform.position.x;
	}

	private void FixedUpdate()
	{
		transform.position = new Vector3(startX + Mathf.Sin(Time.time * speed) * range, transform.position.y, transform.position.z);
	}
}
