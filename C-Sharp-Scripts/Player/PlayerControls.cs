using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    GameManager gameManager;
    public GameObject projectile;
    public Sprite playerTank;
    public GameObject destroyed;
    public float speed = .5f;
    private float halfofTankLength;
    private float leftWall;
    private float rightWall;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.SharedInstance;
        halfofTankLength = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector3.left*speed*Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
             transform.Translate(Vector3.right*speed*Time.deltaTime);
        }

        Vector2 tankPosition = transform.position;

        leftWall = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x + halfofTankLength;
        rightWall = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x - halfofTankLength;

        tankPosition.x = Mathf.Clamp(tankPosition.x, leftWall, rightWall);
        transform.position = tankPosition;
       
    }

    public void Death()
    {
        GameObject explosion = Instantiate(destroyed, transform.position, Quaternion.Euler(0,0,0));
        Destroy(explosion, 0.3f);
        if(gameManager.lives == 0){
            Destroy(gameObject);
        }
    }
}
