using UnityEngine;

public class VisitorGate : MonoBehaviour
{
    public VisitorTypes[] allowedVisitors;

    private void OnTriggerEnter(Collider other)
    {
        VisitorController controller = other.GetComponent<VisitorController>();
        if (controller)
        {
            if (IsCorrectVisitor(controller.visitorType))
            {
                GameSession.Instance.IncreasePopularity(controller.visitorType);
            }
            else
            {
                GameSession.Instance.DecreasePopularity(controller.visitorType);
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
