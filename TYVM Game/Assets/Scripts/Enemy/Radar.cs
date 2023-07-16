using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    [SerializeField]
    LineRenderer lineRenderer;

    [SerializeField]
    private float rotationSpeed = 45f; // Degrees per second. Too high and will often miss, too low and tank will hardly find any suitable shots.

    [SerializeField]
    private Vector2 lookDir;
    private LayerMask layerMask; // Mask to ignore enemies

    [SerializeField]
    private List<Vector3> points = new List<Vector3>();
    private float latestAdvancedAngle;
    private float radarDistance = 50f;
    private int maxReflects = 3; // Max reflects for advanced aiming


    // Update is called once per frame
    void Update() {
        Debug.DrawRay(transform.position, transform.up * 3.0f, Color.red, 0.01f);
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        lookDir = transform.up.normalized;
    }

    private void FixedUpdate() {
        Sweep();
        if (Reflect(transform.position, lookDir, radarDistance)) {
            latestAdvancedAngle = Vector2.SignedAngle(Vector2.up, GetDirection());
        }
        DrawLine();
    }

    private void Start() {
        layerMask = LayerMask.GetMask("Player", "Obstacles", "Default"); // Raycast only hits players and walls (which are in default layer)
        lookDir = transform.up; //TODO: remove this!!!
    }

    // Returns the direction the radar is pointing to.
    public Vector2 GetDirection() { 
        return lookDir;
    }

    // Get the angle associated with the reflected bullet path. 
    public float GetLatestAdvancedAngle() {
        return latestAdvancedAngle;
    }

    //Sweeps the stage for enemies. must call every FixedUpdate
    private void Sweep() { 
        Debug.DrawRay(transform.position, transform.up * 3.0f, Color.red, 0.01f);
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        lookDir = transform.up.normalized;
    }

    //Returns true if a reflected bullet is able to hit the player, else false.
    public bool Reflect(Vector2 position, Vector2 inputDir, float distRemaining) {
        return ReflectHelper(position, inputDir, distRemaining, 0);
    }

    //start this function with reflectCount = 0
    private bool ReflectHelper(Vector2 position, Vector2 inputDir, float distRemaining, int reflectCount) { 
        RaycastHit2D hit = Physics2D.Raycast(position, inputDir, distRemaining, layerMask);
        Vector2 newInputDir = Vector2.Reflect(inputDir, hit.normal);
        Vector2 newPosition = hit.point + newInputDir.normalized * 0.01f; // Prevent infinite reflections (can occur when muzzle inserted into the wall)
        float distTraversed = hit.distance;
        float newDistRemaining = distRemaining - distTraversed;
        // If no reflects yet, add starting position (occurs only once)
        if (reflectCount <= 0) { 
            points.Add(position);
        }
        // We hit something
        if (hit.collider != null) { 
            //Debug.Log("hit something");
            reflectCount++;
            if (distRemaining > 0) {
                points.Add(hit.point);
                if (hit.collider.gameObject.CompareTag("Wall") && reflectCount <= maxReflects) {
                    return ReflectHelper(newPosition, newInputDir, newDistRemaining, reflectCount);
                } else if (hit.collider.gameObject.CompareTag("PlayerHull")) {
                    //Debug.Log("player found");
                    return true;
                }
            }
        // We hit nothing (because there was nothing within range)
        } else { 
            points.Add(position + inputDir.normalized * distRemaining);    
        }
        return false;
    }

    // For debugging purposes. Creates a point array from List<Vector3> points used by lineRenderer.
    private void DrawLine() { 
        lineRenderer.positionCount = points.Count;
        //Debug.Log(points.ToArray().Length);
        lineRenderer.SetPositions(points.ToArray());
        points.Clear();
    }
}
