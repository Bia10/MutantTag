namespace Source.WCSharp.Api.Extensions;

/// <summary>
/// Contains extensions related to mathematical operations used in Warcraft III maps.
/// </summary>
public static class MathExtensions
{
	/// <summary>
	/// The mathematical constant pi (π).
	/// </summary>
	public const float Pi = 3.141593f;

	/// <summary>
	/// Conversion factor from degrees to radians.
	/// </summary>
	public const float DegreesToRadians = Pi / 180.0f;

	/// <summary>
	/// Conversion factor from radians to degrees.
	/// </summary>
	public const float RadiansToDegrees = 180 / Pi;

	/// <summary>
	/// Returns the maximum of two floating point numbers.
	/// </summary>
	/// <param name="a">The first floating point number.</param>
	/// <param name="b">The second floating point number.</param>
	/// <returns>The greater of the two values.</returns>
	public static float MaxFloat(float a, float b)
		=> b > a ? b : a;
}