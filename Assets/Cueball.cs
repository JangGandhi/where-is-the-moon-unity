using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Rigidbody rigid;
    bool isJumpPressed;
    float h;
    float v;
    [SerializeField] private float weight = 1; // 직렬화 SerializeField
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        isJumpPressed = false;
    }
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        // if (Input.GetButtonDown("Jump"))
        // {
        //     isJumpPressed = true;
        // }
    }
    void FixedUpdate()
    {
        // rigid.AddTorque(Vector3.up); // 왼손 법칙
        // if (isJumpPressed)
        // {
        //     isJumpPressed = false;
        //     rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
        // }
        rigid.AddForce(Vector3.forward * v * weight);
        rigid.AddForce(Vector3.right * h * weight);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Triggercube")
        {
            rigid.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
        }
    }

    // OnTrigger(Collider other) == 어떤 녀석이 영역 안으로 넘어왔냐? other가 어떤 녀석
    // OnCollision(Collision collision) == 방금 무슨 사고가 터진 거냐? collision이 사고 보고서

    public void Jump()
    {
        rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }
}