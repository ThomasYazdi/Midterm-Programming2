using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager myManager;

    public float speed = 5f;
    public float lookspeed = 75f;

    public GameObject myCamera;

    Rigidbody RB;
    Vector3 lookDirection;

    public float camLock = 15f;

    public Transform sword;
    public float scaleSword = 1f;

    bool isVertical = true;

    private Vector3 swordOriginalPosition;
    private Vector3 lastHorizontalPosition;

    // Start is called before the first frame update
    void Start()
    {
        myManager = FindObjectOfType<GameManager>();

        RB = GetComponent<Rigidbody>();
        lookDirection = Vector3.zero;

        swordOriginalPosition = sword.localPosition;
        lastHorizontalPosition = swordOriginalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerLook = myCamera.transform.TransformDirection(Vector3.forward);

        lookDirection += DeltaLook() * Time.deltaTime;

        if (lookDirection.y > camLock)
        {
            lookDirection.y = camLock;
        }
        else if (lookDirection.y < -camLock)
        {
            lookDirection.y = -camLock;
        }

        transform.rotation = Quaternion.Euler(0f, lookDirection.x, 0f);
        myCamera.transform.rotation = Quaternion.Euler(-lookDirection.y, lookDirection.x, 0f);

        if (Input.GetKey(KeyCode.R) && myManager.money >= 20 && myManager.playerHealth < 3)
        {
            myManager.money -= 20;
            myManager.playerHealth += 1;
        }

        if (Input.GetKeyDown(KeyCode.F) && myManager.money >= 50)
        {
            myManager.money -= 50;
            Vector3 newScale = sword.localScale;
            newScale.y += scaleSword;
            sword.localScale = newScale;

            Vector3 newPosition = sword.localPosition;
            newPosition.z += scaleSword * 0.5f;
            sword.localPosition = newPosition;

            if (!isVertical)
            {
                lastHorizontalPosition = sword.localPosition;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isVertical == true)
            {
                sword.localRotation = Quaternion.Euler(0, 90, 90);
                sword.localPosition = lastHorizontalPosition;
                isVertical = false;
            }
            else
            {
                sword.localRotation = Quaternion.Euler(0, 0, 0);
                sword.localPosition = swordOriginalPosition;
                isVertical = true;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 myDir = transform.TransformDirection(PlayerDir());
        RB.AddForce(myDir * speed);
    }

    Vector3 PlayerDir()
    {
        Vector3 moveDir = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, 0, z);
        return moveDir;
    }

    Vector3 DeltaLook()
    {
        Vector3 deltaLook = Vector3.zero;
        float rotationY = Input.GetAxisRaw("Mouse Y") * lookspeed;
        float rotationX = Input.GetAxisRaw("Mouse X") * lookspeed;

        deltaLook = new Vector3(rotationX, rotationY, 0);
        return deltaLook;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            myManager.playerHealth--;
        }

        if (collision.gameObject.tag == "Health" && myManager.playerHealth < 3)
        {
            myManager.playerHealth++;
            Destroy(collision.gameObject);
        }
    }
}
