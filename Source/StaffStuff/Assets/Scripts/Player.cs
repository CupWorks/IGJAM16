using UnityEngine;

public class Player : MonoBehaviour 
{
	private const string INPUT_HORIZONTAL = "Horizontal";
	private const string INPUT_VERTICAL = "Vertical";

	public float movementSpeed = 10.0f;

	private void Start()
	{
		Debug.Log("Player start");
	}

	private void Update() 
	{
		if (Input.GetButton(INPUT_HORIZONTAL) && Input.GetAxisRaw(INPUT_HORIZONTAL) > 0)
		{
			transform.Translate(1.0f * movementSpeed * Time.deltaTime, 0, 0);
		}
		if (Input.GetButton(INPUT_HORIZONTAL) && Input.GetAxisRaw(INPUT_HORIZONTAL) < 0)
		{
			transform.Translate(-1 * movementSpeed * Time.deltaTime, 0, 0);
		}
		if (Input.GetButton(INPUT_VERTICAL) && Input.GetAxisRaw(INPUT_VERTICAL) > 0)
		{
			transform.Translate(0, 1 * movementSpeed * Time.deltaTime, 0);
		}
		if (Input.GetButton(INPUT_VERTICAL) && Input.GetAxisRaw(INPUT_VERTICAL) < 0)
		{
			transform.Translate(0, -1 * movementSpeed * Time.deltaTime, 0);
		}
	}
}
