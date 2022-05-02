using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class MapEditorResourses : ScriptableObject
{
    [Serializable] public class Resourse
    {
        [SerializeField] private GameObject _preview;
        [SerializeField] private GameObject _prefab;

        public GameObject Preview => _preview;
        public GameObject Prefab => _prefab;
    }
    [SerializeField] private List<Resourse> _tiles;//0
    [SerializeField] private List<Resourse> _rocks;//1
    [SerializeField] private List<Resourse> _decals;//2
    [SerializeField] private List<Resourse> _roads;//3
    [SerializeField] private List<Resourse> _activeObjects;//4
    [SerializeField] private List<Resourse> _buildings;//5

    public List<Resourse> Tiles => _tiles;
    public List<Resourse> Rocks => _rocks;
    public List<Resourse> Decals => _decals;
    public List<Resourse> Roads => _roads;
    public List<Resourse> ActiveObjects => _activeObjects;
    public List<Resourse> Buildings => _buildings;

    public List<List<Resourse>> AllResourses()
    {
        List<List<Resourse>> temp = new List<List<Resourse>>();
        temp.Add(_tiles);
        temp.Add(_rocks);
        temp.Add(_decals);
        temp.Add(_roads);
        temp.Add(_activeObjects);
        temp.Add(_buildings);
        return temp;
    }
}