using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField]
    private bool isPlayer = true; //When isPlayer is changed to true the playerTank was able to fire projectiles
    [SerializeField]
    private float speed = 0.02f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GameObject.FindGameObjectWithTag("playerProjectile") != null)
            {
                return;
            }
            Quaternion noRot = Quaternion.Euler(0, 0, 0);
            GameObject firedProjectile = Instantiate(projectile, transform.position, noRot);
            firedProjectile.GetComponent<Projectile>().playerProjectile = isPlayer;
            firedProjectile.GetComponent<Projectile>().speed = speed;
            firedProjectile.tag = "playerProjectile";
        }
    }

}
