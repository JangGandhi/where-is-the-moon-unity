using System;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Scene을 관리하는 라이브러리

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    Vector3 directionVector;
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float jumpSpeed = 9.0f;
    [SerializeField] private int playerItemCount = 0;
    [SerializeField] private GameManager manager;
    bool isJump = false;
    bool isFlight = false;
    AudioSource audio;
    Transform camTransform;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector3 camFoward = camTransform.forward;
        Vector3 camRight = camTransform.right;
        camFoward.y = 0.0f;
        camRight.y = 0.0f;
        camFoward.Normalize();
        camRight.Normalize();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        directionVector = ((camFoward * v) + (camRight * h)).normalized;

        if (!isFlight && Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        rigid.AddForce(directionVector * moveSpeed);

        if (isJump)
        {
            isJump = false;
            rigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isFlight = true;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isFlight = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            playerItemCount++;
            audio.Play();
            manager.GetItem(playerItemCount);
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Finish")
        {
            // Find 계열 함수는 부하가 꽤 있으므로 피하는 것을 권장
            if (playerItemCount == manager.totalItemCount)
            {
                Debug.Log($"Stage{manager.stageNumber} Clear!");
                SceneManager.LoadScene("Stage" + (manager.stageNumber + 1));
                other.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Game Over..");
                SceneManager.LoadScene("Stage" + manager.stageNumber);
            }
        }
    }
}