using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace FundAnalysis.DTO
{
    public class CsvFundDtoMapping : CsvMapping<FundDto>
    {
        public CsvFundDtoMapping()
        {
            // var t = new PercentageConverter() as ITypeConverter;
            MapProperty(0, x => x.dm);
            MapProperty(1, x => x.jc);
            MapProperty(2, x => x.jcen);
            MapProperty(3, x => x.jzrq);
            MapProperty(4, x => x.dwjz);
            MapProperty(5, x => x.ljjz);
            MapProperty(6, x => x.rzdf);
            MapProperty(7, x => x.zzf);
            MapProperty(8, x => x.yzf1);
            MapProperty(9, x => x.yzf3);
            MapProperty(10, x => x.yzf6);
            MapProperty(11, x => x.nzf1);
            MapProperty(12, x => x.nzf2);
            MapProperty(13, x => x.nzf3);
            MapProperty(14, x => x.jnzf);
            MapProperty(15, x => x.lnzf);
            MapProperty(16, x => x.clr);
            MapProperty(17, x => x.d17);
            MapProperty(18, x => x.zdy);
            MapProperty(19, x => x.d19);
            MapProperty(20, x => x.sxf, new PercentageConverter());
            MapProperty(21, x => x.d21);
            MapProperty(22, x => x.d22);
            MapProperty(23, x => x.d23);
            MapProperty(24, x => x.d24);
        }
    }
}