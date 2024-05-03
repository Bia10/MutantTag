using Source.WCSharp.Api.Extensions;
using System.Collections.Generic;
using System.Linq;
using WCSharp.Api;
using WCSharp.Events;
using static WCSharp.Api.Blizzard;
using static WCSharp.Api.Common;

namespace Source.Mechanics;

/// <summary>
/// Handles setup and events related to workers in the map.
/// </summary>
public class Worker
{
	/// <summary>
	/// Sets up triggers and events related to workers.
	/// </summary>
	public static void Setup()
	{
		// Trigger: Worker was slain by mutants
		PlayerUnitEvents.Register(UnitTypeEvent.Dies, () =>
		{
			unit deadUnit = GetTriggerUnit();
			if (deadUnit.UnitType is not Constants.UNIT_H007_WORKER_PLAYER)
				return;

			player? owner = deadUnit.Owner;
			deadUnit.SafelyRemove();

			// Remove all units except hero house
			IEnumerable<unit> unitsToKill = group.Create()
				.EnumerateUnitsOfPlayer(owner)
				.EmptyToList()
				.Where(Building.IsNotHeroHouse);

			foreach (unit unit in unitsToKill)
				unit.SafelyRemove();

			// Spawn captured worker at center region facing south
			unit capturedWorker = Regions.CenterReg.SpawnUnitAtRandomPoint(Constants.UNIT_H007_WORKER_PLAYER, owner);
			capturedWorker.SetOwner(owner);
			capturedWorker.Select();

			Program.HumanPlayers.DisplayNoticeToPlayers(
				$"{owner.Name} got slain by mutants, his soul was captured and can be rescued by other worker for 400 gold bounty!");
		});

		// Trigger: Rescue worker
		CreateTrigger()
			.RegisterEnterRegion(Regions.CenterReg)
			.AddAction(() =>
			{
				unit enteringUnit = GetTriggerUnit();
				if (enteringUnit.UnitType is not Constants.UNIT_H007_WORKER_PLAYER)
					return;

				IEnumerable<unit> capturedWorkers = group.Create()
					.EnumerateUnitsInRect(Regions.CenterReg)
					.EmptyToList()
					.Where(unit => unit.UnitType == Constants.UNIT_H000_CAPTURED_WORKER_PLAYER)
					.ToList();

				// For each captured worker, give bounty to rescuing player
				enteringUnit.Owner.Gold += capturedWorkers.Count() * 400;

				// Replace captured worker with live worker
				foreach (unit rescuedWorker in capturedWorkers.Select(unit
					         => unit.ReplaceAndReturnUnit(Constants.UNIT_H007_WORKER_PLAYER, true)))
					PanCameraToForPlayer(rescuedWorker.Owner, rescuedWorker.X, rescuedWorker.Y);
			});

		// Trigger: Get mechanical unit on zeppelin
		PlayerUnitEvents.Register(UnitTypeEvent.SpellCast, () =>
		{
			var abilityBeingCast = GetSpellAbility();
			var unitTargeted = GetEventTargetUnit();

			if (abilityBeingCast.Id == Constants.ABILITY_A00D_GET_VEHICLE_WORKER &&
			    unitTargeted.UnitType == Constants.UNIT_N007_ZEPPELIN_NEUTRAL)
			{
				//unitTargeted.AddAbility()
			}
		});
	}
}