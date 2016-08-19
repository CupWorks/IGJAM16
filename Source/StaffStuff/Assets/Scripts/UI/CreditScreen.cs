using UnityEngine;
using UnityEngine.UI;

public class CreditScreen : MonoBehaviour
{
    public string[] creditEntries;
    public float creditSpeed = 126f;
    public CreditEntry entryPrefab;
    private CreditEntry lastSpawnedEntry;
    private int curEntryPointer = 0;
    private int creditsComplete = 0;

	private void Update()
    {
        if (curEntryPointer < creditEntries.Length)
        {
            if ((curEntryPointer == 0 || lastSpawnedEntry.transform.localPosition.x >= 0))
            {
                SpawnNextCreditItem();
            }
        }
    }

    private void OnEnable()
    {
        curEntryPointer = 0;
    }

    private void OnDisable()
    {
        foreach (Transform child in this.transform)
        {
            if (child.GetComponent<CreditEntry>())
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void SpawnNextCreditItem()
    {
        CreditEntry entry = Instantiate(entryPrefab, new Vector3(-1260,0),Quaternion.identity, this.transform) as CreditEntry;
        entry.creditScreen = this;
        entry.transform.localPosition = new Vector3(-1260, 0);
        entry.GetComponent<Text>().text = creditEntries[curEntryPointer].Replace("<br/>", "\n");
        curEntryPointer++;
        lastSpawnedEntry = entry;
    }
}
