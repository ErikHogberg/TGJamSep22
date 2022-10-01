using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanePoints : MonoBehaviour
{
    public List<Vector2> points;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++) 
        {
            Vector2 point = points[i];
            Gizmos.DrawSphere(point, .1f);
            if(i>0)
                Gizmos.DrawLine(points[i], points[i-1]);

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
