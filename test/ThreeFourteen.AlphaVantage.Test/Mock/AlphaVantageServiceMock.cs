﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ThreeFourteen.AlphaVantage.Parameters;
using ThreeFourteen.AlphaVantage.Service;

namespace ThreeFourteen.AlphaVantage.Test.Mock
{
    public class AlphaVantageServiceMock : IAlphaVantageService
    {
        public IDictionary<string, string> LatestParameters { get; private set; }

        private static readonly Dictionary<string, string> FileLookup = new Dictionary<string, string>
        {
            { "TIME_SERIES_INTRADAY", "ThreeFourteen.AlphaVantage.Test.ExampleData.TimeSeriesIntraDay.json" },
            { "TIME_SERIES_WEEKLY", "ThreeFourteen.AlphaVantage.Test.ExampleData.TimeSeriesWeekly.json" },
            { "ERROR", "ThreeFourteen.AlphaVantage.Test.ExampleData.Error.json" }
        };

        public Task<string> GetRawDataAsync(IDictionary<string, string> parameters)
        {
            LatestParameters = parameters;

            string file = FileLookup[parameters[ParameterFields.Function]];

            return LoadExampleData(file);
        }

        private async Task<string> LoadExampleData(string fileName)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
        }
    }
}