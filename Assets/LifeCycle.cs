using System.Buffers.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LifeCycle : MonoBehaviour
{
    void Start()
    {
        // Vector3 vec = new Vector3(0, 0, 0); // 주문서
        // transform.Translate(vec); // 주방에 주문 넣기
    }
    void Update()
    {
        // if (Input.GetButton("Horizontal"))
        // {
        //     // Debug.Log("Horizon은 수평선, 왼쪽과 오른쪽을 의미합니다.
        //     // Debug.Log($"출력 값은 버튼을 누른 시간과 비례합니다: {Input.GetAxis("Horizontal")}");
        //     // Debug.Log($"출력 값은 버튼을 누른 시간과 상관없이 무조건 1을 가집니다: {Input.GetAxisRaw("Horizontal")}");
        //     // 왼쪽 버튼과 오른쪽 버튼을 동시에 누를 경우 출력 값은 0입니다.
        //     // 즉, GetAxix는 부드러운 이동, GetAxixRow는 즉시 이동이라고 할 수 있습니다.
        //     // Axis는 축, Raw는 가공되지 않은 날것의 신호를 의미합니다.            

        //     switch (Input.GetAxisRaw("Horizontal"))
        //     {
        //         case -1:
        //             Debug.Log("왼쪽으로 이동 중...");
        //             break;
        //         case 1:
        //             Debug.Log("오른쪽으로 이동 중...");
        //             break;
        //         default:
        //             Debug.Log("Horizontal Return 0");
        //             break;
        //     }
        // }
        // if (Input.GetButton("Vertical"))
        // {
        //     switch (Input.GetAxisRaw("Vertical"))
        //     {
        //         case -1:
        //             Debug.Log("아래쪽으로 이동 중...");
        //             break;
        //         case 1:
        //             Debug.Log("위쪽으로 이동 중...");
        //             break;
        //         default:
        //             Debug.Log("Vertical Return 0");
        //             break;
        //     }
        // }

        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal") / 10.0f, Input.GetAxisRaw("Vertical")/10.0f, 0);
        transform.Translate(vec);
    }
}