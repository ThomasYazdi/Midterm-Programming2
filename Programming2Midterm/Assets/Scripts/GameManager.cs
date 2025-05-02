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
    public TextMeshProUGUI myMoney;
    public TextMeshProUGUI myStamina;

    public float EnemyTimer;
    public float EnemyInterval = 5f;
    public GameObject myEnemy;

    public float score = 0f;
    public int money = 0;

    public int playerHealth = 3;
    public GameObject mySword;

    

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
            if (score < 10)
            {
                EnemyInterval = 5f;
            }
            if (score >= 10 && score < 20)
            {
                EnemyInterval = 3f;
            }

            if (score >= 20)
            {
                EnemyInterval = 1f;
            }
            if (playerHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
                        
            myHealth.text = "Health: " + playerHealth + "/3";
            myMoney.text = "Score: " + money;
            myStamina.text = "Stamina: " + Mathf.Round(myPlayer.staminaTimer) + "/5";
        }
    }
    
    private void OnEnabled()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisabled()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        if (scene.name == "MainScene")
        {
            myHealth = GameObject.Find("PlayerHealth").GetComponent<TextMeshProUGUI>();
            myMoney = GameObject.Find("PlayerMoney").GetComponent<TextMeshProUGUI>();
            myStamina = GameObject.Find("PlayerStam").GetComponent<TextMeshProUGUI>();
        }
    }
}
