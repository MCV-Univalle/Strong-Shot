using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour {

    public Text[] buttonMatrix;
    private char[] allLetters;
    private int counterOfLetters;
    private int[] freqOfLetters;
    string letter;
    private string LettersSelected;
    string[] selectedWords;
    public Text WordsToFind;
	private Button[] selectedButtons;
	private int buttonIndexCounter;
	public GameObject gameOverPanel;
	public Text gameOverText;
    public int numberOfWords;
    private AnimationController unityChan;



    private string[] abc = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    private string[] colors = {"RED", "BLUE", "BLACK", "WHITE", "YELLOW", "GREEN", "PURPLE", "PINK", "BROWN", "ORANGE" };

    void SetGameControllerReferenceOnButtonsInitialWords()
    {
        for (int i = 0; i < buttonMatrix.Length; i++)
        {
            buttonMatrix[i].GetComponentInParent<ButtonScript>().SetGameControllerReference(this);
        }
    }

    void Start()
    {
        unityChan = GameObject.Find("unitychan").GetComponent<AnimationController>();
        LettersSelected = "";
		buttonIndexCounter = 0;
		selectedButtons = new Button[56];
		gameOverPanel.SetActive (false);

        DivideWords();
        if(counterOfLetters <= 56)
        {
            SetGameControllerReferenceOnButtonsInitialWords();
        }
        WriteLetterRandomly();
		FillRemainingButtons ();
        CurrentWords();
    }

    void DivideWords()
    {
        System.Random r = new System.Random();    
        int pos;
        selectedWords = new string[numberOfWords];
        int[] freqOfWords = new int[numberOfWords];
        for (int i = 0; i < freqOfWords.Length; i++)
        {
            freqOfWords[i] = 0;
        }
        int h = 0;
        while(!AreAllPositioned(freqOfWords))
        {
            pos = r.Next(10);
            if(!IsWordInArray(selectedWords, colors[pos]))
            {
                selectedWords[h] = colors[pos];
                freqOfWords[h] = 1;
                h++; 
            }
          
        }

        string mixedWords = "";
        for (int i = 0; i < numberOfWords; i++)
        {
            mixedWords += selectedWords[i];
        }
        counterOfLetters = mixedWords.Length;
        allLetters = mixedWords.ToCharArray();
        //Debug.Log(mixedWords);
    }

    bool IsWordInArray(string[] wordArray, string word)
    {
        for(int i = 0; i< wordArray.Length; i++)
        {
            if (wordArray[i] == word)
            {
                return true;
            }
        }

        return false;
    }

    void WriteLetterRandomly()
    {
        System.Random r = new System.Random();

        freqOfLetters = new int[allLetters.Length];
        //Debug.Log(allLetters.Length);
        for (int i = 0; i < freqOfLetters.Length; i++)
        {
            freqOfLetters[i] = 0;
        }

		int[] buttonWithLetter = new int[buttonMatrix.Length];
		int h = 0;
		int counter = counterOfLetters;
		for (int i = 0; i < counter; i++)
        {
            int randomPos = r.Next(buttonMatrix.Length);
			letter = allLetters[h].ToString();
			if(letter == null) 
				letter = "?" ;
			if (!PosAlreadyUsed (buttonWithLetter, randomPos)) {
				
				buttonMatrix [randomPos].text = letter;
				buttonWithLetter [h] = randomPos;
				h++;
			} else 
			{
				counter++;
			}
            
        }
		//Debug.Log (h);
    }

	bool PosAlreadyUsed(int[] array, int randomPos){
		for (int i = 0; i < array.Length; i++) {
			if (array [i] == randomPos) {
				return true;
			}
		}
		return false;
	}

    string SelectLetterRandomly()
    {
        System.Random r = new System.Random();
        while (!AreAllPositioned(freqOfLetters)) {
            int randomPos = r.Next(allLetters.Length);
            if (freqOfLetters[randomPos] == 0)
            {
                freqOfLetters[randomPos] = 1;
                //Debug.Log(allLetters[randomPos].ToString());
                return allLetters[randomPos].ToString();
            }
        }
        return null;
    }

    bool AreAllPositioned(int[] frequencyArray)
    {
        for (int i = 0; i < frequencyArray.Length; i++)
        {
            if (frequencyArray[i] == 0)
            {
                return false;
            }
        }
        return true;
    }

	void FillRemainingButtons(){
		System.Random r = new System.Random();
		int randomPos;
		for (int i = 0; i < buttonMatrix.Length; i++)
		{
			if (buttonMatrix[i].text == "")
			{
				randomPos = r.Next (abc.Length);
				buttonMatrix [i].text = abc [randomPos];
			}
		}
	}

    void CurrentWords()
    {
        string CurrentWordsToFind = "Mixed Words \n\n";
        for(int i = 0; i < selectedWords.Length; i++)
		{
            CurrentWordsToFind += selectedWords[i] + "\n";
        }
        WordsToFind.text = CurrentWordsToFind;
    }

	public string CurrentWord(string currentLetter, ref Button button)
    {
        LettersSelected += currentLetter;
		selectedButtons [buttonIndexCounter] = button;
		buttonIndexCounter++;
        return LettersSelected;
    }

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space) ) {
			if (WordCorrect ()) {
                unityChan.WordCorrect();
                for (int i = 0; i < selectedWords.Length; i++) {
					if (selectedWords [i] == LettersSelected) {
						LettersSelected = "";
						selectedWords [i] = "";
						CurrentWords ();
						break;
					}
				}
			} else {
                unityChan.WordMiss();
                for (int i = 0; i < selectedButtons.Length; i++) {
					selectedButtons [i].interactable = true;
					LettersSelected = "";
				}
			}
		}
		GameOver ();
	}

	private bool WordCorrect(){
		for (int i = 0; i < selectedWords.Length; i++) {
			if (selectedWords [i] == LettersSelected) {
				return true;
			}
		}
		return false;
	}

	void GameOver(){
		for (int i = 0; i < selectedWords.Length; i++) {
			if (selectedWords [i] != "") {
				return;
			}
		}
		for (int i = 0; i < buttonMatrix.Length; i++) {
			buttonMatrix [i].GetComponentInParent<ButtonScript> ().button.interactable = false;
			gameOverPanel.SetActive (true);
            
		}
        unityChan.EndGame();
    }

}

