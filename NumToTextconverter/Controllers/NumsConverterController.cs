using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NumToTextconverter.Models;

namespace NumToTextconverter.Controllers;

public class NumsConverterController : Controller
{
    public IActionResult NumsConverter()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Convert(decimal inputNumber)
    {
        //get our mapping list model
        var mappingList = new NumberTextMappingList();

        if (inputNumber == 0)
        {
            ViewBag.Result = "Zero";
            return View("NumsConverter");
        }

        var basicNum = mappingList.Mappings.FirstOrDefault(m => m.GroupType == "Units")?.GroupNumberInText.Split(',');
        var teens = mappingList.Mappings.FirstOrDefault(m => m.GroupType == "Teens")?.GroupNumberInText.Split(',');
        var tens = mappingList.Mappings.FirstOrDefault(m => m.GroupType == "Tens")?.GroupNumberInText.Split(',');
        var mults = mappingList.Mappings.FirstOrDefault(m => m.GroupType == "Multiplier")?.GroupNumberInText.Split(',');
        
        string result = "";
        return View("NumsConverter");
    }

    //process input of hundereds and below
    private string ProcessHundredNumber(long input, string[] basicNum, string[] teens, string[] tens, string[] mults)
    {
        
        string subResult = "";
        if (input >= 100)
        {
            subResult += basicNum?[input / 100 - 1] + " " + mults?[0] + " ";
            input %= 100;
        }
        if (input >= 20)
        {
            subResult += tens?[(input / 10) - 2] + " ";
            input %= 10;
        }
        if (input >= 10)
        {
            subResult += teens?[(int)(input % 10)] + " ";
            input = 0;
        }
        if (input > 0)
        {
            subResult += basicNum?[(int)input - 1] + " ";
        }
        return subResult.Trim();
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}