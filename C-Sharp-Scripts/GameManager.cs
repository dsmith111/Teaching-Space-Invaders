using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [Header("Timing Information")]
    public float alienSpeed;
    public float elapsedSinceAlienMove = 0;
    public float timeSinceBonusSpawn = 0;
    public float timeSinceAlienFire = 0;
    [Space(10)]

    public static GameManager SharedInstance;
    #region Player Stats
    [Header("Public Variables")]
    public int playerScore;
    public int lives;
    #endregion
    #region Enemy Stats
    public float amountOfEnemies;
    public float totalEnemies;
    public float speedModifier = .5f;
    public float speedOfEnemies = 4f;
    public float timeSinceLastMove = 0;
    public bool decreaseHeight = false;
    public bool directionRight = true;
    public float timeSinceDecreaseHeight = 0;
    private float timeSinceFire = 0;
    private float fireDelay = 5;

    #endregion
    #region Bonus
    public GameObject bonusSpawnLocation;
    public float bonusSpawnDelay = 20f;
    private float lastBonusSpawn;
    public GameObject bonusSpawnObject;
    #endregion
    public Text scoreText;
    public Text livesText;
    public GameObject gameOver;
    public GameObject playerWins;
    private float defaultFixedTime;
    private bool endedGame = false;
    public GameObject leftWall;
    public GameObject rightWall;
    private float timeSinceAlienMove = 0;

    private void Awake()
    {
        SharedInstance = this;
        defaultFixedTime = Time.fixedDeltaTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        lastBonusSpawn = Time.time;
        playerScore = 0;
        lives = 3;
        bool beginning = true;
        CountAliens(beginning);

    }

    // Update is called once per frame
    void Update()
    {  
        if (lives == 0 && !endedGame)
        {  
            EndGameLoss();
        }
        if (amountOfEnemies <= 1 && !endedGame)
        {  
            EndGameWin();
        }

        if (decreaseHeight && (Time.time - timeSinceDecreaseHeight) >= (speedOfEnemies * speedModifier) + speedModifier)
        {
            DescendAliens();
        }
        if((Time.time - timeSinceLastMove) >= speedOfEnemies * speedModifier)
        {
            elapsedSinceAlienMove = Time.time - timeSinceAlienMove;
            timeSinceAlienMove = Time.time;
            MoveAliens();
        }
        if ((Time.time - timeSinceFire) >= fireDelay * speedModifier)
        {
            timeSinceAlienFire = Time.time - timeSinceAlienFire;
            FireAliens();
        }
        if((Time.time - lastBonusSpawn) >= bonusSpawnDelay)
        {
            timeSinceBonusSpawn = Time.time - timeSinceBonusSpawn;
            SpawnBonus();
        }
        scoreText.text = playerScore.ToString();
        livesText.text = lives.ToString();
    }

    public void DescendAliens()
    {
        leftWall.SetActive(!leftWall.activeSelf);
        rightWall.SetActive(!rightWall.activeSelf);
        decreaseHeight = false;
        GameObject[] totalAliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach (GameObject alien in totalAliens)
        {
            alien.GetComponent<AlienBehavior>().descend = true;
        }
        timeSinceDecreaseHeight = Time.time;
        directionRight = !directionRight;
    }

    public void SetSpeed()
    {
        speedModifier = amountOfEnemies / totalEnemies;
        alienSpeed = speedOfEnemies * speedModifier;
    }

    public void MoveAliens()
    {

        GameObject[] totalAliens = GameObject.FindGameObjectsWithTag("Alien");
        int i = 0;
        foreach (GameObject alien in totalAliens)
        {
            alien.GetComponent<AlienBehavior>().move = true;
            ++i;
        }
        timeSinceLastMove = Time.time;
    }

    public void FireAliens()
    {
        GameObject[] totalAliens = GameObject.FindGameObjectsWithTag("Alien");
        int whoFires = Random.Range(0, totalAliens.Length + 1);
        int i = 0;
        foreach (GameObject alien in totalAliens)
        {
            if (i == whoFires)
            {
                alien.GetComponent<AlienBehavior>().Fire();
                break;
            }
            ++i;
        }
        timeSinceFire = Time.time;
    }

    public void CountAliens(bool beginning = false)
    {
        GameObject[] totalAliens = GameObject.FindGameObjectsWithTag("Alien");
        amountOfEnemies = totalAliens.Length;
        if (beginning)
        {
        totalEnemies = amountOfEnemies;
        }
        SetSpeed();
    }

    public void SpawnBonus()
    {
        Instantiate(bonusSpawnObject, bonusSpawnLocation.transform.position, Quaternion.Euler(0, 0, 0));
        lastBonusSpawn = Time.time;
        bonusSpawnDelay = Random.Range(15f, 30f);
    }

    public void EndGameLoss()
    {
        if (!endedGame)
        {
            endedGame = true;
            gameOver.SetActive(true);
            TogglePause();
        }
    }

    public void EndGameWin()
    {
        if (!endedGame)
        {
            endedGame = true;
            playerWins.SetActive(true);
            TogglePause();
        }
    }
    public void TogglePause()
    {
        if(Time.timeScale == 1.0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        //Debug.Log(Time.timeScale);
        Time.fixedDeltaTime = Time.timeScale * defaultFixedTime;
    }
}
