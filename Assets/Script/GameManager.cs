using System.Runtime.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalItemCount;
    public int stageNumber;
    public TMP_Text playerCoinCount;
    public TMP_Text totalCoinCount;
    private Material skyboxMaterial;
    [SerializeField] private float skyboxRotationSpeed = 1.2f;


    void Awake()
    {
        totalCoinCount.text = totalItemCount.ToString("00");
    }

    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
    }
    void Update()
    {
        if (skyboxMaterial != null)
        {
            float skyboxCurrentRotation = skyboxMaterial.GetFloat("_Rotation");
            skyboxMaterial.SetFloat("_Rotation", skyboxCurrentRotation + skyboxRotationSpeed * Time.deltaTime);
        }
    }
    public void GetItem(int count)
    {
        playerCoinCount.text = count.ToString("00");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(stageNumber - 1); // Build Scene - Scene List에서 할당된 번호로도 선택 가능
        }
    }
}
