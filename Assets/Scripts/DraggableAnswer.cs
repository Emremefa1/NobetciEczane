using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableAnswer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public int answerID;                       
    public AnswerMatchingGame manager;         
    private RectTransform rect;
    private Vector2 originalPos;
    private Vector2 dragOffset;

    void Awake() {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        
        RectTransform parentRect = rect.parent as RectTransform;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, eventData.pressEventCamera, out localPoint);
        dragOffset = rect.anchoredPosition - localPoint;
    }

    public void OnDrag(PointerEventData eventData) {
        RectTransform parentRect = rect.parent as RectTransform;
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, eventData.pressEventCamera, out localPoint))
            rect.anchoredPosition = localPoint + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData) {
      
        manager.OnAnswerDropped(this, eventData.position);
    }

    public void ResetPosition() {
        rect.anchoredPosition = originalPos;
    }
}
