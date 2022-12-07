using System.Collections.Generic;
using InterviewApp.Data.Dtos;
using InterviewApp.Data.Entities;

namespace InterviewApp.Core.Mappers;

public static class MapperExtension
{
    public static Currency MapCurrencyDtoToCurrency(this CurrencyDto currencyDto)
    {
        return new Currency()
        {
            Name = currencyDto.Name, Rate = currencyDto.Rate, NumCode = currencyDto.NumCode, Scale = currencyDto.Scale
        };
    }

    public static List<Currency> MapCurrencyDtosToCurrencyEntity(this List<CurrencyDto> currencyDto)
    {
        var currencies = new List<Currency>();
        foreach (var dto in currencyDto)
        {
            currencies.Add(new Currency()
            {
                Name = dto.Name, Rate = dto.Rate, NumCode = dto.NumCode, Scale = dto.Scale,CharCode = dto.CharCode
            });
        }
        return currencies;
    }
}