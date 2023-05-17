using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // LAST PLAYED LEVEL    *****************************************
    public static void SetLastPlayedLevel(int i)
    {
        PlayerPrefs.SetInt("lastPlayedLevel", i);
    }
    public static int GetLastPlayedLevel()
    {
        if (PlayerPrefs.HasKey("lastPlayedLevel"))
            return PlayerPrefs.GetInt("lastPlayedLevel");
        else return 1;
    }

    // FAKE LEVEL    *****************************************
    public static void IncreaseFakeLevel()
    {
        PlayerPrefs.SetInt("fakeLevel", GetFakeLevel() + 1);
    }

    public static int GetFakeLevel()
    {
        if (PlayerPrefs.HasKey("fakeLevel"))
            return PlayerPrefs.GetInt("fakeLevel");
        else return 1;
    }

}
