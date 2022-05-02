using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MapView
{
    [SerializeField] private List<Transform> _tileParents;//0-6 (6 - grid)
    
    [SerializeField] private GameObject _mapCell;
    [SerializeField] private GameObject _voidCell;
    [SerializeField] private MapEditorResourses _mapEditorResourses;
    [SerializeField] private Slider _load;
    [SerializeField] private CameraBounds _cameraBounds;

    public Transform GridParent => _tileParents[6];

    private Map _map;
    private Transform _flyingObject;

    public void ClearAll()
    {
        foreach (var tile in _tileParents)
            for (int i = 0; i < tile.childCount; i++)
            {
                GameObject.Destroy(tile.GetChild(i).gameObject);
            }
    }


    public IEnumerator LoadMap(Map map)
    {
        ClearAll();
        _map = map;
        _load.transform.parent.gameObject.SetActive(true);
        _load.value = 0;
        _load.maxValue = map.GridWidth * map.GridHeight;
        GameObject temp;
        _cameraBounds.ResetBounds();
        _cameraBounds.transform.position = new Vector3(-1200, -map.GridHeight * 25 - 400);
        _cameraBounds.pointa = new Vector3(map.GridWidth * 100 + 300, 400);

        for (int x = 0; x < map.GridWidth; x++)
        {
            for (int y = 0; y < map.GridHeight; y++)
            {
                //grid
                temp = GameObject.Instantiate(_mapCell, _tileParents[6]);
                temp.transform.localPosition = new Vector3(x * 100 + ((y % 2 == 0) ? 0 : -50), y * -25, 0);
                temp.GetComponent<Cell>().SetCell(x, y, map.Cells(x, y));

                SetTile(0, x, y);
                SetTile(1, x, y);
                SetTile(2, x, y);
                SetTile(3, x, y);
                SetTile(4, x, y);
                SetTile(5, x, y);
                _load.value++;
                
            }
            yield return null;
        }
        _load.transform.parent.gameObject.SetActive(false);
    }
    private void SetTile(int view, int x, int y)
    {
        var tile = _map.Cells(x, y).GetTile(view);
        GameObject temp;
        if (tile.id != -1)
        {
            temp = GameObject.Instantiate(_mapEditorResourses.AllResourses()[view][tile.id].Prefab, _tileParents[view]);
            temp.transform.localPosition = new Vector3(x * 100 + ((y % 2 == 0) ? 0 : -50) + tile.delta.x, y * -25 + tile.delta.y, 0);
        }
        else
        {
            temp = GameObject.Instantiate(_voidCell, _tileParents[view]);
            temp.transform.localPosition = new Vector3(x * 100 + ((y % 2 == 0) ? 0 : -50), y * -25, 0);
        }
        temp.name = string.Format("Cell[{0}; {1}]", x.ToString("000"), y.ToString("000"));
    }
    public Transform SelectTile(int view, GameObject tile)
    {
        _flyingObject = GameObject.Instantiate(tile, _tileParents[view]).transform;
        _flyingObject.transform.position = new Vector3(0, 0, -10f);
        return _flyingObject;
    }
    public void SortingTile(int view)
    {
        foreach (Transform t in _tileParents[view].GetComponentsInChildren<Transform>())
        {
            List<Transform> children = t.Cast<Transform>().ToList();
            children.Sort((Transform t1, Transform t2) => { return t1.name.CompareTo(t2.name); });
            for (int i = 0; i < children.Count; ++i)
            {
                children[i].SetSiblingIndex(i);
            }
        }
    }
}
