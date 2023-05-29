using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collection : MonoBehaviour //Скрипт для коллекции
{
    //Скрипт должен быть компонентом коллекции
    public Text collectionName;                     //Название коллекции
    public Text collectionReward;                   //Награда за обмен
    public Text collectionTradeCounter;             //Количество обменов


    public Image collectionItem1;                   //Изображение [1]-го предмета коллекции
    public Image collectionItem2;                   //Изображение [2]-го предмета коллекции
    public Image collectionItem3;                   //Изображение [3]-го предмета коллекции
    public Image collectionItem4;                   //Изображение [4]-го предмета коллекции
    public Image collectionItem5;                   //Изображение [5]-го предмета коллекции
}
