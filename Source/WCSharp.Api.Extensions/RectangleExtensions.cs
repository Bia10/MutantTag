using WCSharp.Api;
using WCSharp.Shared.Data;
using static WCSharp.Api.Common;

namespace Source.WCSharp.Api.Extensions;

/// <summary>
/// Contains extension methods for manipulating <seealso cref="Rectangle"> rectangles </seealso> in Warcraft III maps.
/// </summary>
public static class RectangleExtensions
{
	/// <summary>
	/// Adds a sound to the center of the specified rectangle region.
	/// </summary>
	/// <param name="region">The rectangle region to add the sound to.</param>
	/// <param name="soundHandle">The handle of the sound to add.</param>
	/// <param name="z">The z-coordinate where the sound should be positioned. Default is 0.</param>
	public static void AddSound(this Rectangle region, sound soundHandle, float z = 0)
	{
		var width = GetRectMaxX(region.Rect) - GetRectMinX(region.Rect);
		var height = GetRectMaxY(region.Rect) - GetRectMinY(region.Rect);
		SetSoundPosition(soundHandle, GetRectCenterX(region.Rect), GetRectCenterY(region.Rect), z);
		RegisterStackedSound(soundHandle, true, width, height);
	}

	/// <summary>
	/// Adds a special effect at the center of the specified rectangle region.
	/// </summary>
	/// <param name="region">The rectangle region where the special effect will be added.</param>
	/// <param name="modelName">The model name of the special effect to be added.</param>
	/// <returns>The handle of the added special effect.</returns>
	public static effect AddEffectToCenter(this Rectangle region, string modelName)
	{
		var centerX = GetRectCenterX(region.Rect);
		var centerY = GetRectCenterY(region.Rect);

		return AddSpecialEffect(modelName, centerX, centerY);
	}

	/// <summary>
	/// Spawns a unit of the specified type for the given player at a random point within the rectangle area.
	/// </summary>
	/// <param name="area">The rectangle area to spawn the unit within.</param>
	/// <param name="unitType">The type of unit to spawn.</param>
	/// <param name="player">The player who owns the spawned unit.</param>
	/// <returns>The spawned unit.</returns>
	public static unit SpawnUnitAtRandomPoint(this Rectangle area, int unitType, player player)
	{
		Point randomPoint = area.GetRandomPoint();
		return unit.Create(player, unitType, randomPoint.X, randomPoint.Y);
	}
}