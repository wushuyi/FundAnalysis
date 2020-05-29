using System;
using TinyCsvParser.TypeConverter;

namespace FundAnalysis.DTO
{
    public class PercentageConverter : NonNullableConverter<float>
    {
        protected override bool InternalConvert(string value, out float result)
        {
            value = value.Split("%")[0];
            var res = float.TryParse(value, out result);
            // Console.WriteLine($"{res}, {value}, {result}");
            return res;
        }
    }
}