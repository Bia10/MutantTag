using WCSharp.Api;
using static WCSharp.Api.Common;

namespace Source.Lib;

/// <summary>
/// Provides a collection of sounds for various in-game events and notifications.
/// </summary>
public static class Sounds
{
	/// <summary>
	/// Sound for when a new quest is discovered.
	/// </summary>
	public static sound? QuestDiscovered { get; private set; }

	/// <summary>
	/// Sound for when a quest is completed.
	/// </summary>
	public static sound? QuestCompleted { get; private set; }

	/// <summary>
	/// Sound for when a quest fails.
	/// </summary>
	public static sound? QuestFailed { get; private set; }

	/// <summary>
	/// Sound for when a secret is found.
	/// </summary>
	public static sound? SecretFound { get; set; }

	/// <summary>
	/// Sound for rescue operations.
	/// </summary>
	public static sound? Rescue { get; private set; }

	/// <summary>
	/// Sound for providing hints.
	/// </summary>
	public static sound? Hint { get; private set; }

	/// <summary>
	/// Sound for warning messages.
	/// </summary>
	public static sound? Warning { get; private set; }

	/// <summary>
	/// Sound for indicating errors.
	/// </summary>
	public static sound? Error { get; private set; }

	/// <summary>
	/// Sets up the sound resources during initialization.
	/// </summary>
	public static void Setup()
	{
		QuestDiscovered = CreateSoundFromLabel("QuestNew", false, false, false, 10000, 10000);
		QuestCompleted = CreateSoundFromLabel("QuestCompleted", false, false, false, 10000, 10000);
		QuestFailed = CreateSoundFromLabel("QuestFailed", false, false, false, 10000, 10000);
		Rescue = CreateSoundFromLabel("Rescue", false, false, false, 10000, 10000);
		SecretFound = CreateSoundFromLabel("SecretFound", false, false, false, 10000, 10000);
		Hint = CreateSoundFromLabel("Hint", false, false, false, 10000, 10000);
		Warning = CreateSoundFromLabel("Warning", false, false, false, 10000, 10000);
		Error = CreateSoundFromLabel("Error", false, false, false, 10000, 10000);
	}
}