using System.Collections.Generic;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Server.Chemistry.Components;

/// <summary>
/// Used for melee weapon entities that should try to inject a
/// contained solution into a target when used to hit it.
/// </summary>
[RegisterComponent]
public sealed partial class MeleeChemicalInjectorComponent : BaseSolutionInjectOnEventComponent
{
    /// <summary>
    /// Optional per-reagent transfer overrides; falls back to <see cref="TransferAmount"/> when empty or missing.
    /// </summary>
    [DataField("transferOverrides")]
    public Dictionary<ProtoId<ReagentPrototype>, FixedPoint2> TransferOverrides = new();
}
