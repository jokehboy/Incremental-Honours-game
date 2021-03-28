using System;
using UnityEngine.UI;
using BreakInfinity;
using UnityEngine;
using TMPro;

public class OfflineManager : MonoBehaviour
{
    public GameController game;
    public TextMeshProUGUI timeAwayText;
    public TextMeshProUGUI coinsEarned;

    public DateTime currentTime;
    public DateTime oldTime;

    //public GameObject offlinePopup;

    public float efficiency;

   public void LoadOffline()
    {
        
        if(game.data.offlineProgCheck)
        {
            var tempOfflineTime =  Convert.ToInt64(PlayerPrefs.GetString("OfflineTime"));
            var oldTime = DateTime.FromBinary(tempOfflineTime);
            var currentTime = DateTime.Now;

            var difference = currentTime.Subtract(oldTime);
            var rawTime = (float)difference.TotalSeconds;

            var offlineTime = rawTime * efficiency; //5% efficiency  

            //offlinePopup.gameObject.SetActive(true);
            TimeSpan timer = TimeSpan.FromSeconds(rawTime);
            timeAwayText.text = $"You were away for\n {timer:dd\\:mm\\:ss} ";

            BigDouble offlineGain = game.TotalCPS() * offlineTime;
            game.data.currency += offlineGain;

            coinsEarned.text = $"{game.UpdateNotation(offlineGain, "F2")} coins earned at an efficiency of {efficiency * 100}%";

        }
    }
}
