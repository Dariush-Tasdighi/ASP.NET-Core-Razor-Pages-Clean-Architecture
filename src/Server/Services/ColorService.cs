namespace Services;

/// <summary>
/// https://www.schemecolor.com/dullish-pastels.php
/// 
/// 
/// https://www.schemecolor.com/20-best-gray-colors-for-ui-web-design.php
/// </summary>
public class ColorService : object
{
	public ColorService() : base()
	{
		HtmlColors =
			new System.Collections.Generic.List<string>();

		Colors =
			new System.Collections.Generic.List<System.Drawing.Color>();

		AddColor(htmlColor: "#B0C2C4");
		AddColor(htmlColor: "#F1E7E3");
		AddColor(htmlColor: "#EDD9C6");
		AddColor(htmlColor: "#B5A0A1");
		AddColor(htmlColor: "#C4E4DE");

		AddColor(htmlColor: "#B7C9B2");
		AddColor(htmlColor: "#F7D878");
		AddColor(htmlColor: "#E5E4DE");
		AddColor(htmlColor: "#AEC9D3");
		AddColor(htmlColor: "#E6C5D4");

		AddColor(htmlColor: "#7E7E7E");
		AddColor(htmlColor: "#EEEEEE");
		AddColor(htmlColor: "#6D6D6D");
		AddColor(htmlColor: "#CCCCCC");
		AddColor(htmlColor: "#BBBBBB");
		AddColor(htmlColor: "#333333");
		AddColor(htmlColor: "#8C8C8C");
		AddColor(htmlColor: "#D3D3D3");
		AddColor(htmlColor: "#C0C0C0");
		AddColor(htmlColor: "#555555");
		AddColor(htmlColor: "#F2F2F2");
		AddColor(htmlColor: "#979797");
		AddColor(htmlColor: "#999999");
		AddColor(htmlColor: "#888888");
		AddColor(htmlColor: "#E5E5E5");
		AddColor(htmlColor: "#C5C5C5");
		AddColor(htmlColor: "#DDDDDD");
		AddColor(htmlColor: "#708090");
		AddColor(htmlColor: "#F5F5F5");
		AddColor(htmlColor: "#8B8B81");
	}

	private System.Collections.Generic.IList<string> HtmlColors { get; }

	public System.Collections.Generic.IList<System.Drawing.Color> Colors { get; }

	private void AddColor(string? htmlColor)
	{
		if (string.IsNullOrWhiteSpace(value: htmlColor))
		{
			return;
		}

		htmlColor = htmlColor.Replace
			(oldValue: " ", newValue: string.Empty).ToLower();

		if (htmlColor.StartsWith(value: "#") == false)
		{
			htmlColor = $"#{htmlColor}";
		}

		if (htmlColor.Length > 7)
		{
			return;
		}

		System.Drawing.Color color;

		try
		{
			color = System.Drawing
				.ColorTranslator.FromHtml(htmlColor: htmlColor);
		}
		catch
		{
			return;
		}

		if (HtmlColors.Contains(item: htmlColor))
		{
			return;
		}

		Colors.Add(item: color);
		HtmlColors.Add(item: htmlColor);
	}
}
