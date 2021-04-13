using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool playerProjectile = false;
    public float speed = 1f;
    private float spawnTime;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.SharedInstance;
        spawnTime = Time.time;
    }
 
    // Update is called once per frame
    void Update()
    {
        if ((Time.time - spawnTime) >= 4f)
        {
            Destroy(gameObject);
        }
        if (playerProjectile)
        {
            transform.Translate(Vector3.up * speed);
        }
        else
        {
            transform.Translate(Vector3.down * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Alien"){
            //Debug.Log(other.tag);
            //Debug.Log("Is player projectile? "+playerProjectile);
            if (!playerProjectile)
            {
                Physics2D.IgnoreCollision(other, gameObject.GetComponent<Collider2D>());
                return;
            }
            other.GetComponent<AlienBehavior>().Death();
            Destroy(gameObject);
            gameManager.playerScore += 1;
            gameManager.CountAliens();
        }
        else if(other.tag == "Player")
        {
            if (playerProjectile)
            {
                return;
            }
            gameManager.lives -= 1;
            other.GetComponent<PlayerControls>().Death();
            Destroy(gameObject);   
        }
        else if(other.tag == "Ufo")
        {
            if (!playerProjectile)
            {
                return;
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.playerScore += 5;
        }
        else if (other.CompareTag("ceiling"))
        {
            Destroy(gameObject);
        }
        else if(!other.CompareTag("wall") && !other.CompareTag("EndZone"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }


    }
}
