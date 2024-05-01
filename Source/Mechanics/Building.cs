using Source.WCSharp.Api.Extensions;
using WCSharp.Api;
using WCSharp.Events;
using static WCSharp.Api.Common;

namespace Source.Mechanics;

/// <summary>
/// Represents a building in the map.
/// </summary>
public class Building
{
	/// <summary>
	/// Sets up the triggers for buildings.
	/// </summary>
	public static void Setup()
	{
		// Trigger: Building casts Sell Building ability
		PlayerUnitEvents.Register(UnitTypeEvent.SpellEffectOn, () =>
		{
			ability castedSpell = GetSpellAbility();
			if (castedSpell.Id != Constants.ABILITY_A007_SELL_BUILDING_WORKER) return;

			unit caster = GetTriggerUnit();
			switch (caster.UnitType)
			{
				case Constants.UNIT_H004_WEAKER_WALL_HUMAN:
					caster.Owner.Gold += 15;
					break;
				case Constants.UNIT_H009_NORMAL_WALL_HUMAN:
					caster.Owner.Gold += 20;
					break;
				case Constants.UNIT_H003_STRONGER_WALL_HUMAN:
					caster.Owner.Gold += 25;
					break;
				case Constants.UNIT_H008_KEEP_HUMAN:
					caster.Owner.Gold += 225;
					break;
				case Constants.UNIT_H005_SLOW_TOWER_HUMAN:
					caster.Owner.Gold += 300;
					break;
				case Constants.UNIT_N003_WAY_GATE_HUMAN or Constants.UNIT_H00A_REPAIR_SHOP_HUMAN or Constants.UNIT_H002_FLAME_TOWER_HUMAN:
					caster.Owner.Gold += 375;
					break;
				case Constants.UNIT_H001_CASTLE_HUMAN:
					caster.Owner.Gold += 600;
					break;
				case Constants.UNIT_H00B_SPELL_STONE_HUMAN:
					caster.Owner.Gold += 635;
					break;
				case Constants.UNIT_N002_FEEDBACK_TOWER_HUMAN:
					caster.Owner.Gold += 875;
					break;
			}

			// Play alchemist gen sound
			caster.Remove();
		});
	}

	/// <summary>
	/// Checks if the unit is not a hero house.
	/// </summary>
	/// <param name="unit">The unit to check.</param>
	/// <returns>True if the unit is not a hero house, false otherwise.</returns>
	public static bool IsNotHeroHouse(unit unit)
		=> unit.UnitType != Constants.UNIT_H00C_HOUSE_PLAYER &&
		   unit.UnitType != Constants.UNIT_H00D_WARRIOR_HOUSE_PLAYER &&
		   unit.UnitType != Constants.UNIT_N005_WARDEN_HOUSE_PLAYER &&
		   unit.UnitType != Constants.UNIT_N001_MAGE_HOUSE_PLAYER;

	/// <summary>
	/// Checks if the unit is a hero house.
	/// </summary>
	/// <param name="unit">The unit to check.</param>
	/// <returns>True if the unit is a hero house, false otherwise.</returns>
	public static bool IsHeroHouse(unit unit)
		=> !IsNotHeroHouse(unit);
}