using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Dollar dollarScript;

    private float countdownTimer = 5;

    // game state bools
    public bool gameIsActive = false;
    public bool countdownActive = false;

    // dollar prefabs
    public List<GameObject> dollars; 

    // audio
    private AudioSource fanAudioSource;
    private AudioSource countdownAudioSource;
    
    // UI screens (this will allow toggling)
    public GameObject titleScreen;
    public GameObject countdownScreen;
    public GameObject gameUIScreen;
    public GameObject endGameScreen;

    // UI for start of game countdown
    public TextMeshProUGUI countdownText;
    
    // UI for end of game stats 
    public TextMeshProUGUI gameStatsOnes;
    public TextMeshProUGUI gameStatsTwos;
    public TextMeshProUGUI gameStatsFives;
    public TextMeshProUGUI gameStatsTens;
    public TextMeshProUGUI gameStatsTwenties;
    public TextMeshProUGUI gameStatsFifties;
    public TextMeshProUGUI gameStatsHundreds;
    public TextMeshProUGUI gameStatsTotals;


    // Start is called before the first frame update
    void Start()
    {
        // get audio for countdown beep and fan
        countdownAudioSource = GameObject.Find("CountdownAudioSource").GetComponent<AudioSource>(); 
        fanAudioSource = GameObject.Find("FanAudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownActive) {
            HandleCountdown();
        }
    }

    // Countdown starts with "Start" button
    public void StartCountdown() {
        countdownActive = true;
        // toggle UI screens
        titleScreen.SetActive(false);
        countdownScreen.SetActive(true);
        // play audio   
        StartCoroutine(PlayCountdownBeep());
        fanAudioSource.PlayOneShot(fanAudioSource.clip);  
    }


    IEnumerator PlayCountdownBeep() {
        while (countdownActive) {
            countdownAudioSource.PlayOneShot(countdownAudioSource.clip);
            yield return new WaitForSeconds(1);
        }
    }

    void HandleCountdown() {
        // countdown and update display
        countdownTimer -= Time.deltaTime;
        countdownText.text = $"{Mathf.Round(countdownTimer)}";
        
        // start game if countdown finished
        if (countdownTimer <= 0) {
            countdownActive = false;
            StartGame();
        } 
    }


    // called at end of countdown
    public void StartGame() {
        gameIsActive = true;
        countdownScreen.SetActive(false);
        gameUIScreen.SetActive(true);
        SpawnDollars();
    }


    void SpawnDollars() {
        foreach (GameObject dollar in dollars) {
            dollarScript = dollar.GetComponent<Dollar>();
            // get 100 ones, 50 twos.... 2 Fifties, 1 Hundred
            int numberOfDollars = 100 / dollarScript.dollarValue; 
            
            // spawn each type of dollar 
            for (int i = 0; i < numberOfDollars; i++) {
                Vector3 randomLocation = new Vector3(Random.Range(-2f, 2f), 8.0f, Random.Range(-2f, 2f));
                Instantiate(dollar, randomLocation, dollar.transform.rotation);
            }
        }
    }


    // called in Counter.cs
    public void EndGame(int[] dollarTypeTotals, int billCount, int moneyTotal) {
        gameIsActive = false;
        FormatEndGameStats(dollarTypeTotals, billCount, moneyTotal);

        // toggle UI screens
        gameUIScreen.SetActive(false);
        endGameScreen.SetActive(true);   
    }


    private void FormatEndGameStats(int[] dollarTypeTotals, int billCount, int moneyTotal) {
        //  UI text for endgame stats
        gameStatsOnes.text = $"Ones\n{dollarTypeTotals[0]}/100\n${dollarTypeTotals[0]}";
        gameStatsTwos.text = $"Twos\n{dollarTypeTotals[1]}/50\n${dollarTypeTotals[1] * 2}";
        gameStatsFives.text = $"Fives\n{dollarTypeTotals[2]}/20\n${dollarTypeTotals[2] * 5}";
        gameStatsTens.text = $"Tens\n{dollarTypeTotals[3]}/10\n${dollarTypeTotals[3] * 10}";
        gameStatsTwenties.text = $"Twenties\n{dollarTypeTotals[4]}/5\n${dollarTypeTotals[4] * 20}";
        gameStatsFifties.text = $"Fifties\n{dollarTypeTotals[5]}/2\n${dollarTypeTotals[5] * 50}";
        gameStatsHundreds.text = $"Hundreds\n{dollarTypeTotals[6]}/1\n${dollarTypeTotals[6] * 100}";
        gameStatsTotals.text = $"Total Bills: {billCount}     Cash Total:  ${moneyTotal}";
    }


    // called by clicking end game screen's "play again" button 
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
