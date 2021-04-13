using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Task3 : MonoBehaviour
{
    #region Text Components
    public Text[] userInputs;
    public string[] inputAnswers;
    #endregion
    #region Text GameObjects
    public GameObject CodingInterface,
        instructionObject,
        successObject,
        errorObject,
        iAHint,
        iUHint,
        dBHint,
        dAHint,
        CodeInstructionsText,
        informObject,
        backgroundObject;
    #endregion
    private GameObject playerTank;
    private GameManager gameManager;
    #region Stages
    private bool showInstructions = false;
    public bool showError,
        hasShownIUHint,
        hasShownIAHint,
        hasShownDBHint,
        hasShownDAHint,
        checkingSubmission,
        showSuccess = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerTank = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
        if (Time.timeScale != 1)
        {
            gameManager.TogglePause();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.playerScore != 0
            && !showInstructions)
        {
            playerTank.GetComponent<PlayerControls>().enabled = false;
            gameManager.TogglePause();
            CodingInterface.SetActive(true);
            backgroundObject.SetActive(true);
            informObject.SetActive(true);
            showInstructions = true;

        }

    }
    public void ResetTextColor(Text text)
    {
        if (text.color == Color.red)
        {
            text.color = Color.white;
        }
    }
    public void CheckSubmission()
    {
        if (!checkingSubmission)
        {
            checkingSubmission = true;
            int[] incScoreUfo = { 2, 3 };
            int[] incScoreAlien = { 0, 1 };
            int[] decLivesBullet = { 4, 5 };
            int[] decLivesAlien = { 6, 7 };
            bool passed = true;
            for (int i = 0; i < userInputs.Length; i++)
            {
                if (userInputs[i].text.ToLower() != inputAnswers[i].ToLower())
                {
                    if (incScoreUfo.Contains(i) && !hasShownIUHint)
                    {
                        iUHint.SetActive(true);
                        hasShownIUHint = true;
                    }
                    else if (incScoreAlien.Contains(i) && !hasShownIAHint)
                    {
                        iAHint.SetActive(true);
                        hasShownIAHint = true;
                    }
                    else if (decLivesBullet.Contains(i) && !hasShownDBHint)
                    {
                        dBHint.SetActive(true);
                        hasShownDBHint = true;
                    }
                    else if (decLivesAlien.Contains(i) && !hasShownDAHint)
                    {
                        dAHint.SetActive(true);
                        hasShownDAHint = true;
                    }
                    userInputs[i].color = Color.red;
                    errorObject.SetActive(true);
                    passed = false;
                }
            }
            if (passed)
            {
                successObject.SetActive(true);   
                gameManager.TogglePause();
            }
            checkingSubmission = false;
        }

    }

}
