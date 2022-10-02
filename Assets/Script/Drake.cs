using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Drake : MonoBehaviour
{
    public GameObject head, origin;
    float maxDistance = 0.75f;
    public SpriteShapeController neck;
    public AnimationCurve neckCurve;
    public AnimationCurve mouseCurve;
    private float timer = 0f;
    [Range(0.1f, 50f)] public float multiplier = 1f;

    private Camera cam;
    Vector2 point = Vector3.zero;
    Vector2 mousePos = Vector2.zero;


    [Min(2)]
    public int NeckResolusion = 5;
    public Vector2 NeckGravity = Vector2.up;
    public float MaxNeckLinkDistance = 1f;
    public float CloseMaxNeckLinkDistance = .1f;
    public float NeckRefDistance = 15;
    public float CloseNeckRefDistance = 2;
    public float MouseMax = 20f;

    [System.NonSerialized]
    private List<Vector2> neckPoints = new();

    private void Start()
    {
        if (!head)
            head = gameObject;
        if (!origin)
            origin = GameObject.Find("Origin");
        cam = Camera.main;

        neck.transform.position = Vector2.zero;

        neckPoints.Clear();
        for (int i = 0; i < NeckResolusion; i++)
        {
            neckPoints.Add(Vector2.zero);
        }
    }

    private void Update()
    {
        // head.transform.position = point;

        //if (Vector2.Distance(head.transform.position, origin.transform.position) > 7f)
        //{
        //    head.transform.position = (head.transform.position - origin.transform.position).normalized * 7f;
        //}

        Vector3 originPosition = origin.transform.position;
        float mouseDistance = Vector2.Distance(point, originPosition);
        if (mouseDistance > MouseMax)
            point = originPosition + (new Vector3(point.x, point.y, originPosition.z) - originPosition).normalized * MouseMax;
        float lerp = Mathf.Clamp01((mouseDistance - CloseNeckRefDistance) / (NeckRefDistance - CloseNeckRefDistance));
        float neckLinkDistance = Mathf.Lerp(CloseMaxNeckLinkDistance, MaxNeckLinkDistance, lerp);

        neckPoints[^1] = point;

        for (int i = neckPoints.Count - 1; i >= 0; i--)
        {
            neckPoints[i] += NeckGravity * neckCurve.Evaluate((float)i / neckPoints.Count);
            if (i < neckPoints.Count - 1 && Vector2.Distance(neckPoints[i], neckPoints[i + 1]) > neckLinkDistance)
            {
                neckPoints[i] = neckPoints[i + 1] + (neckPoints[i] - neckPoints[i + 1]).normalized * neckLinkDistance;
            }
        }

        neckPoints[0] = originPosition;
        for (int i = 0; i < neckPoints.Count; i++)
        {
            if (i > 0 && Vector2.Distance(neckPoints[i], neckPoints[i - 1]) > neckLinkDistance)
            {
                neckPoints[i] = neckPoints[i - 1] + (neckPoints[i] - neckPoints[i - 1]).normalized * neckLinkDistance;
            }
        }

        neck.spline.Clear();

        int offset = 0;
        Vector2 lastValid = Vector2.zero;
        for (int i = 0; i < neckPoints.Count; i++)
        {
            if (i > 0 && Vector2.Distance(neckPoints[i], lastValid) < .1f)
            {
                offset++;
                continue;
            }
            lastValid = neckPoints[i];
            neck.spline.InsertPointAt(i - offset, neckPoints[i]);
            neck.spline.SetTangentMode(i - offset, ShapeTangentMode.Continuous);
        }

        head.transform.position = neckPoints[^1];


        // neck.spline.InsertPointAt(0, origin.transform.position);
        // neck.spline.SetTangentMode(0, ShapeTangentMode.Continuous);

        // neck.spline.InsertPointAt(1, (origin.transform.position + head.transform.position) / 3 * 2);
        // neck.spline.SetTangentMode(1, ShapeTangentMode.Continuous);

        // neck.spline.InsertPointAt(2, (origin.transform.position + head.transform.position) / 3);
        // neck.spline.SetTangentMode(2, ShapeTangentMode.Continuous);

        // neck.spline.InsertPointAt(3, head.transform.position);
        // neck.spline.SetTangentMode(3, ShapeTangentMode.Continuous);


        // timer += Time.deltaTime * multiplier;

        // if (timer > 1f)
        //     timer -= 1f;

        // neck.spline.SetLeftTangent(1, new Vector2(curve.Evaluate(timer), curve.Evaluate(timer)));
        // neck.spline.SetRightTangent(1, new Vector2(-curve.Evaluate(timer), -curve.Evaluate(timer)));

        // neck.spline.SetLeftTangent(2, new Vector2(-curve.Evaluate(timer), -curve.Evaluate(timer)));
        // neck.spline.SetRightTangent(2, new Vector2(curve.Evaluate(timer), curve.Evaluate(timer)));

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(mousePos, .2f);
    }
}
