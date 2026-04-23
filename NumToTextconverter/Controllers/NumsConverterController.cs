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

        //Consider decimal number
        long wholeNumber = (long)Math.Truncate(inputNumber); 
        decimal fractionPart = inputNumber - wholeNumber;  


        string resultConverted = "";

        // Process whole number
        if(wholeNumber == 0)
        {
            resultConverted = "Zero";
        }
        else
        {
            //process billion and attach multiplier text
            if (wholeNumber >= 1_000_000_000)
            {
                resultConverted += ProcessNumber(wholeNumber / 1_000_000_000, basicNum, teens, tens, mults) + " " + mults[3] + " ";
                wholeNumber %= 1_000_000_000;
            }
            if (wholeNumber >= 1_000_000)
            {
                resultConverted += ProcessNumber(wholeNumber / 1_000_000, basicNum, teens, tens, mults) + " " + mults[2] + " ";
                wholeNumber %= 1_000_000;
            }
            if (wholeNumber >= 1_000)
            {
                resultConverted += ProcessNumber(wholeNumber / 1_000, basicNum, teens, tens, mults) + " " + mults[1] + " ";
                wholeNumber %= 1_000;
            }
            resultConverted += ProcessNumber(wholeNumber, basicNum, teens, tens, mults);
        }

        // Process fraction part if exists
        if (fractionPart > 0)
        {
            resultConverted += " point";
            string fractionStr = fractionPart.ToString().Split('.')[1]; 
            foreach (char digit in fractionStr)
            {
                int digitValue = int.Parse(digit.ToString());
                if (digitValue == 0)
                {
                    resultConverted += " Zero";
                }
                else
                {
                    resultConverted += " " + basicNum?[digitValue - 1];
                }
            }
        }

        ViewBag.Result = resultConverted.Trim();
        return View("NumsConverter");
    }

    //process input of hundereds and below
    private string ProcessNumber(long input, string[] basicNum, string[] teens, string[] tens, string[] mults)
    {
        
        string subResult = "";
        if (input >= 100)
        {
            subResult += basicNum?[input / 100 - 1] + " " + mults?[0] + " ";
            input %= 100;
            if(input > 0) //got balance if 201/200 = 1 , make it and 1
            {
                subResult += "and ";
            }
            else
            {
                return subResult.Trim();
            }
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