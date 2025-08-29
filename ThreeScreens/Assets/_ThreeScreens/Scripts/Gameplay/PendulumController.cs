using UnityEngine;

public class PendulumController : MonoBehaviour
{
  [SerializeField] private float _length = 2f;
  [SerializeField] private float _maxAngle = 45f;
  [SerializeField] private float _gravity = 9.81f;

  [SerializeField] private Transform _hookPoint;
  [SerializeField] private DiskSpawner _spawner;

  private float time;

  public Transform Hook => _hookPoint;

  private void Update()
  {
    time += Time.deltaTime;

    float theta = _maxAngle * Mathf.Deg2Rad * Mathf.Cos(Mathf.Sqrt(_gravity / _length) * time);

    float x = Mathf.Sin(theta) * _length;
    float y = -Mathf.Cos(theta) * _length;
    
    _hookPoint.SetLocalPositionAndRotation(new Vector3(x, y, 0), Quaternion.Euler(0, 0, theta * Mathf.Rad2Deg));

    if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
      _spawner.DropCurrentDisk();
  }
}