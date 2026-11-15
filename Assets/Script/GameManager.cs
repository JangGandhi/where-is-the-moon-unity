using System.Runtime.Serialization;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalItemCount;
    public int stageNumber;
    public int score;
    public TMP_Text scoreValue;

    void Update()
    {
        scoreValue.text = $"{score:000000}";
    }
    public void GetItem(string tag)
    {
        switch (tag)
        {
            case "Coin":
                {
                    score += 3000;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(stageNumber - 1); // Build Scene - Scene List에서 할당된 번호로도 선택 가능
        }
    }
}
