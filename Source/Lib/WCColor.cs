using System;

namespace Source.Lib;

/// <summary>
/// Represents a Warcraft III color.
/// </summary>
public readonly struct WCColor
{
	/// <summary>
	/// Gets the color value.
	/// </summary>
	public int Value { get; }

	/// <summary>
	/// Gets the name of the color.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Gets the alpha value of the color.
	/// </summary>
	public int A => (Value >> 24) & 0xFF;

	/// <summary>
	/// Gets the red component of the color.
	/// </summary>
	public int R => (Value >> 16) & 0xFF;

	/// <summary>
	/// Gets the green component of the color.
	/// </summary>
	public int G => (Value >> 8) & 0xFF;

	/// <summary>
	/// Gets the blue component of the color.
	/// </summary>
	public int B => Value & 0xFF;

	/// <summary>
	/// Initializes a new instance of the <see cref="WCColor"/> struct.
	/// </summary>
	/// <param name="value">The color value.</param>
	/// <param name="name">The name of the color.</param>
	public WCColor(int value, string name = "")
	{
		Value = value;
		Name = name;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="WCColor"/> struct.
	/// </summary>
	/// <param name="a">The alpha component.</param>
	/// <param name="r">The red component.</param>
	/// <param name="g">The green component.</param>
	/// <param name="b">The blue component.</param>
	/// <param name="name">The name of the color.</param>
	public WCColor(int a, int r, int g, int b, string name = "")
	{
		Value = FromArgb(a, r, g, b);
		Name = name;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="WCColor"/> struct.
	/// </summary>
	/// <param name="r">The red component.</param>
	/// <param name="g">The green component.</param>
	/// <param name="b">The blue component.</param>
	/// <param name="name">The name of the color.</param>
	public WCColor(int r, int g, int b, string name = "")
	{
		Value = FromArgb(255, r, g, b);
		Name = name;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="WCColor"/> struct.
	/// </summary>
	/// <param name="hexValue">The color value in hexadecimal format.</param>
	/// <param name="name">The name of the color.</param>
	/// <exception cref="ArgumentException">Thrown when the hex value is not valid.</exception>
	public WCColor(string hexValue, string name = "")
	{
		// if alpha is omitted, default to 255
		if (hexValue.Length == 6)
			hexValue = "ff" + hexValue;

		if (hexValue.Length != 8)
			throw new ArgumentException("Hex value must be 8 characters long.");

		Value = Convert.ToInt32(hexValue, 16);
		Name = name;
	}

	/// <summary>
	/// Combines the specified alpha, red, green, and blue components into a color value.
	/// </summary>
	/// <param name="a">The alpha component.</param>
	/// <param name="r">The red component.</param>
	/// <param name="g">The green component.</param>
	/// <param name="b">The blue component.</param>
	/// <returns>The color value.</returns>
	public static int FromArgb(int a, int r, int g, int b)
		=> (a << 24) | (r << 16) | (g << 8) | b;

	/// <summary>
	/// Converts a color value into its alpha, red, green, and blue components.
	/// </summary>
	/// <param name="value">The color value.</param>
	/// <returns>The color components.</returns>
	public static (int A, int R, int G, int B) ToArgb(int value)
	{
		var a = (value >> 24) & 0xFF;
		var r = (value >> 16) & 0xFF;
		var g = (value >> 8) & 0xFF;
		var b = value & 0xFF;
		return (a, r, g, b);
	}

	/// <summary>
	/// Converts the color to its hexadecimal representation.
	/// </summary>
	/// <returns>The hexadecimal representation of the color.</returns>
	public string ToHexString()
		=> $"{A:X2}{R:X2}{G:X2}{B:X2}".ToLower();

	// Hex Code: "ffffcc00"
	// RGB Code: 255, 204, 0
	// Name: Tangerine Yellow, USC Gold
	// Use: MAIN QUEST/OPTIONAL QUEST/SECRET FOUND messages
	public static readonly WCColor MainQuest = new(255, 255, 204, "Tangerine Yellow");

	// Hex Code: "ff32cd32"
	// RGB Code: 50, 205, 50
	// Name: Lime Green
	// Use: HINT messages
	public static readonly WCColor Hint = new(50, 205, 50, "Lime Green");

	// Hex Code: "ffff8c00"
	// RGB Code: 255, 140, 0
	// Name: Dark Orange
	// Use: NOTICE messages
	public static readonly WCColor Notice = new(255, 140, 0, "Dark Orange");

	// Hex Code: "ffff0000"
	// RGB Code: 255, 0, 0
	// Name: Red
	// Use: WARNING messages
	public static readonly WCColor Warning = new(255, 0, 0, "Red");

	// Hex Code: "ff87ceeb"
	// RGB Code: 135, 206, 235
	// Name: Sky Blue
	// Use: NEW UNIT ACQUIRED/NEW BUILDING AVAILABLE messages
	public static readonly WCColor NewUnitOrBuilding = new(135, 206, 235, "Sky Blue");

	// Hex Code: "ffc3dbff"
	// RGB Code: 195, 219, 255
	// Name: Hawkes Blue
	// Use: ToolTips, enable/disable auto-casting
	public static readonly WCColor TooltipAbilityAutoCast = new(195, 219, 255, "Hawkes Blue");

	// Hex Code: "ff808080"
	// RGB Code: 128, 128, 128
	// Name: Gray
	// Use: Completed quest requirements
	public static readonly WCColor CompletedQuestRequirements = new(128, 128, 128, "Gray");

	// Hex Code: "ffffdead"
	// RGB Code: 255, 222, 173
	// Name: Navajo White
	// Use: Artifact descriptions in the Rexxar's campaign
	public static readonly WCColor ArtifactDescription = new(255, 222, 173, "Navajo White");

	// Hex: "ffffffff"
	// RGB: 255, 255, 255
	// Name: White
	// Use: General use
	public static readonly WCColor White = new(255, 255, 255, "White");

	// Hex Code: "ff000000"
	// RGB Code: 0, 0, 0
	// Name: Black
	// Use: General use
	public static readonly WCColor Black = new(0, 0, 0, "Black");
}