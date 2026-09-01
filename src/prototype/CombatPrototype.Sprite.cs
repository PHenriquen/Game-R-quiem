using Godot;

namespace RequiemEcosDoSilencio.Prototype;

public partial class CombatPrototype
{
    private const int NoahSpriteColumns = 4;
    private const int NoahSpriteRows = 3;
    private const int NoahIdleFrames = 4;
    private const int NoahRunFrames = 8;
    private const float NoahIdleFps = 4f;
    private const float NoahRunFps = 12f;

    private readonly Texture2D? _noahSpriteSheet =
        GD.Load<Texture2D>("res://assets/sprites/noah/noah_idle_run_v1.png");

    private bool DrawNoahProductionSprite()
    {
        if (_noahSpriteSheet == null)
            return false;

        Vector2 movement = Input.GetVector(
            PrototypeInput.MoveLeft,
            PrototypeInput.MoveRight,
            PrototypeInput.MoveUp,
            PrototypeInput.MoveDown);

        bool moving = movement.LengthSquared() > 0.01f || _dashRemaining > 0f;
        int frame = moving
            ? NoahIdleFrames + (int)(_elapsed * NoahRunFps) % NoahRunFrames
            : (int)(_elapsed * NoahIdleFps) % NoahIdleFrames;

        Vector2 textureSize = _noahSpriteSheet.GetSize();
        Vector2 frameSize = new(
            textureSize.X / NoahSpriteColumns,
            textureSize.Y / NoahSpriteRows);

        int column = frame % NoahSpriteColumns;
        int row = frame / NoahSpriteColumns;
        Rect2 source = new(new Vector2(column, row) * frameSize, frameSize);

        bool faceLeft = _playerFacing.X < -0.05f;
        float dashStretch = _dashRemaining > 0f ? 1.08f : 1f;
        Vector2 drawSize = new(112f * dashStretch, 112f);
        Rect2 destination = new(
            new Vector2(-drawSize.X * 0.5f, -82f),
            drawSize);

        DrawSetTransform(
            _playerPosition,
            0f,
            new Vector2(faceLeft ? -1f : 1f, 1f));
        DrawTextureRectRegion(destination, _noahSpriteSheet, source);
        DrawSetTransform(Vector2.Zero, 0f, Vector2.One);

        bool requiem = _cadence >= 90f;
        Vector2 forward = _playerFacing.LengthSquared() > 0.01f
            ? _playerFacing.Normalized()
            : Vector2.Right;
        Vector2 side = new(-forward.Y, forward.X);
        Vector2 leftHand = _playerPosition + forward * 4f + side * 19f;
        Vector2 rightHand = _playerPosition + forward * 4f - side * 19f;

        DrawClamor(forward, side, leftHand, rightHand, requiem);

        if (_clamorShiftDisplay > 0f)
            DrawClamorShift(forward, side, leftHand, rightHand);

        if (requiem)
        {
            DrawArc(_playerPosition, 33f, 0f, Mathf.Tau, 40, Spectral, 2f);
            DrawArc(
                _playerPosition + forward * 7f,
                15f,
                -0.45f,
                Mathf.Pi + 0.45f,
                20,
                Ivory.Darkened(0.18f),
                1.4f);
        }

        DrawPulseIndicator();
        return true;
    }
}
