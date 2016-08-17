using UnityEngine;



public class Player : MonoBehaviour
{
	private const string INPUT_HORIZONTAL = "Horizontal";
	private const string INPUT_VERTICAL = "Vertical";

	private Rigidbody2D spriteRigidbody;
	private Vector2 velocity = new Vector2(0.0f, 0.0f);
	private string inputHorizontal;
	private string inputVertical;

	public float movementSpeed = 10.0f;
	public Players currentPlayer = Players.P1;

	private void Start()
	{
		spriteRigidbody = GetComponent<Rigidbody2D>();
		inputHorizontal = INPUT_HORIZONTAL + "_" + currentPlayer;
		inputVertical = INPUT_VERTICAL + "_" + currentPlayer;
	}

	private void FixedUpdate()
	{
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
	}
}