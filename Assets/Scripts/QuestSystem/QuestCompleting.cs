using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompleting : MonoBehaviour
{
    //Скрипт должен быть компонентом Объекта Квеста
    public Text QuestName;                                      //Компонент [Text] для отображения названия квеста
    public Text QuestStepProgress;                              //Компонент [Text] для отображения прогресса квеста
    public Image QuestIcon;                                     //Компонент [Image] для отображения названия квеста
    private int StepsComplete = 0;                              //Счетчик шагов квеста
    public int StepsToComplete;                                 //Количество шагов для завершения квеста
    private void Start() 
    {
        QuestStepProgress.text = StepsComplete+"/"+StepsToComplete;
    }
    public void Complete()
    {
        StepsComplete++;
        QuestStepProgress.text = StepsComplete+"/"+StepsToComplete;
        if (StepsComplete==StepsToComplete)
        {
            Destroy(this.gameObject);
            QuestList.instance.RemoveQuest(Convert.ToInt32(this.gameObject.name));
        }        
    }
}
