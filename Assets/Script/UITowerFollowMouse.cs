using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UITowerFollowMouse : MonoBehaviour
{
    public RectTransform MovingObject;
    public Vector3 offset;
    public RectTransform BasisObject;
    public Camera cam;

    public bool isActive = false;

    public GameObject freezeTowerPrefab;
    public Vector3 towerOffset;
    public float towerOffY;
    public float towerOffX;

    public int resources;
    public int towerCost = 1;
    public TMP_Text scrapsText;

    public bool canPlace;
    public Image movingObjectImage;

    public float minDis = 5f;
    public float maxDis = 10f;
    private void Start()
    {

    }
    void Update()
    {
        foreach(LanePoints instances in LanePoints.instances)
        {
            Vector3 pos = Input.mousePosition + offset;
            pos.z = BasisObject.position.z;
            pos = cam.ScreenToWorldPoint(pos);

            Vector3 ClosestPoint = instances.edgeCollider.ClosestPoint(pos);
            float distance = Vector3.Distance(pos, ClosestPoint);
            
            if (distance > minDis && distance < maxDis)
            {
                canPlace = true;
            }
            else
            {
                canPlace = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && towerCost <= resources)
        {
            ToggleActive();
        }
        scrapsText.text = resources.ToString();
        MoveObject();

        SpawnTower();

        if (Input.GetKeyDown(KeyCode.W))
        {
            canPlace = !canPlace;
        }
    }

    public void MoveObject()
    {
        if (canPlace)
        {
            MovingObject.GetComponent<Image>().color = new Color32(255, 190, 255, 255);
        }
        if (!canPlace)
        {
            MovingObject.GetComponent<Image>().color = new Color32(6, 45, 250, 255);
        }

        Vector3 pos = Input.mousePosition + offset;
        pos.z = BasisObject.position.z;
        MovingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public void ToggleActive()
    {
        if (!isActive)
        {
            MovingObject.gameObject.SetActive(true);
            isActive = true;
        }
        else
        {
            MovingObject.gameObject.SetActive(false);
            isActive = false;

            //Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            //mousePos.z = 0;
            //mousePos.x += towerOffX;
            //mousePos.y += towerOffY;
            //Instantiate(freezeTowerPrefab, mousePos, Quaternion.identity);

        }
    }

    public void SpawnTower()
    {
        if (!isActive && Input.GetKeyDown(KeyCode.Q) && towerCost <= resources && canPlace)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos.x += towerOffX;
            mousePos.y += towerOffY;
            Instantiate(freezeTowerPrefab, mousePos, Quaternion.identity);
            resources--;
        }
    }
}
