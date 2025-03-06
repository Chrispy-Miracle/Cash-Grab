using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{    
    public GameObject gameManager;
    private GameManager gameManagerScript;
    private float timeLeft = 30;

    // UI 
    public Text billCounterText;
    public TextMeshProUGUI cashCounterText;
    public TextMeshProUGUI latestGrabText;
    public TextMeshProUGUI timerText;
    
    // particles
    private ParticleSystem greenExplosionLeft;
    private ParticleSystem greenExplosionRight;
    
    // counters
    private int billCount = 0;
    private int moneyTotal = 0;
    private int[] dollarTypeTotals = {0,0,0,0,0,0,0}; // number of bills of each type: {1s,2s,5s,10s,20s,50s,100s}


    private void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();

        // particles
        greenExplosionLeft = GameObject.Find("Explosion_Green_Left").GetComponent<ParticleSystem>();
        greenExplosionRight = GameObject.Find("Explosion_Green_Right").GetComponent<ParticleSystem>();
    }


    private void Update() {
        if (gameManagerScript.gameIsActive) {
            HandleTimer();
        } 
    }

    // called in Dollar.cs upon dollar being clicked
    public void UpdateCounter(int dollarValue) {
        // counters
        billCount++;
        moneyTotal += dollarValue;
        HandleEndGameTotals(dollarValue);

        // UI
        latestGrabText.text = $"+ {dollarValue}";
        billCounterText.text = $"{billCount}  Bills collected";
        cashCounterText.text = $"${moneyTotal}";
        
        // particles
        greenExplosionLeft.Play();
        greenExplosionRight.Play();
    }


    void HandleTimer() {
        timeLeft -= Time.deltaTime;

        // format extra 0 if seconds left is 10
        string seconds = Mathf.Round(timeLeft) >= 10 ? $"{Mathf.Round(timeLeft)}" : $"0{Mathf.Round(timeLeft)}";
        timerText.text = $"00:{seconds}";

        // end game when time is up
        if (timeLeft <= 0) {
            gameManagerScript.EndGame(dollarTypeTotals, billCount, moneyTotal);
        }
    }


    void HandleEndGameTotals(int dollarValue) {
        switch (dollarValue) {
            case 1:
                dollarTypeTotals[0]++;
                break;
            case 2:
                dollarTypeTotals[1]++;
                break;
            case 5:
                dollarTypeTotals[2]++;
                break;
            case 10:
                dollarTypeTotals[3]++;
                break;
            case 20:
                dollarTypeTotals[4]++;
                break;
            case 50:
                dollarTypeTotals[5]++;
                break;
            case 100:
                dollarTypeTotals[6]++;
                break;
            default:
                Debug.Log("something broke");
                break;
        }
    }
}
