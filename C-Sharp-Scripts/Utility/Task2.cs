using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Task2 : MonoBehaviour
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
        alienFiringHintObject,
        destroyHintObject,
        CodeInstructionsText,
        informObject,
        backgroundObject;
    #endregion
    private GameObject playerTank;
    private GameManager gameManager;
    #region Stages
    private bool showInstructions = false;
    public bool showError,
        hasShownDestroyHint,
        hasShownAlienFireHint,
        checkingSubmission,
        hasFired,
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

        if (hasFired
            && !showInstructions)
        {
            hasFired = false;
            showInstructions = true;
            playerTank.GetComponent<PlayerControls>().enabled = false;
            gameManager.TogglePause();
            CodingInterface.SetActive(true);
            backgroundObject.SetActive(true);
            informObject.SetActive(true);



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
            int[] alienFiringHintIndices = { 2, 3, 4 };
            int[] destroyHintIndices = { 0, 1 };
            bool passed = true;
            for (int i = 0; i < userInputs.Length; i++)
            {
                if (userInputs[i].text.ToLower() != inputAnswers[i].ToLower())
                {
                    if (destroyHintIndices.Contains(i) && !hasShownDestroyHint)
                    {
                        destroyHintObject.SetActive(true);
                        hasShownDestroyHint = true;
                    }
                    else if (alienFiringHintIndices.Contains(i) && !hasShownAlienFireHint)
                    {
                        alienFiringHintObject.SetActive(true);
                        hasShownAlienFireHint = true;
                    }
                    userInputs[i].color = Color.red;
                    errorObject.SetActive(true);
                    passed = false;
                }
            }
            if (passed)
            {
                successObject.SetActive(true);
            }
            checkingSubmission = false;
        }

    }

}
