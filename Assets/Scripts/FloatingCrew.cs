using Enums;
using UnityEngine;
using Util.Extensions;

public class FloatingCrew : MonoBehaviour
{
    public PlayerColorType colorType;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _direction;
    private float _floatingSpeed;
    private const float SpeedOutsideScreen = 5f; // 화면 밖으로 너무 나가 있으면 스피드 올려주기 위함.
    private float _rotateSpeed;
    private static readonly int PlayerColor = Shader.PropertyToID("_PlayerColor");
    private const float ScreenBorderOffset = 1f; // 스크린 경계와의 오프셋을 두기 위함. 속도 변화가 유저에게 보이지 않도록.
    private const int SortingOrderMax = 32767;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitFloatingCrew(Sprite sprite, PlayerColorType color, Vector3 dir, float floatSpeed,
        float rotateSpeed, float size)
    {
        colorType = color;
        _direction = dir;
        _floatingSpeed = floatSpeed;
        _rotateSpeed = rotateSpeed;

        _spriteRenderer.sprite = sprite;
        _spriteRenderer.material.SetColor(PlayerColor, PlayerColors.GetColor(colorType));

        transform.localScale = new Vector3(size, size, size);
        _spriteRenderer.sortingOrder = (int)Mathf.Lerp(1, SortingOrderMax, size); // z order
    }

    private void Update()
    {
        var isPositionInScreen = transform.position.IsPositionInScreen(Camera.main, ScreenBorderOffset);
        var optimizedFloatingSpeed = isPositionInScreen
            ? _floatingSpeed
            : SpeedOutsideScreen;
        transform.position += _direction * (optimizedFloatingSpeed * Time.unscaledDeltaTime); // ordered for multiplication efficiency
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, _rotateSpeed));
    }
}
