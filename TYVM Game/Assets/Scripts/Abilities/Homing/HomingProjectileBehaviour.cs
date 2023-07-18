using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectileBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject explosionPrefab;
    GameObject playerTank;
    private bool isUsed = false;
    public float activeTime;
    // Start is called before the first frame update
    private void Start() {
        playerTank = GameObject.FindWithTag("Player"); //not very good to do this
        activeTime = playerTank.GetComponent<SkillSelect>().skill.activeTime;
        Invoke("Explosion", activeTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if ((other.CompareTag("EnemyHull") || other.CompareTag("Wall")) && !isUsed) {
            isUsed = true;
            Explosion();
        }
    }

    private void Explosion() {
        for (int i = 0; i < 3; i++) {
            Vector2 pos = transform.position;
            Instantiate(explosionPrefab, pos + Random.insideUnitCircle, transform.rotation);
        }
        Destroy(gameObject);
    }

    private void OnDestroy() {
        playerTank.GetComponent<PlayerControl>().Enable();
    }
}
