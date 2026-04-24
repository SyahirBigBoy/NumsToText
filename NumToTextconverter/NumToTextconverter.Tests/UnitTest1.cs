using System.Globalization;
using NumToTextconverter.Controllers;

namespace NumToTextconverter.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData("0", "Zero")]
    [InlineData("13", "Thirteen")]
    [InlineData("205", "Two Hundred Five")]
    [InlineData("1003", "One Thousand and Three")]
    [InlineData("1120022", "One Million One Hundred Twenty Thousand and Twenty Two")]
    [InlineData("32.25", "Thirty Two point Two Five")]
    [InlineData("-1", "Negative numbers are not supported.")]
    [InlineData("1000000000000", "Numbers greater than 999,999,999,999 are not supported.")]
    public void TestNumberToText(string input, string expected)
    {
        var controller = new NumsConverterController();
        var decimalInput = decimal.Parse(input, CultureInfo.InvariantCulture);

        controller.Convert(decimalInput);

        Assert.Equal(expected, controller.ViewBag.Result as string);
    }
}