using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeSplitter _cubePrefab;

    public CubeSplitter Spawn(Vector3 position, Vector3 scale, float chance)
    {
        CubeSplitter cube = Instantiate(_cubePrefab, position, Quaternion.identity);
        cube.transform.localScale = scale;
        cube.SetSplitChance(chance);
        return cube;
    }
}
