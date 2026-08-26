using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Nebula : MonoBehaviour
{
    Transform transform;
    Material material;
    Color color;
    void Start()
    {
        transform = GetComponent<MeshRenderer>().transform;
        material = GetComponent<MeshRenderer>().material;
        color = material.GetColor("_Color");
    }

    void Update()
    {
        Vector3 rotateLength = new Vector3(0, 0.4f, 0);
        transform.Rotate(rotateLength * Time.deltaTime);

        color.a = (Mathf.PingPong(Time.time * 4.0f, 40.0f) + 2.0f) / 255.0f;
        material.SetColor("_Color", color);
    }
}
