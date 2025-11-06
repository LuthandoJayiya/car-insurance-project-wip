using log4net;
using System.Runtime.CompilerServices;

namespace LLJ_CarInsuranceMS_RESTAPI.Services
{
    public class LocalLogHelper
    {
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}

