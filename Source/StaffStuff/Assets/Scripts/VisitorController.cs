using UnityEngine;

public class VisitorController : MonoBehaviour
{
	public VisitorTypes visitorType = VisitorTypes.Cosplayer;
	public float movmentSpeed = 5.0f;
	public Vector3 moveTo = new Vector3(0.0f, 0.0f, 0.0f);
	public VisitorMovementMode movementMode = VisitorMovementMode.Target;
	public float fadeOutTime = 3.0f;

	private bool isDestroyed = false;
	private float gonesFadeoutTime = 0.0f;
	private float alpha = 1.0f;

	private Rigidbody2D spriteRigidbody;

	private void Start()
	{
		spriteRigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (!isDestroyed)
		{
			var velocity = (moveTo - transform.position).normalized * movmentSpeed;
			spriteRigidbody.velocity = velocity;
		}
		else 
		{
			alpha = 1.0f - gonesFadeoutTime / fadeOutTime;
			gonesFadeoutTime += Time.deltaTime;

			if (alpha < 0.2f) 
			{
				Destroy(gameObject);
			}
			if (alpha < 0.5f)
			{
				GetComponent<Rigidbody2D>().Sleep();
			}
			var oldColor = GetComponent<Renderer>().material.color;
			oldColor.a = alpha;
			GetComponent<Renderer>().material.color = oldColor;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!isDestroyed && collision.gameObject.tag == "Stage")
		{
			GameSession.Instance.DecreasePopularity(visitorType);
			isDestroyed = true;
		}
	}
}
