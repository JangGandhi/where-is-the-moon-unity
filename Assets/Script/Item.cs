using NUnit.Framework.Constraints;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 60.0f;
    AudioSource audio;
    void Awake() // Awake는 내부 자원을 요구하는 준비 작업, Start는 외부 자원을 요구하는 준비 작업
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}