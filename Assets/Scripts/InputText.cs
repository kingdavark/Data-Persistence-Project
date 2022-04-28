using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputText : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    
    public void UpdatePlayerName()
    {
        MainManager.instance.playerName = playerNameText.text;

        Debug.Log(MainManager.instance.playerName);
    }
}
