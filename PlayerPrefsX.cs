using UnityEngine;
using System.Collections;
//Grabbed from Eric5h5 at http://wiki.unity3d.com/index.php/BoolPrefs
public class PlayerPrefsX {

    public static void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
    }

    public static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    public static bool GetBool(string name, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return GetBool(name);
        }

        return defaultValue;
    }
}
