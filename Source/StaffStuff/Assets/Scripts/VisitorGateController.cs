using UnityEngine;

public class VisitorGateController : MonoBehaviour
{
    public VisitorTypes[] allowedVisitors;

	private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Visitor" || other.gameObject.tag == "Player") {
			VisitorController controller;
			if (other.gameObject.tag == "Player") {
				var player = other.gameObject.GetComponent<PlayerController>();
				controller = player.followingVisitorController;

				player.followingVisitorController = null;
				controller.moveTo = gameObject.transform.position;
				controller.isAttachAble = false;
			} else {
			 controller = other.gameObject.GetComponent<VisitorController> ();
				if (IsCorrectVisitor (controller.visitorType)) {
					GameSession.Instance.IncreasePopularity (controller.visitorType);
				} else {
					GameSession.Instance.DecreasePopularity (controller.visitorType);
				}
				controller.shallBeQueued = true;
				controller.Destroy ();
			}

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
