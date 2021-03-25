using System;
using UnityEngine.UI;
using BreakInfinity;
using UnityEngine;

public class OfflineManager : MonoBehaviour
{
    //public GameController game;
    public Text timeAwayText;
    public Text coinsEarned;

    public DateTime currentTime;
    public DateTime oldTime;

    public GameObject offlinePopup;

   /*public void LoadOffline()
    {
        if(game.data.offlineProgCheck)
        {
            var tempOfflineTime =  Convert.ToInt64(PlayerPrefs.GetString("OfflineTime"));
            var oldTime = DateTime.FromBinary(tempOfflineTime);
            var currentTime = DateTime.Now;

            var difference = currentTime.Subtract(oldTime);
            var rawTime = (float)difference.TotalSeconds;

            var offlineTime = rawTime / 20;

            offlinePopup.gameObject.SetActive(true);
            TimeSpan timer = TimeSpan.FromSeconds(rawTime);
            timeAwayText.text = $"You were away for\n {timer:dd\\:mm\\:ss} ";
        }
    }*/
}
