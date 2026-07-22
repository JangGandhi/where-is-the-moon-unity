using System;
using UnityEngine;

public class Objectball : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material material;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    // 이벤트 함수는 대부분 On으로 시작
    void OnCollisionEnter(Collision collision) // 물리적 충돌이 시작될 때 호출되는 함수
    {
        if (collision.gameObject.name == "Cueball")
        {
            material.color = new Color(0, 0, 0);
        }
    }

    // void OnCollisionStay(Collision collision) // 물리적 충돌이 진행되고 있을 때 호출되는 함수
    // {
        
    // }

    void OnCollisionExit(Collision collision) // 물리적 충돌이 끝날 때 호출되는 함수
    {
        if (collision.gameObject.name == "Cueball")
        {
            material.color = new Color(1, 1, 1);
        }
    }
}
