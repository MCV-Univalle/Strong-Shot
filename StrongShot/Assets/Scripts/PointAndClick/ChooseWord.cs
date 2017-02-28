using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWord : MonoBehaviour
{
    public void Choose()
    {
        string word = gameObject.name;

        GameManagerPointAndClick.gmpac.VerifySelection(word);
    }
}
