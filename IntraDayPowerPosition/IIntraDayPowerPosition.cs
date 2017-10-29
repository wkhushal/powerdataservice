using System;
using System.Threading.Tasks;

namespace IntraDayPowerPosition
{
    internal interface IIntraDayPowerPosition
    {
        Task OnExecuteAsync(DateTime tenor);
        void StartService();
        void StopService();
    }
}
