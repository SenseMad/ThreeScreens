using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
  [SerializeField] private float _minAmplitude = 1f;
  [SerializeField] private float _maxAmplitude = 3f;
  [SerializeField] private float _speed = 2f;

  private Vector3 startPos;
  private float amplitude;
  private float previousSin;

  private void Start()
  {
    startPos = transform.position;
    amplitude = Random.Range(_minAmplitude, _maxAmplitude);
    previousSin = Mathf.Sin(0);
  }

  private void Update()
  {
    float sinValue = Mathf.Sin(Time.time * _speed);

    if (previousSin > 0 && sinValue <= 0)
      amplitude = Random.Range(_minAmplitude, _maxAmplitude);

    previousSin = sinValue;

    float y = Mathf.Abs(sinValue) * amplitude;
    transform.position = startPos + new Vector3(0, y, 0);
  }
}