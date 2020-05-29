using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConsoleTables;
using FundAnalysis.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TinyCsvParser;
using Volo.Abp.AutoMapper;
using Volo.Abp.ObjectMapping;

namespace FundAnalysis
{
    public class RunService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RunService> _logger;
        private readonly IMapperAccessor _mapperAccessor;
        private IObjectMapper<AppModule> _objectMapper;

        public RunService(IConfiguration configuration, ILogger<RunService> logger,
            IMapperAccessor mapperAccessor, IObjectMapper<AppModule> objectMapper)
        {
            _configuration = configuration;
            _logger = logger;
            _mapperAccessor = mapperAccessor;
            _objectMapper = objectMapper;
        }

        private async Task Run(CancellationToken stoppingToken)
        {
            string json;
            var dataPath = _configuration["dataPath"] ??
                           throw new ArgumentNullException("_configuration[\"dataPath\"]");
            _logger.LogInformation($"{dataPath}");
            using (var stream = new FileStream(dataPath, FileMode.Open, FileAccess.Read))
            using (var reader1 = new StreamReader(stream))
            {
                var rawData = await reader1.ReadToEndAsync();
                json = rawData.Split("=")[1].Trim();
                json = json.Remove(json.Length - 1, 1);
            }

            // _logger.LogInformation($"{json.Length}, {json.Substring(json.Length-100, 100)}");
            var fundData = JObject.Parse(json);
            var datas1 = fundData["datas"].Children();
            var datas2 = datas1.Select(token => token.ToObject<string>()).ToList();
            var datas3 = string.Join(Environment.NewLine, datas2);


            var csvParserOptions = new CsvParserOptions(false, ',');
            var csvReaderOptions = new CsvReaderOptions(new[] {Environment.NewLine});
            var csvMapper = new CsvFundDtoMapping();
            var csvParser = new CsvParser<FundDto>(csvParserOptions, csvMapper);
            var result = csvParser
                .ReadFromString(csvReaderOptions, datas3)
                .Where(mappingResult => mappingResult.IsValid)
                .Select(mappingResult => mappingResult.Result)
                .ToList();
            RunAll(result, stoppingToken);
            // result
            //     .Where(dto => { return dto.d17 != "1"; })
            //     .Where((dto, i) =>
            //     {
            //         var _mapper = _mapperAccessor.Mapper;
            //         var lookDto = _mapper.Map<FundDto, LookDto>(dto);
            //
            //         Console.WriteLine(JsonConvert.SerializeObject(lookDto));
            //         return false;
            //     })
            //     .ToList();
        }

        private async Task RunAll(List<FundDto> result, CancellationToken stoppingToken)
        {
            Console.WriteLine(result.Count());
            var res1 = result.Where(dto =>
                {
                    return (dto.yzf6.HasValue && dto.yzf6 > 10)
                           && (dto.nzf1.HasValue && dto.nzf1 > 20)
                           && (dto.nzf2.HasValue && dto.nzf2 > 40)
                           && (!dto.nzf3.HasValue | (dto.nzf3.HasValue && dto.nzf3 > 60))
                           && (dto.nzf2 - dto.nzf1 > 20)
                           && (dto.nzf3.HasValue && (dto.nzf3 - dto.nzf2 > 20))
                        ;
                })
                // .Where(dto =>
                // {
                //     return dto.jc.Contains("工业");
                // })
                .OrderByDescending(dto => dto.yzf6);
            Console.WriteLine(res1.Count());

            var table = new ConsoleTable("基金代码", "近1月", "近3月", "近6月", "近1年", "近2年", "近3年", "基金简称");
            foreach (var fundDto in res1)
            {
                table.AddRow(fundDto.dm, fundDto.yzf1, fundDto.yzf3, fundDto.yzf6, fundDto.nzf1, fundDto.nzf2,
                    fundDto.nzf3, fundDto.jc);
            }
            table.Write();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Run(stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // throw;
            }
        }
    }
}