using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Map : ScriptableObject
{
    [SerializeField] private int _gridHeight;
    [SerializeField] private int _gridWidth;
    [SerializeField] private Sprite _background;//TODO добавить
    [SerializeField] private List<CellColumn> _cellColumns;

    public int GridHeight => _gridHeight;
    public int GridWidth => _gridWidth;
    public CellData Cells(int x, int y)
    {
        return _cellColumns[x].CellDatas[y];
    }

    public void CreateMap(int height, int width)
    {
        //TODO добабить ограничение на размер карты
        _gridHeight = height > 300 ? 300: height;
        _gridWidth = width > 300 ? 300 : width;
        _cellColumns = new List<CellColumn>();
        for (int x = 0; x < GridWidth; x++)
        {
            _cellColumns.Add(new CellColumn());
            for (int y = 0; y < GridHeight; y++)
            {
                _cellColumns[x].CellDatas.Add(new CellData(x, y));
            }
        }
    }

}