using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Scenery scenery;                                             //Префаб объекта со скриптом
    public Text sceneryText;                                            //Компонент [Text] для отображения названия объекта
    public Image sceneryIcon;                                           //Компонент [Image] для отображения названия объекта
    public ShopManager shopManager;                                     //Менеджер магазина




    public void OpenInfo()      //Открытие информации об объекте
    {
        
    }

    public void Instant()       //Вызов объекта на сцену
    {
        GridPlacementSystem.instance.AddScenery(scenery);
        //shopManager.Close("CloseMenu");
        shopManager.gameObject.SetActive(false);
    }
}
