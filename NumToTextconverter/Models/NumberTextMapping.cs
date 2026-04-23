using System.Data;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace NumToTextconverter.Models;

public class NumberTextMapping
{
    public string GroupType { get; set; } = string.Empty;
    public string GroupNumberInText { get; set; } = string.Empty;
}

public class NumberTextMappingList
{
    //initalize list
    public List<NumberTextMapping> Mappings { get; set; } = new List<NumberTextMapping>();

    //create mapping for each group of numbers
    public NumberTextMappingList()
    {
        Mappings.Add(new NumberTextMapping { GroupType = "Units", GroupNumberInText = "One, Two, Three, Four, Five, Six, Seven, Eight, Nine" });
        Mappings.Add(new NumberTextMapping { GroupType = "Teens", GroupNumberInText = "Ten, Eleven, Twelve, Thirteen, Fourteen, Fifteen, Sixteen, Seventeen, Eighteen, Nineteen" });
        Mappings.Add(new NumberTextMapping { GroupType = "Tens", GroupNumberInText = "Twenty, Thirty, Forty, Fifty, Sixty, Seventy, Eighty, Ninety" });
        Mappings.Add(new NumberTextMapping { GroupType = "Multiplier", GroupNumberInText = "Hundred,Thousand,Million,Billion" });
    }
}