using System;

namespace IntraDayPowerPosition
{
    internal interface IIntraDayPowerPosition
    {
        void OnExecute(DateTime tenor);
        void StartService();
        void StopService();
    }
}
