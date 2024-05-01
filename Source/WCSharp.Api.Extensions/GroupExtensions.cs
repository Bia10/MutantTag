using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Shared.Data;
using static WCSharp.Api.Common;

namespace Source.WCSharp.Api.Extensions;

/// <summary>
/// Contains extension methods for manipulating <seealso cref="group"> groups </seealso> in Warcraft III maps.
/// </summary>
public static class GroupExtensions
{
	/// <summary>
	/// Empties the group and returns its units as a list.
	/// </summary>
	/// <param name="whichGroup">The group to empty.</param>
	/// <returns>An enumerable collection of the units in the group.</returns>
	public static IEnumerable<unit> EmptyToList(this group whichGroup)
	{
		var unitList = new List<unit>();
		unit firstOfGroup = FirstOfGroup(whichGroup);
		while (firstOfGroup != null)
		{
			unitList.Add(firstOfGroup);
			GroupRemoveUnit(whichGroup, firstOfGroup);
			firstOfGroup = FirstOfGroup(whichGroup);
		}

		return unitList;
	}

	/// <summary>
	/// Adds a unit to the group.
	/// </summary>
	/// <param name="whichGroup">The group to add the unit to.</param>
	/// <param name="unit">The unit to add to the group.</param>
	/// <returns>The group with the unit added.</returns>
	public static group AddUnit(this group whichGroup, unit unit)
	{
		GroupAddUnit(whichGroup, unit);
		return whichGroup;
	}

	/// <summary>
	/// Enumerates the selected units of a player and adds them to the group.
	/// </summary>
	/// <param name="whichGroup">The group to add the selected units to.</param>
	/// <param name="whichPlayer">The player whose selected units will be added to the group.</param>
	/// <returns>The group with the selected units added.</returns>
	public static group EnumSelectedUnits(this group whichGroup, player whichPlayer)
	{
		SyncSelections();
		GroupEnumUnitsSelected(whichGroup, whichPlayer, filter: null);
		return whichGroup;
	}

	/// <summary>
	/// Enumerates units of a specific type and adds them to the group.
	/// </summary>
	/// <param name="whichGroup">The group to add the units to.</param>
	/// <param name="unitType">The type of units to enumerate and add.</param>
	/// <returns>The group with the enumerated units of the specified type added.</returns>
	public static group EnumUnitsOfType(this group whichGroup, int unitType)
	{
		GroupEnumUnitsOfType(whichGroup, GetObjectName(unitType), filter: null);
		return whichGroup;
	}

	/// <summary>
	/// Enumerates units owned by a specific player and adds them to the group.
	/// </summary>
	/// <param name="whichGroup">The group to add the units to.</param>
	/// <param name="player">The player whose units will be enumerated and added.</param>
	/// <returns>The group with the enumerated units owned by the specified player added.</returns>
	public static group EnumerateUnitsOfPlayer(this group whichGroup, player player)
	{
		GroupEnumUnitsOfPlayer(whichGroup, player, filter: null);
		return whichGroup;
	}

	/// <summary>
	/// Enumerates units within a rectangular area and adds them to the group.
	/// </summary>
	/// <param name="whichGroup">The group to add the units to.</param>
	/// <param name="rect">The rectangular area to enumerate units within.</param>
	/// <returns>The group with the enumerated units within the specified rectangular area added.</returns>
	public static group EnumUnitsInRect(this group whichGroup, Rectangle rect)
		=> EnumUnitsInRect(whichGroup, rect.Rect);

	/// <summary>
	/// Enumerates units within a rectangular area and adds them to the group.
	/// </summary>
	/// <param name="whichGroup">The group to add the units to.</param>
	/// <param name="rect">The rectangular area to enumerate units within.</param>
	/// <returns>The group with the enumerated units within the specified rectangular area added.</returns>
	public static group EnumUnitsInRect(this group whichGroup, rect rect)
	{
		GroupEnumUnitsInRect(whichGroup, rect, filter: null);
		return whichGroup;
	}

	/// <summary>
	/// Enumerates units within a certain range from a point and adds them to the group.
	/// </summary>
	/// <param name="whichGroup">The group to add the units to.</param>
	/// <param name="point">The center point to enumerate units around.</param>
	/// <param name="radius">The radius of the circular area to enumerate units within.</param>
	/// <returns>The group with the enumerated units within the specified range added.</returns>
	public static group EnumUnitsInRange(this group whichGroup, Point point, float radius)
	{
		GroupEnumUnitsInRange(whichGroup, point.X, point.Y, radius, filter: null);
		return whichGroup;
	}

	/// <summary>
	/// Creates a copy of the group.
	/// </summary>
	/// <param name="whichGroup">The group to copy.</param>
	/// <returns>A new group containing the same units as the original group.</returns>
	public static group Copy(this group whichGroup)
	{
		group addGroup = CreateGroup();
		BlzGroupAddGroupFast(whichGroup, addGroup);
		return addGroup;
	}
}