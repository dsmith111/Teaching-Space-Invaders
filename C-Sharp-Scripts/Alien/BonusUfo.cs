using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusUfo : MonoBehaviour
{
    public float timeToDespawn = 10f;
    public GameObject destroyed;
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        //Debug.Log("BonusUFO Time since startup: " + Time.realtimeSinceStartup);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - spawnTime) >= timeToDespawn)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.left * 0.012f);
    }

    public void Death()
    {
        GameObject explosion = Instantiate(destroyed, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
        Destroy(explosion, 0.3f);
    }
}
