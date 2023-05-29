using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CollectionList : MonoBehaviour     //Скрипт для списка Коллекций
{
    //Скрипт должен быть компонентом Списка Коллекций
    [SerializeField] GameObject prefab;                                             //Префаб Коллекции
    [SerializeField] GameObject content;                                            //Префаб Контента для размещения Коллекций
    [SerializeField] GameObject NoOjects;                                           //Префаб Отсуствия Объектов


    [SerializeField] Scrollbar scrollbar;                                           //СкроллБар для Списка Коллекций


    private List<GameObject> Collections = new List<GameObject>();                  //Список Коллекций




    private void Start() 
    {
        
    }
    public void OpenMenu()
    {
        this.gameObject.SetActive(true);
        UPD();
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
    }
}
