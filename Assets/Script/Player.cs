using System;
using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using Unity.Mathematics;
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

    public bool isJump = false;
    public bool isFlight = false;

    [SerializeField] private AudioSource getAudioSource;
    [SerializeField] private AudioClip coinGet;

    [SerializeField] private AudioSource rollingAudioSource;
    [SerializeField] private AudioSource impactAudioSource;
    [SerializeField] private AudioClip stoneRolling;
    [SerializeField] private AudioClip stoneImpact;
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
            float playerVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z).magnitude / 17.64f;
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
                    float rollingVolume = Mathf.Clamp01(playerVelocity / 2.0f);
                    rollingAudioSource.volume = rollingVolume;
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Finish"))
        {
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            manager.GetItem(collider.gameObject.tag);
            switch (collider.tag)
            {
                case "Coin":
                    {
                        getAudioSource.PlayOneShot(coinGet);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            collider.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string collisionLayerName = LayerMask.LayerToName(collision.gameObject.layer);
        switch (collisionLayerName)
        {
            case "Platform":
                {
                    // 사용자가 Jump 했을 때의 충격량을 1로 계산할 수 있도록 14.43으로 나누어 보정
                    float impactPower = collision.impulse.magnitude / 14.43f;
                    // 충격량이 1일 때 충격음의 음량이 0.6이 되도록 보정 후 반영
                    float impactVolume = Mathf.Clamp01(impactPower * 0.6f);
                    impactAudioSource.volume = impactVolume;

                    switch (collision.gameObject.tag)
                    {
                        case "Stone":
                            {
                                impactAudioSource.PlayOneShot(stoneImpact);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}