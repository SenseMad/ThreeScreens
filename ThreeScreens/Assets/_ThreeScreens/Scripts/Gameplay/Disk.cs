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

      // даже если колонка переполнена, ставим snapped = true, 
      // чтобы Update() не продолжал бесконечно пытаться
      snapped = true;

      if (!snappedSuccessfully)
      {
        // Можно добавить эффект "ударился и исчез" или что-то ещё
        Debug.Log("Диск не поместился в колонку!");
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