using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class UImanager : MonoBehaviour
{
    public Image[] hearts;
    public Image life1;
    public Image life2;
    public Image life3;

    public Image CoinCollected;

    public int cons;
    public int collcons = 3;

    public TextMeshProUGUI Text;
    public float count;
    private float endTimer = 10;

    
   

    
    void Update()
    {
        UpdateHealthUI();
        Timer();
        CoinColecter();
    }

    public void UpdateHealthUI()
    {
        int remainingHearts = GameManager.Instance.lives;

        // Update individual heart images based on the remaining lives
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < remainingHearts)
            {
                hearts[i].gameObject.SetActive(true); // Show the heart
            }
            else
            {
                hearts[i].gameObject.SetActive(false); // Hide the heart
            }
        }

        // Update individual life indicator images based on the remaining lives
        life1.gameObject.SetActive(remainingHearts >= 1);
        life2.gameObject.SetActive(remainingHearts >= 2);
        life3.gameObject.SetActive(remainingHearts >= 3);
    }
    public void Timer()
    {
        count += Time.deltaTime;
        Text.text = count.ToString("0");
        
        if(count == endTimer)
        {
            Debug.Log(count+"TimeOut!!");
        }
    }
    public void CoinColecter()
    {
       cons = GameManager.Instance.coins ;
       Debug.Log(cons);
       if(cons == collcons)
       {
            CoinCollected.gameObject.SetActive(true);
       }
    }

}
