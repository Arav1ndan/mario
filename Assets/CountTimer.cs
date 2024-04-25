using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CountTimer : MonoBehaviour
{
    private float timer = 0;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        Debug.Log(timer);
        
    }
    private void Timer()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("0");
        
    }
}
