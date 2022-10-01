using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Drake : MonoBehaviour
{
    public GameObject head, origin;
    //public GameObject body;
    //List<GameObject> bodies;
    float maxDistance = 0.75f;
    public SpriteShapeController neck;
    public AnimationCurve curve;
    private float timer = 0f;
    [Range(0.1f, 50f)] public float multiplier = 1f;

    private Camera cam;
    Vector2 point = Vector3.zero;
    Vector2 mousePos = Vector2.zero;

    private void Start()
    {
        head = gameObject;
        origin = GameObject.Find("Origin");
        //bodies = new List<GameObject>();
        cam = Camera.main;

        neck.transform.position = Vector2.zero;
    }

    private void Update()
    {
        head.transform.position = point;

        //if (Vector2.Distance(head.transform.position, origin.transform.position) > 7f)
        //{
        //    head.transform.position = (head.transform.position - origin.transform.position).normalized * 7f;
        //}

        neck.spline.Clear();
        neck.spline.InsertPointAt(0, origin.transform.position);
        neck.spline.SetTangentMode(0, ShapeTangentMode.Continuous);

        neck.spline.InsertPointAt(1, (origin.transform.position + head.transform.position) / 3 * 2);
        neck.spline.SetTangentMode(1, ShapeTangentMode.Continuous);

        neck.spline.InsertPointAt(2, (origin.transform.position + head.transform.position) / 3);
        neck.spline.SetTangentMode(2, ShapeTangentMode.Continuous);

        neck.spline.InsertPointAt(3, head.transform.position);
        neck.spline.SetTangentMode(3, ShapeTangentMode.Continuous);


        timer += Time.deltaTime * multiplier;

        if (timer > 1f)
            timer -= 1f;

        neck.spline.SetLeftTangent(1, new Vector2(curve.Evaluate(timer), curve.Evaluate(timer)));
        neck.spline.SetRightTangent(1, new Vector2(-curve.Evaluate(timer), -curve.Evaluate(timer)));

        neck.spline.SetLeftTangent(2, new Vector2(-curve.Evaluate(timer), -curve.Evaluate(timer)));
        neck.spline.SetRightTangent(2, new Vector2(curve.Evaluate(timer), curve.Evaluate(timer)));

        //Debug.Log(curve.Evaluate(timer));

        //float distance = Vector2.Distance(head.transform.position, origin.transform.position);
        //int parts = (int)(distance / maxDistance);

        //if (parts > bodies.Count)
        //{
        //    while (bodies.Count < parts)
        //    {
        //        bodies.Add(Instantiate(body, origin.transform));
        //    }   
        //}

        //else if (parts < bodies.Count)
        //{
        //    while(bodies.Count > parts)
        //    {
        //        Destroy(bodies[bodies.Count - 1]);
        //        bodies.Remove(bodies[bodies.Count - 1]);
        //    }
        //}

        //for (int i = 0; i < bodies.Count; i++)
        //{
        //    Vector2 segment = (head.transform.position + origin.transform.position) / bodies.Count;
        //    bodies[i].transform.position = new Vector2(segment.x, segment.y) * i;
        //}
        //
    }

    private void OnGUI()
    {
        Event currentEvent = Event.current;

        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
    }
}
