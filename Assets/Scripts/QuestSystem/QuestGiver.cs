using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    //Скрипт должен быть компонентом Списка Квестов
    public List<Quest> quests = new List<Quest>();                                              //Список квестов
    [SerializeField] QuestList questList;                                                       //Ссылка на Список квестов
    public static QuestGiver instance;                                                          //Ссылка на скрипт
    private void Start() 
    {
        instance = this;
    }

    public void GiveQuest()
    {
        for (int i=0; i<quests.Count; i++)
        {
            questList.currentQuests.Add(new Quest{  questId = quests[i].questId,
                                                    questName = quests[i].questName,
                                                    questType = quests[i].questType,
                                                    questIcon = quests[i].questIcon,
                                                    questSteps = quests[i].questSteps});
        }        
    }    
}

[System.Serializable] public struct Quest       //Структура квеста
{
    public string questName;                            //Название квеста
    public string questType;                            //Тип квеста
    public Sprite questIcon;                            //Иконка квеста
    public int questSteps;                              //Количество шагов для завершения квеста
    public int questId;                                 //ID квеста
}
