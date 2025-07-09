using Dalamud.Interface.Colors;

namespace ExampleRotations.Healer;

[Rotation("Example", CombatType.PvE, GameVersion = "7.25")]
[SourceCode(Path = "main/BasicRotations/Healer/WHM_Example.cs")]
[Api(5)]
public sealed class WHM_Example : WhiteMageRotation
{
    #region Config Options
    [RotationConfig(CombatType.PvE, Name = "Use GCDs to heal. (Ignored if you are the only healer in party)")]
    public bool GCDHeal { get; set; } = true;
    #endregion

    #region Tracking Properties
    public override void DisplayStatus()
    {
        ImGui.TextColored(ImGuiColors.DalamudViolet, "Rotation Tracking:");
        ImGui.Text($"GCDHeal: {GCDHeal}");
        ImGui.TextColored(ImGuiColors.DalamudViolet, "Base Tracking:");
        base.DisplayStatus();
    }
    #endregion

    #region Countdown Logic
    protected override IAction? CountDownAction(float remainTime)
    {

        return base.CountDownAction(remainTime);
    }
    #endregion

    #region oGCD Logic
    protected override bool EmergencyAbility(IAction nextGCD, out IAction? act)
    {

        return base.EmergencyAbility(nextGCD, out act);
    }

    protected override bool GeneralAbility(IAction nextGCD, out IAction? act)
    {

        return base.GeneralAbility(nextGCD, out act);
    }

    [RotationDesc()]
    protected override bool DefenseAreaAbility(IAction nextGCD, out IAction? act)
    {

        return base.DefenseAreaAbility(nextGCD, out act);
    }

    [RotationDesc()]
    protected override bool DefenseSingleAbility(IAction nextGCD, out IAction? act)
    {

        return base.DefenseSingleAbility(nextGCD, out act);
    }

    [RotationDesc()]
    protected override bool HealAreaAbility(IAction nextGCD, out IAction? act)
    {
        if (AquaveilPvE.CanUse(out act))
        {
            return true;
        }
        return base.HealAreaAbility(nextGCD, out act);
    }

    [RotationDesc()]
    protected override bool HealSingleAbility(IAction nextGCD, out IAction? act)
    {

        return base.HealSingleAbility(nextGCD, out act);
    }

    protected override bool AttackAbility(IAction nextGCD, out IAction? act)
    {

        return base.AttackAbility(nextGCD, out act);
    }
    #endregion

    #region GCD Logic

    [RotationDesc()]
    protected override bool HealAreaGCD(out IAction? act)
    {

        return base.HealAreaGCD(out act);
    }

    [RotationDesc()]
    protected override bool HealSingleGCD(out IAction? act)
    {

        return base.HealSingleGCD(out act);
    }

    protected override bool GeneralGCD(out IAction? act)
    {
        if (StonePvE.CanUse(out act))
        {
            return true;
        }

        return base.GeneralGCD(out act);
    }
    #endregion

    #region Extra Methods
    public override bool CanHealSingleSpell
    {
        get
        {
            int aliveHealerCount = 0;
            IEnumerable<IBattleChara> healers = PartyMembers.GetJobCategory(JobRole.Healer);
            foreach (IBattleChara h in healers)
            {
                if (!h.IsDead)
                    aliveHealerCount++;
            }

            return base.CanHealSingleSpell && (GCDHeal || aliveHealerCount == 1);
        }
    }
    public override bool CanHealAreaSpell
    {
        get
        {
            int aliveHealerCount = 0;
            IEnumerable<IBattleChara> healers = PartyMembers.GetJobCategory(JobRole.Healer);
            foreach (IBattleChara h in healers)
            {
                if (!h.IsDead)
                    aliveHealerCount++;
            }

            return base.CanHealAreaSpell && (GCDHeal || aliveHealerCount == 1);
        }
    }
    #endregion
}