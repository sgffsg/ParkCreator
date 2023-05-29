using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventList : MonoBehaviour
{
    //Скрипт должен быть компонентом Списка Событий
    public void OpenMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
}
