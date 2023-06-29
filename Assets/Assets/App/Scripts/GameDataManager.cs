using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SaveName
{
    SAVE1,
    SAVE2,
    SAVE3
}
public class GameDataManager : MonoBehaviour
{
    public void SaveDocument(SaveName name, string data)
    {
        PlayerPrefs.SetString(name.ToString(), data);
        PlayerPrefs.Save();
    }
    public string GetDocument(SaveName name)
    {
        return PlayerPrefs.GetString(name.ToString());
    }
}
