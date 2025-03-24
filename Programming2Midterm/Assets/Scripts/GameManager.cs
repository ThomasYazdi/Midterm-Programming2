using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public PlayerMovement myPlayer;
    public TextMeshProUGUI myHealth;
    public TextMeshProUGUI myScore;

    public float EnemyTimer;
    public float EnemyInterval = 5f;
    public GameObject myEnemy;

    public float HealthTimer;
    public float HealthInterval = 15f;
    public GameObject myHealthObject;

    public float score = 0f;

    public int playerHealth = 3;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerMovement>();

        //myHealth = GameObject.Find("PlayerHealth").GetComponent<TextMeshProUGUI>();
        //myScore = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            EnemyTimer += Time.deltaTime;
            Vector3 EnemyTargetPos = new Vector3
                (Random.Range(-24, 24), 1, Random.Range(-24, 24));

            if (EnemyTimer > EnemyInterval)
            {
                EnemyTimer = 0f;
                Instantiate(myEnemy, EnemyTargetPos, Quaternion.identity);
            }

            HealthTimer += Time.deltaTime;
            Vector3 HealthTargetPos = new Vector3
            (Random.Range(-24, 24), 1, Random.Range(-24, 24));

            if (HealthTimer > HealthInterval)
            {
                HealthTimer = 0f;
                Instantiate(myHealthObject, HealthTargetPos, Quaternion.identity);
            }

            if (playerHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            myHealth.text = "Health: " + playerHealth;
            myScore.text = "Score: " + score;
        }
    }
}
