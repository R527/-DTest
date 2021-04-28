using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataProcessing
{
    public static void SaveData(string stageName, int seconds) {
        if (PlayerPrefs.HasKey(stageName)) {
            if(seconds < PlayerPrefs.GetInt(stageName)) {
                PlayerPrefs.SetInt(stageName, seconds);
            }
        } else {
            PlayerPrefs.SetInt(stageName, seconds);
        }
    }

    public static int LoadData(string stageName) {
        return PlayerPrefs.GetInt(stageName, int.MaxValue);
    }

    public static void DeleteData(string stageName) {
        if (PlayerPrefs.HasKey(stageName)) {
            PlayerPrefs.DeleteKey(stageName);
        }
    }
}
