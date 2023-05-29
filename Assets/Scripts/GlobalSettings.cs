using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    private string DebugStr = null;                                                         //Переменная для Логов
    public bool Mobile = false;
    public bool Play = true;
    public bool Build = false;
    public bool Zoom = false;
    public bool Select = false;
    public bool Drag = false;
    public string Object = null;
    public static GlobalSettings instance;
    
    private void Awake() 
    {
        instance = this;
        Mobile = Application.isMobilePlatform;
        Application.targetFrameRate = 30;
        
        DebugStr = $"App Is Started. Platform:{Application.platform}. FrameRate:{Application.targetFrameRate}.";
        DEBUGGER.Log(ColorType.Purple, DebugStr);
    }
}


public static class DEBUGGER
{
    public static string ToHexColor(this Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255.0f);
        int g = Mathf.RoundToInt(color.g * 255.0f);
        int b = Mathf.RoundToInt(color.b * 255.0f);
        int a = Mathf.RoundToInt(color.a * 255.0f);
        string hex = string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
        //string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
        return hex;
    }

    public static void Log(object log)
    {
        Debug.Log(string.Format("<color=#FFFFFF>{0}</color>", log));
    }

    public static void Log(Color color, object log)
    {
        string col = "<color=#" + color.ToHexColor() + ">{0}</color>";
        Debug.Log(string.Format(col, log));
    }

    public static void Log(string hexColor, object log)
    {
        string col = "<color=#" + hexColor + ">{0}</color>";
        Debug.Log(string.Format(col, log));
        //Debug.Log(col);
    }

    public static void Log(ColorType type, object log)
    {
        string hexColor = "FFFFFF";
        switch (type)
        {
            case ColorType.Red:
                hexColor = "f80000";
                break;
            case ColorType.Yellow:
                hexColor = "FFE900";
                break;
            case ColorType.LightBlue:
                hexColor = "00EAFF";
                break;
            case ColorType.Green:
                hexColor = "07FF00";
                break;
            case ColorType.Purple:
                hexColor = "9800FF";
                break;
            case ColorType.Pink:
                hexColor = "FF00D2";
                break;
            case ColorType.Orange:
                hexColor = "ffa000";
                break;
            default:
                break;
        }
        string col = "<color=#" + hexColor + ">{0}</color>";
        Debug.Log(string.Format(col, log));
    }
}
public enum ColorType
{
    Red,
    Yellow,
    LightBlue,
    Green,
    Purple,
    Pink,
    Orange
}
