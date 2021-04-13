using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Task1 : MonoBehaviour
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
        movingHintObject,
        firingHintObject,
        CodeInstructionsText,
        informObject,
        backgroundObject;
    #endregion
    private GameObject playerTank;
    private GameManager gameManager;
    #region Stages
    private bool showInstructions = false;
    public bool showError,
        hasShownMoveHint,
        hasShownFireHint,
        checkingSubmission,
        showSuccess = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerTank = GameObject.FindGameObjectWithTag("Player");
        playerTank.GetComponent<PlayerControls>().enabled = false;
        gameManager = FindObjectOfType<GameManager>();
        if (Time.timeScale != 1)
        {
            gameManager.TogglePause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetKeyDown(KeyCode.Space))
            && !showInstructions)
        {
            gameManager.TogglePause();
            CodingInterface.SetActive(true);
            backgroundObject.SetActive(true);
            informObject.SetActive(true);
            showInstructions = true;
            

        }
       
    }
    public void ResetTextColor(Text text)
    {
        if(text.color == Color.red)
        {
            text.color = Color.white;
        }
    }
    public void CheckSubmission()
    {
        if (!checkingSubmission)
        {
            checkingSubmission = true;
            int[] movingHintIndices = { 0, 1, 2, 3 };
            int[] firingHintIndices = { 5 };
            bool passed = true;
            for (int i = 0; i < userInputs.Length; i++)
            {
                if (userInputs[i].text.ToLower() != inputAnswers[i].ToLower())
                {
                    if (movingHintIndices.Contains(i) && !hasShownMoveHint)
                    {
                        movingHintObject.SetActive(true);
                        hasShownMoveHint = true;
                    }
                    else if (firingHintIndices.Contains(i) && !hasShownFireHint)
                    {
                        firingHintObject.SetActive(true);
                        hasShownFireHint = true;
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
