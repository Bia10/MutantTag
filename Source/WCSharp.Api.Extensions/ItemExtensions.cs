using WCSharp.Api;
using WCSharp.Shared.Data;
using static WCSharp.Api.Common;

namespace Source.WCSharp.Api.Extensions;

/// <summary>
/// Contains extension methods for manipulating <seealso cref="item"> items </seealso> in Warcraft III maps.
/// </summary>
public static class ItemExtensions
{
	/// <summary>
	/// Sets whether the item can be dropped by a unit.
	/// </summary>
	/// <param name="whichItem">The item to set the droppability for.</param>
	/// <param name="canBeDropped">True if the item can be dropped; otherwise, false.</param>
	/// <returns>The item with the droppability set.</returns>
	public static item SetDroppable(this item whichItem, bool canBeDropped)
	{
		SetItemDroppable(whichItem, canBeDropped);
		return whichItem;
	}

	/// <summary>
	/// Checks if the item can be dropped by a unit.
	/// </summary>
	/// <param name="whichItem">The item to check.</param>
	/// <returns>True if the item can be dropped; otherwise, false.</returns>
	public static bool IsDroppable(this item whichItem)
		=> BlzGetItemBooleanField(whichItem, ITEM_BF_CAN_BE_DROPPED);

	/// <summary>
	/// Sets the position of the item to the specified point if it is safe for walking.
	/// </summary>
	/// <param name="whichItem">The item to set the position for.</param>
	/// <param name="position">The position to set.</param>
	public static void SetPositionSafe(this item whichItem, Point position)
	{
		if (IsTerrainPathable(position.X, position.Y, PATHING_TYPE_WALKABILITY))
			whichItem.SetPosition(position);
	}

	/// <summary>
	/// Sets the position of the item to the specified coordinates.
	/// </summary>
	/// <param name="whichItem">The item to set the position for.</param>
	/// <param name="x">The x-coordinate.</param>
	/// <param name="y">The y-coordinate.</param>
	/// <returns>The item with the new position set.</returns>
	public static item SetPosition(this item whichItem, float x, float y)
	{
		SetItemPosition(whichItem, x, y);
		return whichItem;
	}

	/// <summary>
	/// Sets the position of the item to the specified point.
	/// </summary>
	/// <param name="whichItem">The item to set the position for.</param>
	/// <param name="position">The position to set.</param>
	/// <returns>The item with the new position set.</returns>
	public static item SetPosition(this item whichItem, Point position)
	{
		SetItemPosition(whichItem, position.X, position.Y);
		return whichItem;
	}

	/// <summary>
	/// Retrieves the position of the item.
	/// </summary>
	/// <param name="whichItem">The item to retrieve the position for.</param>
	/// <returns>The position of the item as a Point.</returns>
	public static Point GetPosition(this item whichItem)
		=> new(GetItemX(whichItem), GetItemY(whichItem));
}