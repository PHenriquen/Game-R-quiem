using Godot;

namespace RequiemEcosDoSilencio.Bootstrap;

public partial class Bootstrap : Control
{
    private const string StatusText = "PRE-PRODUCTION / PULSE PROTOTYPE";

    public override void _Ready()
    {
        var statusLabel = GetNode<Label>("Layout/Status");
        statusLabel.Text = StatusText;
        GD.Print("Réquiem: Ecos do Silêncio bootstrap ready.");
    }
}
