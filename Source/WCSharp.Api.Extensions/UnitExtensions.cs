using Source.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using WCSharp.Api;
using WCSharp.Shared.Data;
using static WCSharp.Api.Common;

// ReSharper disable PossibleUnintendedReferenceComparison

namespace Source.WCSharp.Api.Extensions;

/// <summary>
/// Contains extension methods for manipulating <seealso cref="unit"> units </seealso> in Warcraft III maps.
/// </summary>
public static class UnitExtensions
{
	/// <summary>
	/// Distance for dropping hero items.
	/// </summary>
	private const float HeroDropDistance = 50;

	/// <summary>
	/// Sets the level of the unit to the specified value.
	/// </summary>
	/// <param name="whichUnit">The unit to set the level for.</param>
	/// <param name="newLevel">The new level to set for the unit.</param>
	/// <param name="showEyeCandy">Optional. Whether to show eye candy for the level change. Default is true.</param>
	/// <returns>The unit with the updated level.</returns>
	public static unit SetLevel(this unit whichUnit, int newLevel, bool showEyeCandy = true)
	{
		var oldLevel = GetHeroLevel(whichUnit);
		if (newLevel > oldLevel)
			SetHeroLevel(whichUnit, newLevel, showEyeCandy);
		else if (newLevel < oldLevel)
			UnitStripHeroLevel(whichUnit, oldLevel - newLevel);

		return whichUnit;
	}

	/// <summary>
	/// Toggles the attack UI for the unit.
	/// </summary>
	/// <param name="whichUnit">The unit for which to toggle the attack UI.</param>
	/// <param name="show">Whether to show or hide the attack UI.</param>
	/// <param name="weaponSlot">Optional. The weapon slot to toggle the UI for. Default is 0.</param>
	/// <returns>The unit with the attack UI toggled.</returns>
	public static unit ShowAttackUi(this unit whichUnit, bool show, int weaponSlot = 0)
	{
		BlzSetUnitWeaponBooleanField(whichUnit, UNIT_WEAPON_BF_ATTACK_SHOW_UI, weaponSlot, show);
		return whichUnit;
	}

	/// <summary>
	/// Sets the level of the unit to the specified value.
	/// </summary>
	/// <param name="whichUnit">The unit to set the level for.</param>
	/// <param name="level">The new level to set for the unit.</param>
	/// <returns>The unit with the updated level.</returns>
	public static unit SetUnitLevel(this unit whichUnit, int level)
	{
		BlzSetUnitIntegerField(whichUnit, UNIT_IF_LEVEL, level);
		return whichUnit;
	}

	/// <summary>
	/// Sets the armor of the unit to the specified value.
	/// </summary>
	/// <param name="whichUnit">The unit to set the armor for.</param>
	/// <param name="armor">The armor value to set for the unit.</param>
	/// <returns>The unit with the updated armor.</returns>
	public static unit SetArmor(this unit whichUnit, int armor)
	{
		BlzSetUnitArmor(whichUnit, armor);
		return whichUnit;
	}

	/// <summary>
	/// Gets the maximum hit points of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the maximum hit points for.</param>
	/// <returns>The maximum hit points of the unit.</returns>
	public static int GetMaximumHitPoints(this unit whichUnit)
		=> BlzGetUnitMaxHP(whichUnit);

	/// <summary>
	/// Gets the current hit points of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the current hit points for.</param>
	/// <returns>The current hit points of the unit.</returns>
	public static float GetHitPoints(this unit whichUnit)
		=> GetUnitState(whichUnit, UNIT_STATE_LIFE);

	/// <summary>
	/// Sets the scale of the unit to the specified value.
	/// </summary>
	/// <param name="whichUnit">The unit to set the scale for.</param>
	/// <param name="scale">The scale value to set for the unit.</param>
	/// <returns>The unit with the updated scale.</returns>
	public static unit SetScale(this unit whichUnit, float scale)
	{
		SetUnitScale(whichUnit, scale, scale, scale);
		return whichUnit;
	}

	/// <summary>
	/// Sets the maximum hit points of the unit to the specified value.
	/// </summary>
	/// <param name="whichUnit">The unit to set the maximum hit points for.</param>
	/// <param name="value">The maximum hit points value to set for the unit.</param>
	/// <returns>The unit with the updated maximum hit points.</returns>
	public static unit SetMaximumHitPoints(this unit whichUnit, int value)
	{
		BlzSetUnitMaxHP(whichUnit, value);
		return whichUnit;
	}

	/// <summary>
	/// Sets the current hit points of the unit to the specified value.
	/// </summary>
	/// <param name="whichUnit">The unit to set the current hit points for.</param>
	/// <param name="value">The current hit points value to set for the unit.</param>
	/// <returns>The unit with the updated current hit points.</returns>
	public static unit SetCurrentHitPoints(this unit whichUnit, int value)
	{
		SetUnitState(whichUnit, UNIT_STATE_LIFE, value);
		return whichUnit;
	}

	/// <summary>
	/// Gets the current hit points of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the current hit points for.</param>
	/// <returns>The current hit points of the unit.</returns>
	public static int GetCurrentHitPoints(this unit whichUnit)
		=> (int)GetUnitState(whichUnit, UNIT_STATE_LIFE);

	/// <summary>
	/// Sets the base for the damages of the unit for the specified weapon slot.
	/// </summary>
	/// <param name="whichUnit">The unit to set the base damage for.</param>
	/// <param name="value">The base damage value to set for the unit.</param>
	/// <param name="weaponSlot">Optional. The weapon slot to set the base damage for. Default is 0.</param>
	/// <returns>The unit with the updated base damage.</returns>
	public static unit SetDamageBase(this unit whichUnit, int value, int weaponSlot = 0)
	{
		BlzSetUnitBaseDamage(whichUnit, value, weaponSlot);
		return whichUnit;
	}

	/// <summary>
	/// Sets the number of dice for the damages of the unit for the specified weapon slot.
	/// </summary>
	/// <param name="whichUnit">The unit to set the dice number for.</param>
	/// <param name="value">The number of dice value to set for the unit.</param>
	/// <param name="weaponSlot">Optional. The weapon slot to set the dice number for. Default is 0.</param>
	/// <returns>The unit with the updated dice number.</returns>
	public static unit SetDamageDiceNumber(this unit whichUnit, int value, int weaponSlot = 0)
	{
		BlzSetUnitDiceNumber(whichUnit, value, weaponSlot);
		return whichUnit;
	}

	/// <summary>
	/// Sets the number of dice sides for the damages of the unit for the specified weapon slot.
	/// </summary>
	/// <param name="whichUnit">The unit to set the dice sides for.</param>
	/// <param name="value">The number of dice sides value to set for the unit.</param>
	/// <param name="weaponSlot">Optional. The weapon slot to set the dice sides for. Default is 0.</param>
	/// <returns>The unit with the updated dice sides.</returns>
	public static unit SetDamageDiceSides(this unit whichUnit, int value, int weaponSlot = 0)
	{
		BlzSetUnitDiceSides(whichUnit, value, weaponSlot);
		return whichUnit;
	}

	/// <summary>
	/// Sets the skin of the unit to the specified skin unit type ID.
	/// </summary>
	/// <param name="whichUnit">The unit to set the skin for.</param>
	/// <param name="skinUnitTypeId">The unit type ID of the skin to set for the unit.</param>
	/// <returns>The unit with the updated skin.</returns>
	public static unit SetSkin(this unit whichUnit, int skinUnitTypeId)
	{
		BlzSetUnitSkin(whichUnit, skinUnitTypeId);
		return whichUnit;
	}

	/// <summary>
	/// Sets the name of the unit to the specified name.
	/// </summary>
	/// <param name="whichUnit">The unit to set the name for.</param>
	/// <param name="name">The name to set for the unit.</param>
	/// <returns>The unit with the updated name.</returns>
	public static unit SetName(this unit whichUnit, string name)
	{
		BlzSetUnitName(whichUnit, name);
		return whichUnit;
	}

	/// <summary>
	/// Checks if the unit is an illusion.
	/// </summary>
	/// <param name="whichUnit">The unit to check.</param>
	/// <returns>True if the unit is an illusion, false otherwise.</returns>
	public static bool IsIllusion(this unit whichUnit)
		=> IsUnitIllusion(whichUnit);

	/// <summary>
	/// Checks if the unit is of the specified type.
	/// </summary>
	/// <param name="whichUnit">The unit to check.</param>
	/// <param name="unitType">The type to check against.</param>
	/// <returns>True if the unit is of the specified type, false otherwise.</returns>
	public static bool IsType(this unit whichUnit, unittype unitType)
		=> IsUnitType(whichUnit, unitType);

	/// <summary>
	/// Sets the facing angle of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the facing angle for.</param>
	/// <param name="facing">The facing angle value to set for the unit.</param>
	/// <returns>The unit with the updated facing angle.</returns>
	public static unit SetFacingEx(this unit whichUnit, float facing)
	{
		BlzSetUnitFacingEx(whichUnit, facing);
		return whichUnit;
	}

	/// <summary>
	/// Sets whether the unit should explode on death.
	/// </summary>
	/// <param name="whichUnit">The unit to set the explosion on death flag for.</param>
	/// <param name="flag">Whether the unit should explode on death.</param>
	/// <returns>The unit with the updated explode on death flag.</returns>
	public static unit SetExplodeOnDeath(this unit whichUnit, bool flag)
	{
		SetUnitExploded(whichUnit, flag);
		return whichUnit;
	}

	/// <summary>
	/// Gets the level of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the level for.</param>
	/// <returns>The level of the unit.</returns>
	public static int GetLevel(this unit whichUnit)
		=> IsUnitType(whichUnit, UNIT_TYPE_HERO) ? GetHeroLevel(whichUnit) : GetUnitLevel(whichUnit);

	/// <summary>
	/// Sets the color of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the color for.</param>
	/// <param name="red">The red value of the color.</param>
	/// <param name="green">The green value of the color.</param>
	/// <param name="blue">The blue value of the color.</param>
	/// <param name="alpha">The alpha value of the color.</param>
	/// <returns>The unit with the updated color.</returns>
	public static unit SetColor(this unit whichUnit, int red, int green, int blue, int alpha)
	{
		SetUnitVertexColor(whichUnit, red, green, blue, alpha);
		return whichUnit;
	}

	/// <summary>
	/// Sets the timed life duration for the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the timed life for.</param>
	/// <param name="duration">The duration of the timed life.</param>
	/// <param name="buffId">Optional. The ID of the buff to apply as a timed life. Default is 0.</param>
	/// <returns>The unit with the updated timed life.</returns>
	public static unit SetTimedLife(this unit whichUnit, float duration, int buffId = 0)
	{
		if (duration < 1)
			BlzUnitCancelTimedLife(whichUnit);

		UnitApplyTimedLife(whichUnit, buffId, duration);
		return whichUnit;
	}

	/// <summary>
	/// Gets the type ID of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the type ID for.</param>
	/// <returns>The type ID of the unit.</returns>
	public static int GetTypeId(this unit whichUnit)
		=> GetUnitTypeId(whichUnit);

	/// <summary>
	/// Sets the animation speed of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the animation speed for.</param>
	/// <param name="speed">The animation speed value to set for the unit.</param>
	/// <returns>The unit with the updated animation speed.</returns>
	public static unit SetAnimationSpeed(this unit whichUnit, float speed)
	{
		SetUnitTimeScale(whichUnit, speed);
		return whichUnit;
	}

	/// <summary>
	/// Sets the animation of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the animation for.</param>
	/// <param name="animation">The animation to set for the unit.</param>
	/// <returns>The unit with the updated animation.</returns>
	public static unit SetAnimation(this unit whichUnit, string animation)
	{
		SetUnitAnimation(whichUnit, animation);
		return whichUnit;
	}

	/// <summary>
	/// Gets the proper name of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the proper name for.</param>
	/// <returns>The proper name of the unit.</returns>
	public static string GetProperName(this unit whichUnit)
		=> IsUnitType(whichUnit, UNIT_TYPE_HERO) ? GetHeroProperName(whichUnit) : GetUnitName(whichUnit);

	/// <summary>
	/// Gets the name of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the name for.</param>
	/// <returns>The name of the unit.</returns>
	public static string GetName(this unit whichUnit)
		=> GetUnitName(whichUnit);

	/// <summary>
	/// Drops the specified item from the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to drop the item from.</param>
	/// <param name="whichItem">The item to drop from the unit.</param>
	/// <returns>The unit with the item dropped.</returns>
	public static unit DropItem(this unit whichUnit, item whichItem)
	{
		UnitRemoveItem(whichUnit, whichItem);
		return whichUnit;
	}

	/// <summary>
	/// Shows or hides the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to show or hide.</param>
	/// <param name="show">True to show the unit, false to hide it.</param>
	/// <returns>The unit with its visibility updated.</returns>
	public static unit Show(this unit whichUnit, bool show)
	{
		ShowUnit(whichUnit, show);
		return whichUnit;
	}

	/// <summary>
	/// Kills the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to kill.</param>
	/// <returns>The unit that was killed.</returns>
	public static unit Kill(this unit whichUnit)
	{
		KillUnit(whichUnit);
		return whichUnit;
	}

	/// <summary>
	/// Pings the minimap at the location of the unit for the specified duration.
	/// </summary>
	/// <param name="whichUnit">The unit to ping.</param>
	/// <param name="duration">The duration of the ping.</param>
	public static void Ping(this unit whichUnit, float duration)
		=> PingMinimap(GetUnitX(whichUnit), GetUnitY(whichUnit), duration);

	/// <summary>
	/// Pauses or resumes the unit's animation.
	/// </summary>
	/// <param name="unit">The unit to pause or resume.</param>
	/// <param name="value">True to pause the animation, false to resume it.</param>
	/// <returns>The unit with its animation paused or resumed.</returns>
	public static unit PauseEx(this unit unit, bool value)
	{
		BlzPauseUnitEx(unit, value);
		return unit;
	}

	/// <summary>
	/// Sets the invulnerability state of the unit.
	/// </summary>
	/// <param name="unit">The unit to set the invulnerability state for.</param>
	/// <param name="value">True to make the unit invulnerable, false to make it vulnerable.</param>
	/// <returns>The unit with its invulnerability state updated.</returns>
	public static unit SetInvulnerable(this unit unit, bool value)
	{
		SetUnitInvulnerable(unit, value);
		return unit;
	}

	/// <summary>
	/// Removes the unit from the game.
	/// </summary>
	/// <param name="unit">The unit to remove.</param>
	public static void Remove(this unit unit)
		=> RemoveUnit(unit);

	/// <summary>
	/// Issues an order to the unit to move to the specified location.
	/// </summary>
	/// <param name="unit">The unit to issue the order to.</param>
	/// <param name="where">The location to move to.</param>
	/// <param name="considerPathability">Whether to consider pathability when moving.</param>
	/// <returns>The unit with the movement order issued.</returns>
	public static unit SetPosition(this unit unit, Point where, bool considerPathability = false)
	{
		if (!considerPathability)
		{
			SetUnitX(unit, where.X);
			SetUnitY(unit, where.Y);
		}
		else
			SetUnitPosition(unit, where.X, where.Y);

		return unit;
	}

	/// <summary>
	/// Gets the position of the unit.
	/// </summary>
	/// <param name="unit">The unit to get the position for.</param>
	/// <returns>The position of the unit.</returns>
	public static Point GetPosition(this unit unit)
		=> new(GetUnitX(unit), GetUnitY(unit));

	/// <summary>
	/// Sets the owner of the unit.
	/// </summary>
	/// <param name="unit">The unit to set the owner for.</param>
	/// <param name="whichPlayer">The player to set as the owner.</param>
	/// <param name="changeColor">Whether to change the color of the unit to match the new owner's color.</param>
	/// <returns>The unit with the updated owner.</returns>
	public static unit SetOwner(this unit unit, player whichPlayer, bool changeColor = true)
	{
		SetUnitOwner(unit, whichPlayer, changeColor);
		return unit;
	}

	/// <summary>
	/// Gets the owning player of the unit.
	/// </summary>
	/// <param name="unit">The unit to get the owning player for.</param>
	/// <returns>The owning player of the unit.</returns>
	public static player OwningPlayer(this unit unit)
		=> GetOwningPlayer(unit);

	/// <summary>
	/// Sets whether the waygate is active or not.
	/// </summary>
	/// <param name="waygate">The waygate unit to set the activation for.</param>
	/// <param name="flag">True to activate the waygate, false to deactivate it.</param>
	/// <returns>The waygate with the activation state updated.</returns>
	public static unit SetWaygateActive(this unit waygate, bool flag)
	{
		WaygateActivate(waygate, flag);
		return waygate;
	}

	/// <summary>
	/// Sets the destination for the waygate.
	/// </summary>
	/// <param name="waygate">The waygate unit to set the destination for.</param>
	/// <param name="destination">The destination point for the waygate.</param>
	/// <returns>The waygate with the destination set.</returns>
	public static unit SetWaygateDestination(this unit waygate, Point destination)
	{
		WaygateActivate(waygate, true);
		WaygateSetDestination(waygate, destination.X, destination.Y);
		return waygate;
	}

	/// <summary>
	/// Sets the life of the unit as a percentage of its maximum life.
	/// </summary>
	/// <param name="whichUnit">The unit to set the life percentage for.</param>
	/// <param name="percent">The percentage of maximum life to set.</param>
	/// <returns>The unit with its life percentage updated.</returns>
	public static unit SetLifePercent(this unit whichUnit, float percent)
	{
		SetUnitState(whichUnit, UNIT_STATE_LIFE,
			GetUnitState(whichUnit, UNIT_STATE_MAX_LIFE) * MathExtensions.MaxFloat(0, percent) * 0.01f);
		return whichUnit;
	}

	/// <summary>
	/// Gets the life of the unit as a percentage of its maximum life.
	/// </summary>
	/// <param name="whichUnit">The unit to get the life percentage for.</param>
	/// <returns>The life of the unit as a percentage.</returns>
	public static float GetLifePercent(this unit whichUnit)
		=> GetUnitState(whichUnit, UNIT_STATE_LIFE) / GetUnitState(whichUnit, UNIT_STATE_MAX_LIFE) * 100;

	/// <summary>
	/// Resurrects the unit if it's dead.
	/// </summary>
	/// <param name="whichUnit">The unit to resurrect.</param>
	/// <exception cref="ArgumentException">Thrown when trying to resurrect a unit that is already alive.</exception>
	public static void Resurrect(this unit whichUnit)
	{
		if (UnitAlive(whichUnit))
			throw new ArgumentException("Tried to resurrect a unit that is already alive.");

		var x = GetUnitX(whichUnit);
		var y = GetUnitY(whichUnit);
		var unitType = GetUnitTypeId(whichUnit);
		var face = GetUnitFacing(whichUnit);
		DestroyEffect(AddSpecialEffect(SpecialEffects.ResurrectTarget, x, y));
		RemoveUnit(whichUnit);
		CreateUnit(OwningPlayer(whichUnit), unitType, x, y, face);
	}

	// Todo: public static void ReverseResurrect(this unit whichUnit, Func<int, int> mappingFunc)

	/// <summary>
	/// Applies damage to the target unit.
	/// </summary>
	/// <param name="dmgTarget">The unit receiving the damage.</param>
	/// <param name="dmgDealer">The unit dealing the damage.</param>
	/// <param name="amount">The amount of damage.</param>
	/// <param name="attack">Specifies whether the damage is from an attack. Default is false.</param>
	/// <param name="ranged">Specifies whether the damage is ranged. Default is true.</param>
	/// <param name="attackType">The attack type. Default is ATTACK_TYPE_NORMAL.</param>
	/// <param name="damageType">The damage type. Default is DAMAGE_TYPE_MAGIC.</param>
	/// <param name="weaponType">The weapon type. Default is WEAPON_TYPE_WHOKNOWS.</param>
	public static void TakeDamage(this unit dmgTarget, unit dmgDealer, float amount, bool attack = false,
		bool ranged = true, attacktype? attackType = null, damagetype? damageType = null, weapontype? weaponType = null)
	{
		UnitDamageTarget(dmgDealer, dmgTarget, amount, attack, ranged, attackType ?? ATTACK_TYPE_NORMAL,
			damageType ?? DAMAGE_TYPE_MAGIC, weaponType ?? WEAPON_TYPE_WHOKNOWS);
	}

	/// <summary>
	/// Restores mana to the unit.
	/// </summary>
	/// <param name="whichUnit">The unit whose mana will be restored.</param>
	/// <param name="amount">The amount of mana to restore.</param>
	public static void RestoreMana(this unit whichUnit, float amount)
		=> SetUnitState(whichUnit, UNIT_STATE_MANA, whichUnit.GetMana() + amount);

	/// <summary>
	/// Retrieves the current mana of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the mana from.</param>
	/// <returns>The amount of mana.</returns>
	public static float GetMana(this unit whichUnit)
		=> GetUnitState(whichUnit, UNIT_STATE_MANA);

	/// <summary>
	/// Heals the unit by the specified amount.
	/// </summary>
	/// <param name="unit">The unit to be healed.</param>
	/// <param name="amount">The amount of healing.</param>
	public static void Heal(this unit unit, float amount)
		=> SetUnitState(unit, UNIT_STATE_LIFE, GetUnitState(unit, UNIT_STATE_LIFE) + amount);

	/// <summary>
	/// Retrieves the average damage for the unit's weapon.
	/// </summary>
	/// <param name="whichUnit">The unit to get the average damage for.</param>
	/// <param name="weaponIndex">The index of the weapon.</param>
	/// <returns>The average damage.</returns>
	public static int GetAverageDamage(this unit whichUnit, int weaponIndex)
	{
		float baseDamage = BlzGetUnitBaseDamage(whichUnit, weaponIndex);
		float numberOfDice = BlzGetUnitDiceNumber(whichUnit, weaponIndex);
		float sidesPerDie = BlzGetUnitDiceSides(whichUnit, weaponIndex);
		return R2I(baseDamage + (numberOfDice + sidesPerDie * numberOfDice) / 2);
	}

	/// <summary>
	/// Retrieves the damage reduction of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the damage reduction for.</param>
	/// <returns>The damage reduction value.</returns>
	public static float GetDamageReduction(this unit whichUnit)
	{
		var armor = BlzGetUnitArmor(whichUnit);
		return armor * 006 / ((1 + 006) * armor);
	}

	/// <summary>
	/// Increases the hero attributes (strength, agility, intelligence) of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to increase the attributes for.</param>
	/// <param name="strength">The amount to increase strength by.</param>
	/// <param name="agility">The amount to increase agility by.</param>
	/// <param name="intelligence">The amount to increase intelligence by.</param>
	/// <returns>The modified unit.</returns>
	public static unit AddHeroAttributes(this unit whichUnit, int strength, int agility, int intelligence)
	{
		SetHeroStr(whichUnit, GetHeroStr(whichUnit, false) + strength, true);
		SetHeroAgi(whichUnit, GetHeroAgi(whichUnit, false) + agility, true);
		SetHeroInt(whichUnit, GetHeroInt(whichUnit, false) + intelligence, true);

		string? specialEffect;
		switch (strength)
		{
			case > 0 when agility == 0 && intelligence == 0:
				specialEffect = SpecialEffects.ItemStrengthGain;
				break;
			case 0 when agility > 0 && intelligence == 0:
				specialEffect = SpecialEffects.ItemAgilityGain;
				break;
			case 0 when agility == 0 && intelligence > 0:
				specialEffect = SpecialEffects.ItemIntelligenceGain;
				break;
			default:
				specialEffect = SpecialEffects.ItemStrAgiIntGain;
				break;
		}

		DestroyEffect(AddSpecialEffect(specialEffect, GetUnitX(whichUnit), GetUnitY(whichUnit)));
		return whichUnit;
	}

	/// <summary>
	/// Adds experience points to the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to add experience points to.</param>
	/// <param name="amount">The amount of experience points to add.</param>
	/// <returns>The modified unit.</returns>
	public static unit AddExperience(this unit whichUnit, int amount)
	{
		AddHeroXP(whichUnit, amount, true);
		return whichUnit;
	}

	/// <summary>
	/// Drops all items carried by the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to drop items from.</param>
	/// <returns>The modified unit.</returns>
	/// <exception cref="InvalidOperationException">Thrown if called on a summoned unit.</exception>
	public static unit DropAllItems(this unit whichUnit)
	{
		if (IsUnitType(whichUnit, UNIT_TYPE_SUMMONED))
			throw new InvalidOperationException($"Tried to call {nameof(DropAllItems)} on a summoned unit.");

		var unitX = GetUnitX(whichUnit);
		var unitY = GetUnitY(whichUnit);
		float angleInRadians = 0;

		for (var i = 0; i < 6; i++)
		{
			var x = unitX + HeroDropDistance * Cos(angleInRadians);
			var y = unitY + HeroDropDistance * Sin(angleInRadians);
			angleInRadians += 360 * MathExtensions.DegreesToRadians / 6;
			item itemToDrop = UnitItemInSlot(whichUnit, i);

			if (!itemToDrop.IsDroppable())
				itemToDrop.SetDroppable(true);

			whichUnit.DropItem(itemToDrop);
			itemToDrop.SetPositionSafe(new Point(x, y));
		}

		return whichUnit;
	}

	/// <summary>
	/// Transfers all items from one unit to another.
	/// </summary>
	/// <param name="sender">The unit sending the items.</param>
	/// <param name="receiver">The unit receiving the items.</param>
	public static void TransferItems(this unit sender, unit receiver)
	{
		for (var i = 0; i < 6; i++) UnitAddItem(receiver, UnitItemInSlot(sender, i));
	}

	/// <summary>
	/// Adds an item to the unit, placing it safely at the unit's position.
	/// </summary>
	/// <param name="whichUnit">The unit to add the item to.</param>
	/// <param name="whichItem">The item to add.</param>
	/// <returns>The modified unit.</returns>
	public static unit AddItemSafe(this unit whichUnit, item whichItem)
	{
		SetItemPosition(whichItem, GetUnitX(whichUnit), GetUnitY(whichUnit));
		UnitAddItem(whichUnit, whichItem);
		return whichUnit;
	}

	/// <summary>
	/// Multiplies the base damage for the specified weapon of the unit by the given multiplier.
	/// </summary>
	/// <param name="whichUnit">The unit whose base damage will be multiplied.</param>
	/// <param name="multiplier">The multiplier to apply.</param>
	/// <param name="weaponIndex">The index of the weapon.</param>
	/// <returns>The modified unit.</returns>
	public static unit MultiplyBaseDamage(this unit whichUnit, float multiplier, int weaponIndex)
	{
		BlzSetUnitBaseDamage(whichUnit, R2I(I2R(BlzGetUnitBaseDamage(whichUnit, weaponIndex)) * multiplier),
			weaponIndex);
		return whichUnit;
	}

	/// <summary>
	/// Multiplies the maximum hit points of the unit by the given multiplier.
	/// </summary>
	/// <param name="whichUnit">The unit whose maximum hit points will be multiplied.</param>
	/// <param name="multiplier">The multiplier to apply.</param>
	/// <returns>The modified unit.</returns>
	public static unit MultiplyMaxHitPoints(this unit whichUnit, float multiplier)
	{
		var percentageHitPoints = whichUnit.GetLifePercent();
		BlzSetUnitMaxHP(whichUnit, R2I(I2R(BlzGetUnitMaxHP(whichUnit)) * multiplier));
		whichUnit.SetLifePercent(percentageHitPoints);
		return whichUnit;
	}

	/// <summary>
	/// Multiplies the maximum mana of the unit by the given multiplier.
	/// </summary>
	/// <param name="whichUnit">The unit whose maximum mana will be multiplied.</param>
	/// <param name="multiplier">The multiplier to apply.</param>
	/// <returns>The modified unit.</returns>
	public static unit MultiplyMaxMana(this unit whichUnit, float multiplier)
	{
		var percentageMana = whichUnit.GetManaPercent();
		BlzSetUnitMaxMana(whichUnit, R2I(I2R(BlzGetUnitMaxMana(whichUnit)) * multiplier));
		whichUnit.SetManaPercent(percentageMana);
		return whichUnit;
	}

	/// <summary>
	/// Retrieves the percentage of mana remaining for the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the mana percentage for.</param>
	/// <returns>The percentage of mana.</returns>
	public static float GetManaPercent(this unit whichUnit)
		=> GetUnitState(whichUnit, UNIT_STATE_MANA) / GetUnitState(whichUnit, UNIT_STATE_MAX_MANA) * 100;

	/// <summary>
	/// Sets the percentage of mana for the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the mana percentage for.</param>
	/// <param name="percent">The percentage of mana to set.</param>
	/// <returns>The modified unit.</returns>
	public static unit SetManaPercent(this unit whichUnit, float percent)
	{
		SetUnitState(whichUnit, UNIT_STATE_MANA,
			GetUnitState(whichUnit, UNIT_STATE_MAX_MANA) * MathExtensions.MaxFloat(0, percent) * 0.01f);
		return whichUnit;
	}

	/// <summary>
	/// Sets the maximum mana of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the maximum mana for.</param>
	/// <param name="maximumMana">The maximum mana value.</param>
	/// <returns>The modified unit.</returns>
	public static unit SetMaximumMana(this unit whichUnit, int maximumMana)
	{
		BlzSetUnitMaxMana(whichUnit, maximumMana);
		return whichUnit;
	}

	/// <summary>
	/// Sets the current mana of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the mana for.</param>
	/// <param name="value">The mana value.</param>
	/// <returns>The modified unit.</returns>
	public static unit SetMana(this unit whichUnit, int value)
	{
		SetUnitState(whichUnit, UNIT_STATE_MANA, value);
		return whichUnit;
	}

	/// <summary>
	/// Retrieves the facing angle of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the facing angle for.</param>
	/// <returns>The facing angle.</returns>
	public static float GetFacing(this unit whichUnit)
		=> GetUnitFacing(whichUnit);

	/// <summary>
	/// Retrieves the rally point of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the rally point for.</param>
	/// <returns>The rally point.</returns>
	public static Point GetRallyPoint(this unit whichUnit)
	{
		location rallyLocation = GetUnitRallyPoint(whichUnit);
		var rallyPoint = new Point(GetLocationX(rallyLocation), GetLocationY(rallyLocation));
		return rallyPoint;
	}

	/// <summary>
	/// Adds an ability to the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to add the ability to.</param>
	/// <param name="abilityTypeId">The ID of the ability to add.</param>
	/// <returns>The modified unit.</returns>
	public static unit AddAbility(this unit whichUnit, int abilityTypeId)
	{
		UnitAddAbility(whichUnit, abilityTypeId);
		UnitMakeAbilityPermanent(whichUnit, true, abilityTypeId);
		return whichUnit;
	}

	/// <summary>
	/// Sets the level of an ability for the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the ability level for.</param>
	/// <param name="abilityTypeId">The ID of the ability.</param>
	/// <param name="level">The level of the ability.</param>
	/// <returns>The modified unit.</returns>
	public static unit SetAbilityLevel(this unit whichUnit, int abilityTypeId, int level)
	{
		SetUnitAbilityLevel(whichUnit, abilityTypeId, level);
		return whichUnit;
	}

	/// <summary>
	/// Removes an ability from the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to remove the ability from.</param>
	/// <param name="abilityTypeId">The ID of the ability to remove.</param>
	/// <returns>The modified unit.</returns>
	public static unit RemoveAbility(this unit whichUnit, int abilityTypeId)
	{
		UnitRemoveAbility(whichUnit, abilityTypeId);
		return whichUnit;
	}

	/// <summary>
	/// Checks if the unit is alive.
	/// </summary>
	/// <param name="whichUnit">The unit to check.</param>
	/// <returns>True if the unit is alive, false otherwise.</returns>
	public static bool IsAlive(this unit whichUnit)
		=> UnitAlive(whichUnit);

	/// <summary>
	/// Sets the attack type of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the attack type for.</param>
	/// <param name="attackType">The type of attack.</param>
	/// <returns>The modified unit.</returns>
	public static unit SetAttackType(this unit whichUnit, int attackType)
	{
		BlzSetUnitWeaponIntegerField(whichUnit, UNIT_WEAPON_IF_ATTACK_ATTACK_TYPE, 0, attackType);
		return whichUnit;
	}

	/// <summary>
	/// Retrieves the attack type of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get the attack type for.</param>
	/// <returns>The attack type.</returns>
	public static int GetAttackType(this unit whichUnit)
		=> BlzGetUnitWeaponIntegerField(whichUnit, UNIT_WEAPON_IF_ATTACK_ATTACK_TYPE, 0);

	/// <summary>
	/// Sets the armor type of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to set the armor type for.</param>
	/// <param name="armorType">The type of armor.</param>
	/// <returns>The modified unit.</returns>
	public static unit SetArmorType(this unit whichUnit, int armorType)
	{
		BlzSetUnitIntegerField(whichUnit, UNIT_IF_DEFENSE_TYPE, armorType);
		return whichUnit;
	}

	/// <summary>
	/// Adds a specified type to the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to add the type to.</param>
	/// <param name="whichUnitType">The type to add.</param>
	/// <returns>The modified unit.</returns>
	public static unit AddType(this unit whichUnit, unittype whichUnitType)
	{
		UnitAddType(whichUnit, whichUnitType);
		return whichUnit;
	}

	/// <summary>
	/// Starts the full cooldown of the specified ability for the unit.
	/// </summary>
	/// <param name="whichUnit">The unit whose ability cooldown will be started.</param>
	/// <param name="abilityCode">The code of the ability.</param>
	/// <returns>The modified unit.</returns>
	public static unit StartUnitAbilityCooldownFull(this unit whichUnit, int abilityCode)
	{
		var fullCooldown = BlzGetUnitAbilityCooldown(whichUnit, abilityCode, 0);
		BlzStartUnitAbilityCooldown(whichUnit, abilityCode, fullCooldown);
		return whichUnit;
	}

	/// <summary>
	/// Makes the unit face a specific position.
	/// </summary>
	/// <param name="whichUnit">The unit to rotate.</param>
	/// <param name="targetPoint">The position to face.</param>
	/// <returns>The modified unit.</returns>
	public static unit FacePosition(this unit whichUnit, Point targetPoint)
	{
		Point unitPosition = whichUnit.GetPosition();
		var facing =
			global::WCSharp.Shared.Util.AngleBetweenPoints(unitPosition.X, unitPosition.Y, targetPoint.X,
				targetPoint.Y);
		BlzSetUnitFacingEx(whichUnit, facing);
		return whichUnit;
	}

	/// <summary>
	/// Checks if the unit is resistant.
	/// </summary>
	/// <param name="whichUnit">The unit to check.</param>
	/// <returns>True if the unit is resistant, false otherwise.</returns>
	public static bool IsResistant(this unit whichUnit)
		=> whichUnit.IsType(UNIT_TYPE_RESISTANT) || whichUnit.IsType(UNIT_TYPE_HERO) ||
		   (whichUnit.OwningPlayer() == Player(PLAYER_NEUTRAL_AGGRESSIVE) && whichUnit.GetLevel() >= 6);

	/// <summary>
	/// Removes all abilities from the unit except for those specified.
	/// </summary>
	/// <param name="whichUnit">The unit to remove abilities from.</param>
	/// <param name="ignoredAbilityId">The list of ability IDs to ignore.</param>
	/// <returns>The modified unit.</returns>
	public static unit RemoveAllAbilities(this unit whichUnit, List<int> ignoredAbilityId)
	{
		List<ability> abilities = GetUnitAbilities(whichUnit);

		foreach (ability ability in abilities)
		{
			var abilityId = BlzGetAbilityId(ability);
			if (!ignoredAbilityId.Contains(abilityId))
				RemoveAbility(whichUnit, abilityId);
		}

		return whichUnit;
	}

	/// <summary>
	/// Retrieves all abilities of the unit.
	/// </summary>
	/// <param name="whichUnit">The unit to get abilities from.</param>
	/// <returns>The list of abilities.</returns>
	public static List<ability> GetUnitAbilities(this unit whichUnit)
	{
		var abilities = new List<ability>();
		var index = 0;

		while (true)
		{
			ability ability = BlzGetUnitAbilityByIndex(whichUnit, index);
			if (ability is null) break;

			abilities.Add(ability);
			index++;
		}

		return abilities;
	}

	/// <summary>
	/// Removes the unit safely from the game.
	/// </summary>
	/// <param name="whichUnit">The unit to remove.</param>
	public static void SafelyRemove(this unit whichUnit)
	{
		if (whichUnit.IsType(UNIT_TYPE_HERO))
			whichUnit.DropAllItems();

		whichUnit.Kill();
		whichUnit.Remove();
	}

	/// <summary>
	/// Replaces all units of a specific type belonging to the same player with another type.
	/// </summary>
	/// <param name="caster">The unit initiating the replacement.</param>
	/// <param name="originalUnitType">The type of unit to replace.</param>
	/// <param name="replacingUnitType">The type of unit to replace with.</param>
	public static void ReplaceUnits(this unit caster, int originalUnitType, int replacingUnitType)
	{
		group.Create()
			.EnumerateUnitsOfPlayer(caster.Owner)
			.EmptyToList()
			.Where(unit => unit.UnitType == originalUnitType)
			.ToList()
			.ForEach(unit =>
			{
				ReplaceUnit(unit, replacingUnitType);
			});
	}

	/// <summary>
	/// Replaces a unit with another unit type.
	/// </summary>
	/// <param name="replacedUnit">The unit to be replaced.</param>
	/// <param name="replacingUnitType">The type of unit to replace with.</param>
	/// <param name="selectReplacingUnit">Whether to select the replacing unit.</param>
	public static void ReplaceUnit(this unit replacedUnit, int replacingUnitType, bool selectReplacingUnit = false)
	{
		Point location = replacedUnit.GetPosition();
		replacedUnit.Remove();
		var replacingUnit = unit.Create(replacedUnit.Owner, replacingUnitType, location.X, location.Y);
		if (selectReplacingUnit) replacingUnit.Select();
	}

	/// <summary>
	/// Replaces a unit with another unit type and returns the new unit.
	/// </summary>
	/// <param name="replacedUnit">The unit to be replaced.</param>
	/// <param name="replacingUnitType">The type of unit to replace with.</param>
	/// <param name="selectReplacingUnit">Whether to select the replacing unit.</param>
	/// <returns>The replacing unit.</returns>
	public static unit ReplaceAndReturnUnit(this unit replacedUnit, int replacingUnitType,
		bool selectReplacingUnit = false)
	{
		Point location = replacedUnit.GetPosition();
		replacedUnit.Remove();
		var replacingUnit = unit.Create(replacedUnit.Owner, replacingUnitType, location.X, location.Y);
		if (selectReplacingUnit) replacingUnit.Select();
		return replacingUnit;
	}
}