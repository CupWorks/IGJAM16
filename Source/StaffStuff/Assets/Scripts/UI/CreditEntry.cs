using UnityEngine;

public class CreditEntry : MonoBehaviour
{
    [HideInInspector]
    public CreditScreen creditScreen;

	private void Update ()
    {
        if (this.transform.localPosition.x >= 1260)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(creditScreen.creditSpeed * Time.deltaTime, 0, 0);
        }
	}
}
