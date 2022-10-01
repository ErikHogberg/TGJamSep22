using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private Camera cam;

    public GameObject freezeTowerPrefab;


    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            SpawnTower();
        }
    }

    private void SpawnTower()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Instantiate(freezeTowerPrefab, mousePos, Quaternion.identity);
    }
}
