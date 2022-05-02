using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraBounds : MonoBehaviour
{
    [SerializeField]
    public Vector3 pointa;
    [SerializeField]
    public Color guiColour;

    private Vector3 _pos;
    private Vector3 _pointa;
    private void Start()
    {
        _pos = transform.position;
        _pointa = pointa;
    }
    public void ResetBounds()
    {
        transform.position = _pos;
        pointa = _pointa;
    }
}
