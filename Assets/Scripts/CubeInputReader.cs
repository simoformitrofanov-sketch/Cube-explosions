using System;
using UnityEngine;

public class CubeInputReader : MonoBehaviour
{
    public event Action<Cube> CubeClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Cube>(out Cube cube))
                {
                    CubeClicked?.Invoke(cube);
                }
            }
        }
    }
}