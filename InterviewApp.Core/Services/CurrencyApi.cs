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
    private const string BaseAddress = "https://nbrb.by/";

    public CurrencyApi()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
    }

    public async Task<List<CurrencyDto>> GetByDate(DateTime dateTime)
    {
        var date = dateTime.ToString("d");
        var httpResponseMessage = await _httpClient.GetAsync($"/Services/XmlExRates.aspx?ondate={date}");
        httpResponseMessage.EnsureSuccessStatusCode();
        var readAsStringAsync = await httpResponseMessage.Content.ReadAsStreamAsync();
        var xmlSerializer = new XmlSerializer(typeof(DailyRateDto));
        var dailyEx = xmlSerializer.Deserialize(readAsStringAsync) as DailyRateDto;

        return dailyEx?.Currency ?? new List<CurrencyDto>();
    }
}