using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    Vector3 target = new Vector3(2, 0.5f, 0);
    Vector3 velo = Vector3.up;
    void Update()
    {
        // MoveTowards
        // MoveTowards는 매개변수로 현재 위치, 목표 위치, 속도를 받습니다.
        // 단순 등속 이동으로, 매 프레임마다 일정한 속도를 유지하며 목적지까지 이동합니다.
        // transform.position = Vector3.MoveTowards(transform.position, target, 0.01f);

        // SmoothDamp
        // SmoothDamp는 매개변수로 현재 위치, 목표 위치, 현재 속도(참조 Vector), 예상 도착 시간을 받으며
        // 감속 이동으로, 목적지가 멀면 서서히 가속하고 가까우면 서서히 감속합니다.
        // transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 10.0f);

        // Lerp
        // Lerp는 매개변수로 현재 위치, 목표 위치, 비율을 받습니다.
        // 선형 보간 이동으로, 매 프레임마다 남은 거리의 일정 비율만큼 이동하여 빠른 속도로 출발하며 목적지가 가까워질 수록 느려집니다.
        // transform.position = Vector3.Lerp(transform.position, target, 0.01f);

        // Slerp
        // Slerp는 매개변수로 현재 위치, 목표 위치, 비율을 받습니다.
        // 구면 선형 보간 이동으로 속도는 Lerp와 동일하게 작동하지만, 원점(0, 0, 0)을 중심축 삼아 현재 위치에서 목적지까지 부드러운 호를 그리며 회전하듯이 이동합니다.
        // 원점에서 가까우면 가파르게, 원점에서 멀면 완만하게
        // transform.position = Vector3.Slerp(transform.position, target, 0.01f);

        // deltaTime
        // 이전 프레임 완료까지 걸린 시간
        // 프레임이 낮을 경우 deltaTime은 높은 값을, 프레임이 높을 수록 deltaTime은 낮은 값을 가집니다.
        // 프레임이 높으면 낚시가 쉬워지고 프레임이 낮으면 낚시가 어려워지는 불상사를 막기 위해(프레임률 차이로 생기는 계산 횟수를 보정하기 위해) 사용합니다.
        // Translate : 벡터에 곱하기
        // transform.Translate(vec * Time.deltaTime);
        // Vector 함수 : 시간 매개변수에 곱하기
        // transform.position = Vector3.Lerp(transform.position, target, 0.01f * Time.deltaTime);
        // 프레임 독립인 SmoothDamp를 제외하면 대부분 프레임에 비례하여 계산 횟수가 늘어나기에 프레임에 영향을 받는 이동일 경우 deltaTime을 반드시 곱해야 합니다.
        // deltaTime은 낮은 값이므로 별도의 speed로 속도를 한 번 더 보정해주는 게 일반적입니다.
        float speed = 60.0f;
        transform.position = Vector3.Lerp(transform.position, target, 0.01f * Time.deltaTime * speed);
        // 초당 60 프레임을 유지하고 있다면 deltaTime은 1/60입니다.
    }
}