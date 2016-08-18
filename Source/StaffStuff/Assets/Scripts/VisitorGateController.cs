using UnityEngine;

public class VisitorGateController : MonoBehaviour
{
    public VisitorTypes[] allowedVisitors;

	private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Visitor")
        {
			var controller = other.gameObject.GetComponent<VisitorController>();
            if (IsCorrectVisitor(controller.visitorType))
            {
                GameSession.Instance.IncreasePopularity(controller.visitorType);
            }
            else
            {
                GameSession.Instance.DecreasePopularity(controller.visitorType);
            }
			controller.shallBeQueued = true;
			controller.Destroy();
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
