using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeInputReader _inputReader;

    [Header("Настройки разделения")]
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private float _chanceReductionFactor = 2f;
    [SerializeField] private float _scaleMultiplier = 0.5f;

    [Header("Взрыв")]
    [SerializeField] private float _baseExplosionForce = 5f;
    [SerializeField] private float _baseExplosionRadius = 2f;

    private void OnEnable()
    {
        _inputReader.CubeClicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        _inputReader.CubeClicked -= OnCubeClicked;
    }

    private void OnCubeClicked(Cube cube)
    {
        if (UnityEngine.Random.value > cube.CurrentSplitChance)
        {
            ExplodeCube(cube);
            return;
        }

        Split(cube);
    }

    private void ExplodeCube(Cube cube)
    {
        float scale = cube.transform.localScale.x;
        float force = _baseExplosionForce / scale;
        float radius = _baseExplosionRadius / scale;
        Vector3 center = cube.transform.position;

        _spawner.Explode(center, force, radius);
        _spawner.Destroy(cube);
    }

    private void Split(Cube parent)
    {
        int childCount = UnityEngine.Random.Range(_minChildren, _maxChildren + 1);
        Vector3 spawnPosition = parent.transform.position;
        Vector3 childScale = parent.transform.localScale * _scaleMultiplier;
        float childChance = parent.CurrentSplitChance / _chanceReductionFactor;

        for (int i = 0; i < childCount; i++)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 0.3f;
            Cube child = _spawner.Spawn(spawnPosition + randomOffset, childScale, childChance);
            _spawner.ApplyExplosion(child, spawnPosition, _baseExplosionForce, _baseExplosionRadius);
        }

        _spawner.Destroy(parent);
    }
}
