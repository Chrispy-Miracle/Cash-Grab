using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // dollar prefabs and dollar script
    public List<GameObject> dollars; 
    private Dollar dollarScript;

    // UI screeens
    public GameObject titleScreen;
    public GameObject countdownScreen;
    public GameObject gameUIScreen;
    public GameObject endGameScreen;

    // start of game countdown
    public TextMeshProUGUI countdownText;
    private AudioSource countdownAudioSource;
    public AudioClip countdownBeep;
    private float countdownTimer = 5;
    public bool countdownActive = false;

    // end of game stats
    public TextMeshProUGUI gameStatsOnes;
    public TextMeshProUGUI gameStatsTwos;
    public TextMeshProUGUI gameStatsFives;
    public TextMeshProUGUI gameStatsTens;
    public TextMeshProUGUI gameStatsTwenties;
    public TextMeshProUGUI gameStatsFifties;
    public TextMeshProUGUI gameStatsHundreds;
    public TextMeshProUGUI gameStatsTotals;

    // game state bool
    public bool gameIsActive = false;

    // audiosource for fan
    private AudioSource fanAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        countdownAudioSource = GetComponent<AudioSource>(); // for countdown beep
        fanAudioSource = GameObject.Find("FanAudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCountdown();
    }

    // Countdown starts with "Start" button
    public void StartCountdown() {
        titleScreen.SetActive(false);
        countdownScreen.SetActive(true);
        countdownActive = true;    
        StartCoroutine(PlayCountdownBeep());
        fanAudioSource.PlayOneShot(fanAudioSource.clip);  
    }

    void HandleCountdown() {
        if (countdownActive) {
            // countdown and update display
            countdownTimer -= Time.deltaTime;
            countdownText.text = $"{Mathf.Round(countdownTimer)}";
            
            // start game if countdown finished
            if (countdownTimer <= 0) {
                countdownActive = false;
                StartGame();
                
            }
        }
    }

    // coroutine called in "StartCountdown"
    IEnumerator PlayCountdownBeep() {
        while (countdownActive) {
            countdownAudioSource.PlayOneShot(countdownBeep);
            yield return new WaitForSeconds(1);
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
            int numberOfDollars = 100 / dollarScript.dollarValue; 
            
            // spawn each type of dollar (i.e. 100 ones, 50 twos... 2 Fifties, 1 Hundred)
            for (int i = 0; i < numberOfDollars; i++) {
                Vector3 randomLocation = new Vector3(Random.Range(-2f, 2f), 8.0f, Random.Range(-2f, 2f));
                Instantiate(dollar, randomLocation, dollar.transform.rotation);
            }
        }
    }

    public void EndGame(int[] dollarTypeTotals) {
        gameIsActive = false;

        gameUIScreen.SetActive(false);
        endGameScreen.SetActive(true);

        FormatEndGameStats(dollarTypeTotals);
    }

    // called by end game screen's "play again" button
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void FormatEndGameStats(int[] dollarTypeTotals) {
        int numberOfBills = 0;

        foreach (int dollarTypeTotal in dollarTypeTotals) {
            numberOfBills += dollarTypeTotal;
        }

        int ones = dollarTypeTotals[0];
        int twos = dollarTypeTotals[1] * 2;
        int fives = dollarTypeTotals[2] * 5;
        int tens = dollarTypeTotals[3] * 10;
        int twenties = dollarTypeTotals[4] * 20;
        int fifties = dollarTypeTotals[5] * 50;
        int hundreds = dollarTypeTotals[6] * 100;

        int grandTotal = ones + twos + fives + tens + twenties + fifties + hundreds;

        gameStatsOnes.text = $"Ones\n{dollarTypeTotals[0]}/100\n${ones}";
        gameStatsTwos.text = $"Twos\n{dollarTypeTotals[1]}/50\n${twos}";
        gameStatsFives.text = $"Fives\n{dollarTypeTotals[2]}/20\n${fives}";
        gameStatsTens.text = $"Tens\n{dollarTypeTotals[3]}/10\n${tens}";
        gameStatsTwenties.text = $"Twenties\n{dollarTypeTotals[4]}/5\n${twenties}";
        gameStatsFifties.text = $"Fifties\n{dollarTypeTotals[5]}/2\n${fifties}";
        gameStatsHundreds.text = $"Hundreds\n{dollarTypeTotals[6]}/1\n${hundreds}";
        gameStatsTotals.text = $"Total Bills: {numberOfBills}     Cash Total:  ${grandTotal}";
    }
}
