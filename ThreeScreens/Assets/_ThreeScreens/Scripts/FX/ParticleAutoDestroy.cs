using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleAutoDestroy : MonoBehaviour
{
  private new ParticleSystem particleSystem;

  private void Awake()
  {
    particleSystem = GetComponent<ParticleSystem>();
  }

  private void Update()
  {
    if (particleSystem && !particleSystem.IsAlive())
      Destroy(gameObject);
  }
}