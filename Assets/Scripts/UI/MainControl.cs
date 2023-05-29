using System;
using UnityEngine;
using System.Collections;

public class MainControl : MonoBehaviour
{
    public static MainControl instance;
    public ShopManager shopManager;
    public BuildManager buildManager;
    public GameObject MainController;
    public GameObject BuildController;
    public MetaControl metaControl;
    private string DebugStr = null;                                                         //Переменная для Логов
    
    
    public void Start()
    {
        instance = this;
    }
    public void OpenShopMenu()
    {
        DebugStr = $"Shop: Open\n";
        DEBUGGER.Log(ColorType.Yellow, DebugStr);
        
        GlobalSettings.instance.Play = false;
        MainController.SetActive(false);
        shopManager.InitializeMenu();
    }
    public void OpenQuestMenu()
    {
        DebugStr = $"MetaMenu: Open\n";
        DEBUGGER.Log(ColorType.Yellow, DebugStr);

        GlobalSettings.instance.Play = false;
        MainController.SetActive(false);
        metaControl.Open();
    }
    public void OpenBuildMode()
    {
        DebugStr = $"Build [Edit Mode]: Open\n";
        DEBUGGER.Log(ColorType.Yellow, DebugStr);

        GlobalSettings.instance.Play = false;
        GlobalSettings.instance.Build = false;
        buildManager.InitializePlacement();
        MainController.SetActive(false);
    }
    public void OpenShopBuildMode()
    {
        DebugStr = $"Build [Place Mode]: Open\n";
        DEBUGGER.Log(ColorType.Yellow, DebugStr);

        GlobalSettings.instance.Play = false;
        GlobalSettings.instance.Build = true;
        buildManager.InitializePlacement();
        MainController.SetActive(false);
    }



    public void CloseShopMenu()
    {
        DebugStr = $"Shop: Close\n";
        DEBUGGER.Log(ColorType.Yellow, DebugStr);

        GlobalSettings.instance.Play = true;
        MainController.SetActive(true);
        shopManager.Close("CloseMenu");
    }
    public void CloseQuestMenu()
    {
        DebugStr = $"MetaMenu: Close\n";
        DEBUGGER.Log(ColorType.Yellow, DebugStr);

        GlobalSettings.instance.Play = true;
        MainController.SetActive(true);
        metaControl.Close();
    }
    public void CloseBuildMode()
    {
        DebugStr = $"Build: Close\n";
        DEBUGGER.Log(ColorType.Yellow, DebugStr);
        
        GlobalSettings.instance.Play = true;
        MainController.SetActive(true);
        buildManager.CloseMenu();
    }
}
