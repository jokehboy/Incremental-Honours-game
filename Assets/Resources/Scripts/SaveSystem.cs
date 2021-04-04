using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;
using TMPro;
using BreakInfinity;
using static BreakInfinity.BigDouble;

[System.Serializable]
public class PlayerData
{
    public bool offlineProgCheck = true;
    public BigDouble currency = 0;  //The users currecny count
    public BigDouble totalCurrency = 0;

    public BigDouble coinsPerSecond = 0; //The Coins earned Per Second

    public BigDouble production_level = 0;
    public BigDouble production_multiplier = 0;
    public BigDouble production_level_ToGet = 0;

    public BigDouble product = 0;
    public BigDouble totalProduct = 0;

    public BigDouble ach_lvl1 = 0;
    public BigDouble ach_lvl2 = 0;
    public BigDouble ach_lvl3 = 0;
    public BigDouble ach_lvl4 = 0;
    public BigDouble ach_lvl5 = 0;
    public BigDouble ach_lvl6 = 0;

    public int standard_Upgrade_lvl_1= 0;
    public int standard_Upgrade_lvl_2= 0;
    public int standard_Upgrade_lvl_3= 0;
    public int standard_Upgrade_lvl_4= 0;
    public int standard_Upgrade_lvl_5= 0;
    public int standard_Upgrade_lvl_6= 0;
    public int standard_Upgrade_lvl_7= 0;
    public int standard_Upgrade_lvl_8= 0;
    public int standard_Upgrade_lvl_9 = 0;
    public int standard_Upgrade_lvl_10 = 1;
    public int standard_Upgrade_lvl_11 = 0;

    #region Production upgrades
    public int production_upg_lvl1= 0;
    public int production_upg_lvl2= 0;
    public int production_upg_lvl3= 0;
    public int production_upg_lvl4= 0;
    public int production_upg_lvl5 = 0;
    public int production_upg_lvl6 = 1;
    public int production_upg_lvl7 = 0;


    #endregion

    public bool[] standard_unlocked = new bool[11];
    public bool[] production_unlocked = new bool[7];

    public bool[] button_added = new bool[4];

    public BigDouble cpc_Upgrade_1_base_amount = 0.5; //Coins per click base amount upgrade type 1
    public BigDouble cpc_Upgrade_2_base_amount = 12.5; //Coins per click base amount upgrade type 2

    public BigDouble cps_Upgrade_1_base_amount = 0.2; //Coins per second base amount upgrade type 1
    public BigDouble cps_Upgrade_2_base_amount = 10; //Coins per second base amount upgrade type 2

    public BigDouble cpsAndCpc_Upgrade_base_amount = 15.0; //Upgrade both cpc and cps base amount

    public float criticalClick_Upgrade_chance_base = 0; //Chance of a click being critical | max percentage 
    public float criticalClick_Upgrade_Multiplier = 0; //The smallest possible critical click multiplier

    public float autoClicker_Upgrade_speed = 10.02f; //Speed in which the auto clicker clicks (the time between) 

    public float productionClickChance_Upgrade = 0; //Percentage chance that you will earn product upon click | max percentage 

    public float productionEarnedOnClick_Upgrade = 50f; //The base amount of product gained on click | max level:

    public BigDouble productEarnedPerSec_base_amount = 0.5;

    public int addButton_Upgrade = 1; //Adds another button to the screen, auto click applies, max 4

    public BigDouble base_bet_amount => 500 * production_upg_lvl6;


    #region Settings
    public int notationType;

    #endregion

    public double total_spent_games;
    public double total_lost_games;
    public double total_earned_games;
    public double total_currency_overall;

    public int times_played_coinflip;
    public int times_played_spinwheel;
    public int times_played_dice;



    public PlayerData()
    {
        FullReset();
    }

    public void FullReset()
    {
        //offlineProgCheck = false;
        currency = 0;
        totalCurrency = 0;

        product = 0;
        totalProduct = 0;

        production_level = 0;

        ach_lvl1 = 0;
        ach_lvl2 = 0;
        ach_lvl3 = 0;
        ach_lvl4 = 0;
        ach_lvl5 = 0;
        ach_lvl6 = 0;

        for (int i = 0; i < standard_unlocked.Length; i++)
            standard_unlocked[i] = false;

        for (int i = 0; i < production_unlocked.Length; i++)
            production_unlocked[i] = false;

        for (int i = 0; i < button_added.Length; i++)
        {
            button_added[i] = false;
        }
        button_added[0] = true;

        standard_Upgrade_lvl_1 = 0;
        standard_Upgrade_lvl_2 = 0;
        standard_Upgrade_lvl_3 = 0;
        standard_Upgrade_lvl_4 = 0;
        standard_Upgrade_lvl_5 = 0;
        standard_Upgrade_lvl_6 = 0;
        standard_Upgrade_lvl_7 = 0;
        standard_Upgrade_lvl_8 = 0;
        standard_Upgrade_lvl_9 = 0;
        standard_Upgrade_lvl_10 = 1;
        standard_Upgrade_lvl_11 = 0;

        cpc_Upgrade_1_base_amount = 0.5;
        cpc_Upgrade_2_base_amount = 12.5;

        cps_Upgrade_1_base_amount = 0.2;
        cps_Upgrade_2_base_amount = 10;

        cpsAndCpc_Upgrade_base_amount = 15;


        productionEarnedOnClick_Upgrade = 50f;

        productEarnedPerSec_base_amount = 0.5f;

        production_upg_lvl1 = 0;
        production_upg_lvl2 = 0;
        production_upg_lvl3 = 0;
        production_upg_lvl4 = 0;
        production_upg_lvl5 = 0;
        production_upg_lvl6 = 1;
        production_upg_lvl7 = 0;


    }

}

public class SimpleAES
{
    // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
    private const string initVector = "pdf12167abstfgzd";
    // This constant is used to determine the keysize of the encryption algorithm
    private const int keysize = 256;
    //Encrypt
    public string EncryptString(string plainText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(cipherTextBytes);
    }
    //Decrypt
    public string DecryptString(string cipherText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
}

public class SaveSystem : MonoBehaviour
{
    public TMP_InputField importValue;
    public TMP_InputField exportValue;

    public static string json = "";

    protected static string encryptKey = "tdxf1982obtgf09xztwger29";
    protected static string savePath = "/hounoursIdleGame.save";
    protected static string savePathBackUP = "/hounoursIdleGameBackup.save";

    public static int backUpCount = 0;

    public static void SavePlayer(PlayerData data)
    {
        var saveTo = backUpCount == 4 ? savePathBackUP : savePath;
        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + saveTo))
        {
            json = JsonUtility.ToJson(data);
            ConvertStringToBase64(writer, json);
            writer.Close();

            PlayerPrefs.SetString("OfflineTime", System.DateTime.Now.ToBinary().ToString());
            if(!data.offlineProgCheck)data.offlineProgCheck = true;
        }
        Debug.Log("Saved");
        backUpCount = (backUpCount + 1) % 5;
    }

    public static string ConvertStringToBase64(StreamWriter writer, string x)
    {
        SimpleAES aes = new SimpleAES();
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(x);
        string stringTemp = Convert.ToBase64String(plainTextBytes);
        writer.WriteLine(aes.EncryptString(stringTemp, encryptKey));
        return stringTemp;
    }
    public static bool LoadSaveFile(ref PlayerData data, string path)
    {
        var success = false;
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                json = ConvertBase64ToString(reader);
                data = JsonUtility.FromJson<PlayerData>(json);
                reader.Close();
                success = true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Load Save Failed");
            CreateFile();
            // LoadPlayer(ref data);
            // Load failed, handle codes here
        }
        return success;
    }

    public static void CreateFile()
    {
        if (!File.Exists(Application.persistentDataPath + savePathBackUP))
        {
            File.CreateText(Application.persistentDataPath + savePathBackUP);
        }
        if (!File.Exists(Application.persistentDataPath + savePath))
        {
            File.CreateText(Application.persistentDataPath + savePath);
        }
    }
    public static void LoadPlayer(ref PlayerData data)
    {
        CreateFile();
        if (!LoadSaveFile(ref data, Application.persistentDataPath + savePath))
        { // Try to load main
            LoadSaveFile(ref data, Application.persistentDataPath + savePathBackUP); // Load backup if main failed to load
        }
    }

    public static string ConvertBase64ToString(StreamReader reader)
    {
        SimpleAES aes = new SimpleAES();
        string stringConvert = reader.ReadLine();
        var base64EncodedBytes = Convert.FromBase64String(aes.DecryptString(stringConvert, encryptKey));
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public void ImportPlayer2(GameController playerData)
    {
        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + savePath))
        {
            Debug.Log(importValue.text);
            writer.WriteLine(importValue.text);
            writer.Close();
            using (StreamReader reader = new StreamReader(Application.persistentDataPath + savePath))
            {
                json = ConvertBase64ToString(reader);
                playerData.data = JsonUtility.FromJson<PlayerData>(json);
                reader.Close();
            }
        }
    }

    public void ExportPlayer2()
    {
        using (StreamReader reader = new StreamReader(Application.persistentDataPath + savePath))
        {
            SimpleAES aes = new SimpleAES();
            string outputData = reader.ReadLine();
            reader.Close();
            exportValue.text = outputData;
            Debug.Log(aes.DecryptString(outputData, encryptKey));
            Debug.Log(outputData);
        }
    }

    public void ClearFields()
    {
        exportValue.text = "";
        importValue.text = "";
    }
}
