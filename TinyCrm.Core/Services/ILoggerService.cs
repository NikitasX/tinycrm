using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Services
{
    interface ILoggerService
    {
        void LogError(string errorcode, string text);

        void LogInformation(string text);
    }
}
