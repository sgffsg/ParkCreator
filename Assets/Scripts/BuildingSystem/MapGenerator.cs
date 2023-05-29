using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Map map;
    public Terrain terrain;
    public SceneryManager sceneryManager;
    [SerializeField, Range(0, 1)] private float thickness;

    [SerializeField, Range(1, 6)] private ushort fillStep;
    public ushort fogOffset;
    public Transform fogPlane;

    void Start()
    {
        //BoardSceneryGenerate();
        TerrainGenerate();
        FogPlaneGenerate();
    }

    //Заполнение краев карты объектами
    /* private void BoardSceneryGenerate()
    {
        for (int x = 1; x < map.XLength; x += fillStep)
        {
            for (int z = 1; z < map.ZLength; z += fillStep)
            {
                //пропуск ячейки, если она находится вне определенных границ "края" карты
                if (z  > map.LenghtOfBoard && map.ZLength - z > map.LenghtOfBoard
                    && x > map.LenghtOfBoard && map.XLength - x > map.LenghtOfBoard)
                    continue;

                 if (UnityEngine.Random.Range(0, 1.0f) > thickness)      //проверка на шанс установки декорации, в соответствии с установленной частотой появления
                    continue;

                int index = UnityEngine.Random.Range(0, sceneryManager.sceneryList.Count);   //Индекс Декорации для спауна

                if (!map.IsСellsEmpty(x, z, sceneryManager.sceneryList[index].Size.x, sceneryManager.sceneryList[index].Size.y))          //Пропуск если клетка занята
                    continue;
                GameObject.Instantiate(sceneryManager.sceneryList[index].gameObject, sceneryManager.getPosition(x,z,map.CellSize,index), Quaternion.identity); 
				//Отметка клеток как занятых в карте
                map.markCellsAsBusy(x,z, sceneryManager.sceneryList[index].Size.x, sceneryManager.sceneryList[index].Size.y);
            }
        }
    } */

    //Генерация поверхности под размер карты
    private void TerrainGenerate()
    {

        terrain.terrainData.size = new Vector3(map.CellSize * map.XLength, 1, map.CellSize * map.ZLength);  //розмір поверхні по розміру мапи
        terrain.transform.position = new Vector3(map.xStartPoint, 0, map.zStartPoint);          //початкова точка в початковій точці мапи
    }

    //Генерация тумана
    private void FogPlaneGenerate()
    {
        setFogPanelTransform(Transform.Instantiate(fogPlane, new Vector3((map.CellSize*map.XLength)/2,-2f, (map.CellSize * (map.ZLength - fogOffset))),  Quaternion.identity), false, 3.2f);
        setFogPanelTransform(Transform.Instantiate(fogPlane, new Vector3((map.CellSize * (map.XLength - fogOffset)), -2f, (map.CellSize * map.ZLength)/2), Quaternion.identity), true, 3.2f);
        setFogPanelTransform(Transform.Instantiate(fogPlane, new Vector3(map.xStartPoint+fogOffset * map.CellSize, -2f, (map.CellSize * map.ZLength)/2), Quaternion.identity), true, 3.2f);
        //setFogPanelTransform(Transform.Instantiate(fogPlane, new Vector3((map.CellSize * map.XLength) / 2, -2f, map.zStartPoint+ fogOffset* map.CellSize), Quaternion.identity), true);

        setFogPanelTransform(Transform.Instantiate(fogPlane, new Vector3((map.CellSize * map.XLength) / 2, -2f, (map.CellSize * (map.ZLength - fogOffset-1))), Quaternion.identity), false, 5f);
        setFogPanelTransform(Transform.Instantiate(fogPlane, new Vector3((map.CellSize * (map.XLength - fogOffset-1)), -2f, (map.CellSize * map.ZLength) / 2), Quaternion.identity), true, 5f);
        setFogPanelTransform(Transform.Instantiate(fogPlane, new Vector3(map.xStartPoint + (fogOffset+1) * map.CellSize, -2f, (map.CellSize * map.ZLength) / 2), Quaternion.identity), true, 5f);
    }

    private void setFogPanelTransform(Transform fogPanel, bool isHorizontal, float scaleZ)
    {
        fogPanel.localScale = new Vector3(map.XLength / 8, 0, scaleZ);
        fogPanel.Rotate(90, isHorizontal?90:0, 0);
    }
}