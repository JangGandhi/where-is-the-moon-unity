using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalItemCount;
    public int stageNumber;
    public TMP_Text playerCoinCount;
    public TMP_Text totalCoinCount;

    void Awake()
    {
        totalCoinCount.text = totalItemCount.ToString("00");
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
