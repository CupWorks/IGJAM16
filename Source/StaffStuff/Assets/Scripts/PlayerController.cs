using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private const string INPUT_HORIZONTAL = "Horizontal";
	private const string INPUT_VERTICAL = "Vertical";

	private Rigidbody2D spriteRigidbody;
	private SpriteRenderer spriteRenderer;
	private Vector2 velocity = new Vector2(0.0f, 0.0f);
	private string inputHorizontal;
	private string inputVertical;
	private VisitorController followingVisitorController;

	public float movementSpeed = 10.0f;
	public float visitorSpeedMultiplicator = 1.5f;
	public Players currentPlayer = Players.P1;
	public VisitorSpriteDefinition spriteDefinition;

	private void Awake()
	{
		spriteRigidbody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		inputHorizontal = INPUT_HORIZONTAL + "_" + currentPlayer;
		inputVertical = INPUT_VERTICAL + "_" + currentPlayer;
        GameSession.Instance.OnGameEnd += () => { this.gameObject.SetActive(false); };
	}

	private void FixedUpdate()
	{
		if (Input.GetButtonDown("Pause"))
		{
			GameSession.Instance.PauseSession();
            MainMenu.Instance.ShowPauseMenu();
		}
		if (!GameSession.Instance.IsRunning()) return;

		if (Input.GetButton(inputHorizontal) && Input.GetAxisRaw(inputHorizontal) > 0)
		{
			velocity.x = 1.0f * movementSpeed;
		}
		if (Input.GetButton(inputHorizontal) && Input.GetAxisRaw(inputHorizontal) < 0)
		{
			velocity.x = -1.0f * movementSpeed;
		}
		if (Input.GetButton(inputVertical) && Input.GetAxisRaw(inputVertical) > 0)
		{
			velocity.y = 1.0f * movementSpeed;
		}
		if (Input.GetButton(inputVertical) && Input.GetAxisRaw(inputVertical) < 0)
		{
			velocity.y = -1.0f * movementSpeed;
		}
		spriteRigidbody.velocity = velocity;
		velocity.x = 0.0f;
		velocity.y = 0.0f;

		if (followingVisitorController != null)
		{
			followingVisitorController.moveTo = transform.position;
		}
	}

	public void SetSpriteDefinition(VisitorSpriteDefinition newSpriteDefinition)
	{
		spriteDefinition = newSpriteDefinition;
		spriteRenderer.sprite = spriteDefinition.up;
	}

	private void ChangeSpriteForDirection()
	{
		if (spriteDefinition != null)
		{
			if (spriteRigidbody.velocity.y >= 0.0f && spriteRigidbody.velocity.y >= spriteRigidbody.velocity.x)
			{
				spriteRenderer.sprite = spriteDefinition.up;
			}
			if (spriteRigidbody.velocity.y < 0.0f && spriteRigidbody.velocity.y < spriteRigidbody.velocity.x)
			{
				spriteRenderer.sprite = spriteDefinition.down;
			}
			if (spriteRigidbody.velocity.x >= 0.0f && spriteRigidbody.velocity.x >= spriteRigidbody.velocity.y)
			{
				spriteRenderer.sprite = spriteDefinition.right;
			}
			if (spriteRigidbody.velocity.x < 0.0f && spriteRigidbody.velocity.x < spriteRigidbody.velocity.y)
			{
				spriteRenderer.sprite = spriteDefinition.left;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Visitor" && followingVisitorController == null)
		{
			var visitorController = collision.gameObject.GetComponent<VisitorController>() as VisitorController;
			visitorController.movementMode = VisitorMovementMode.Follow;
			visitorController.movmentSpeed *= visitorSpeedMultiplicator;
			followingVisitorController = visitorController;
			followingVisitorController.Destroyed += FollowingVisitorDestroyed;
		}
	}

	private void FollowingVisitorDestroyed()
	{
		followingVisitorController = null;
	}
}