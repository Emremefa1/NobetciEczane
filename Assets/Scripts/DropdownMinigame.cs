using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DropdownMinigame : MonoBehaviour {
    public TMP_Dropdown[] dropdowns;          
    public int[] correctAnswers;             
    public UnityEvent onSuccess;             
    public UnityEvent onFailure;              

    public void CheckDropdownAnswers() {
        if (dropdowns.Length != correctAnswers.Length) {
            Debug.LogError("Dropdown sayısı ile doğru cevap sayısı uyuşmuyor!");
            return;
        }

        bool allCorrect = true;
      
        for (int i = 0; i < dropdowns.Length; i++) {
            TMP_Dropdown dropdown = dropdowns[i];
       
            if (dropdown.value != correctAnswers[i]) {
                dropdown.targetGraphic.color = Color.red;
                allCorrect = false;
            } else {
                dropdown.targetGraphic.color = Color.green;
            }
        }

        if (allCorrect)
            onSuccess.Invoke();
        else
            onFailure.Invoke();
    }
}
