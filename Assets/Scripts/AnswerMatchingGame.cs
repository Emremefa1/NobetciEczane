using UnityEngine;
using UnityEngine.UI;

public class AnswerMatchingGame : MonoBehaviour {
    [Header("Slot Ayarları")]
    public RectTransform[] slotRects; 
    public int[] correctAnswers;        

    [Header("Cevap Objeleri")]
    public DraggableAnswer[] answers;   

    [Header("Ekranlar")]
    public GameObject winScreen;
    public GameObject loseScreen;

    private DraggableAnswer[] assignedAnswers;

    void Start() {
        assignedAnswers = new DraggableAnswer[slotRects.Length];
    }

 
    public void OnAnswerDropped(DraggableAnswer answer, Vector2 dropPosition) {
        bool placed = false;

        for (int i = 0; i < slotRects.Length; i++) {
            if (RectTransformUtility.RectangleContainsScreenPoint(slotRects[i], dropPosition)) {
                
                if (assignedAnswers[i] != null) {
                    assignedAnswers[i].ResetPosition();
                }
           
                assignedAnswers[i] = answer;
                answer.GetComponent<RectTransform>().position = slotRects[i].position;
                placed = true;
                break;
            }
        }
  
        if (!placed) {
            answer.ResetPosition();
        }
    }

    public void CheckAnswers() {
        bool allCorrect = true;
        for (int i = 0; i < slotRects.Length; i++) {
            if (assignedAnswers[i] == null || assignedAnswers[i].answerID != correctAnswers[i]) {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect) {
            if (winScreen != null) winScreen.SetActive(true);
        } else {
            if (loseScreen != null) loseScreen.SetActive(true);
        }

    
        ResetAllAnswers();
    }

  
    public void ResetAllAnswers() {
        for (int i = 0; i < answers.Length; i++) {
            answers[i].ResetPosition();
        }
        for (int i = 0; i < assignedAnswers.Length; i++) {
            assignedAnswers[i] = null;
        }
    }
}
