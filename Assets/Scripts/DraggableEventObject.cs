using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DraggableEventObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public RectTransform dropTarget;
    public UnityEvent onDroppedSuccessfully;
    private RectTransform rect;
    private Vector2 originalPosition;
    private Vector2 dragOffset;

    void Awake() {
        rect = GetComponent<RectTransform>();
        originalPosition = rect.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        RectTransform parentRect = rect.parent as RectTransform;
        Vector2 localPointerPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, eventData.pressEventCamera, out localPointerPos);
        dragOffset = rect.anchoredPosition - localPointerPos;
    }

    public void OnDrag(PointerEventData eventData) {
        RectTransform parentRect = rect.parent as RectTransform;
        Vector2 localPointerPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, eventData.pressEventCamera, out localPointerPos))
            rect.anchoredPosition = localPointerPos + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (RectTransformUtility.RectangleContainsScreenPoint(dropTarget, eventData.position)) {
            onDroppedSuccessfully.Invoke();
            gameObject.SetActive(false);
        } else {
            rect.anchoredPosition = originalPosition;
        }
    }
}
