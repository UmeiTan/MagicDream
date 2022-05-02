using System;
using UnityEngine;

[Serializable]
public class CellData
{
    [SerializeField] private int _arrX;
    [SerializeField] private int _arrY;
    [SerializeField] private int _tile = -1;
    [SerializeField] private float _tileDx;
    [SerializeField] private float _tileDy;
    [SerializeField] private int _rock = -1;
    [SerializeField] private float _rockDx;
    [SerializeField] private float _rockDy;
    [SerializeField] private int _decal = -1;
    [SerializeField] private float _decalDx;
    [SerializeField] private float _decalDy;
    [SerializeField] private int _road = -1;
    [SerializeField] private float _roadDx;
    [SerializeField] private float _roadDy;
    [SerializeField] private int _activeObject = -1;
    [SerializeField] private float _activeObjectDx;
    [SerializeField] private float _activeObjectDy;
    [SerializeField] private int _building = -1;
    [SerializeField] private float _buildingDx;
    [SerializeField] private float _buildingDy;

    public CellData(int x, int y)
    {
        _arrX = x;
        _arrY = y;
    }
    public void SetTile(int view, int id, Vector3 delta)
    {
        switch (view)
        {
            case 0:
                {
                    _tile = id;
                    _tileDx = delta.x;
                    _tileDy = delta.y;
                    break;
                }
            case 1:
                {
                    _rock = id;
                    _rockDx = delta.x;
                    _rockDy = delta.y;
                    break;
                }
            case 2:
                {
                    _decal = id;
                    _decalDx = delta.x;
                    _decalDy = delta.y;
                    break;
                }
            case 3:
                {
                    _road = id;
                    _roadDx = delta.x;
                    _roadDy = delta.y;
                    break;
                }
            case 4:
                {
                    _activeObject = id;
                    _activeObjectDx = delta.x;
                    _activeObjectDy = delta.y;
                    break;
                }
            case 5:
                {
                    _building = id;
                    _buildingDx = delta.x;
                    _buildingDy = delta.y;
                    break;
                }
        }
    }
    public (int id, Vector2 delta) GetTile(int view)
    {
        switch (view)
        {
            case 0: return (_tile, new Vector2(_tileDx, _tileDy));
            case 1: return (_rock, new Vector2(_rockDx, _rockDy));
            case 2: return (_decal, new Vector2(_decalDx, _decalDy));
            case 3: return (_road, new Vector2(_roadDx, _roadDy));
            case 4: return (_activeObject, new Vector2(_activeObjectDx, _activeObjectDy));
            case 5: return (_building, new Vector2(_buildingDx, _buildingDy));
        }
        return (-1, Vector2.zero);
    }
 
}