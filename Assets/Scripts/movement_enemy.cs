using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_enemy : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Vlastnosti")]

    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathI = 0;    //path index

    private void Start(){
        target = level_manager.main.path[pathI];
    }
    private void Update(){
        if(Vector2.Distance(target.position, transform.position) <= 0.01f) {

            pathI++;
            
            if (pathI == level_manager.main.path.Length){
                Spawner.onEnemyKill.Invoke();
                Destroy(gameObject);
                return;
            }
            else{
                target = level_manager.main.path[pathI];
            }
        }
    }

    private void FixedUpdate(){
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }
}
    

