using Godot;

namespace RequiemEcosDoSilencio.Prototype;

internal static class PrototypeInput
{
    public const string MoveLeft = "prototype_move_left";
    public const string MoveRight = "prototype_move_right";
    public const string MoveUp = "prototype_move_up";
    public const string MoveDown = "prototype_move_down";
    public const string Dash = "prototype_dash";
    public const string ShiftClamor = "prototype_shift_clamor";
    public const string CardOne = "prototype_card_1";
    public const string CardTwo = "prototype_card_2";
    public const string CardThree = "prototype_card_3";
    public const string CardFour = "prototype_card_4";
    public const string Restart = "prototype_restart";
    public const string Pause = "prototype_pause";

    public static readonly string[] CardActions = { CardOne, CardTwo, CardThree, CardFour };

    public static void EnsureDefaultBindings()
    {
        EnsureAction(MoveLeft, Key.A, Key.Left, JoyAxis.LeftX, -1f);
        EnsureAction(MoveRight, Key.D, Key.Right, JoyAxis.LeftX, 1f);
        EnsureAction(MoveUp, Key.W, Key.Up, JoyAxis.LeftY, -1f);
        EnsureAction(MoveDown, Key.S, Key.Down, JoyAxis.LeftY, 1f);
        EnsureAction(Dash, Key.Space, JoyButton.A);
        EnsureAction(ShiftClamor, Key.Q, JoyButton.LeftShoulder);
        EnsureAction(CardOne, Key.Key1, JoyButton.DpadLeft);
        EnsureAction(CardTwo, Key.Key2, JoyButton.DpadUp);
        EnsureAction(CardThree, Key.Key3, JoyButton.DpadRight);
        EnsureAction(CardFour, Key.Key4, JoyButton.DpadDown);
        EnsureAction(Restart, Key.R, JoyButton.Back);
        EnsureAction(Pause, Key.Escape, JoyButton.Start);
    }

    private static void EnsureAction(string action, Key primary, Key secondary, JoyAxis axis, float axisValue)
    {
        EnsureActionExists(action);
        if (InputMap.ActionGetEvents(action).Count > 0)
            return;

        AddKey(action, primary);
        AddKey(action, secondary);
        InputMap.ActionAddEvent(action, new InputEventJoypadMotion
        {
            Axis = axis,
            AxisValue = axisValue
        });
    }

    private static void EnsureAction(string action, Key key, JoyButton button)
    {
        EnsureActionExists(action);
        if (InputMap.ActionGetEvents(action).Count > 0)
            return;

        AddKey(action, key);
        InputMap.ActionAddEvent(action, new InputEventJoypadButton { ButtonIndex = button });
    }

    private static void EnsureActionExists(string action)
    {
        if (!InputMap.HasAction(action))
            InputMap.AddAction(action, 0.22f);
    }

    private static void AddKey(string action, Key key)
    {
        InputMap.ActionAddEvent(action, new InputEventKey { PhysicalKeycode = key });
    }
}
