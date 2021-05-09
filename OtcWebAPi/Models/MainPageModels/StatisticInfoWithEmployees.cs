using OtcWebAPi.Models.DTO;
using System.Collections.Generic;

namespace OtcWebAPi.Models.MainPageModels
{
    public class StatisticInfoWithEmployees : StatisticInfo
    {

        public List<Employee> Employees { get; set; }

    }
}
