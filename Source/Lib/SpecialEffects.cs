﻿using WCSharp.Api;

namespace Source.Lib;

/// <summary>
/// Contains paths to special effects used in the map or api.
/// </summary>
internal static class SpecialEffects
{
	/// <summary>
	/// Path to the special effect for dark summoning.
	/// </summary>
	internal const string DarkSummoning = @"Abilities\Spells\Undead\Darksummoning\DarkSummonTarget.mdl";

	/// <summary>
	/// Path to the special effect for crystal ball.
	/// </summary>
	internal const string CrystalBall = @"Abilities\Spells\Items\AIta\CrystalBallCaster.mdl";

	/// <summary>
	/// Path to the special effect for hero awakening.
	/// </summary>
	internal const string AwakenHero = @"Abilities\Spells\Other\Awaken\Awaken.mdl";

	/// <summary>
	/// Path to the special effect for resurrect target.
	/// </summary>
	internal const string ResurrectTarget = @"Abilities\Spells\Human\Resurrect\ResurrectTarget.mdl";

	/// <summary>
	/// Path to the special effect for strength gain for target.
	/// </summary>
	internal const string ItemStrengthGain = @"Abilities\Spells\Items\AIsm\AIsmTarget.mdl";

	/// <summary>
	/// Path to the special effect for agility gain for target.
	/// </summary>
	internal const string ItemAgilityGain = @"Abilities\Spells\Items\AIam\AIamTarget.mdl";

	/// <summary>
	/// Path to the special effect for intelligence gain for target.
	/// </summary>
	internal const string ItemIntelligenceGain = @"Abilities\Spells\Items\AIim\AIimTarget.mdl";

	/// <summary>
	/// Path to the special effect for strength/agility/inteligence gain for target.
	/// </summary>
	internal const string ItemStrAgiIntGain = @"Abilities\Spells\Items\AIlm\AIlmTarget.mdl";

	/// <summary>
	/// Creates and immediately disposes a special effect at the specified position.
	/// </summary>
	/// <param name="modelPath">The path to the model of the special effect.</param>
	/// <param name="posX">The X-coordinate where the special effect should be created.</param>
	/// <param name="posY">The Y-coordinate where the special effect should be created.</param>
	public static void FireAway(string modelPath, float posX, float posY)
		=> effect.Create(modelPath, posX, posY).Dispose();
}