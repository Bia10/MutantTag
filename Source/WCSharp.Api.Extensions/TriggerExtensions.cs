using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Shared;
using WCSharp.Shared.Data;
using static WCSharp.Api.Common;

namespace Source.WCSharp.Api.Extensions;

/// <summary>
/// Contains extension methods for creating, registering and destroying <seealso cref="trigger"> triggers </seealso> in Warcraft III maps.
/// </summary>
public static class TriggerExtensions
{
	/// <summary>
	/// Registers a chat event for all players on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the chat event on.</param>
	/// <param name="chatMessageToDetect">The chat message to detect.</param>
	/// <param name="exactMatchOnly">Determines if only exact matches should be detected.</param>
	/// <returns>The trigger with the chat event registered.</returns>
	public static trigger RegisterSharedChatEvent(this trigger whichTrigger, string chatMessageToDetect,
		bool exactMatchOnly)
	{
		foreach (player? player in Util.EnumeratePlayers())
			TriggerRegisterPlayerChatEvent(whichTrigger, player, chatMessageToDetect, exactMatchOnly);
		return whichTrigger;
	}

	/// <summary>
	/// Registers a chat event for a specific player on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the chat event on.</param>
	/// <param name="whichPlayer">The player whose chat event will be registered.</param>
	/// <param name="chatMessageToDetect">The chat message to detect.</param>
	/// <param name="exactMatchOnly">Determines if only exact matches should be detected.</param>
	/// <returns>The trigger with the chat event registered.</returns>
	public static trigger RegisterChatEvent(this trigger whichTrigger, player whichPlayer, string chatMessageToDetect,
		bool exactMatchOnly)
	{
		TriggerRegisterPlayerChatEvent(whichTrigger, whichPlayer, chatMessageToDetect, exactMatchOnly);
		return whichTrigger;
	}

	/// <summary>
	/// Registers chat events for a specific player on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the chat events on.</param>
	/// <param name="whichPlayer">The player whose chat events will be registered.</param>
	/// <param name="chatMessagesToDetect">The collection of chat messages to detect.</param>
	/// <param name="exactMatchOnly">Determines if only exact matches should be detected.</param>
	/// <returns>The trigger with the chat events registered.</returns>
	public static trigger RegisterChatEvents(this trigger whichTrigger, player whichPlayer,
		IEnumerable<string> chatMessagesToDetect, bool exactMatchOnly)
	{
		foreach (var chatMessage in chatMessagesToDetect)
			TriggerRegisterPlayerChatEvent(whichTrigger, whichPlayer, chatMessage, exactMatchOnly);
		return whichTrigger;
	}

	/// <summary>
	/// Registers a key event for all players on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the key event on.</param>
	/// <param name="key">The key to register.</param>
	/// <param name="metaKey">The meta key (SHIFT, CTRL, ALT).</param>
	/// <param name="keyDown">Determines if the key event is triggered on key down.</param>
	/// <returns>The trigger with the key event registered.</returns>
	public static trigger RegisterSharedKeyEvent(this trigger whichTrigger, oskeytype key, int metaKey, bool keyDown)
	{
		foreach (player? player in Util.EnumeratePlayers())
			BlzTriggerRegisterPlayerKeyEvent(whichTrigger, player, key, metaKey, keyDown);
		return whichTrigger;
	}

	/// <summary>
	/// Registers a timer event on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the timer event on.</param>
	/// <param name="timeout">The duration of the timer in seconds.</param>
	/// <param name="periodic">Determines if the timer should repeat.</param>
	/// <returns>The trigger with the timer event registered.</returns>
	public static trigger RegisterTimerEvent(this trigger whichTrigger, float timeout, bool periodic = false)
	{
		TriggerRegisterTimerEvent(whichTrigger, timeout, periodic);
		return whichTrigger;
	}

	/// <summary>
	/// Registers an enter region event on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the enter region event on.</param>
	/// <param name="region">The region to monitor for enter events.</param>
	/// <param name="filter">An optional filter to apply.</param>
	/// <returns>The trigger with the enter region event registered.</returns>
	public static trigger RegisterEnterRegion(this trigger whichTrigger, Rectangle region, boolexpr? filter = null)
	{
		TriggerRegisterEnterRegion(whichTrigger, region.Region, filter);
		return whichTrigger;
	}

	/// <summary>
	/// Registers enter region events for multiple regions on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the enter region events on.</param>
	/// <param name="regions">The collection of regions to monitor for enter events.</param>
	/// <param name="filter">An optional filter to apply.</param>
	/// <returns>The trigger with the enter region events registered.</returns>
	public static trigger RegisterEnterRegions(this trigger whichTrigger, IEnumerable<Rectangle> regions,
		boolexpr? filter = null)
	{
		foreach (Rectangle region in regions)
			TriggerRegisterEnterRegion(whichTrigger, region.Region, filter);
		return whichTrigger;
	}

	/// <summary>
	/// Registers a leave region event on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the leave region event on.</param>
	/// <param name="region">The region to monitor for leave events.</param>
	/// <param name="filter">An optional filter to apply.</param>
	/// <returns>The trigger with the leave region event registered.</returns>
	public static trigger RegisterLeaveRegion(this trigger whichTrigger, Rectangle region, boolexpr? filter = null)
	{
		TriggerRegisterLeaveRegion(whichTrigger, region.Region, filter);
		return whichTrigger;
	}

	/// <summary>
	/// Registers leave region events for multiple regions on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the leave region events on.</param>
	/// <param name="regions">The collection of regions to monitor for leave events.</param>
	/// <param name="filter">An optional filter to apply.</param>
	/// <returns>The trigger with the leave region events registered.</returns>
	public static trigger RegisterLeaveRegions(this trigger whichTrigger, IEnumerable<Rectangle> regions,
		boolexpr? filter = null)
	{
		foreach (Rectangle region in regions)
			TriggerRegisterLeaveRegion(whichTrigger, region.Region, filter);
		return whichTrigger;
	}

	/// <summary>
	/// Registers a life event on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the life event on.</param>
	/// <param name="whichUnit">The unit to monitor.</param>
	/// <param name="unitState">The unit state to monitor.</param>
	/// <param name="limitOp">The limit operator.</param>
	/// <param name="limitValue">The value to compare against.</param>
	/// <returns>The trigger with the life event registered.</returns>
	public static trigger RegisterLifeEvent(this trigger whichTrigger, unit whichUnit, unitstate unitState,
		limitop limitOp, float limitValue)
	{
		TriggerRegisterUnitStateEvent(whichTrigger, whichUnit, unitState, limitOp, limitValue);
		return whichTrigger;
	}

	/// <summary>
	/// Registers a unit event on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the unit event on.</param>
	/// <param name="whichUnit">The unit to monitor.</param>
	/// <param name="whichEvent">The event to monitor.</param>
	/// <returns>The trigger with the unit event registered.</returns>
	public static trigger RegisterUnitEvent(this trigger whichTrigger, unit whichUnit, unitevent whichEvent)
	{
		TriggerRegisterUnitEvent(whichTrigger, whichUnit, whichEvent);
		return whichTrigger;
	}

	/// <summary>
	/// Registers a dialog button event on the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to register the dialog button event on.</param>
	/// <param name="whichButton">The button to monitor.</param>
	/// <returns>The trigger with the dialog button event registered.</returns>
	public static trigger RegisterDialogButtonEvent(this trigger whichTrigger, button whichButton)
	{
		TriggerRegisterDialogButtonEvent(whichTrigger, whichButton);
		return whichTrigger;
	}

	/// <summary>
	/// Adds an action to the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to add the action to.</param>
	/// <param name="actionFunc">The action to add.</param>
	/// <returns>The trigger with the action added.</returns>
	public static trigger AddAction(this trigger whichTrigger, Action actionFunc)
	{
		TriggerAddAction(whichTrigger, actionFunc);
		return whichTrigger;
	}

	/// <summary>
	/// Executes the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to execute.</param>
	/// <returns>The executed trigger.</returns>
	public static trigger Execute(this trigger whichTrigger)
	{
		TriggerExecute(whichTrigger);
		return whichTrigger;
	}

	/// <summary>
	/// Disables the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to disable.</param>
	/// <returns>The disabled trigger.</returns>
	public static trigger Disable(this trigger whichTrigger)
	{
		DisableTrigger(whichTrigger);
		return whichTrigger;
	}

	/// <summary>
	/// Enables the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to enable.</param>
	/// <returns>The enabled trigger.</returns>
	public static trigger Enable(this trigger whichTrigger)
	{
		EnableTrigger(whichTrigger);
		return whichTrigger;
	}

	/// <summary>
	/// Destroys the specified trigger.
	/// </summary>
	/// <param name="whichTrigger">The trigger to destroy.</param>
	public static void Destroy(this trigger whichTrigger)
		=> DestroyTrigger(whichTrigger);
}