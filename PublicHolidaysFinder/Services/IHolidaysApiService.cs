using PublicHolidaysFinder.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicHolidaysFinder.Services
{
    public interface IHolidaysApiService
    {
        Task<List<HolidayModel>> GetHolidays(string countryCode, int year);
    }
}
