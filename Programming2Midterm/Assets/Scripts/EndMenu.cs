using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public GameManager myManager;

    public TextMeshProUGUI playerScore;
    // Start is called before the first frame update
    void Start()
    {
        myManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerScore.text = "You Scored a Total of " + myManager.score + " Points!";

        if (Input.GetKey(KeyCode.R))
        {
            myManager.score = 0;
            myManager.playerHealth = 3;
            myManager.EnemyTimer = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
