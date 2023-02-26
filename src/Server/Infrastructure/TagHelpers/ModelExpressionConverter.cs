using Dtat;

namespace Infrastructure.TagHelpers;

public class ModelExpressionConverter : object
{
	public ModelExpressionConverter
		(Microsoft.AspNetCore.Mvc.ViewFeatures.ModelExpression modelExpression)
	{
		IsLeftToRight = false;
		HasBeenConverted = false;
		ModelExpression = modelExpression;

		switch (modelExpression.ModelExplorer.ModelType)
		{
			case System.Type typeGuid when typeGuid == typeof(System.Guid):
			{
				IsLeftToRight = true;
				HasBeenConverted = true;

				break;
			}

			case System.Type typeInt when typeInt == typeof(int):
			case System.Type typeByte when typeByte == typeof(byte):
			case System.Type typeLong when typeLong == typeof(long):
			{
				ConvertLong();

				break;
			}

			case System.Type typeDateTime when typeDateTime == typeof(System.DateTime):
			{
				ConvertDateTime();

				break;
			}

			case System.Type typeDateTimeOffset when typeDateTimeOffset == typeof(System.DateTimeOffset):
			{
				ConvertDateTimeOffset();

				break;
			}
		}
	}

	public string? Value { get; protected set; }

	public bool IsLeftToRight { get; protected set; }

	public bool HasBeenConverted { get; protected set; }

	protected Microsoft.AspNetCore.Mvc.ViewFeatures.ModelExpression ModelExpression { get; }

	protected void ConvertLong()
	{
		object value =
			ModelExpression.Model;

		IsLeftToRight = true;
		HasBeenConverted = true;

		if (value is null)
		{
			Value = Constants.Format.NullValue;
			return;
		}

		var valueInteget =
			System.Convert.ToInt64(value: value);

		var result =
			valueInteget.ToString
			(format: Constants.Format.Integer);

		result = result
			.ConvertDigitsToUnicode();

		if (result is null)
		{
			Value = Constants.Format.NullValue;
			return;
		}

		Value = result;
	}

	protected void ConvertDateTime()
	{
		object value =
			ModelExpression.Model;

		IsLeftToRight = true;
		HasBeenConverted = true;

		if (value is null)
		{
			Value = Constants.Format.NullValue;
			return;
		}

		var valueDateTime =
			(System.DateTime)value;

		var result =
			valueDateTime.ToString
			(format: Constants.Format.DateTime);

		result = result
			.ConvertDigitsToUnicode();

		if (result is null)
		{
			Value = Constants.Format.NullValue;
			return;
		}

		Value = result;
	}

	protected void ConvertDateTimeOffset()
	{
		object value =
			ModelExpression.Model;

		IsLeftToRight = true;
		HasBeenConverted = true;

		if (value is null)
		{
			Value = Constants.Format.NullValue;
			return;
		}

		var valueDateTime =
			(System.DateTimeOffset)value;

		var result =
			valueDateTime.DateTime.ToString
			(format: Constants.Format.DateTime);

		result = result
			.ConvertDigitsToUnicode();

		if (result is null)
		{
			Value = Constants.Format.NullValue;
			return;
		}

		Value = result;
	}
}
