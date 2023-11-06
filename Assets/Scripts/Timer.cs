using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    
    public TextMeshProUGUI timeText;
    
    [Header("Reward")]
    public WheelSkid wheelSkid;

    public GameObject rewardScreen;
    public int moneyAmount;
    public TextMeshProUGUI moneyText;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timeRemaining *= 60;
    }
    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                rewardScreen.SetActive(true);
                moneyAmount = wheelSkid.pointsAmount / 10;
                moneyText.text = $"Money Collected: {moneyAmount}";
            }
        }
    }

    public void CollectMoney()
    {
        var money = PlayerPrefs.GetInt("MoneyAmount");
        moneyAmount = money + moneyAmount;
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        Debug.Log($"Collected Money: {moneyAmount}");
    }
    
    public void DoubleMoney()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.init ("YOUR_APP_KEY", IronSourceAdUnits.REWARDED_VIDEO);
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
        }
        moneyAmount *= 2;
        moneyText.text = $"Money Collected: {moneyAmount}";
    }
    
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}