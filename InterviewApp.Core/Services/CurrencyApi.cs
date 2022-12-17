using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using InterviewApp.Data.Dtos;

namespace InterviewApp.Core.Services;

public class CurrencyApi
{
    private readonly HttpClient _httpClient;
    private const string BaseAddress = "https://www.nbrb.by/Services/XmlExRates.aspx";

    public CurrencyApi()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<CurrencyDto>> GetByDate(DateTime dateTime)
    {
        var date = dateTime.ToString("d");
        var uriBuilder = new UriBuilder(BaseAddress)
        {
            Query = $"ondate={date}"
        }.ToString();
        var httpResponseMessage = await _httpClient.GetAsync(uriBuilder);
        httpResponseMessage.EnsureSuccessStatusCode();
        var readAsStringAsync = await httpResponseMessage.Content.ReadAsStreamAsync();
        var xmlSerializer = new XmlSerializer(typeof(DailyRateDto));
        var dailyEx = xmlSerializer.Deserialize(readAsStringAsync) as DailyRateDto;

        return dailyEx?.Currency ?? new List<CurrencyDto>();
    }
}