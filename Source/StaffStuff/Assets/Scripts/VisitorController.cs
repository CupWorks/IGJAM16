using UnityEngine;

public class VisitorController : MonoBehaviour
{
	public VisitorTypes visitorType = VisitorTypes.Cosplayer;
	public float movmentSpeed = 5.0f;
	public Vector3 moveTo = new Vector3(0.0f, 0.0f, 0.0f);
	public VisitorMovementMode movementMode = VisitorMovementMode.Target;

	private Rigidbody2D spriteRigidbody;

	private void Start()
	{
		spriteRigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		var velocity = (moveTo - transform.position).normalized * movmentSpeed;
		spriteRigidbody.velocity = velocity;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Stage")
		{
			GameSession.Instance.DecreasePopularity(visitorType);
			Destroy(gameObject);
		}
	}
}
