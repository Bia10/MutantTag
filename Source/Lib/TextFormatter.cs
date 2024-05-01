using Source.WCSharp.Api.Extensions;
using System;
using System.Text;
using WCSharp.Api;

namespace Source.Lib;

/// <summary>
/// Provides methods for formatting text with colors and special tokens as they are used in Warcraft III.
/// </summary>
public static class TextFormatter
{
	/// <summary>
	/// Denotes the opening token for colored text.
	/// </summary>
	private const string ColorOpeningToken = "|c";

	/// <summary>
	/// Denotes the closing token for colored text.
	/// </summary>
	private const string ColorClosingToken = "|r";

	/// <summary>
	/// Denotes the new line token.
	/// TODO: Valid use of this is toolTips and messageLog, not player displayed msgs!
	/// </summary>
	private const string NewLineToken = "|n";

	/// <summary>
	/// Denotes a single empty line.
	/// </summary>
	private const string EmptyLineToken = NewLineToken + NewLineToken;

	/// <summary>
	/// Formats the message as a hint text.
	/// </summary>
	/// <param name="msg">The message to format.</param>
	/// <param name="useColonSeparator">Determines whether to use a colon separator.</param>
	/// <returns>The formatted hint text.</returns>
	public static string ToHintText(string msg, bool useColonSeparator = false)
		=> $"\n{ColorOpeningToken}{WCColor.Hint.ToHexString()}[HINT]{(useColonSeparator ? ": " : " - ")}{msg}{ColorClosingToken}";

	/// <summary>
	/// Formats the message as a notice text.
	/// </summary>
	/// <param name="msg">The message to format.</param>
	/// <param name="useColonSeparator">Determines whether to use a colon separator.</param>
	/// <returns>The formatted notice text.</returns>
	public static string ToNoticeText(string msg, bool useColonSeparator = false)
		=> $"\n{ColorOpeningToken}{WCColor.Notice.ToHexString()}[NOTICE]{(useColonSeparator ? ": " : " - ")}{msg}{ColorClosingToken}";

	/// <summary>
	/// Formats the message as a warning text.
	/// </summary>
	/// <param name="msg">The message to format.</param>
	/// <param name="useColonSeparator">Determines whether to use a colon separator.</param>
	/// <returns>The formatted warning text.</returns>
	public static string ToWarningText(string msg, bool useColonSeparator = false)
		=> $"\n{ColorOpeningToken}{WCColor.Warning.ToHexString()}[WARNING]{(useColonSeparator ? ": " : " - ")}{msg}{ColorClosingToken}";

	/// <summary>
	/// Formats the message as a main quest text.
	/// </summary>
	/// <param name="msg">The message to format.</param>
	/// <param name="useColonSeparator">Determines whether to use a colon separator.</param>
	/// <returns>The formatted main quest text.</returns>
	public static string ToMainQuestText(string msg, bool useColonSeparator = false)
		=> $"\n{ColorOpeningToken}{WCColor.MainQuest.ToHexString()}[QUEST]{(useColonSeparator ? ": " : " - ")}{msg}{ColorClosingToken}";

	/// <summary>
	/// Formats a custom message with the specified color type.
	/// </summary>
	/// <param name="msg">The message to format.</param>
	/// <param name="messageType">The type of the message color.</param>
	/// <param name="useColonSeparator">Determines whether to use a colon separator.</param>
	/// <returns>The formatted custom text.</returns>
	public static string ToCustomText(string msg, WCColorType messageType, bool useColonSeparator = false)
	{
		var messageColor = messageType switch
		{
			WCColorType.MainQuest or WCColorType.OptionalQuest or WCColorType.SecretFound => WCColor.MainQuest.ToHexString(),
			WCColorType.Hint => WCColor.Hint.ToHexString(),
			WCColorType.NewUnitOrBuilding => WCColor.NewUnitOrBuilding.ToHexString(),
			WCColorType.Notice => WCColor.Notice.ToHexString(),
			WCColorType.Warning => WCColor.Warning.ToHexString(),
			WCColorType.CompletedQuestRequirements => WCColor.CompletedQuestRequirements.ToHexString(),
			WCColorType.ArtifactDescription => WCColor.ArtifactDescription.ToHexString(),
			WCColorType.White => WCColor.White.ToHexString(),
			WCColorType.Black => WCColor.Black.ToHexString(),
			_ => WCColor.White.ToHexString()
		};

		return
			$"{EmptyLineToken}{ColorOpeningToken}{messageColor}[{messageType.ToString().ToUpper()}]{(useColonSeparator ? ": " : " - ")}{msg}{ColorClosingToken}";
	}

	/// <summary>
	/// Colorizes the message with the specified color code.
	/// </summary>
	/// <param name="msg">The message to colorize.</param>
	/// <param name="wcColor">The WCColor to use.</param>
	/// <returns>The colorized text.</returns>
	public static string Colorize(string msg, WCColor wcColor)
		=> $"{ColorOpeningToken}{NormalizeColorCode(wcColor.ToHexString())}{msg}{ColorClosingToken}";

	/// <summary>
	/// Colorizes the message with the specified color code.
	/// </summary>
	/// <param name="msg">The message to colorize.</param>
	/// <param name="colorCode">The color code.</param>
	/// <returns>The colorized text.</returns>
	public static string Colorize(string msg, string colorCode)
		=> $"{ColorOpeningToken}{NormalizeColorCode(colorCode)}{msg}{ColorClosingToken}";

	/// <summary>
	/// Colorizes the specified text within the input text using the provided color code.
	/// </summary>
	/// <param name="inputText">The input text.</param>
	/// <param name="colorCode">The color code.</param>
	/// <param name="textToColorize">The text to colorize.</param>
	/// <returns>The text with colorized portions.</returns>
	public static string ColorizeText(string inputText, string colorCode, string textToColorize)
	{
		var coloredText = $"{ColorOpeningToken}{NormalizeColorCode(colorCode)}{textToColorize}{ColorClosingToken}";
		return inputText.Replace(textToColorize, coloredText);
	}

	/// <summary>
	/// Generates a gradient text with colors transitioning from startColor to endColor.
	/// </summary>
	/// <param name="text">The text to generate gradient for.</param>
	/// <param name="startColor">The starting color.</param>
	/// <param name="endColor">The ending color.</param>
	/// <param name="useLowercaseHex">Determines whether to use lowercase hexadecimals.</param>
	/// <returns>The gradient coloured text.</returns>
	public static string GenerateGradient(string text, string startColor, string endColor, bool useLowercaseHex = false)
	{
		// Count the number of characters in the text (excluding spaces)
		var charCount = text.Replace(" ", "").Length;

		// Calculate the difference between start and end colors
		var diffR = Math.Abs(Convert.ToInt32(startColor.Substring(0, 2), 16) - Convert.ToInt32(endColor.Substring(0, 2), 16));
		var diffG = Math.Abs(Convert.ToInt32(startColor.Substring(2, 2), 16) - Convert.ToInt32(endColor.Substring(2, 2), 16));
		var diffB = Math.Abs(Convert.ToInt32(startColor.Substring(4, 2), 16) - Convert.ToInt32(endColor.Substring(4, 2), 16));

		// Calculate the step values for R, G, and B
		var stepR = diffR != 0 ? diffR / charCount : 0;
		var stepG = diffG != 0 ? diffG / charCount : 0;
		var stepB = diffB != 0 ? diffB / charCount : 0;

		var gradientText = new StringBuilder();

		for (var i = 0; i < text.Length; i++)
		{
			var currentChar = text[i];

			// Calculate the color for the current character
			var currentR = Math.Min(255, Math.Max(0, Convert.ToInt32(startColor.Substring(0, 2), 16) - stepR * i));
			var currentG = Math.Min(255, Math.Max(0, Convert.ToInt32(startColor.Substring(2, 2), 16) - stepG * i));
			var currentB = Math.Min(255, Math.Max(0, Convert.ToInt32(startColor.Substring(4, 2), 16) - stepB * i));

			// Convert the RGB values to hexadecimal
			var hexColor = $"{currentR:X2}{currentG:X2}{currentB:X2}";

			if (useLowercaseHex)
				hexColor = hexColor.ToLower();

			// Append the colored character to the gradient text
			gradientText.Append($"{ColorOpeningToken}{NormalizeColorCode(hexColor)}{currentChar}{ColorClosingToken}");
		}

		return gradientText.ToString();
	}

	/// <summary>
	/// Normalizes the color code format to AARRGGBB.
	/// </summary>
	/// <param name="colorCode">The color code to normalize.</param>
	/// <returns>The normalized color code.</returns>
	public static string NormalizeColorCode(string colorCode)
	{
		switch (colorCode.Length)
		{
			// Format RRGGBB, missing alpha
			case 6: return "ff" + colorCode;
			// Format AARRGGBB, alpha is 00
			case 8 when colorCode.StartsWith("00", StringComparison.Ordinal):
				return colorCode.Replace("00", "ff");
			// Format seems valid
			case 8 when colorCode.StartsWith("ff", StringComparison.Ordinal):
				return colorCode;
			default:
				Console.WriteLine($"Unrecognized color code format: {colorCode}");
				return string.Empty;
		}
	}

	/// <summary>
	/// Tests the text coloring methods.
	/// </summary>
	public static void TestTextColoring()
	{
		Console.WriteLine($"WCColor Hint hex value: {WCColor.Hint.ToHexString()} result: {Colorize(WCColor.Hint.Name, WCColor.Hint)}");
		Console.WriteLine($"WCColor Notice hex value: {WCColor.Notice.ToHexString()} result: {Colorize(WCColor.Notice.Name, WCColor.Notice)}");
		Console.WriteLine($"WCColor Warning hex value: {WCColor.Warning.ToHexString()} result: {Colorize(WCColor.Warning.Name, WCColor.Warning)}");
		Console.WriteLine($"WCColor NewUnitOrBuilding hex value: {WCColor.NewUnitOrBuilding.ToHexString()} result: {Colorize(WCColor.NewUnitOrBuilding.Name, WCColor.NewUnitOrBuilding)}");
		Console.WriteLine($"WCColor TooltipAbilityAutoCast hex value: {WCColor.TooltipAbilityAutoCast.ToHexString()} result: {Colorize(WCColor.TooltipAbilityAutoCast.Name, WCColor.TooltipAbilityAutoCast)}");
		Console.WriteLine($"WCColor CompletedQuestRequirements hex value: {WCColor.CompletedQuestRequirements.ToHexString()} result: {Colorize(WCColor.CompletedQuestRequirements.Name, WCColor.CompletedQuestRequirements)}");
		Console.WriteLine($"WCColor ArtifactDescription hex value: {WCColor.ArtifactDescription.ToHexString()} result: {Colorize(WCColor.ArtifactDescription.Name, WCColor.ArtifactDescription)}");
		Console.WriteLine($"WCColor White hex value: {WCColor.White.ToHexString()} result: {Colorize(WCColor.White.Name, WCColor.White)}");
		Console.WriteLine($"WCColor Black hex value: {WCColor.Black.ToHexString()} result: {Colorize(WCColor.Black.Name, WCColor.Black)}");

		const string msg = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vehicula felis sit amet libero ultricies, sit amet viverra nulla venenatis.";

		Console.WriteLine($"Colorized text: {ColorizeText(msg, WCColor.MainQuest.ToHexString(), "libero ultricies")}");
		Console.WriteLine($"Gradient text: {GenerateGradient(msg, WCColor.White.ToHexString(), WCColor.Warning.ToHexString())}");

		Common.GetLocalPlayer().DisplayHint("This is test of colored and formatted hint msg :]");
		Common.GetLocalPlayer().DisplayNotice("This is test of colored and formatted notice msg :]");
		Common.GetLocalPlayer().DisplayWarning("This is test of colored and formatted warning msg :]");
		Common.GetLocalPlayer().DisplayMainQuest("This is test of colored and formatted main quest msg :]");
	}
}