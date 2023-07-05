using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour {
    float maxReflects = 3f; // Number of possible reflections
    float range = 12f; // Range of trajectory line

    [SerializeField]
    private Vector2 lookDir;

    [SerializeField]
    private Vector3 startPosition;
    private List<Vector3> points = new List<Vector3>();

    private PlayerMovement playerMovement;
    private LineRenderer lineRenderer;

    private LayerMask layerMask;

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        lineRenderer = GetComponent<LineRenderer>();
        layerMask = LayerMask.GetMask("Obstacles", "Default", "Enemy");

    }

    // Start is called before the first frame update
    void Start() {
        UpdatePosition();
    }

    // Update is called once per frame
    void Update() {
        UpdatePosition();
    }

    private void FixedUpdate() {
        DrawLine();
    }

    private void UpdatePosition() {
        startPosition = transform.Find("Tower/ProjectileSource").position;
        lookDir = playerMovement.GetLookDir();
    }

    private void Reflect(Vector2 position, Vector2 inputDir, float distRemaining, int reflectCount) { 
        RaycastHit2D hit = Physics2D.Raycast(position, inputDir, distRemaining, layerMask);
        Vector2 newInputDir = Vector2.Reflect(inputDir, hit.normal);
        Vector2 newPosition = hit.point + newInputDir.normalized * 0.01f; 
        float distTraversed = hit.distance;
        float newDistRemaining = distRemaining - distTraversed;
        // If no reflects yet, add starting position (occurs only once)
        if (reflectCount <= 0) { 
            points.Add(position);
        }
        // If we hit something
        if (hit.collider != null) { 
            reflectCount++;
            if (distRemaining > 0) {
                points.Add(hit.point);
                if (hit.collider.gameObject.CompareTag("Wall") && reflectCount <= maxReflects) {
                    // Prevents infinite reflections (can occur when muzzle inserted into the wall)
                    Reflect(newPosition, newInputDir, newDistRemaining, reflectCount);
                } else if (hit.collider.gameObject.CompareTag("Enemy")) {
                    // Debug.Log("locked on enemy");
                }
            } 
        } else { // We hit nothing (because there was nothing within range)
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
