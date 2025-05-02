using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameManager myManager;
    public PlayerMovement myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myManager = FindObjectOfType<GameManager>();
        myPlayer = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && myPlayer.isVertical == false)
        {
            Destroy(collision.gameObject);
            myManager.score++;
            myManager.money++;

        }
    }
}
