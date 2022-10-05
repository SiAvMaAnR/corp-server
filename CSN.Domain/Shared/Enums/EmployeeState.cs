using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Domain.Shared.Enums
{
    public enum EmployeeState
    {
        /// <summary>
        /// В сети
        /// </summary>
        Online,
        /// <summary>
        /// Не в сети
        /// </summary>
        Offline,
        /// <summary>
        /// Не беспокоить
        /// </summary>
        NotDisturb,
        /// <summary>
        /// Нет на месте
        /// </summary>
        Away
    }
}
