using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementSystem : MonoBehaviour
{
    //Скрипт должен быть компонентом сцены
    public static GridPlacementSystem instance;                                                     //Ссылка на скрипт
    public SceneryManager sceneryManager;                                                           //менеджер объектов
    public Map map;                                                                                 //Карта


    public int scenerySizeX;                                                                        //Размер распологаемого объекта по оси [X]
    public int scenerySizeY;                                                                        //Размер распологаемого объекта по оси [Y]
    public int scenerySize;                                                                         //Площадь распологаемого объекта [X*Y]
    private string DebugStr;


    private List<SpriteRenderer> fillCellSpriteRenders = new List<SpriteRenderer>();                //Список спрайтов подсветки ячеек
    private List<GameObject> PlacementObjects = new List<GameObject>();                             //Список расположенных объектов
    private List<GameObject> Extensions = new List<GameObject>();                                   //Иконки выделения границ объекта


    public GameObject placementObject;                                                              //Объект, который устанавливается на карту
    [SerializeField] GameObject fillCellSprite;                                                     //Префаб спрайта подсветки ячейки под объектом
    [SerializeField] GameObject extensionSprite;                                                    //Префаб спрайта Иконки для выделения границ объекта


    private Vector3 startPos = Vector3.zero;                                                        //Стартовая позиция объекта
    private Vector3 curPos = Vector3.zero;                                                          //Текущая позиция объекта
    private Vector3 move = Vector3.zero;                                                            //Централизование позиции для модели объекта




    void Start()
    {
        instance = this;
    }

    public int cellX(Vector3 realPosition)=> (int)((realPosition.x - map.xStartPoint) / map.CellSize);
    public int cellZ(Vector3 realPosition) => (int)((realPosition.z - map.zStartPoint) / map.CellSize);

    public void SetCellFillSpritePosition(int cellX, int cellZ, int scenerySizeX, int scenerySizeY)
    {
        if (fillCellSpriteRenders.Count == 0)
            return;
        
        Cell[] cells = map.getCells(cellX, cellZ, scenerySizeX, scenerySizeY);
        if (cells.Length!= fillCellSpriteRenders.Count)
        {
            return;
        }
        curPos = placementObject.transform.position;
        for (int i = 0; i < fillCellSpriteRenders.Count; i++)
        {
            if (cells[i]==null)
            {
                continue;
            }
            fillCellSpriteRenders[i].gameObject.transform.position = new Vector3(cells[i].x *cells[i].cellSize+ cells[i].cellSize/2, 0.1f, cells[i].z * cells[i].cellSize + cells[i].cellSize / 2);

            if (cells[i].isFill)
                fillCellSpriteRenders[i].color = new Color(1.0f, 0f, 0f, 0.5f);
            else
                fillCellSpriteRenders[i].color = new Color(0f, 1.0f, 0f, 0.5f);
        }
    }
    
    public void ShowFills(int cellX, int cellZ, int scenerySizeX, int scenerySizeY)
    {
        if (fillCellSpriteRenders.Count == 0)
            return;
        
        Cell[] cells = map.getCells(cellX, cellZ, scenerySizeX, scenerySizeY);
        if (cells.Length!= fillCellSpriteRenders.Count)
        {
            return;
        }

        for (int i = 0; i < fillCellSpriteRenders.Count; i++)
        {
            if (cells[i]==null)
            {
                continue;
            }
            fillCellSpriteRenders[i].color = new Color(1.0f, 0f, 0f, 0.5f);
        }
    }

    public void SetExtensionsSprite()
    {
        GameObject sprite;
        //Левый
        sprite = Instantiate(extensionSprite);
        //sprite.transform.localScale = new Vector3 (Mathf.Ceil((scenerySizeY / 2)), Mathf.Ceil((scenerySizeX / 2)),1);
        sprite.transform.rotation = Quaternion.Euler(90,0,-90);
        Extensions.Add(sprite);
        sprite = null;

        //Правый
        sprite = Instantiate(extensionSprite);
        //sprite.transform.localScale = new Vector3 (Mathf.Ceil((scenerySizeX / 2)), Mathf.Ceil((scenerySizeY / 2)),1);
        sprite.transform.rotation = Quaternion.Euler(90,0,90);
        Extensions.Add(sprite);
        sprite = null;
        
        //Верхний
        sprite = Instantiate(extensionSprite);
        //sprite.transform.localScale = new Vector3 (Mathf.Ceil((scenerySizeX / 2)), Mathf.Ceil((scenerySizeY / 2)),1);
        sprite.transform.rotation = Quaternion.Euler(90,0,180);
        Extensions.Add(sprite);
        sprite = null;

        //Нижний
        sprite = Instantiate(extensionSprite);
        //sprite.transform.localScale = new Vector3 (Mathf.Ceil((scenerySizeX / 2)), Mathf.Ceil((scenerySizeY / 2)),1);
        sprite.transform.rotation = Quaternion.Euler(90,0,0);
        Extensions.Add(sprite);
        sprite = null;
        
        
        MoveExtensions();
    }

    public void MoveExtensions()
    {
        //Левая     Иконка Выделения Объекта - Extensions[0]
        //Правая    Иконка Выделения Объекта - Extensions[1]
        //Верхняя   Иконка Выделения Объекта - Extensions[2]
        //Нижняя    Иконка Выделения Объекта - Extensions[3]
        if (scenerySizeX % 2 == 0)
        {
            if (scenerySizeY %2 ==0)
            {
                Extensions[0].transform.position = new Vector3 (placementObject.transform.position.x - (scenerySizeX*0.5f) - 0.75f, extensionSprite.transform.position.y, placementObject.transform.position.z);
                Extensions[1].transform.position = new Vector3 (placementObject.transform.position.x + (scenerySizeX*0.5f) + 0.75f, extensionSprite.transform.position.y, placementObject.transform.position.z);
                Extensions[2].transform.position = new Vector3 (placementObject.transform.position.x, extensionSprite.transform.position.y, placementObject.transform.position.z + (scenerySizeY*0.5f) + 0.75f);
                Extensions[3].transform.position = new Vector3 (placementObject.transform.position.x, extensionSprite.transform.position.y, placementObject.transform.position.z - (scenerySizeY*0.5f) - 0.75f);
            }
            else
            {
                Extensions[0].transform.position = new Vector3 (placementObject.transform.position.x - (scenerySizeX*0.5f) - 1f, extensionSprite.transform.position.y, placementObject.transform.position.z + 0.5f);
                Extensions[1].transform.position = new Vector3 (placementObject.transform.position.x + (scenerySizeX*0.5f) + 1f, extensionSprite.transform.position.y, placementObject.transform.position.z + 0.5f);
                Extensions[2].transform.position = new Vector3 (placementObject.transform.position.x , extensionSprite.transform.position.y, placementObject.transform.position.z +  (scenerySizeY*0.5f) + 1.5f);
                Extensions[3].transform.position = new Vector3 (placementObject.transform.position.x , extensionSprite.transform.position.y, placementObject.transform.position.z -  (scenerySizeY*0.5f) - 0.5f);
            }
        }
        else
        {
            if (scenerySizeY %2 ==0)
            {
                Extensions[0].transform.position = new Vector3 (placementObject.transform.position.x - (scenerySizeX*0.5f), extensionSprite.transform.position.y, placementObject.transform.position.z);
                Extensions[1].transform.position = new Vector3 (placementObject.transform.position.x + (scenerySizeX*0.5f) + 1f, extensionSprite.transform.position.y, placementObject.transform.position.z);
                Extensions[2].transform.position = new Vector3 (placementObject.transform.position.x + 0.5f, extensionSprite.transform.position.y, placementObject.transform.position.z + (scenerySizeY*0.5f) + 0.75f);
                Extensions[3].transform.position = new Vector3 (placementObject.transform.position.x + 0.5f, extensionSprite.transform.position.y, placementObject.transform.position.z - (scenerySizeY*0.5f) - 0.75f);
            }
            else
            {
                Extensions[0].transform.position = new Vector3 (placementObject.transform.position.x - (scenerySizeX*0.5f) - 0.25f, extensionSprite.transform.position.y, placementObject.transform.position.z + 0.5f);
                Extensions[1].transform.position = new Vector3 (placementObject.transform.position.x + (scenerySizeX*0.5f) + 1.25f, extensionSprite.transform.position.y, placementObject.transform.position.z + 0.5f);
                Extensions[2].transform.position = new Vector3 (placementObject.transform.position.x + 0.5f, extensionSprite.transform.position.y, placementObject.transform.position.z + (scenerySizeY*0.5f) + 1.25f);
                Extensions[3].transform.position = new Vector3 (placementObject.transform.position.x + 0.5f, extensionSprite.transform.position.y, placementObject.transform.position.z - (scenerySizeY*0.5f) - 0.25f);
            }
        }
    }
    

    public void AddCellFillSprite()
    {
        if (fillCellSprite == null)
            return;

        for (int i = 0; i < scenerySizeX * scenerySizeY; i++)
        {
            fillCellSpriteRenders.Add(GameObject.Instantiate(fillCellSprite).GetComponent<SpriteRenderer>());
            fillCellSpriteRenders[i].transform.SetParent(this.gameObject.transform.Find("Fill_Sprites").transform, false);
        }
    }

    private void ClearCellFillSprite()
    {
        for (int i = 0; i < fillCellSpriteRenders.Count; i++)
            GameObject.Destroy(fillCellSpriteRenders[i].gameObject);

        for (int i = 0; i < Extensions.Count; i++)
            Destroy(Extensions[i]);

        fillCellSpriteRenders.Clear();
        Extensions.Clear();
    }    

    public void AddScenery(Scenery scenery)
    {
        
        if (placementObject != null)
        {
            Destroy(placementObject);
            ClearCellFillSprite();
        }
        GetScenery(scenery.nameScenery);

        placementObject = GameObject.Instantiate(scenery.gameObject, new Vector3(cellX(Camera.main.transform.position), 0, cellZ(Camera.main.transform.position) + 20), Quaternion.identity);
        placementObject.transform.SetParent(this.gameObject.transform.Find("Placements").transform, false);
        
        move = map.selectCells(cellX(placementObject.transform.position),cellZ(placementObject.transform.position),scenerySizeX,scenerySizeY);

        placementObject.GetComponent<Scenery>().MainModel.transform.position = move;
        placementObject.name = placementObject.GetComponent<Scenery>().nameScenery+"#"+placementObject.GetHashCode().ToString();
        
        AddCellFillSprite();
        ShowFills(cellX(placementObject.transform.position), cellZ(placementObject.transform.position), scenerySizeX, scenerySizeY);
        SetExtensionsSprite();
        
        SetCellFillSpritePosition(cellX(placementObject.transform.position), cellZ(placementObject.transform.position), scenerySizeX, scenerySizeY);
        
        GlobalSettings.instance.Object = placementObject.GetHashCode().ToString();
        GlobalSettings.instance.Select = true;
        
        Camera.main.transform.position = new Vector3(placementObject.transform.position.x, 25, Camera.main.transform.position.z);
        MainControl.instance.OpenShopBuildMode();
        
        
        DebugStr = $"Object:{placementObject.name} Is Instantiate";
        DEBUGGER.Log(ColorType.Green, DebugStr);
        DebugStr = $"Placement Is Available";
        DEBUGGER.Log(ColorType.Purple, DebugStr);
    }


    public void OnEventButtonCancel()
    {
        if (GlobalSettings.instance.Build)
        {
            Destroy(placementObject);
            placementObject = null;
            
            MainControl.instance.BuildController.SetActive(false);
            ShopManager.instance.gameObject.SetActive(true);
        }
        else
        {
            if (startPos != curPos)
            {
                placementObject.transform.position = startPos;
                map.markCellsAsBusy(cellX(placementObject.transform.position), cellZ(placementObject.transform.position), scenerySizeX, scenerySizeY);
                placementObject = null;
            }

            MainControl.instance.CloseBuildMode();
            MainControl.instance.OpenBuildMode();
        }

        GlobalSettings.instance.Drag = false;
        GlobalSettings.instance.Object = null;
        GlobalSettings.instance.Select = false;

        ClearCellFillSprite();

        DebugStr = $"Placement Is Not Available";
        DEBUGGER.Log(ColorType.Purple, DebugStr);
    }

    public void OnEventSetScenery()
    {
        if (placementObject == null)
            return;
        if (!map.IsСellsEmpty(cellX(placementObject.transform.position), cellZ(placementObject.transform.position), scenerySizeX, scenerySizeY))
            return;

        map.markCellsAsBusy(cellX(placementObject.transform.position), cellZ(placementObject.transform.position), scenerySizeX, scenerySizeY);

        PlacementObjects.Add(placementObject);
        


        if (GlobalSettings.instance.Build)
        {
            MainControl.instance.CloseBuildMode();
        }
        else
        {
            MainControl.instance.CloseBuildMode();
            MainControl.instance.OpenBuildMode();
            GlobalSettings.instance.Build = false;
        }

        GlobalSettings.instance.Object = null;
        GlobalSettings.instance.Select = false;

        ClearCellFillSprite();

        DebugStr = $"Object:{placementObject.name} Is Set";
        DEBUGGER.Log(ColorType.Green, DebugStr);
        DebugStr = $"Placement Is Not Available";
        DEBUGGER.Log(ColorType.Purple, DebugStr);

        placementObject = null;
    }

    public void SelectScenery(GameObject placement)
    {
        ClearCellFillSprite();
        
        placementObject = placement;

        startPos = placementObject.transform.position;
        GetScenery(placementObject.GetComponent<Scenery>().nameScenery);
        
        map.markCellsAsFree(cellX(placementObject.transform.position), cellZ(placementObject.transform.position), scenerySizeX, scenerySizeY); 

        SetCellFillSpritePosition(cellX(placementObject.transform.position), cellZ(placementObject.transform.position), scenerySizeX, scenerySizeY);
        AddCellFillSprite();
        
        MainControl.instance.CloseBuildMode();
        MainControl.instance.OpenShopBuildMode();

        GlobalSettings.instance.Object = placementObject.GetHashCode().ToString();
        GlobalSettings.instance.Build = false;

        Camera.main.transform.position = new Vector3(placementObject.transform.position.x, 25, Camera.main.transform.position.z);

        DebugStr = $"Object:{placementObject.name} Is Selected";
        DEBUGGER.Log(ColorType.Green, DebugStr);
        DebugStr = $"Placement Is Available";
        DEBUGGER.Log(ColorType.Purple, DebugStr);
    }

    public Scenery GetScenery(string name)
    {
        Scenery scenery = SceneryManager.instance.FindScenery(name);
        scenerySize = scenery.ScenerySize;
        scenerySizeX = scenery.Size.x;
        scenerySizeY = scenery.Size.y;
        return scenery;
    }
}