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

    private string theme;
    private GameObject[] words;
    private ArrayList playedIndexes;
    private string currentWord;

    public int score;
    public float startTime = 10f;
    private float currentTime;
    private float timeMillis;

    public bool isPlaying = false;
    public bool gameOver = false;

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
        playedIndexes = new ArrayList();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isPlaying)
        {
            currentTime -= Time.deltaTime;
            timeMillis -= Time.deltaTime * 1000;
            if (timeMillis < 0)
                timeMillis = 1000f;
            //Debug.Log(currentTime);
            timeText.text = (((int)currentTime) / 60).ToString("00") + ":" + (((int)currentTime) % 60).ToString("00") + ":" + ((int)(timeMillis * 60 / 1000)).ToString("00");

            

        }
	}

    public void StartGame()
    {
        scoreText.text = score.ToString();
        timeText.text = (((int)startTime) / 60).ToString("00") + ":" + (((int)startTime) % 60).ToString("00") + ":60";

        theme = "Food"; // choose theme from theme database when available
        themeText.text = theme;

        words = GameObject.FindGameObjectsWithTag("Food");

        int[] indexes = getNextRandomIndexes();

        image1.SetActive(true);
        image1.GetComponent<Image>().sprite = words[indexes[0]].GetComponent<Image>().sprite;
        image2.SetActive(true);
        image2.GetComponent<Image>().sprite = words[indexes[1]].GetComponent<Image>().sprite;
        image3.SetActive(true);
        image3.GetComponent<Image>().sprite = words[indexes[2]].GetComponent<Image>().sprite;

        currentWord = words[indexes[(int)(Random.value * 2)]].name;
        wordText.text = currentWord;

        isPlaying = true;
    }

    public int[] getNextRandomIndexes()
    {
        int[] numbers = new int[3];

        for (int i = 0; i < numbers.Length; i++)
        {
            int number;

            while (playedIndexes.Contains(number = (int)(Random.value * 11))){}

            playedIndexes.Add(number);
            numbers[i] = number;
        }

        return numbers;
    }

    
}
