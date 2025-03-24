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

    // Start is called before the first frame update
    void Start()
    {
        myManager = FindObjectOfType<GameManager>();

        RB = GetComponent<Rigidbody>();
        lookDirection = Vector3.zero;
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
