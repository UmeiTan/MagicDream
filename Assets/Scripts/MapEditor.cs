using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    [SerializeField] private MapView _mapView;
    [SerializeField] private MapEditorResourses _mapEditorResourses;
    [SerializeField] private List<Transform> _resoursesView;
    [SerializeField] private InputField _fileName;
    [SerializeField] private InputField _gridHeight;
    [SerializeField] private InputField _gridWidth;
    [SerializeField] private Text _currentFileName;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Toggle _shift;
    [SerializeField] private GraphicRaycaster _graphicRaycaster;
    [SerializeField] private EventSystem _EventSystem;

    private Map _map;
    private int _idResourse;
    private int _view;
    private Transform _flyingObject;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Start()
    {
        GameObject temp;
        int id, view = 0;
        foreach (var list in _mapEditorResourses.AllResourses())
        {
            id = 0;
            foreach (var item in list)
            {
                temp = Instantiate(item.Preview, _resoursesView[view]);
                temp.GetComponentInChildren<Text>().text = item.Preview.name;
                temp.name = id.ToString();
                int t = new int();
                t = id;
                temp.GetComponent<Button>().onClick.AddListener(() =>
                {
                    SetResourse(t);
                });
                id++;
            }
            view++;
        }
    }
    public void Save()
    {
        if (_map != null)
        {
            EditorUtility.SetDirty(_map);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
    public void CreateMap()
    {
        Save();
        try
        {
            int height = int.Parse(_gridHeight.text);
            int width = int.Parse(_gridWidth.text);
            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Maps/" + _fileName.text + ".asset");
            _map = ScriptableObject.CreateInstance<Map>();
            _currentFileName.text = path[12..];
            _map.CreateMap(height, width);
            AssetDatabase.CreateAsset(_map, path);
            StartCoroutine(_mapView.LoadMap(_map));
        }
        catch (System.Exception)
        {

            throw;
        }
    }
    public void LoadMap()
    {
        Save();
        string path = "Assets/Maps/" + _fileName.text + ".asset";
        try
        {
            _map = AssetDatabase.LoadAssetAtPath<Map>(path);
            _currentFileName.text = path[12..];
            StartCoroutine(_mapView.LoadMap(_map));
        }
        catch (System.Exception)
        {
            Debug.Log("Файл не найден");
        }
    }
    public void NewSaveFile()
    {
        if (_map != null)
        {
            Save();
            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Maps/" + _currentFileName.text);
            AssetDatabase.CopyAsset("Assets/Maps/" + _currentFileName.text, path);
            _currentFileName.text = path[12..];
            _map = null;
            _map = AssetDatabase.LoadAssetAtPath<Map>(path);
            StartCoroutine(_mapView.LoadMap(_map));
        }
    }

    private void Update()
    {
        if (_flyingObject != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SetResourse(-1);
                return;
            }
            _flyingObject.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, _layer); 
                PointerEventData pointer = new PointerEventData(_EventSystem);
                pointer.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                _graphicRaycaster.Raycast(pointer, results);
                
                if (results.Count == 0)
                {
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, _layer);

                    if (hit && hit.collider.GetComponent<Cell>())
                    {
                        var hitXY = hit.collider.GetComponent<Cell>().XYposition;
                        var temp = _flyingObject.parent.GetChild(hitXY.y + _map.GridHeight * hitXY.x).gameObject;
                        Debug.Log((_map.GridWidth * hitXY.y + hitXY.x) + "  |   " + hitXY + "  |   " + temp);
                        Destroy(temp);
                        var pos = hit.transform.position;
                        if (_shift.isOn)
                        {
                            _map.Cells(hitXY.x, hitXY.y).SetTile(_view, _idResourse, _flyingObject.transform.position - pos);
                        }
                        else
                        {
                            _map.Cells(hitXY.x, hitXY.y).SetTile(_view, _idResourse, Vector3.zero);
                            _flyingObject.transform.position = pos;
                        }

                        _flyingObject.name = string.Format("Cell[{0}; {1}]", hitXY.x.ToString("000"), hitXY.y.ToString("000"));//Cell[" + hitXY.x + "; " + hitXY.y + "]";
                        _flyingObject.SetSiblingIndex(hitXY.y + _map.GridHeight * hitXY.x);
                        _flyingObject = null;
                        //_mapView.SortingTile(_view);
                        SetResourse(_idResourse);
                    }
                    else
                    {
                        Debug.Log("none");
                    }
                }
            }
        }
    }
    public void SetResourse(int idResourse)
    {
        if (_flyingObject != null)
        {
            Destroy(_flyingObject.gameObject);
        }
        if (idResourse != -1)
        {
            _idResourse = idResourse;
            _flyingObject = _mapView.SelectTile(_view, _mapEditorResourses.AllResourses()[_view][idResourse].Prefab);
        }
    }
    public void SetView(int view)
    {
        _view = view;
    }
    private void OnDestroy()
    {
        Save();
    }
}