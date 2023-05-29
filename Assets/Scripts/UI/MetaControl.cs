using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MetaControl : MonoBehaviour
{
    private string DebugStr = null;                                                         //Переменная для Логов
    public GameObject QuestList;
    public GameObject EventList;
    public GameObject CollectionList;
    public GameObject Menu;
    public void Open()
    {
        ResetSections();
        this.gameObject.SetActive(true);
        Menu.transform.Find("Quests").GetComponent<Image>().color = new Color(242, 232, 201, 255);
        SwitchSections("Quests");
    }

    public void SwitchSections(string sectionName)
    {
        ResetSections();
        switch (sectionName)
        {
            case "Quests":
            
            Menu.transform.Find("Quests").GetComponent<Image>().color = new Color(242, 232, 201, 255);
            QuestList.gameObject.GetComponent<QuestGiver>().GiveQuest();
            QuestList.GetComponent<QuestList>().OpenMenu();
            DebugStr = $"MetaMenu[Quests]: Open\n";
            DEBUGGER.Log(ColorType.Yellow, DebugStr);
            break;
            
            case "Events":
            Menu.transform.Find("Events").GetComponent<Image>().color = new Color(242, 232, 201, 255);
            EventList.GetComponent<EventList>().OpenMenu();
            DebugStr = $"MetaMenu[Events]: Open\n";
            DEBUGGER.Log(ColorType.Yellow, DebugStr);
            break;

            case "Collections":
            CollectionList.SetActive(true);
            Menu.transform.Find("Collections").GetComponent<Image>().color = new Color(242, 232, 201, 255);
            CollectionList.GetComponent<CollectionList>().OpenMenu();
            DebugStr = $"MetaMenu[Collections]: Open\n";
            DEBUGGER.Log(ColorType.Yellow, DebugStr);
            break;
        }
    }
    
    private void ResetSections()
    {
        Menu.transform.Find("Quests").GetComponent<Image>().color = Color.white;
        Menu.transform.Find("Events").GetComponent<Image>().color = Color.white;
        Menu.transform.Find("Collections").GetComponent<Image>().color = Color.white;
        CollectionList.GetComponent<CollectionList>().CloseMenu();
        QuestList.GetComponent<QuestList>().CloseMenu();
        EventList.GetComponent<EventList>().CloseMenu();
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
