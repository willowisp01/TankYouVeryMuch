using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    int maxReflects = 3; //number of possible reflections
    float range = 12f; //range of trajectory line

    [SerializeField]
    private Vector2 lookDir;
    [SerializeField]
    private Vector3 startPosition;
    private List<Vector3> points = new List<Vector3>();
    private Shooting shooting;

    private PlayerMovement playerMovement;
    private LineRenderer lineRenderer;

    private void Awake() {
        playerMovement = GetComponentInParent<PlayerMovement>();
        shooting = GetComponentInParent<Shooting>();
        lineRenderer = GetComponentInParent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void FixedUpdate() {
        DrawLine();
    }

    void UpdatePosition() {
        startPosition = shooting.GetFirePoint().position;
        lookDir = playerMovement.GetLookDir();
    }

    private void Reflect(Vector2 position, Vector2 inputDir, float distRemaining, int reflectCount) { 
        RaycastHit2D hit = Physics2D.Raycast(position, inputDir, distRemaining);
        Vector2 newInputDir = Vector2.Reflect(inputDir, hit.normal);
        Vector2 newPosition = hit.point + newInputDir.normalized * 0.01f; 
        float distTraversed = hit.distance;
        float newDistRemaining = distRemaining - distTraversed;

        if (reflectCount <= 0) { //if no reflects yet, add starting position (occurs only once)
            points.Add(position);
        }

        if (hit.collider != null) { //we hit something
            reflectCount++;
            if (distRemaining > 0) {
                points.Add(hit.point);
                if (hit.collider.gameObject.tag == "Wall" && reflectCount <= maxReflects) {
                    //prevent infinite reflections (can occur when muzzle inserted into the wall)
                    Reflect(newPosition, newInputDir, newDistRemaining, reflectCount);
                } else if (hit.collider.gameObject.tag == "Enemy") {
                    //Debug.Log("locked on enemy");
                }
            } 
        } else { //we hit nothing (because there was nothing within range)
            points.Add(position + inputDir.normalized * distRemaining);    
        }

    }

    private void DrawLine() {
        Reflect(startPosition, lookDir, range, 0);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        points.Clear();
    }
}
