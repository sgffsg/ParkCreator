using UnityEngine.EventSystems;
using UnityEngine;

public class MapMoving : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public TouchEventSystem touchEventSystem;                                               //Система свайпов
    private bool available = false;                                                         //Переменная для определения объекта для Drag'a
    private string[] Hash = null;                                                           //Переменная для хранения Hash'a перемещаемого объекта
    private string DebugStr = null;                                                         //Переменная для Логов




    public int cellX(Vector3 realPosition)=> (int)((realPosition.x - GridPlacementSystem.instance.map.xStartPoint) / GridPlacementSystem.instance.map.CellSize);
    public int cellZ(Vector3 realPosition) => (int)((realPosition.z - GridPlacementSystem.instance.map.zStartPoint) / GridPlacementSystem.instance.map.CellSize);

    public void OnDrag(PointerEventData eventData)  //Перемещение объекта
    {   
        float speed = 0.05f;
        if (!available)
        {
            if (GlobalSettings.instance.Mobile)
                speed = 0.025f;
            Vector2 delta = eventData.delta * speed;
            Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x - delta.x, MainCamera.instance.minCamDistance.x, MainCamera.instance.maxCamDistance.x), 
                                                        Camera.main.transform.position.y, 
                                                        Mathf.Clamp(Camera.main.transform.position.z - delta.y, MainCamera.instance.minCamDistance.y, MainCamera.instance.maxCamDistance.y));
        }
        else if (available && GlobalSettings.instance.Drag)
        {            speed = 0.05f;
            Vector3 realPosition = Vector3.zero;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                if (GlobalSettings.instance.Object == Hash[1])
                {
                    Vector2 delta = eventData.delta * speed;
                    Transform transform = GridPlacementSystem.instance.placementObject.transform;
                    GridPlacementSystem.instance.placementObject.transform.position = Vector3.MoveTowards(transform.position,new Vector3(cellX(hit.point), 0 ,cellZ(hit.point)), 100 * Time.deltaTime); 
                    GridPlacementSystem.instance.SetCellFillSpritePosition(cellX(GridPlacementSystem.instance.placementObject.transform.position), cellZ(GridPlacementSystem.instance.placementObject.transform.position), GridPlacementSystem.instance.scenerySizeX, GridPlacementSystem.instance.scenerySizeY);
                    GridPlacementSystem.instance.MoveExtensions();
                }
                else
                {
                    if (GlobalSettings.instance.Mobile)
                        speed = 0.025f;
                    Vector2 delta = eventData.delta * speed;
                    Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x - delta.x, MainCamera.instance.minCamDistance.x, MainCamera.instance.maxCamDistance.x), 
                                                                Camera.main.transform.position.y, 
                                                                Mathf.Clamp(Camera.main.transform.position.z - delta.y, MainCamera.instance.minCamDistance.y, MainCamera.instance.maxCamDistance.y));
                }
            }
        }
    }


    
    public void OnBeginDrag(PointerEventData eventData) //При начале Перемещения объекта
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!GlobalSettings.instance.Zoom)
        {
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                if (objectHit.tag == "map")
                {
                    DebugStr = $"MoveObject: Map\n";
                    DEBUGGER.Log(ColorType.LightBlue, DebugStr);
                    available = false;
                }
                if (objectHit.tag == "PlacementObject")
                {
                    while(true)
                    {
                        if (objectHit.name != "Model")
                            objectHit = objectHit.transform.parent.gameObject;
                        else
                        {
                            objectHit = objectHit.transform.parent.gameObject;
                            break;
                        }
                    }
                    available = false;
                    if (!GlobalSettings.instance.Play)
                    {
                        if (GlobalSettings.instance.Select)
                        {
                            available = true;
                        }
                    }

                    if (GlobalSettings.instance.Object == Hash[1])
                    {
                        DebugStr = $"MoveObject: {GridPlacementSystem.instance.placementObject.name}\n";
                        DebugStr += $"Current MoveObject Is Match with SelectedObject";
                        DEBUGGER.Log(ColorType.LightBlue, DebugStr);
                    }
                    else
                    {
                        DebugStr = $"MoveObject: {objectHit.transform.name}\n";
                        DebugStr += $"Current MoveObject Is Not Match with SelectedObject\n";
                        DebugStr += $"Map Was Selected as MoveObject";
                        DEBUGGER.Log(ColorType.Orange, DebugStr);
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)   //При окончании Перемещения объекта
    {
        
    }

    void Start()    //Подписка на событие клика по объекту
    {
        touchEventSystem = TouchEventSystem.instance;
        touchEventSystem.touchMessage += CheckTap;
    }
    private void OnDestroy()   //Отписка от событие клика по объекту
    {
        touchEventSystem.touchMessage -= CheckTap;
    }

    public void CheckTap()  //Клик по объекту (Выделение)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!GlobalSettings.instance.Zoom)
        {
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                if (objectHit.tag == "PlacementObject")
                {
                    while(true)
                    {
                        if (objectHit.name != "Model")
                            objectHit = objectHit.transform.parent.gameObject;
                        else
                        {
                            objectHit = objectHit.transform.parent.gameObject;
                            break;
                        }
                    }
                    GlobalSettings.instance.Drag = true;
                    Hash = objectHit.name.Split('#');
                    if (!GlobalSettings.instance.Select)
                    {
                        if (!GlobalSettings.instance.Play)
                        {
                            if (GlobalSettings.instance.Build == false)
                            {
                                GlobalSettings.instance.Select = true;
                                GridPlacementSystem.instance.SelectScenery(objectHit);
                                GridPlacementSystem.instance.SetExtensionsSprite();
                                GridPlacementSystem.instance.SetCellFillSpritePosition(cellX(GridPlacementSystem.instance.placementObject.transform.position), cellZ(GridPlacementSystem.instance.placementObject.transform.position), GridPlacementSystem.instance.scenerySizeX, GridPlacementSystem.instance.scenerySizeY);
                            }
                        }
                    }
                }
            }
        }
    }
}
