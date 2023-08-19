using System;

namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {
        public static string ClassCode() 
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10 < 32 ? 10 : 32);
        }
    }
}
