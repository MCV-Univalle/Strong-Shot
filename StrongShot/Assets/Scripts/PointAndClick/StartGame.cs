using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
    public GameObject startPanel;

    public void Play()
    {
        startPanel.SetActive(false);
        GameManagerPointAndClick.gmpac.StartGame();
    }
}
