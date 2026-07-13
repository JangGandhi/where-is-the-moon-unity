using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Rigidbody rigid;
    bool isJumpPressed;
    Vector3 vec;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        // rigid.linearVelocity = new Vector3(1, 0, 0); // 해당 방향을 향해 손으로 한 번 툭
        // rigid.AddForce(Vector3.up * 10, ForceMode.Impulse); // 무게(Mass)에 영향을 받으며 순간적인 힘
        isJumpPressed = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }
        vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    void FixedUpdate()
    {
        // rigid.linearVelocity = new Vector3(1, 0, 0); // 해당 방향을 향해 무한 동력 제트 엔진 가동
        // rigid.AddForce(Vector3.up * 9.81f, ForceMode.Force); // ForceMode를 생략하면 기본 값으로 ForceMode.Force가 실행, 무게(Mass)에 영향을 받으며 지속적인 힘
        if (isJumpPressed)
        {
            isJumpPressed = false;
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            Debug.Log(rigid.linearVelocity);
        }
        rigid.AddForce(vec * 5);
    }
}
