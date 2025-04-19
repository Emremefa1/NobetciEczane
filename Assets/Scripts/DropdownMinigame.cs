using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DropdownMinigame : MonoBehaviour {
    public TMP_Dropdown[] dropdowns;          
    public int[] correctAnswers;             
    public UnityEvent onSuccess;             
    public UnityEvent onFailure;              


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        for (int i = 0; i < dropdowns.Length; i++)
        {
            int index = i;  // capture for closure
            dropdowns[index].onValueChanged.AddListener(_ => CheckSingleAnswer(index));
        }
    }

     public void CheckSingleAnswer(int index)
    {
        var dropdown = dropdowns[index];
        bool correct = dropdown.value == correctAnswers[index];

        // Color just this dropdown
        dropdown.targetGraphic.color = correct
            ? Color.green
            : Color.red;
    }

    public void CheckDropdownAnswers() {
        if (dropdowns.Length != correctAnswers.Length) {
            Debug.LogError("Dropdown sayısı ile doğru cevap sayısı uyuşmuyor!");
            return;
        }

        bool allCorrect = true;
      
        for (int i = 0; i < dropdowns.Length; i++) {
            TMP_Dropdown dropdown = dropdowns[i];
       
            if (dropdown.value != correctAnswers[i]) {
                
                allCorrect = false;
            } else {
                
            }
        }

        if (allCorrect)
            onSuccess.Invoke();
        else
            onFailure.Invoke();
    }
}
