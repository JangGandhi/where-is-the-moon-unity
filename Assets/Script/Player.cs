using System;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Scene을 관리하는 라이브러리

public class Player : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector3 directionVector;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private int playerItemCount;

    public bool isJump = false;
    public bool isFlight = false;

    [SerializeField] private AudioSource rollingAudioSource;
    [SerializeField] private AudioClip stoneRolling;
    bool isRolling = false;

    Transform camTransform;

    [SerializeField] private GameManager manager;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camTransform = Camera.main.transform;
    }

    void Update()
    {
        // 카메라가 바라보는 방향 계산
        Vector3 camFoward = camTransform.forward;
        Vector3 camRight = camTransform.right;
        camFoward.y = 0.0f;
        camRight.y = 0.0f;
        camFoward.Normalize();
        camRight.Normalize();

        // 키보드 입력에 카메라가 바라보는 방향을 보정해서 Player 이동
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        directionVector = ((camFoward * v) + (camRight * h)).normalized;

        if (Physics.Raycast(rigidbody.position, Vector3.down, 1, LayerMask.GetMask("Platform"))) // Player가 Platform 위에 있으면
        {
            isFlight = false;
            float playerVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z).magnitude;
            if (playerVelocity > 0.5f) // Player가 Platform 위에서 움직이면
            {
                if (!isRolling) // 발소리 재생
                {
                    isRolling = true;
                    rollingAudioSource.clip = stoneRolling;
                    rollingAudioSource.Play();
                }
                else // Player의 속도에 비례한 발소리 음량 조절
                {
                    rollingAudioSource.volume = (playerVelocity / 17.64f) / 2.0f;
                }
            }
            else
            {
                rollingAudioSource.Stop();
                isRolling = false;
            }
        }
        else
        {
            isFlight = true;
            rollingAudioSource.Stop();
            isRolling = false;
        }

        if (!isFlight && Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(directionVector * moveSpeed);

        if (isJump)
        {
            isJump = false;
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            playerItemCount++;
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