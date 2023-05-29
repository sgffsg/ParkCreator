using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    //Скрипт должен быть компонентом Списка Квестов
    public static QuestList instance;



    [SerializeField] GameObject prefab;                                                         //Префаб Квеста
    [SerializeField] GameObject content;                                                        //Префаб Контента для размения Квестов
    [SerializeField] GameObject NoObjects;                                                      //Префаб Отсуствия Объектов


    [SerializeField] Scrollbar scrollbar;                                                       //Компонент [ScrollBar] для Списка Квестов
    [SerializeField] Text QuestCounter;                                                         //Компонент [Text] для отображения количества квестов на главном экране

    
    public List<Quest> currentQuests = new List<Quest>();                                       //Список полученных квестов
    private List<GameObject> questList = new List<GameObject>();                                //Список объектов квестов



    
    private void Start() 
    {
        instance = this;
    }
    public void RemoveQuest(int questId)
    {
        for (int i = 0; i < currentQuests.Count; i++)
        {
            if (questId == currentQuests[i].questId)
            {
                currentQuests.RemoveAt(i);
            }
        }
        QuestCounter.text = currentQuests.Count.ToString();
        if (currentQuests.Count == 0)
        {
            NoObjects.SetActive(true);
        }
    }
    private void Clear()
    {
        if (questList.Count != 0)
        {
            for (int i=0; i< questList.Count; i++)
            {
                Destroy(questList[i]);
            }
            questList.Clear();
        }
    }
    public void OpenMenu()
    {
        this.gameObject.SetActive(true);

        for (int i=0; i< currentQuests.Count; i++)
        {
            GameObject questObj = Instantiate(prefab);
            questObj.transform.SetParent(content.transform, false);
            questObj.name = i.ToString();
            questObj.GetComponent<QuestCompleting>().QuestIcon.sprite = currentQuests[i].questIcon;
            questObj.GetComponent<QuestCompleting>().QuestName.text = currentQuests[i].questName;
            questObj.GetComponent<QuestCompleting>().StepsToComplete = currentQuests[i].questSteps;
            
            questList.Add(questObj);
        }
        QuestCounter.text = questList.Count.ToString();
        if (currentQuests.Count != 0)
            NoObjects.SetActive(false);
        else
            NoObjects.SetActive(true);
        scrollbar.value = 1;
    }
    public void UPD()
    {
        if (scrollbar.value < 0)
            scrollbar.value = 0;
        if (scrollbar.value > 1)
            scrollbar.value = 1;
    }
    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
        Clear();
    }
}
