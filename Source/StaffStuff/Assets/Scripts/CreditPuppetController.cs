using UnityEngine;

public class CreditPuppetController : MonoBehaviour
{
	public float jumpTimer = 3.0f;
	public float jumpSin = 0.0f;

	private float currentJumpTimer = 0.0f;
	private bool isJumping = false;
	private float startY = 0.0f;

	private void Start()
	{
		jumpTimer = Random.Range(1.0f, 5.0f);
		startY = transform.position.y;
	}

	private void Update()
	{
		if (!isJumping)
		{
			currentJumpTimer += Time.deltaTime;

			if (currentJumpTimer >= jumpTimer)
			{
				isJumping = true;
				currentJumpTimer = 0.0f;
				jumpTimer = Random.Range(1.0f, 5.0f);
			}
		}
		else
		{
			jumpSin = jumpSin + 3.0f * Time.deltaTime;
			if (jumpSin >= Mathf.PI)
			{
				transform.position = new Vector3(transform.position.x, startY, transform.position.z);
				isJumping = false;
				jumpSin = 0.0f;
			}
			else
			{
				transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(jumpSin), transform.position.z);
			}
		}
	}
}
