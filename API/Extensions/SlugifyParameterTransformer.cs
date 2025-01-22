using System.Text.RegularExpressions;

namespace Merchants.API.Extensions;

public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
	public string? TransformOutbound(object? value)
	{
		return value != null ? MyRegex().Replace(value.ToString()!, "$1-$2").ToLower() : null;
	}

	[GeneratedRegex("([a-z])([A-Z])")]
	private static partial Regex MyRegex();
}
