using System;
using System.Collections.Generic;

[Serializable]
public class CellColumn
{
    public List<CellData> CellDatas;
    public CellColumn()
    {
        CellDatas = new List<CellData>();
    }
}