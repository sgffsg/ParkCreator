using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] GameObject GridLine;                               //Сетка карты
    [SerializeField] GameObject EditMenu;                               //Сетка карты
    [SerializeField] GameObject PlacementMenu;                          //Сетка карты

    public void InitializePlacement()
    {
        PlacementMenu.transform.parent.gameObject.SetActive(true);
        if (GlobalSettings.instance.Build)
        {
            PlacementMenu.SetActive(true);
            GridLine.SetActive(true);
        }
        else
        {
            EditMenu.SetActive(true);
            GridLine.SetActive(true);
        }
    }
    public void CloseMenu()
    {
        PlacementMenu.transform.parent.gameObject.SetActive(false);
        PlacementMenu.SetActive(false);
        EditMenu.SetActive(false);
        GridLine.SetActive(false);
    }
}
