using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class Scenery : MonoBehaviour
{
    //Скрипт должен быть компонентом перемещаемого объекта
    public string nameScenery;                                                                      //Название объекта
    public string description;                                                                      //Описание объекта
    public Sprite sceneryIcon;                                                                      //Иконка объекта
    public GameObject MainModel;                                                                    //Основной объект Модели
    public Renderer[] MainRenders;                                                                  //Модели объекта
    public bool IsNewObject;                                                                        //Новизна объекта
    public bool IsRecomended;                                                                       //Рекомендованность объекта
    public bool IsLimited;                                                                          //Лимитированность объекта
    public int cost;                                                                                //Стоимость объекта


    [SerializeField] private Vector2Int size = Vector2Int.one;                                      //Размер объекта по клеткам
    public Vector2Int Size { get { return size; } }                                                 //Ссылка на Размер объекта по клеткам
    public int ScenerySize { get { return (size.x*size.y)/2; } }                                    //Ссылка на Площадь клеток объекта




    public void SetTransparent()    //Установка полупрозрачности объекта
    {
        for (int i = 0; i < MainRenders.Length; i++)
        {
            MainRenders[i].material.color = new Color32(140, 137, 149, 10);
        }
    }

    public void SetNormal()         //Восстановление нормального цвета объекта
    {
        for (int i = 0; i < MainRenders.Length; i++)
        {
            MainRenders[i].material.color = Color.white;
        }
    }
}