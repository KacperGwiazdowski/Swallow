using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Swallow.Core.Domains.CollectedData;
using Swallow.DataCollector.Gis.Dtos;

namespace Swallow.DataCollector.Gis
{
    public class GisDataCollector : IDataCollector
    {
        private readonly HttpClient _httpClient;

        public GisDataCollector()
        {
            _httpClient = new HttpClient();
        }
        public ICollection<DataMeasurment> GetSensorData(int sensorId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Sensor> GetStationData(int stationId)
        {
            var response = GetAsync($"http://api.gios.gov.pl/pjp-api/rest/station/sensors/{stationId}").Result;
            var sensors = JsonConvert.DeserializeObject<ICollection<SensorDto>>(response);

            Console.WriteLine(response);
            return null;
        }

        public ICollection<MeasurmentStation> GetStations()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StationDto, MeasurmentStation>()
                    .ForPath(x => x.Coordinates.Latitude, y => y.MapFrom(z => z.GegrLat))
                    .ForPath(x => x.Coordinates.Longitude, y => y.MapFrom(z => z.GegrLon))
                    .ForMember(x => x.Name, y => y.MapFrom(z => z.StationName))
                    .ForMember(x => x.CreationDate, y => y.MapFrom(z => DateTime.Now));
            });
            IMapper mapper = mapperConfiguration.CreateMapper();

            var response = GetAsync("http://api.gios.gov.pl/pjp-api/rest/station/findAll").Result;
            var stationsDtos = JsonConvert.DeserializeObject<ICollection<StationDto>>(response);
            var stations = mapper.Map<ICollection<MeasurmentStation>>(stationsDtos);
            return stations;
        }

        private async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
