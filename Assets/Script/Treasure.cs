using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public float targetTime = 3.0f;
    public GameObject FastEnemy;
    bool enemySpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        targetTime -= Time.deltaTime;

        if(targetTime <= 0 && !enemySpawned)
            {
                Spawn();
                enemySpawned = true;
            }
    }
    void Spawn()
    {
        GameObject newEnemy = Instantiate(FastEnemy, new Vector2(40.0f, Random.Range(-10f, 10f)), Quaternion.identity);
        newEnemy.GetComponent<FastEnemy>().currentHp = 5;
        newEnemy.GetComponent<FastEnemy>().hp = 5;
        newEnemy.GetComponent<FastEnemy>().treasure = this.gameObject;

    }
}

