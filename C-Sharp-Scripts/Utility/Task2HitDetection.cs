using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2HitDetection : MonoBehaviour
{
    public Task2 SceneManager;
    public bool pauseGame = false;

    private void Start()
    {
        SceneManager = FindObjectOfType<Task2>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectile>().playerProjectile)
        {
            SceneManager.hasFired = true;
        }
    }
}
