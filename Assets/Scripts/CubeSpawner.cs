using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public Cube Spawn(Vector3 position, Vector3 scale, float chance)
    {
        Cube cube = Instantiate(_cubePrefab, position, Quaternion.identity);
        cube.transform.localScale = scale;
        cube.SetSplitChance(chance);
        cube.GetComponent<CubeView>().SetRandomColor();
        return cube;
    }

    public void Destroy(Cube cube)
    {
        Destroy(cube.gameObject);
    }

    public void ApplyExplosion(Cube cube, Vector3 center, float force, float radius)
    {
        cube.GetComponent<Rigidbody>()
            .AddExplosionForce(force, center, radius, 1f, ForceMode.Impulse);
    }

    public void Explode(Vector3 center, float force, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(center, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(force, center, radius, 1f, ForceMode.Impulse);
            }
        }
    }
}
