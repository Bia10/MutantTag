using Source.Lib;
using Source.Mechanics;
using Source.WCSharp.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using WCSharp.Api;
using WCSharp.Events;
using WCSharp.Shared;
using WCSharp.Shared.Data;
using WCSharp.Sync;
using static WCSharp.Api.Common;

namespace Source;

/// <summary>
/// Main routine of the map code
/// </summary>
public static class Program
{
	/// <summary>
	/// Debug flag
	/// </summary>
	public static bool Debug { get; private set; }

	/// <summary>
	/// Collection fo human players
	/// </summary>
	public static List<player> HumanPlayers { get; } = new();

	/// <summary>
	/// Map main
	/// </summary>
	public static void Main()
	{
		// Delay a little since some stuff can break otherwise
		timer timer = CreateTimer();
		TimerStart(timer, 0.01f, false, () =>
		{
			timer.Dispose();
			Start();
		});
	}

	/// <summary>
	/// Map code begins here
	/// </summary>
	private static void Start()
	{
		try
		{
			// Todo: setup quests for map info

			#if DEBUG
			Debug = true;
			Console.WriteLine("This map is in debug mode. The map may not function as expected.");
			PeriodicEvents.EnableDebug();
			PlayerUnitEvents.EnableDebug();
			SyncSystem.EnableDebug();
			Delay.EnableDebug();
			// Todo: cheat commands
			#endif

			Console.WriteLine("Map initialization begins ...");
			MapInitialization();
			Sounds.Setup();
			TextFormatter.TestTextColoring();

			trigger bugsTrigger = SetupBugs();
			SetupDifficulty(bugsTrigger);

			Building.Setup();
			Hero.Setup();
			HeroAbilities.Setup();
			Console.WriteLine("Map initialization done!");
		}
		catch (Exception ex)
		{
			GetLocalPlayer()
				.DisplayWarning(ex.Message);

			#if DEBUG
			if (ex.StackTrace is not null)
				GetLocalPlayer()
					.DisplayNotice(ex.StackTrace);
			#endif
		}
	}

	/// <summary>
	/// Base initialization of map
	/// </summary>
	private static void MapInitialization()
	{
		#if DEBUG
		FogEnable(false);
		FogMaskEnable(false);
		#endif

		// Player init part
		for (var i = 0; i < GameplayConstants.HumanPlayerCount - 1; i++)
		{
			player curPlayer = Player(i);
			if (curPlayer == null || string.IsNullOrEmpty(curPlayer.Name) || curPlayer.Name == "Mutants") continue;

			HumanPlayers.Add(curPlayer);

			// spawn worker at random point in central region facing south
			unit spawnedUnit = Regions.CenterReg.SpawnUnitAtRandomPoint(Constants.UNIT_H007_WORKER_PLAYER, curPlayer);
			spawnedUnit.SetOwner(curPlayer);
			spawnedUnit.Select();

			// select random from neutral hero houses
			unit randomHouse = group.Create()
				.EnumerateUnitsInRect(Rectangle.WorldBounds)
				.EmptyToList()
				.Where(u => u.UnitType == Constants.UNIT_H00C_HOUSE_PLAYER)
				.OrderBy(_ => new Random().Next())
				.First();

			randomHouse.SetOwner(curPlayer);
			curPlayer.SetState(PLAYER_STATE_RESOURCE_GOLD, GameplayConstants.StartingGold);
			curPlayer.SetState(PLAYER_STATE_RESOURCE_LUMBER, GameplayConstants.StartingLumber);
			curPlayer.SetHandicapXP(GameplayConstants.HeroExperienceRate);

			// limit to zero things I have not researched yet like better walls
			foreach (var entityId in GameplayConstants.LimitedToNone)
				curPlayer.SetTechMaxAllowed(entityId, 0);

			// each player is allowed only 1 hero and way gate
			foreach (var entityId in GameplayConstants.LimitedToOne)
				curPlayer.SetTechMaxAllowed(entityId, 1);

			// UnitShareVision(Constants.UNIT_N007_ZEPPELIN_NEUTRAL, curPlayer, true);
		}

		// Monsters init part
		// TODO: visibility for mutants team
		player? mutantsPlayer = Player(0);
		player? neutralAggressivePlayer = Player(PLAYER_NEUTRAL_AGGRESSIVE);
		mutantsPlayer.SetState(PLAYER_STATE_NO_CREEP_SLEEP, 1);
		mutantsPlayer.SetState(PLAYER_STATE_GIVES_BOUNTY, 1);
		mutantsPlayer.SetAlliance(neutralAggressivePlayer, ALLIANCE_PASSIVE, true);
		neutralAggressivePlayer.SetAlliance(mutantsPlayer, ALLIANCE_PASSIVE, true);

		// Add special effects onto spawn regions
		Regions.Spawn1Reg.AddEffectToCenter(SpecialEffects.DarkSummoning);
		Regions.Spawn2Reg.AddEffectToCenter(SpecialEffects.DarkSummoning);
		Regions.Spawn3Reg.AddEffectToCenter(SpecialEffects.DarkSummoning);
		Regions.Spawn4Reg.AddEffectToCenter(SpecialEffects.DarkSummoning);

		// Add special effects onto default gateway blink regions
		Regions.Blink1Reg.AddEffectToCenter(SpecialEffects.CrystalBall);
		Regions.Blink2Reg.AddEffectToCenter(SpecialEffects.CrystalBall);
	}

	/// <summary>
	/// Setups bugs trigger
	/// </summary>
	/// <returns></returns>
	private static trigger SetupBugs()
	{
		trigger? trigger = CreateTrigger();

		// Todo: some better mechanics the old sux ass
		PlayerUnitEvents.Register(UnitTypeEvent.Dies, () =>
		{
			unit deadUnit = GetTriggerUnit();
			if (deadUnit.UnitType != Constants.UNIT_N000_MUTANT_MUTANT)
				return;

			player? owner = deadUnit.Owner;
			var x = deadUnit.X;
			var y = deadUnit.Y;

			deadUnit.SafelyRemove();

			var chance = new Random().Next(100);
			if (chance >= 75) return;

			var beetle = unit.Create(owner, Constants.UNIT_U003_CARRION_BEETLE_MUTANT_BUG, x, y);
			beetle.IssueOrder(Constants.ORDER_ATTACK);
		});

		return trigger;
	}

	/// <summary>
	/// Difficulty trigger
	/// </summary>
	private static void SetupDifficulty(trigger bugsTrigger)
	{
		player hostPlayer = Player(1);
		const float diffTriggerTimeout = 60.00f;

		// Delay 45 seconds
		timer timer = CreateTimer();
		TimerStart(timer, 45.00f, false, () =>
		{
			var msg =
				$"{GetPlayerName(hostPlayer)} difficulty is {TextFormatter.Colorize("EASY", WCColor.Hint)} type" +
				$" \"{TextFormatter.Colorize("Normal", WCColor.Notice)}\" or \"{TextFormatter.Colorize("Hard", WCColor.Warning)}\"" +
				$" to the chat channel to change it, {diffTriggerTimeout} seconds left.";

			HumanPlayers.DisplayMessageToPlayers(msg);

			CreateTrigger()
				.RegisterTimerEvent(diffTriggerTimeout)
				.RegisterChatEvents(hostPlayer, new List<string> { "normal", "hard", "Normal", "Hard" }, true)
				.AddAction(() =>
				{
					trigger thisTrigger = GetTriggeringTrigger();
					player mutantsPlayer = Player(0);

					switch (GetEventPlayerChatString())
					{
						case "normal" or "Normal":
							mutantsPlayer.SetTechResearched(Constants.UPGRADE_R005_HARD_DIFFICULTY_SETTINGS, 1);
							HumanPlayers.DisplayMessageToPlayers("Difficulty is just normal.");
							thisTrigger.Disable();
							break;
						case "hard" or "Hard":
							mutantsPlayer.SetTechResearched(Constants.UPGRADE_R005_HARD_DIFFICULTY_SETTINGS, 2);
							HumanPlayers.DisplayMessageToPlayers("Difficulty might get hard.");
							thisTrigger.Disable();
							bugsTrigger.Enable();
							break;
					}

					thisTrigger.Dispose();
				});

			timer.Dispose();
		});
	}
}