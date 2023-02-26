namespace Infrastructure.TagHelpers;

/// <summary>
/// https://stackoverflow.com/questions/51904629/how-to-create-custom-tag-helper-containing-other-tag-helpers
/// https://stackoverflow.com/questions/47547844/tag-helper-embedded-in-another-tag-helpers-code-doesnt-render
/// https://stackoverflow.com/questions/46681692/combine-taghelper-statements
/// </summary>
[Microsoft.AspNetCore.Razor.TagHelpers
	.HtmlTargetElement(tag: Constants.TagHelper.ReadOnlyInput,
	TagStructure = Microsoft.AspNetCore.Razor.TagHelpers.TagStructure.WithoutEndTag)]
public class ReadOnlyInputTagHelper :
	Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
{
	public ReadOnlyInputTagHelper
		(Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator generator) : base()
	{
		Generator = generator;
	}

	private Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator Generator { get; }

	[Microsoft.AspNetCore.Mvc.ViewFeatures.ViewContext]
	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeNotBound]
	public Microsoft.AspNetCore.Mvc.Rendering.ViewContext? ViewContext { get; set; }

	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName(name: "asp-for")]
	public Microsoft.AspNetCore.Mvc.ViewFeatures.ModelExpression? For { get; set; }

	public override async System.Threading.Tasks.Task ProcessAsync
		(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
		Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
	{
		// **************************************************
		var div =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "div");

		div.AddCssClass(value: "mb-3");
		// **************************************************

		// **************************************************
		var labelElement =
			await
			CreateLabelElementAsync();

		div.InnerHtml.AppendHtml(encoded: labelElement);
		// **************************************************

		// **************************************************
		string? dirString = null;

		var dirAttribute =
			output.Attributes["dir"];

		if (dirAttribute is not null)
		{
			var dirValue =
				dirAttribute.Value;

			if (dirValue is not null)
			{
				dirString =
					dirValue.ToString()?
					.Replace(oldValue: "{", newValue: string.Empty)
					.Replace(oldValue: "}", newValue: string.Empty);
			}
		}
		// **************************************************

		// **************************************************
		var textBoxElement =
			await
			CreateTextBoxElementAsync(dir: dirString);

		div.InnerHtml.AppendHtml(encoded: textBoxElement);
		// **************************************************

		// **************************************************
		output.TagName = null;

		output.TagMode =
			Microsoft.AspNetCore.Razor
			.TagHelpers.TagMode.StartTagAndEndTag;

		output.Content.SetHtmlContent(htmlContent: div);
		// **************************************************
	}

	private async System.Threading.Tasks.Task<string> CreateLabelElementAsync()
	{
		if (For is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(For));
		}

		var tagBuilder =
			Generator.GenerateLabel
			(viewContext: ViewContext,
			modelExplorer: For.ModelExplorer, expression: For.Name, labelText: null,
			htmlAttributes: new { @class = "form-label" });

		var writer =
			new System.IO.StringWriter();

		tagBuilder.WriteTo(writer: writer, encoder: Microsoft
			.AspNetCore.Razor.TagHelpers.NullHtmlEncoder.Default);

		var result =
			writer.ToString();

		await writer.DisposeAsync();

		return result;
	}

	private async System.Threading.Tasks
		.Task<string> CreateTextBoxElementAsync(string? dir = null)
	{
		if (For is null)
		{
			throw new System.Exception
				(message: $"'{nameof(For)}' property is null ");
		}

		Microsoft.AspNetCore.Mvc.Rendering.TagBuilder tagBuilder;

		var converter =
			new ModelExpressionConverter(modelExpression: For);

		if (converter.HasBeenConverted)
		{
			tagBuilder =
				Generator.GenerateTextBox
				(viewContext: ViewContext,
				modelExplorer: For.ModelExplorer,
				expression: For.Name, value: converter.Value,
				format: null, htmlAttributes: null);
		}
		else
		{
			tagBuilder =
				Generator.GenerateTextBox
				(viewContext: ViewContext,
				modelExplorer: For.ModelExplorer,
				expression: For.Name, value: For.Model,
				format: null, htmlAttributes: null);
		}

		tagBuilder.AddCssClass(value: "form-control");

		tagBuilder.Attributes.Add(key: "disabled", value: null);
		//tagBuilder.Attributes.Add(key: "disabled", value: "disabled");
		//tagBuilder.Attributes.Add(key: "readonly", value: null);
		//tagBuilder.Attributes.Add(key: "readonly", value: "readonly");

		if (converter.IsLeftToRight)
		{
			tagBuilder.AddCssClass(value: "ltr");
		}
		else
		{
			if (string.IsNullOrWhiteSpace(value: dir) == false)
			{
				tagBuilder.Attributes.Add(key: "dir", value: dir);
			}
		}

		var writer =
			new System.IO.StringWriter();

		tagBuilder.WriteTo(writer: writer, encoder: Microsoft
			.AspNetCore.Razor.TagHelpers.NullHtmlEncoder.Default);

		var result =
			writer.ToString();

		await writer.DisposeAsync();

		return result;
	}
}
