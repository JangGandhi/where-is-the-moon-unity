using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Rigidbody rigid;
    bool isJumpPressed;
    float h;
    float v;
    [SerializeField] private float weight = 715; // 직렬화 SerializeField
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        isJumpPressed = false;
    }
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }
    }
    void FixedUpdate()
    {
        // rigid.AddTorque(Vector3.up); // 왼손 법칙
        if (isJumpPressed)
        {
            isJumpPressed = false;
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        rigid.AddTorque(Vector3.back * h * weight);
        rigid.AddTorque(Vector3.right * v * weight);
    }
}