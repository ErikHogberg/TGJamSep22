using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleActive();
        }

        MoveObject();
    }

    public void MoveObject()
    {
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

            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos.x += towerOffX;
            mousePos.y += towerOffY;
            Instantiate(freezeTowerPrefab, mousePos, Quaternion.identity);

        }
    }
}
