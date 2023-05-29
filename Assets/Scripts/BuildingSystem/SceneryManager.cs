using System.Collections.Generic;
using UnityEngine;

public class SceneryManager : MonoBehaviour
{
    //Скрипт должен быть компонентом сцены
    public static SceneryManager instance;                              //Ссылка на скрипт

    //Скрипт [Scenery] должен находится на префабе объекта
    public Scenery[] DonateItems;                                       //Префабы для магазина покупок
    public Scenery[] AttractionsItems;                                  //Префабы аттракционов
    public Scenery[] FloraItems;                                        //Префабы растительности
    public Scenery[] ObstaclesItems;                                    //Префабы ограждений
    public Scenery[] BuildingsItems;                                    //Префабы зданий
    public Scenery[] DecorItems;                                        //Префабы украшений
    public Scenery[] OtherItems;                                        //Префабы других предметов
    public Scenery[] GroundItems;                                       //Префабы поверхностей
    



    private void Start() 
    {
        instance = this;
    }

    public Scenery FindScenery(string name)     //Поиск объекта по его имени
    {
        for (int i = 0; i < AttractionsItems.Length; i++)
        {
            if (AttractionsItems[i].nameScenery == name)
            {
                return AttractionsItems[i];
            }
        }
        for (int i = 0; i < FloraItems.Length; i++)
        {
            if (FloraItems[i].nameScenery == name)
            {
                return FloraItems[i];
            }
        }
        for (int i = 0; i < ObstaclesItems.Length; i++)
        {
            if (ObstaclesItems[i].nameScenery == name)
            {
                return ObstaclesItems[i];
            }
        }
        for (int i = 0; i < BuildingsItems.Length; i++)
        {
            if (BuildingsItems[i].nameScenery == name)
            {
                return BuildingsItems[i];
            }
        }
        for (int i = 0; i < DecorItems.Length; i++)
        {
            if (DecorItems[i].nameScenery == name)
            {
                return DecorItems[i];
            }
        }
        for (int i = 0; i < OtherItems.Length; i++)
        {
            if (OtherItems[i].nameScenery == name)
            {
                return OtherItems[i];
            }
        }
        for (int i = 0; i < GroundItems.Length; i++)
        {
            if (GroundItems[i].nameScenery == name)
            {
                return GroundItems[i];
            }
        }
        return null;
    }

    /* public Vector3 getPosition(int x, int z, float cellSize, int size)  //Определение координат для расположения объекта
    {
        bool isOffset = size % 2 == 0;
        float offsetToCenter = !isOffset ? cellSize / 2f : 0;
        Vector3 math = new Vector3(x * cellSize + offsetToCenter, 0, z * cellSize + offsetToCenter);
        return math;
    } */
}