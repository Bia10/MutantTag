using Source.Lib;
using System.Collections.Generic;
using WCSharp.Api;
using static WCSharp.Api.Common;

// ReSharper disable PossibleUnintendedReferenceComparison

namespace Source.WCSharp.Api.Extensions;

/// <summary>
/// Contains extension methods for displaying messages to <seealso cref="player"> players </seealso> in Warcraft III maps.
/// </summary>
public static class PlayerDisplayExtensions
{
	/// <summary>
	/// Displays a message to the specified player.
	/// </summary>
	/// <param name="whichPlayer">The player to display the message to.</param>
	/// <param name="msg">The message to display.</param>
	/// <param name="x">The x-coordinate where the message should be displayed. Default is 0.</param>
	/// <param name="y">The y-coordinate where the message should be displayed. Default is 0.</param>
	public static void DisplayMessage(this player whichPlayer, string msg, float x = 0, float y = 0)
		=> DisplayTextToPlayer(whichPlayer, x, y, msg);

	/// <summary>
	/// Displays a message to multiple players.
	/// </summary>
	/// <param name="whichPlayers">The players to display the message to.</param>
	/// <param name="msg">The message to display.</param>
	public static void DisplayMessageToPlayers(this IEnumerable<player> whichPlayers, string msg)
	{
		foreach (player curPlayer in whichPlayers) curPlayer.DisplayMessage(msg);
	}

	/// <summary>
	/// Displays a hint message to the specified player.
	/// </summary>
	/// <param name="whichPlayer">The player to display the hint message to.</param>
	/// <param name="msg">The hint message to display.</param>
	/// <param name="x">The x-coordinate where the message should be displayed. Default is 0.</param>
	/// <param name="y">The y-coordinate where the message should be displayed. Default is 0.</param>
	public static void DisplayHint(this player whichPlayer, string msg, float x = 0, float y = 0)
	{
		DisplayTextToPlayer(whichPlayer, x, y, TextFormatter.ToHintText(msg));

		if (GetLocalPlayer() == whichPlayer) StartSound(Sounds.Hint);
	}

	/// <summary>
	/// Displays a hint message to multiple players.
	/// </summary>
	/// <param name="whichPlayers">The players to display the hint message to.</param>
	/// <param name="msg">The hint message to display.</param>
	public static void DisplayHintToPlayers(this IEnumerable<player> whichPlayers, string msg)
	{
		foreach (player curPlayer in whichPlayers) curPlayer.DisplayHint(msg);
	}

	/// <summary>
	/// Displays a notice message to the specified player.
	/// </summary>
	/// <param name="whichPlayer">The player to display the notice message to.</param>
	/// <param name="msg">The notice message to display.</param>
	/// <param name="x">The x-coordinate where the message should be displayed. Default is 0.</param>
	/// <param name="y">The y-coordinate where the message should be displayed. Default is 0.</param>
	public static void DisplayNotice(this player whichPlayer, string msg, float x = 0, float y = 0)
	{
		DisplayTextToPlayer(whichPlayer, x, y, TextFormatter.ToNoticeText(msg));

		if (GetLocalPlayer() == whichPlayer) StartSound(Sounds.Hint);
	}

	/// <summary>
	/// Displays a notice message to multiple players.
	/// </summary>
	/// <param name="whichPlayers">The players to display the notice message to.</param>
	/// <param name="msg">The notice message to display.</param>
	public static void DisplayNoticeToPlayers(this IEnumerable<player> whichPlayers, string msg)
	{
		foreach (player curPlayer in whichPlayers) curPlayer.DisplayNotice(msg);
	}

	/// <summary>
	/// Displays a warning message to the specified player.
	/// </summary>
	/// <param name="whichPlayer">The player to display the warning message to.</param>
	/// <param name="msg">The warning message to display.</param>
	/// <param name="x">The x-coordinate where the message should be displayed. Default is 0.</param>
	/// <param name="y">The y-coordinate where the message should be displayed. Default is 0.</param>
	public static void DisplayWarning(this player whichPlayer, string msg, float x = 0, float y = 0)
	{
		DisplayTextToPlayer(whichPlayer, x, y, TextFormatter.ToWarningText(msg));

		if (GetLocalPlayer() == whichPlayer) StartSound(Sounds.Warning);
	}

	/// <summary>
	/// Displays a warning message to multiple players.
	/// </summary>
	/// <param name="whichPlayers">The players to display the warning message to.</param>
	/// <param name="msg">The warning message to display.</param>
	public static void DisplayWarningToPlayers(this IEnumerable<player> whichPlayers, string msg)
	{
		foreach (player curPlayer in whichPlayers) curPlayer.DisplayWarning(msg);
	}

	/// <summary>
	/// Displays a main quest message to the specified player.
	/// </summary>
	/// <param name="whichPlayer">The player to display the main quest message to.</param>
	/// <param name="msg">The main quest message to display.</param>
	/// <param name="x">The x-coordinate where the message should be displayed. Default is 0.</param>
	/// <param name="y">The y-coordinate where the message should be displayed. Default is 0.</param>
	public static void DisplayMainQuest(this player whichPlayer, string msg, float x = 0, float y = 0)
	{
		DisplayTextToPlayer(whichPlayer, x, y, TextFormatter.ToMainQuestText(msg));

		if (GetLocalPlayer() == whichPlayer) StartSound(Sounds.QuestDiscovered);
	}

	/// <summary>
	/// Displays a main quest message to multiple players.
	/// </summary>
	/// <param name="whichPlayers">The players to display the main quest message to.</param>
	/// <param name="msg">The main quest message to display.</param>
	public static void DisplayMainQuestToPlayers(this IEnumerable<player> whichPlayers, string msg)
	{
		foreach (player curPlayer in whichPlayers) curPlayer.DisplayMainQuest(msg);
	}
}