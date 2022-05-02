using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int XYposition { get; private set; }
    private CellData _data;
    private bool _target = false;
    private GameObject _targetGO;

    private void Start()
    {
        _targetGO = transform.GetChild(0).gameObject;
    }
    public void SetCell(int x, int y, CellData data)
    {
        gameObject.name = string.Format("Cell[{0}; {1}]", x.ToString("000"), y.ToString("000"));//"%05d"
        XYposition = new Vector2Int(x, y);
        _data = data;
    }
    private void OnMouseEnter()
    {
        if (!_target)
        {
            _targetGO.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if (!_target)
        {
            _targetGO.SetActive(false);
        }
    }

}