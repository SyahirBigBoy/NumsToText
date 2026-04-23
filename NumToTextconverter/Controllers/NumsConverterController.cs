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

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}