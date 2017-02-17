using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    public Button button;
    public Text letter;
    public string letterSelected;
    private GameController gameController;
    public Text CurrentWord;
    private string message = "";

    public void GetLetter()
    {
		
		letterSelected = letter.text;
		message = gameController.CurrentWord (letterSelected, ref button);
		button.interactable = false;
		CurrentWord.text = message;
    }

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			message = "";
			CurrentWord.text = message;
		}
	}

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }
}
