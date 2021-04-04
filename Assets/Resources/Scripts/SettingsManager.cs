using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public GameController game;
    public TextMeshProUGUI notationType_Text;
    public TextMeshProUGUI notationExample_Text;

    public string HelpUrl;

    /*
     * Notation Key
     * 0 = Scientific
     * 1 = Engineering
     * 2 = Letter (a, b, c ....aa, ab, ac...)
     */
    private void Awake()
    {
        
    }
    private void Start()
    {
        UpdateNotationText();
        Debug.Log(game.data.notationType);
    }

    public void OpenHelp()
    {
        Application.OpenURL(HelpUrl);
    }

    public void UpdateNotationText()
    {
        var notation = game.data.notationType;

        switch (notation)
        {
            case 0:
                notationType_Text.text = "Scientific Notation";
                notationExample_Text.text = "Example: 12,300 is 1.23e4";
                break;
            case 1:
                notationType_Text.text = "Engineering Notation";
                notationExample_Text.text = "Example: 12,300 is 12.30e3";
                break;
            case 2:
                notationType_Text.text = "Letter Notation";
                notationExample_Text.text = "Example: 12,300 is 12.3a";
                break;
        }
    }

    public void ChangeNotation()
    {
        var notation = game.data.notationType;

        switch(notation)
        {
            case 0:
                notation = 1;
                break;
            case 1:
                notation = 2;
                break;
            case 2:
                notation = 0;
                break;
        }
        game.data.notationType = notation;
        UpdateNotationText();
    }
}
