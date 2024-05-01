namespace Source;

/// <summary>
/// Contains constants related to gameplay of the map.
/// </summary>
internal static class GameplayConstants
{
	/// <summary>
	/// Maximum number of human players allowed in the map.
	/// </summary>
	internal const int HumanPlayerCount = 11;

	/// <summary>
	/// Amount of gold each player starts with.
	/// </summary>
	internal const int StartingGold = 1500;

	/// <summary>
	/// Amount of lumber each player starts with.
	/// </summary>
	internal const int StartingLumber = 15;

	/// <summary>
	/// Experience rate for heroes.
	/// </summary>
	internal const int HeroExperienceRate = 400;

	/// <summary>
	/// Units, upgrades, or abilities limited to none per player.
	/// </summary>
	internal static readonly int[] LimitedToNone =
	{
		Constants.UNIT_H009_NORMAL_WALL_HUMAN,
		Constants.UNIT_H003_STRONGER_WALL_HUMAN,
		Constants.UPGRADE_R002_MAGE_TRAINING_LEVEL_10_ARCHMAGE,
		Constants.UPGRADE_R003_WARRIOR_TRAINING_LEVEL_D_MOUTAIN_KING,
		Constants.UPGRADE_R006_WARDEN_TRAINING_LEVEL_D_WARDEN,
		Constants.UPGRADE_R007_RAIN_OF_ROCKS_MOUTAIN_KING,
		Constants.UPGRADE_R008_SHADOW_WALK_WARDEN,
		Constants.UPGRADE_R009_POWER_CHARM_ARCHMAGE,
		Constants.UPGRADE_R00B_MIND_TRAINING_HEROES
	};

	/// <summary>
	/// Units, upgrades, or abilities limited to one per player.
	/// </summary>
	internal static readonly int[] LimitedToOne =
	{
		Constants.UNIT_N003_WAY_GATE_HUMAN,
		Constants.UNIT_H00G_PALADIN_HERO
	};
}