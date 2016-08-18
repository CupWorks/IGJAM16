using UnityEngine;

using UnityRandom = UnityEngine.Random;
public delegate void VisitorEventHandler();

public class VisitorController : MonoBehaviour
{
	public VisitorTypes visitorType = VisitorTypes.Cosplayer;
	public float movmentSpeed = 5.0f;
	public Vector3 moveTo = new Vector3(0.0f, 0.0f, 0.0f);
	public VisitorMovementMode movementMode = VisitorMovementMode.Target;
	public float queueTime = 2.0f;
	[HideInInspector]
	public bool isAttachedToPlayer = false;
	[HideInInspector]
	public bool shallBeQueued = false;
	private bool isQueueTimeOver = false;
	private float goneQueueTime = 0.0f;
	public float fadeOutTime = 3.0f;
	public bool isDestroyed = false;
	public Vector3[] pointsInBetween = {
		new Vector3(-16.0f, 0.0f, 0.0f),
		new Vector3(-15.0f, -2.0f, 0.0f),
		new Vector3(16.0f, -2.0f, 0.0f),
		new Vector3(15.0f, 0.0f, 0.0f)
	};

	public event VisitorEventHandler Destroyed;

	private float gonesFadeoutTime = 0.0f;
	private float alpha = 1.0f;

	private Rigidbody2D spriteRigidbody;
	private SpriteRenderer spriteRenderer;
	private VisitorSpriteDefinition spriteDefinition;

	private Vector3 pointInBetween;
	private Vector3 originalGoal;
	private bool wasAtPointInBetween;

	private void Awake()
	{
		spriteRigidbody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		wasAtPointInBetween = false;
		pointInBetween = pointsInBetween [UnityRandom.Range (0, pointsInBetween.Length)];
		originalGoal = moveTo;
		moveTo = pointInBetween;
	}

	private void Update()
	{
        if (GameSession.Instance.IsRunning())
        {

			if (!wasAtPointInBetween) {
				if (Vector2.Distance(GetComponent<Transform>().position, pointInBetween)<=1.5f){
					wasAtPointInBetween = true;
					moveTo = originalGoal;
				}
			}
			if (!isQueueTimeOver) {
				if (goneQueueTime >= queueTime) {
					isQueueTimeOver = true;
				}
			}


			if (!isDestroyed) {
				var velocity = (moveTo - transform.position).normalized * movmentSpeed;
				spriteRigidbody.velocity = velocity;
				transform.Rotate (0.0f, 0.0f, Mathf.Sin (Time.time * 15.0f));

				ChangeSpriteForDirection ();
			} else if (shallBeQueued && !isQueueTimeOver) {
				spriteRigidbody.velocity = Vector3.zero;
				GetComponent<Collider2D>().enabled = false;
				goneQueueTime += Time.deltaTime;
			}
            else
            {
                if (alpha >= 1.0f)
                {
                    spriteRigidbody.velocity = Vector3.zero;
                    GetComponent<Collider2D>().enabled = false;
                }

                alpha = 1.0f - gonesFadeoutTime / fadeOutTime;
                gonesFadeoutTime += Time.deltaTime;

                if (alpha < 0.2f)
                {
                    Destroy(gameObject);
                }

                var oldColor = GetComponent<Renderer>().material.color;
                oldColor.a = alpha;
                GetComponent<Renderer>().material.color = oldColor;
            }
        }
	}

	private void ChangeSpriteForDirection()
	{
		if (spriteDefinition != null)
		{
			if (spriteRigidbody.velocity.y > 0.0f && spriteRigidbody.velocity.y > spriteRigidbody.velocity.x)
			{
				spriteRenderer.sprite = spriteDefinition.up;
			}
			if (spriteRigidbody.velocity.y < 0.0f && spriteRigidbody.velocity.y < spriteRigidbody.velocity.x)
			{
				spriteRenderer.sprite = spriteDefinition.down;
			}
			if (spriteRigidbody.velocity.x > 0.0f && spriteRigidbody.velocity.x > spriteRigidbody.velocity.y)
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
		if (!isDestroyed && collision.gameObject.tag == "Stage" && movementMode == VisitorMovementMode.Target)
		{
			GameSession.Instance.DecreasePopularity(visitorType);
			Destroy();
		}
	}

	public void Destroy()
	{
		isDestroyed = true;
        GameSession.Instance.OnGameEnd -= Destroy;
        if (Destroyed != null)
		{
			Destroyed.Invoke();
		}
	}

	public void SetSpriteDefinition(VisitorSpriteDefinition newSpriteDefinition)
	{
		spriteDefinition = newSpriteDefinition;
		spriteRenderer.sprite = spriteDefinition.down;
	}
}
