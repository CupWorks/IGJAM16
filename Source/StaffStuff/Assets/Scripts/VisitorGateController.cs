using UnityEngine;

public class VisitorGateController : MonoBehaviour
{
	public VisitorTypes[] allowedVisitors;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Visitor")
		{
			var visitorController = other.gameObject.GetComponent<VisitorController>();
			if (IsCorrectVisitor(visitorController.visitorType))
			{
				GameSession.Instance.IncreasePopularity(visitorController.visitorType);
			}
			else
			{
				GameSession.Instance.DecreasePopularity(visitorController.visitorType);
			}
			visitorController.shallBeQueued = true;
			visitorController.Destroy();

		}
		if (other.gameObject.tag == "Player")
		{
			var playerController = other.gameObject.GetComponent<PlayerController>();
			var visitorController = playerController.followingVisitorController;
			if (visitorController == null) return;

			playerController.followingVisitorController = null;
			visitorController.moveTo = gameObject.transform.position;
			visitorController.isAttachAble = false;
		}
	}

	private bool IsCorrectVisitor(VisitorTypes visitorType)
	{
		bool result = false;
		for (int i = 0; i < allowedVisitors.Length; i++)
		{
			result = allowedVisitors[i] == visitorType ? true : result;
		}

		return result;
	}
}
