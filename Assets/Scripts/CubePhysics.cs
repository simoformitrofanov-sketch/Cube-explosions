using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubePhysics : MonoBehaviour
{
    private Rigidbody _cubeRigidbody;

    private void Awake()
    {
        _cubeRigidbody = GetComponent<Rigidbody>();
    }

    public void Explode(Vector3 center, float force, float radius)
    {
        _cubeRigidbody.AddExplosionForce(force, center, radius, 1f, ForceMode.Impulse);
    }
}
 