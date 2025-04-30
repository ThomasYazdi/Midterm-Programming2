using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject targetPlayer;
    public float speed = 50f;
    Rigidbody myRB;

    float proxAttack = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindWithTag("Player");
        //get rigidbody
        myRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance
            (targetPlayer.transform.position, transform.position);

        if (distanceToPlayer > proxAttack)
        {
            myRB.AddForce(VectorToPlayer() * speed);
            transform.LookAt(targetPlayer.transform);
        }
        
    }

    Vector3 VectorToPlayer()
    {
        Vector3 targetDirection;
        targetDirection = targetPlayer.transform.position - transform.position;
        targetDirection = targetDirection.normalized;
        return targetDirection;
    }

}
