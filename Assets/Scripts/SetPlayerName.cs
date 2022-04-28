using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetPlayerName : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;

    private void Start()
    {
        playerNameText.text = MainManager.instance.playerName;
    }
}
