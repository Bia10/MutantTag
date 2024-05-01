namespace Source.Lib;

/// <summary>
/// Represents the types of Warcraft III colors.
/// </summary>
public enum WCColorType : byte
{
	MainQuest = 0,
	OptionalQuest = 1,
	SecretFound = 2,
	Hint = 3,
	Notice = 4,
	Warning = 5,
	NewUnitOrBuilding = 6,
	CompletedQuestRequirements = 7,
	ArtifactDescription = 8,

	White = 254,
	Black = 255
}