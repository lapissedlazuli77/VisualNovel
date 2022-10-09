using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> phaseZeroDialogue;
    public List<string> phaseOneDialogue;
    public List<string> phaseTwoDialogue;
    public List<string> phaseThreeDialogue;
    public List<string> phaseFourDialogue;
    public List<string> phaseFiveDialogue;
    List<string> currentDialogue;

    int phaseIndex = 0;
    int dialogueIndex = 0;
    int correctpath = 0;
    public TMP_Text dialogueBox;

    public GameObject Akira;
    public GameObject Allison;
    public GameObject choiceOne;
    public GameObject choiceTwo;
    public GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {
        choiceOne.SetActive(false);
        choiceTwo.SetActive(false);
        Akira.SetActive(false);
        Allison.SetActive(false);

        currentDialogue = phaseZeroDialogue;
        dialogueBox.text = currentDialogue[dialogueIndex];
    }

    void SetDialogueText()
    {
        if (phaseIndex < 5)
        {
            dialogueBox.text = currentDialogue[dialogueIndex];
        }
    }

    public void AdvanceDialog()
    {
        if (phaseIndex < 5)
        {
            dialogueIndex++;
            SetDialogueText();
            if (dialogueIndex == currentDialogue.Count - 1)
            {
                SetupChoices();
            }
        }
        else
        {
            SceneManager.LoadScene("Start");
        }
    }

    void SetupChoices()
    {
        //turn off the next button and turn on the choice buttons
        nextButton.SetActive(false);
        choiceOne.SetActive(true);
        choiceTwo.SetActive(true);
    }

    void GoToNextPhase()
    {
        //turn on the next button and turn off the choice buttons
        nextButton.SetActive(true);
        choiceOne.SetActive(false);
        choiceTwo.SetActive(false);
        dialogueIndex = 0;
        switch (phaseIndex)
        {
            case 0:
                currentDialogue = phaseOneDialogue;
                Akira.SetActive(true);
                Allison.SetActive(true);
                phaseIndex = 1;
                break;
            case 1:
                currentDialogue = phaseTwoDialogue;
                phaseIndex = 2;
                break;
            case 2:
                currentDialogue = phaseThreeDialogue;
                phaseIndex = 3;
                break;
            case 3:
                currentDialogue = phaseFourDialogue;
                phaseIndex = 4;
                break;
            case 4:
                currentDialogue = phaseFiveDialogue;
                phaseIndex = 5;
                break;
        }
        SetDialogueText();
    }
}
