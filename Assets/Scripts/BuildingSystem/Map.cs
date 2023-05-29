using UnityEngine;

public class Map : MonoBehaviour
{
    private Cell[,] grid = new Cell[5, 5];                                                                          //массив клеток карты
    //Параметры карты(Для редактора)
    private float cellSize = 1;                                                                                     //Размер Клетки карты
    [SerializeField, Range(25, 150)]    private int xLength;                                                        //Длина  карты по оси [X]
    [SerializeField, Range(25, 150)]    private int zLength;                                                        //Ширина карты по оси [Y] 

    //начальные координаты построения карты (по умолчанию от нуля)
    public float xStartPoint;
    public float zStartPoint;
    public Cell[,] Grid { get { return grid; } }                                                                    //Ссылка на карточную сетку
    public float CellSize
    {
        get
        {
            return cellSize;
        }
        set
        {
            cellSize = value;
        }
    }

    public int XLength
    {
        get
        {
            return Mathf.Clamp(xLength, 25, 150);
        }
        set
        {
            xLength = value;
        }
    }

    public int ZLength
    {
        get
        {
            return Mathf.Clamp(zLength, 25, 150);
        }
        set
        {
            zLength = value;     
        }
    }
    

    void Awake()
    {
        grid = new Cell[XLength, ZLength];

        for (int x = 0; x < XLength; x++)
            for (int z = 0; z < ZLength; z++)
            {
                Cell cell = new Cell(x, z, cellSize);
                grid[x, z] = cell;
            }

        int rows = grid.GetUpperBound(0) + 1;
        int columns = grid.Length / rows;


        gameObject.transform.position = new Vector3(xStartPoint+(rows * cellSize)/2, 0, zStartPoint + (columns * cellSize) / 2);
        gameObject.transform.localScale = new Vector3(rows  * cellSize, 0.5f, columns * cellSize);
	}

    public void markCellsAsBusy(int x, int z, int sizeX, int sizeY)     //Обозначение ячеек как занятых
    {
        Cell[] cells = getCells(x, z, sizeX, sizeY);
        foreach (Cell cell in cells)
        {
            if (cell == null)
                continue;

            cell.isFill = true;
        }
    }

    public void markCellsAsFree(int x, int z, int sizeX, int sizeY)     //Обозначение ячеек как свободных
    {
        Cell[] cells = getCells(x, z, sizeX, sizeY);
        foreach (Cell cell in cells)
        {
            if (cell == null)
                continue;

            cell.isFill = false;
        }
    }

    public Cell[] getCells(int x, int z, int sizeX, int sizeY)          //Выделение ячеек по размеру объекта
    {
        Cell[] result = new Cell[sizeX * sizeY];

        bool offsetX = sizeX % 2 == 0 ? true : false;
        bool offsetY = sizeY % 2 == 0 ? true : false;

        int Xstart = x - (sizeX - 1) / 2 - (offsetX ? 1 : 0);
        int Zstart = z - (sizeY - 1) / 2 - (offsetY ? 1 : 0);

        int index = 0;
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (j + Zstart >= ZLength || i + Xstart >= XLength || j + Zstart<0 || i + Xstart < 0)
                {
                    continue;
                }
                result[index] = Grid[i+ Xstart, j+ Zstart];
                index++;
            }
        }
        
        return result;
    }

    
    public bool IsСellsEmpty(int x, int z, int sizeX, int sizeY)        //Проверка на занятость клеток по размеру объетка
    {
        foreach (Cell cell in getCells(x, z, sizeX, sizeY))
            if (cell!=null)
                if (cell.isFill)
                    return false;

        return true;
    }

    public Vector3 selectCells(int x, int z, int sizeX, int sizeY)      //Вычисление координаты для Централизования позиции Модели объекта
    {
        Vector3 sum = Vector3.zero;
        Cell[] cells = getCells(x, z, sizeX, sizeY);
        foreach (Cell cell in cells)
        {
            if (cell == null)
                continue;
            sum += new Vector3 (cell.x, 0, cell.z);
        }
        sum = new Vector3 (sum.x/(sizeX*sizeY), sum.y, sum.z/(sizeX*sizeY));
        return sum;
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()      //Отрисовка поля в Гизмосе
    {
        if (Grid.Length == 0)
            return;

        int rows = grid.GetUpperBound(0) + 1;
        int columns = Grid.Length / rows;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < XLength+1; i++)
        {
            //Gizmos.DrawLine(new Vector3(xStartPoint + cellSize * i, 0, zStartPoint), new Vector3(xStartPoint + cellSize * i, 0, zStartPoint + cellSize * ZLength));
        }

        for (int i = 0; i < ZLength+1; i++)
        {
            //Gizmos.DrawLine(new Vector3(xStartPoint , 0, zStartPoint + cellSize * i), new Vector3(xStartPoint + cellSize * XLength, 0, zStartPoint + cellSize * i));
        }
    }
#endif
}

public class Cell
{
    public float cellSize;      //размер ячейки
    public bool isFill;         //заполненность

    // координаты ячейки в сетке (для возможности получения координат из конкретной ячейки, а не из объекта карты)
    public int x;
    public int z;
    public Cell(int x, int z, float cellSize)
    {
        this.x = x;
        this.z = z;
        this.cellSize = cellSize;
    }


}

