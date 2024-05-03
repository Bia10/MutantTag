using Source.Lib;
using Source.WCSharp.Api.Extensions;
using System.Linq;
using WCSharp.Api;
using WCSharp.Events;
using static WCSharp.Api.Common;

namespace Source.Mechanics;

/// <summary>
/// Represents a hero in the map.
/// </summary>
public class Hero
{
	/// <summary>
	/// Sets up the triggers for heroes.
	/// </summary>
	public static void Setup()
	{
		// Trigger: Player finished upgrading to castle spawn hero
		PlayerUnitEvents.Register(UnitTypeEvent.FinishesUpgrade, () =>
		{
			unit caster = GetTriggerUnit();
			if (caster.UnitType is not Constants.UNIT_H001_CASTLE_HUMAN) return;

			var numberOfHeroesOwnedByOwner = caster.Owner.GetUnitCount(GetObjectName(Constants.UNIT_H00G_PALADIN_HERO));
			if (numberOfHeroesOwnedByOwner != 0) return;

			unit.Create(caster.Owner, Constants.UNIT_H00G_PALADIN_HERO, caster.X, caster.Y);
			SpecialEffects.FireAway(SpecialEffects.AwakenHero, caster.X, caster.Y);
		});

		// Trigger: Hero died respawn after 10 secs at hero house owned by dying hero's player.
		PlayerUnitEvents.Register(UnitTypeEvent.Dies, () =>
		{
			unit deadUnit = GetTriggerUnit();
			if (!deadUnit.IsType(UNIT_TYPE_HERO)) return;

			timer revivalTimer = CreateTimer();
			revivalTimer.Start(10f, false, () =>
			{
				revivalTimer.Dispose();

				unit playerHeroHouse = group.Create()
					.EnumerateUnitsOfPlayer(deadUnit.Owner)
					.EmptyToList()
					.Where(Building.IsHeroHouse)
					.ToList()
					.First();

				deadUnit.Revive(playerHeroHouse.X, playerHeroHouse.Y);
			});
		});

		// Trigger: Various hero upgrades
		PlayerUnitEvents.Register(UnitTypeEvent.FinishesResearch, () =>
		{
			var research = GetResearched();
			unit caster = GetTriggerUnit();
			unit upgradedHero = group.Create()
				.EnumerateUnitsOfPlayer(caster.Owner)
				.EmptyToList()
				.Where(unit => unit.IsType(UNIT_TYPE_HERO))
				.ToList()
				.First();

			switch (research)
			{
				case Constants.UPGRADE_R00B_MIND_TRAINING_HEROES:
					upgradedHero.SetLevel(upgradedHero.Level + 7);
					break;
				case Constants.UPGRADE_R009_POWER_CHARM_ARCHMAGE:
					caster.Owner.SetTechResearched(Constants.UPGRADE_R009_POWER_CHARM_ARCHMAGE, 1);
					break;
				case Constants.UPGRADE_R008_SHADOW_WALK_WARDEN:
					// Add invisibility
					caster.Owner.SetTechResearched(Constants.UPGRADE_R008_SHADOW_WALK_WARDEN, 1);
					break;
				case Constants.UPGRADE_R007_RAIN_OF_ROCKS_MOUTAIN_KING:
					caster.Owner.SetTechResearched(Constants.UPGRADE_R007_RAIN_OF_ROCKS_MOUTAIN_KING, 1);
					upgradedHero.AddAbility(Constants.ABILITY_A00G_RAIN_OF_ROCKS_MOUNTAIN_KING);
					break;
			}
		});
	}
}