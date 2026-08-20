using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform playerTransform;
    Vector3 offset;
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }
    void LateUpdate() // 업데이트를 마치고 실행
    {
        transform.position = playerTransform.position + offset;
    }
}
