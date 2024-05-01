using System.Collections.Generic;
using WCSharp.Api;
using static WCSharp.Api.Common;

namespace Source.WCSharp.Api.Extensions;

/// <summary>
/// Contains extension methods for manipulating <seealso cref="player"> players </seealso> in Warcraft III maps.
/// </summary>
public static class PlayerExtensions
{
	/// <summary>
	/// Creates multiple units for the player at the specified location and orientation.
	/// </summary>
	/// <param name="whichPlayer">The player who will own the created units.</param>
	/// <param name="unitId">The ID of the unit type to create.</param>
	/// <param name="x">The x-coordinate where the units will be created.</param>
	/// <param name="y">The y-coordinate where the units will be created.</param>
	/// <param name="face">The orientation of the units.</param>
	/// <param name="count">The number of units to create.</param>
	/// <returns>An enumerable collection of the created units.</returns>
	public static IEnumerable<unit> CreateUnits(this player whichPlayer, int unitId, float x, float y, float face,
		int count)
	{
		var createdUnits = new List<unit>();

		for (var i = 0; i < count; i++)
			createdUnits.Add(CreateUnit(whichPlayer, unitId, x, y, face));

		return createdUnits;
	}

	/// <summary>
	/// Sets the XP handicap for the specified player.
	/// </summary>
	/// <param name="whichPlayer">The player whose XP handicap is to be set.</param>
	/// <param name="value">The value to set as the XP handicap.</param>
	public static void SetHandicapXP(this player whichPlayer, int value)
		=> SetPlayerHandicapXP(whichPlayer, value);
}