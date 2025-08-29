using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Disk : MonoBehaviour
{
  public ColorType Color { get; private set; }

  [SerializeField] private float _snapSpeedThreshold = 0.2f;

  private new Rigidbody2D rigidbody;
  private SpriteRenderer spriteRenderer;
  private ColumnTrigger currentColumn;

  private bool snapped;

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    if (snapped || currentColumn == null || rigidbody.bodyType != RigidbodyType2D.Dynamic)
      return;

    if (rigidbody.velocity.sqrMagnitude < _snapSpeedThreshold * _snapSpeedThreshold)
    {
      if (GridController.Instance.TrySnapDiskToColumn(this, currentColumn.ColumnIndex))
        snapped = true;
    }
    /*if (snapped || currentColumn == null || rigidbody.bodyType != RigidbodyType2D.Dynamic)
      return;

    if (rigidbody.velocity.sqrMagnitude < _snapSpeedThreshold * _snapSpeedThreshold)
    {
      bool snappedSuccessfully = GridController.Instance.TrySnapDiskToColumn(this, currentColumn.ColumnIndex);

      // ���� ���� ������� �����������, ������ snapped = true, 
      // ����� Update() �� ��������� ���������� ��������
      snapped = true;

      if (!snappedSuccessfully)
      {
        // ����� �������� ������ "�������� � �����" ��� ���-�� ���
        Debug.Log("���� �� ���������� � �������!");
      }
    }*/
  }

  public void Initialize(ColorType parColor, Sprite parSprite)
  {
    Color = parColor;
    spriteRenderer.sprite = parSprite;
  }

  public void SetKinematic(bool parIsKinematic)
  {
    rigidbody.bodyType = parIsKinematic ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
    rigidbody.velocity = Vector2.zero;
    rigidbody.angularVelocity = 0;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.TryGetComponent(out ColumnTrigger parCol))
      currentColumn = parCol;
  }
}