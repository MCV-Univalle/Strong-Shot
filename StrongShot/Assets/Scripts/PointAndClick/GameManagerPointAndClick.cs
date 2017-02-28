using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerPointAndClick : MonoBehaviour
{
    public static GameManagerPointAndClick gmpac;

    public Text scoreText;
    public Text themeText;
    public Text wordText;
    public Text timeText;
    public GameObject gameOverPanel;
    public Text finalScoreText;

    private string theme;
    private GameObject[] words;
    private int wordCount;
    private ArrayList nonPlayedIndexes;
    private ArrayList playedIndexes;
    private string currentWord;

    public int score;
    public float startTime = 10f;
    private float currentTime;
    private float timeMillis;

    public bool isPlaying = false;
    public bool isGameOver = false;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    // Use this for initialization
    void Start ()
    {
        if (!gmpac)
            gmpac = gameObject.GetComponent<GameManagerPointAndClick>();

        currentTime = startTime;
        timeMillis = 1000f;

        nonPlayedIndexes = new ArrayList();
        playedIndexes = new ArrayList();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isPlaying)
        {
            currentTime -= Time.deltaTime;

            if (currentTime >= 0)
            {
                timeMillis -= Time.deltaTime * 1000;
                if (timeMillis < 0)
                    timeMillis = 1000f;
                //Debug.Log(currentTime);
                timeText.text = (((int)currentTime) / 60).ToString("00") + ":" + (((int)currentTime) % 60).ToString("00") + ":" + ((int)(timeMillis * 60 / 1000)).ToString("00");
            }
            else
            {
                isPlaying = false;
                isGameOver = true;
                timeText.text = "00:00:00";
            }

        }
        else if (isGameOver)
        {
            GameOver();
            //RestartGame();
        }
	}

    public void StartGame()
    {
        scoreText.text = score.ToString();
        timeText.text = (((int)startTime) / 60).ToString("00") + ":" + (((int)startTime) % 60).ToString("00") + ":60";

        theme = "Food"; // choose theme from theme database when available
        themeText.text = theme;

        words = GameObject.FindGameObjectsWithTag(theme);
        wordCount = words.Length;
        InitializeIndexes();
        SetWords();

        gameOverPanel.SetActive(false);

        isPlaying = true;
    }

    public void VerifySelection(string word)
    {
        if (word == currentWord)
        {
            score += 1;
            UpdateScore();
        }

        if (playedIndexes.Count < words.Length)
        {
            SetWords();
        }
        else
        {
            isPlaying = false;
            isGameOver = true;
        }
    }

    public void SetWords()
    {
        int[] indexes = GetNextRandomIndexes();

        image1.SetActive(true);
        image1.name = words[indexes[0]].name;
        image1.GetComponent<Image>().sprite = words[indexes[0]].GetComponent<Image>().sprite;

        image2.SetActive(true);
        image2.name = words[indexes[1]].name;
        image2.GetComponent<Image>().sprite = words[indexes[1]].GetComponent<Image>().sprite;

        image3.SetActive(true);
        image3.name = words[indexes[2]].name;
        image3.GetComponent<Image>().sprite = words[indexes[2]].GetComponent<Image>().sprite;

        currentWord = words[indexes[(int)(Random.Range(0, 3))]].name;
        wordText.text = currentWord;
    }

    public int[] GetNextRandomIndexes()
    {
        int numberOfCards = 3;

        int[] numbers = new int[numberOfCards];

        if (nonPlayedIndexes.Count > numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                int number;

                while (playedIndexes.Contains(number = (int)(Random.Range(0, wordCount)))) { }

                nonPlayedIndexes.Remove(number);
                playedIndexes.Add(number);
                numbers[i] = number;
            }
        }
        else if (nonPlayedIndexes.Count == numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                int number = (int)nonPlayedIndexes[i];
                
                playedIndexes.Add(number);
                numbers[i] = number;
            }

            nonPlayedIndexes.Clear();
        }
        else
        {
            Debug.Log("Number of words is not a multiple of the number of cards displayed");
        }      

        return numbers;
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void InitializeIndexes()
    {
        for (int i = 0; i < wordCount; i++)
        {
            nonPlayedIndexes.Add(i);
        }
    }

    public void GameOver()
    {
        finalScoreText.text = score.ToString();
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        currentTime = startTime;
        timeMillis = 1000f;
        score = 0;

        nonPlayedIndexes.Clear();
        playedIndexes.Clear();

        StartGame();
    }
}
