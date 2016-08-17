using UnityEngine;

public class VisitorController : MonoBehaviour
{
	public float movmentSpeed = 5.0f;
	public Vector2 moveTo = new Vector2(0.0f, 0.0f);

	private void Start()
	{
	}

	private void Update()
	{
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), moveTo, movmentSpeed * Time.deltaTime);
	}
}
