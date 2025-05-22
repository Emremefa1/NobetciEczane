using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class OrganMapping {
    public DraggableOrgan.OrganType organType;
    public GameObject bodyChild;    // Vücut içindeki o organa ait GameObject (başta inactive)
}

public class OrganPlacementManager : MonoBehaviour {
    [Header("Drop Area")]
    public RectTransform bodyDropZone;

    [Header("Organ ↔ Body Mapping")]
    public OrganMapping[] mappings;

    [Header("All Placed Event")]
    public UnityEvent onAllOrgansPlaced;

    private int placedCount;

    void Start() {
        placedCount = 0;
        // Başlangıçta tüm bodyChild'ları gizle
        foreach (var m in mappings)
            m.bodyChild.SetActive(false);
    }

    public void HandleDrop(DraggableOrgan organ, Vector2 dropPos) {
        if (!RectTransformUtility.RectangleContainsScreenPoint(bodyDropZone, dropPos))
            return;

        // Doğru mapping'i bul
        foreach (var m in mappings) {
            if (m.organType == organ.organType) {
                // Henüz yerleşmemişse
                if (!m.bodyChild.activeSelf) {
                    m.bodyChild.SetActive(true);
                    placedCount++;
                    // Son organı da yerleştirdiysek event'i çağır
                    if (placedCount >= mappings.Length)
                        onAllOrgansPlaced.Invoke();
                }
                return;
            }
        }
    }

    /// <summary>
    /// Her şeyi sıfırlar: bodyChild'ları gizler ve sayaç sıfırlar.
    /// </summary>
    public void ResetAll() {
        placedCount = 0;
        foreach (var m in mappings)
            m.bodyChild.SetActive(false);
    }
}
