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
    public List<string> phaseSixDialogue;
    public List<string> phaseSevenDialogue;
    public List<string> phaseEightDialogue;
    List<string> currentDialogue;

    int phaseIndex = 0;
    int dialogueIndex = 0;
    int correctpath = 0;
    public TMP_Text dialogueBox;

    public GameObject Akira;
    public GameObject Allison;
    public GameObject Adrien;
    public GameObject choiceOne;
    public GameObject choiceTwo;
    public GameObject choiceThree;
    public GameObject nextButton;

    public TextMeshProUGUI choiceOneText;
    public TextMeshProUGUI choiceTwoText;
    public TextMeshProUGUI choiceThreeText;

    public Animator AkiraAnim;
    public Animator AllisonAnim;
    public Animator AdrienAnim;

    // Start is called before the first frame update
    void Start()
    {
        choiceOne.SetActive(false);
        choiceTwo.SetActive(false);
        choiceThree.SetActive(false);
        Akira.SetActive(false);
        Allison.SetActive(false);
        Adrien.SetActive(false);

        currentDialogue = phaseZeroDialogue;
        dialogueBox.text = currentDialogue[dialogueIndex];

        choiceOneText = choiceOne.GetComponentInChildren<TextMeshProUGUI>();
        choiceTwoText = choiceTwo.GetComponentInChildren<TextMeshProUGUI>();
        choiceThreeText = choiceThree.GetComponentInChildren<TextMeshProUGUI>();
        choiceSetWord();
    }

    public void choiceSetWord()
    {
        switch (phaseIndex)
        {
            case 0:
                choiceTwoText.text = "Continue";
                break;
            case 1:
                choiceOneText.text = "Left";
                choiceTwoText.text = "Forward";
                choiceThreeText.text = "Right";
                break;
            case 2:
                choiceOneText.text = "Hell yeah!";
                choiceTwoText.text = "Maybe?";
                choiceThreeText.text = "I guess...";
                break;
            case 3:
                choiceOneText.text = "Turn back";
                choiceThreeText.text = "Nah, we're right";
                break;
            case 4:
                choiceOneText.text = "The first clawed-up one";
                choiceTwoText.text = "The unmarked one";
                choiceThreeText.text = "The other clawed-up one";
                break;
            case 5:
                choiceTwoText.text = "Continue";
                break;
            case 6:
                choiceTwoText.text = "End";
                break;
            case 7:
                choiceTwoText.text = "End";
                break;
            case 8:
                choiceTwoText.text = "End";
                break;
        }
    }

    void SetDialogueText()
    {
        dialogueBox.text = currentDialogue[dialogueIndex];
    }

    public void AdvanceDialog()
    {
        dialogueIndex++;
        SetDialogueText();
        WhoIsTalking();
        if (dialogueIndex == currentDialogue.Count - 1)
        {
            SetupChoices();
        }
    }

    void SetupChoices()
    {
        nextButton.SetActive(false);
        if (phaseIndex > 0 && phaseIndex < 6)
        {
            choiceOne.SetActive(true);
            choiceThree.SetActive(true);
        } else
        {
            choiceOne.SetActive(false);
            choiceThree.SetActive(false);
        }
        if (phaseIndex != 3)
        {
            choiceTwo.SetActive(true);
        } else
        {
            choiceTwo.SetActive(false);
        }
    }

    public void CorrectChoice()
    {
        correctpath += 2;
        GoToNextPhase();
    }

    public void NeutralChoice()
    {
        GoToNextPhase();
    }

    public void WrongChoice()
    {
        correctpath += -1;
        GoToNextPhase();
    }

    void GoToNextPhase()
    {
        //turn on the next button and turn off the choice buttons
        nextButton.SetActive(true);
        choiceOne.SetActive(false);
        choiceTwo.SetActive(false);
        choiceThree.SetActive(false);
        dialogueIndex = 0;
        switch (phaseIndex)
        {
            case 0:
                currentDialogue = phaseOneDialogue;
                Akira.SetActive(true);
                Allison.SetActive(true);
                AkiraAnim.SetTrigger("isTalking");
                phaseIndex = 1;
                choiceSetWord();
                break;
            case 1:
                currentDialogue = phaseTwoDialogue;
                AkiraAnim.SetTrigger("isTalking");
                phaseIndex = 2;
                choiceSetWord();
                break;
            case 2:
                currentDialogue = phaseThreeDialogue;
                AllisonAnim.SetTrigger("isTalking");
                phaseIndex = 3;
                choiceSetWord();
                break;
            case 3:
                currentDialogue = phaseFourDialogue;
                AllisonAnim.SetTrigger("isTalking");
                phaseIndex = 4;
                choiceSetWord();
                break;
            case 4:
                currentDialogue = phaseFiveDialogue;
                AllisonAnim.SetTrigger("isTalking");
                phaseIndex = 5;
                choiceSetWord();
                break;
            case 5:
                phaseIndex = 6;
                Results();
                break;
            case 6:
                SceneManager.LoadScene("Start");
                break;
        }
        SetDialogueText();
    }

    void WhoIsTalking()
    {
        if (currentDialogue[dialogueIndex].Contains("Akira: "))
        {
            AkiraAnim.SetTrigger("isTalking");
        }
        if (currentDialogue[dialogueIndex].Contains("Allison: "))
        {
            AllisonAnim.SetTrigger("isTalking");
        }
        if (currentDialogue[dialogueIndex].Contains("Adrien: "))
        {
            Adrien.SetActive(true);
            Akira.SetActive(false);
            AdrienAnim.SetTrigger("isTalking");
        }
    }

    void Results()
    {
        if (correctpath > 6)
        {
            currentDialogue = phaseSixDialogue;
        } else if (correctpath > 3)
        {
            currentDialogue = phaseSevenDialogue;
            AllisonAnim.SetTrigger("isTalking");
        } else
        {
            currentDialogue = phaseEightDialogue;
            AllisonAnim.SetTrigger("isTalking");
        }
    }
}
