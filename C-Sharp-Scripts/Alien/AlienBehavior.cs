using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehavior : MonoBehaviour
{
    GameManager gameManager;
    private float speedModifier = 1;
    public bool move = false;
    public bool descend = false;
    public GameObject projectile;
    public Sprite alien1;
    public Sprite alien2;
    public GameObject destroyed;
    private bool sprite = false;

    private void Awake()
    {
   
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.SharedInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (sprite)
            {
                GetComponent<SpriteRenderer>().sprite = alien1;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = alien2;
            }
            if (gameManager.directionRight)
            {
                transform.position = (transform.position + new Vector3(0.12f, 0f));
            }
            else
            {
                transform.position = (transform.position + new Vector3(-0.12f, 0f));
            }
            move = false;
            sprite = !sprite;
        }
        if (descend)
        {
            transform.position = (transform.position + new Vector3(0f, -0.12f));
            descend = false;
        }
        //Fire()
    }

    public void Fire()
    {
            Quaternion noRot = Quaternion.Euler(0, 0, 0);
            GameObject firedProjectile = Instantiate(projectile, transform.position, noRot);
        speedModifier = gameManager.speedModifier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall")){
            gameManager.decreaseHeight = true;
        }
        else if (collision.CompareTag("Player"))
        {
            gameManager.lives = 0;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("EndZone"))
        {  
            gameManager.EndGameLoss();
        }
        else if(!collision.CompareTag("Alien") && !collision.CompareTag("alienProjectile"))
        {
            Destroy(collision.gameObject);
        }

    }
    public void Death()
    {
        GameObject explosion = Instantiate(destroyed, transform.position, Quaternion.Euler(0,0,0));
        Destroy(gameObject);
        Destroy(explosion, 0.3f);
    }
}
