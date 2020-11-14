using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUIManager : MonoBehaviour
{
    public GameObject background;
    public GameObject mainText;
    public GameObject responseTab;
    public GameObject continueText;
    public List<Button> responsesHolder;

    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private int responses = 0;
    private DialogueBranch branch;

    // Start is called before the first frame update
    void Start()
    {
        DeactiveDialogue();
    }

    public void ActiveDialogue()
    {
        background.SetActive(true);
        mainText.SetActive(true);
        continueText.SetActive(true);
        responseTab.SetActive(true);
        foreach (Button response in responsesHolder)
        {
            response.gameObject.SetActive(false);
        }
    }

    public void DeactiveDialogue()
    {
        background.SetActive(false);
        mainText.SetActive(false);
        continueText.SetActive(false);
        responseTab.SetActive(false);
        foreach (Button response in responsesHolder)
        {
            response.gameObject.SetActive(false);
        }
    }

    public void NextBranch(int branchSelect)
    {
        // Add ReciveDialogueBranch with newBranch being next branch
        RecieveDialogueBranch(branch.ResponseOption[branchSelect].nextBranch);
        ActiveDialogue();
        NextDialogue();
    }

    
    public void RecieveDialogueBranch(DialogueBranch newBranch)
    {
        // Add branch info here
        this.branch = newBranch;
        responses = Mathf.Clamp(branch.ResponseOption.Count, 0, 3);
        currentIndex = 0;
    }

    public void NextDialogue()
    {
        // if at end of branch
        if (currentIndex >= branch.DialogueLines.Count)
        {
            // No response --> End dialogue
            if (responses == 0)
            {
                DeactiveDialogue();
            }
            else
            {
                // Responses --> Activate Buttons
                continueText.SetActive(false);
                for (int i=0; i < responses; i++)
                {
                    if (i >= 3)
                    {
                        break;
                    }
                    // Active the button and place responses text
                    responsesHolder[i].gameObject.SetActive(true);
                    responsesHolder[i].GetComponentInChildren<TextMeshProUGUI>().text = branch.ResponseOption[i].text;
                }
            }
        }
        // There is still more text to show
        else
        {
            mainText.GetComponent<TextMeshProUGUI>().text = branch.DialogueLines[currentIndex];
            continueText.SetActive(true);
            currentIndex++;
        }
    }
}
