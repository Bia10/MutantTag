using Source.WCSharp.Api.Extensions;
using WCSharp.Api;
using WCSharp.Events;
using static WCSharp.Api.Common;

namespace Source.Mechanics;

/// <summary>
/// Represents a hero ability in the map.
/// </summary>
public class HeroAbilities
{
	/// <summary>
	/// Sets up the triggers for heroes abilities.
	/// </summary>
	public static void Setup()
	{
		// Trigger: Upon casting resurrection, transform units from undead to living version.
		PlayerUnitEvents.Register(UnitTypeEvent.SpellEffectOn, () =>
		{
			ability castedSpell = GetSpellAbility();
			if (castedSpell.Id != Constants.ABILITY_A00W_RESURRECTION_PALADIN) return;

			unit caster = GetTriggerUnit();
			caster.ReplaceUnits(Constants.UNIT_N000_MUTANT_MUTANT, Constants.UNIT_H00W_FOOTMAN_RESSURECTION);
			caster.ReplaceUnits(Constants.UNIT_U000_GHOUL_MUTANT, Constants.UNIT_H00Y_RIFLEMAN_RESSURECTION);
			caster.ReplaceUnits(Constants.UNIT_U001_ABOMINATION_MUTANT, Constants.UNIT_H00X_KNIGHT_RESSURECTION);
			caster.ReplaceUnits(Constants.UNIT_U008_NECROMANCER_MUTANT, Constants.UNIT_H00Z_SORCERESS_RESSURECTION);
			caster.ReplaceUnits(Constants.UNIT_U007_LICH_MUTANT, Constants.UNIT_H010_SPELLBREAKER_RESSURECTION);
		});

		// Trigger: charm enemy unit
		PlayerUnitEvents.Register(UnitTypeEvent.SpellCast, () =>
		{
			ability castedSpell = GetSpellAbility();
			if (castedSpell.Id == Constants.ABILITY_A01K_CHARM_ARCHMAGE)
			{
				// Condition: has TechResearched(Constants.UPGRADE_R009_POWER_CHARM_ARCHMAGE, 1) ?
				// Enhance charmed unit with bloodlust
			}
		});
	}
}