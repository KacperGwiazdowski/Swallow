using AutoMapper;
using Newtonsoft.Json;
using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Services;
using Swallow.DataCollector.Gis.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Swallow.DataCollector.Gis
{
    public class GisDataCollector : IDataCollector
    {
        private readonly string _gisBaseUrl;
        public GisDataCollector(string gisBaseUrl)
        {
            _gisBaseUrl = gisBaseUrl;
        }

        public async Task<ICollection<DataMeasurment>> GetSensorData(int sensorId, int externalSensorId)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SingleCheckDataDto, DataMeasurment>()
                .ForMember(x => x.CreationDate, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.Value, y => y.MapFrom(z => z.Value))
                .ForMember(x => x.SensorId, y => y.MapFrom(z => sensorId));

            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            var response = await GetAsync($"{_gisBaseUrl}/data/getData/{externalSensorId}");
            var measurmentDtos = JsonConvert.DeserializeObject<MeasurmentDataDto>(response);
            var measurments = mapper.Map<ICollection<DataMeasurment>>(measurmentDtos.Values);

            return measurments;
        }

        public async Task<ICollection<Sensor>> GetStationData(int stationId, int externalStationId)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SensorDto, Sensor>()
                .ForMember(x => x.ChemicalFormula, y => y.MapFrom(z => z.Param.ParamFormula))
                .ForMember(x => x.ParameterName, x => x.MapFrom(z => z.Param.ParamName))
                .ForMember(x => x.MeasurmentStationId, y => y.MapFrom(z => stationId))
                .ForMember(x => x.ExternalId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalDataProvider, y => y.MapFrom(z => ExternalDataProvider.GIOS));

            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            var response = await GetAsync($"{_gisBaseUrl}/station/sensors/{externalStationId}");
            var sensorsDtos = JsonConvert.DeserializeObject<ICollection<SensorDto>>(response);
            var sensors = mapper.Map<ICollection<Sensor>>(sensorsDtos);
            return sensors;
        }

        public async Task<ICollection<MeasurmentStation>> GetStations()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StationDto, MeasurmentStation>()
                    .ForPath(x => x.Latitude, y => y.MapFrom(z => z.GegrLat))
                    .ForPath(x => x.Longitude, y => y.MapFrom(z => z.GegrLon))
                    .ForMember(x => x.Name, y => y.MapFrom(z => z.StationName))
                    .ForMember(x => x.CreationDate, y => y.MapFrom(z => DateTime.Now))
                    .ForMember(x => x.ExternalId, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.Id, y => y.Ignore())
                    .ForMember(x => x.ExternalDataProvider, y => y.MapFrom(z => ExternalDataProvider.GIOS));

            });
            IMapper mapper = mapperConfiguration.CreateMapper();

            var response = await GetAsync($"{_gisBaseUrl}/station/findAll");
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
