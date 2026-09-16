using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CubeInputReader : MonoBehaviour
{
    public event Action Clicked;

    private void OnMouseDown()
    {
        Clicked?.Invoke();
    }
}