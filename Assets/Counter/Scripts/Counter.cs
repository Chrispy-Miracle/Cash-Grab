using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{

    public Text billCounterText;
    public Text cashCounterText;
    public TextMeshProUGUI latestGrabText;
    public TextMeshProUGUI timerText;
    
    
    
    public GameObject gameManager;
    private GameManager gameManagerScript;

    private ParticleSystem greenExplosion;
    
    public int count = 0;
    public int moneyTotal = 0;
    private float timer = 30;
    private int[] dollarTypeTotals = {0,0,0,0,0,0,0};


    private void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        greenExplosion = GameObject.Find("Explosion_Green").GetComponent<ParticleSystem>();
    }


    private void Update() {
        if (gameManagerScript.gameIsActive) {
            HandleTimer();
        } 
    }


    public void UpdateCounter(int dollarValue) {
        count++;
        moneyTotal += dollarValue;
        greenExplosion.Play();

        HandleEndGameTotals(dollarValue);

        latestGrabText.text = $"+ {dollarValue}";
        billCounterText.text = $"Bills collected: {count}";
        cashCounterText.text = $"Total Cash:\n${moneyTotal}";
    }


    void HandleTimer() {
        timer -= Time.deltaTime;

        // format extra 0 if seconds left is 10
        string seconds = Mathf.Round(timer) >= 10 ? $"{Mathf.Round(timer)}" : $"0{Mathf.Round(timer)}";
        timerText.text = $"00:{seconds}";

        // end game when time is up
        if (timer <= 0) {
            gameManagerScript.EndGame(dollarTypeTotals);
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
