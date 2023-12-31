using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SaveName
{
    NONE = 0,
    SAVE1 = 1,
    SAVE2 = 2,
    SAVE3 = 3
}
public class GameDataManager : MonoBehaviour
{
    public void SaveDocument(SaveName name, string data)
    {
        var saveName = name.ToString();
        PlayerPrefs.SetString(saveName, data);
        PlayerPrefs.SetString("LASTSAVE", saveName.ToString());
        PlayerPrefs.Save();
    }
    public string GetDocument(SaveName name)
    {
        return PlayerPrefs.GetString(name.ToString());
    }
    public SaveName GetLastSaveName()
    {
        if (!PlayerPrefs.HasKey("LASTSAVE"))
        {
            return SaveName.NONE;
        }
        var name = PlayerPrefs.GetString("LASTSAVE");
        switch (name)
        {
            case "SAVE3":
                return SaveName.SAVE3;
            case "SAVE2":
                return SaveName.SAVE2;
            default:
                return SaveName.SAVE1;
        }
    }
}
