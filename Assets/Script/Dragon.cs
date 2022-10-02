using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public LayerMask castMask;
    public static Dragon MainInstance = null;
    public float spellRadius = 1f;
    public float spellDamage = 1f;
    public float scrap = 10;

    public GameObject enemy;

    PlayAudioSource audio;
    private void Awake()
    {
        MainInstance = this;
    }

    private void OnDestroy()
    {
        MainInstance = null;
    }

    readonly RaycastHit2D[] hits = new RaycastHit2D[10];
    void Update()
    {
        //If the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            PlayAudioSource.mainInstance.dragonBite.Play();
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int hitCount = Physics2D.CircleCastNonAlloc(worldPoint, spellRadius, Vector2.zero, hits, Mathf.Infinity, layerMask: castMask);

            //If something was hit, the RaycastHit2D.collider will not be null.
            Debug.Log($"Listing hits  ({hitCount})");
            for (int i = 0; i < hitCount; i++)
            {
                Debug.Log(hits[i].collider.name);
            }

            if (hitCount > 0)
                Debug.Log("Done Listing hits");
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(enemy, worldPoint, Quaternion.identity);
        }
    }
    void powerUp()
    {
        //Reduce recources
        spellDamage++;
        Debug.Log("Spell damage has been increased");
    }
}
