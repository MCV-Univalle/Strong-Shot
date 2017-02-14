using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    public Button button;
    public Text letter;
    public string letterSelected;
    private GameController gameController;

    public void GetLetter()
    {
        letterSelected = letter.text;
        letter.text = "?"; 
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }
}
