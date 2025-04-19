using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public enum VisualMode { FullBody, HeadOnly }

[System.Serializable]
public class DialogueSegment {
    public string dialogue;
    public UnityEvent onDialogueEvent;
    public Sprite characterSprite;
    public VisualMode visualMode;
}

public class DialogueManager : MonoBehaviour {
    public bool canContinue = true;
    public GameObject fullBodyUI;
    public TMP_Text fullBodyText;
    public Image fullBodyImage;
    public GameObject headOnlyUI;
    public TMP_Text headOnlyText;
    public Image headOnlyImage;
    public Button continueButton;
    public TMP_InputField nameInputField;
    public Button confirmNameButton;
    public DialogueSegment[] dialogues;
    private int dialogueIndex = 0;
    private string playerName = "";

    

    void Start() {
        if (dialogues.Length > 0) {
            SetVisualMode(dialogues[dialogueIndex].visualMode);
            UpdateDialogueUI(dialogues[dialogueIndex]);
            if (dialogues[dialogueIndex].onDialogueEvent != null)
                dialogues[dialogueIndex].onDialogueEvent.Invoke();
        }
        continueButton.onClick.AddListener(NextDialogue);
        confirmNameButton.onClick.AddListener(ConfirmName);
    }

    public void NextDialogue() {

        if(!canContinue)
            return;
            dialogueIndex++;
            if (dialogueIndex < dialogues.Length) {
                SetVisualMode(dialogues[dialogueIndex].visualMode);
                UpdateDialogueUI(dialogues[dialogueIndex]);
                if (dialogues[dialogueIndex].onDialogueEvent != null)
                    dialogues[dialogueIndex].onDialogueEvent.Invoke();
            }
    }

    void ConfirmName() {
        playerName = nameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
            UpdateDialogueUI(dialogues[dialogueIndex]);
    }

    string ReplacePlayerName(string text) {
        return !string.IsNullOrEmpty(playerName) ? text.Replace("{playerName}", playerName) : text;
    }

    void SetVisualMode(VisualMode mode) {
        if (mode == VisualMode.FullBody) {
            fullBodyUI.SetActive(true);
            headOnlyUI.SetActive(false);
        }
        else if (mode == VisualMode.HeadOnly) {
            fullBodyUI.SetActive(false);
            headOnlyUI.SetActive(true);
        }
    }

    void UpdateDialogueUI(DialogueSegment segment) {
        string updatedText = ReplacePlayerName(segment.dialogue);
        
            if (segment.visualMode == VisualMode.FullBody) {
                fullBodyText.text = updatedText;
                fullBodyImage.sprite = segment.characterSprite;
            }
            else if (segment.visualMode == VisualMode.HeadOnly) {
                headOnlyText.text = updatedText;
                headOnlyImage.sprite = segment.characterSprite;
            }
        
    }

    public void SetCanContinue(bool value)
    {
        canContinue = value;
    }

    public void FastForward()
    {
        SetCanContinue(true); 
        NextDialogue();

    }
}
