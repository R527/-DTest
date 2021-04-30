using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager
{
    public static void DisplayDebugLog<T>(T t) {
        string str = "";

        Debug.Log(t.ToString() + t);
    }
}
