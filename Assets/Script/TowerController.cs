using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Camera cam;

    public GameObject freezeTowerPrefab;
    public SpriteRenderer placementVisuals;

    public float MinLaneDistance = .5f;
    public float MaxLaneDistance = 2f;

    bool placing = false;
    bool Placing
    {
        get { return placing; }
        set
        {
            if (placementVisuals)
                placementVisuals.gameObject.SetActive(value);
            placing = value;
        }
    }


    void Start()
    {
        if (!cam)
            cam = Camera.main;
    }


    void Update()
    {

        if (!Placing)
        {
            return;
        }

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        placementVisuals.transform.position = mousePos;
        placementVisuals.color = Color.white;
        bool canPlace = false;
        foreach (var item in LanePoints.instances)
        {
            Vector2 closestPos = item.edgeCollider.ClosestPoint(mousePos);
            float distance = Vector2.Distance(mousePos, closestPos);
            if (distance < MinLaneDistance)
            {
                placementVisuals.color = Color.red;
                return;
            }
            if (distance < MaxLaneDistance){
                placementVisuals.color = Color.green;
                canPlace = true;
            }
            
        }

        if (canPlace && Input.GetMouseButtonDown(1))
        {
            SpawnTower(mousePos);
            Placing = false;
        }
    }

    public void SpawnTower(Vector3 mousePos)
    {
        Instantiate(freezeTowerPrefab, mousePos, Quaternion.identity);
    }

    public void BuyTower(bool value)
    {
        if (!Placing && ResourceManager.MainInstance.Score > 50)
        {
            ResourceManager.MainInstance.Score -= 50;
            Placing = value;
        }
    }
}
