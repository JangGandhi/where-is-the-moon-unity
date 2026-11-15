using NUnit.Framework.Constraints;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 60.0f;
    
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}