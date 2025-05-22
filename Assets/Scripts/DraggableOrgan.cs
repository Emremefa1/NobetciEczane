using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableOrgan : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public enum OrganType { Akciger, Burun, Alveol, SolukBorusu, Yutak, Diyafram }
    public OrganType organType;
    public OrganPlacementManager manager;

    private RectTransform rect;
    private Vector2 startPos;
    private Vector2 dragOffset;

    void Awake() {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData e) {
        RectTransform parent = rect.parent as RectTransform;
        Vector2 local;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, e.position, e.pressEventCamera, out local);
        dragOffset = rect.anchoredPosition - local;
    }

    public void OnDrag(PointerEventData e) {
        RectTransform parent = rect.parent as RectTransform;
        Vector2 local;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, e.position, e.pressEventCamera, out local))
            rect.anchoredPosition = local + dragOffset;
    }

    public void OnEndDrag(PointerEventData e) {
        manager.HandleDrop(this, e.position);
        rect.anchoredPosition = startPos;
    }
}
