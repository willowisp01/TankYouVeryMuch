using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpawn : MonoBehaviour {

    [SerializeField]
    private GameObject[] consumables = new GameObject[0]; // Array of consumables currently implemented (add new ones in inspector)

    [SerializeField]
    private float cooldown = 10f;
    private float timer;
    private int size;

    [SerializeField]
    private GameEventListener victoryListener;

    [SerializeField]
    private GameEventListener defeatListener;

    // Start is called before the first frame update
    void Start() {
        size = consumables.Length;
        timer = 0.1f;
        // The spawning should stop when the stage ends
        victoryListener.nextEvent.AddListener(Stop);
        defeatListener.nextEvent.AddListener(Stop);
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Spawn();
        }
    }

    private void Spawn() {
        timer = cooldown;
        int consumableIndex = Random.Range(0, size); // Pick a random consumable from the array
        Vector2 location = new Vector2(Random.Range(-16f, 16f), Random.Range(-8f, 8f)); // Generate a random location for it to spawn
        GameObject consumable = Instantiate(consumables[consumableIndex], location, transform.rotation);
        // We set up a filter to check if the consumable overlaps with any walls
        Collider2D[] overlap = new Collider2D[1];
        ContactFilter2D obstacleFilter = new ContactFilter2D();
        obstacleFilter.SetLayerMask(LayerMask.NameToLayer("Obstacles", "BreakableWall"));
        consumable.GetComponent<Collider2D>().OverlapCollider(obstacleFilter, overlap);
        if (overlap[0] != null) { // If there is an overlap, we destroy the current consumable and call Spawn() again
            Destroy(consumable);
            Spawn();
        }
    }

    private void Stop() {
        enabled = false;
    }
}
