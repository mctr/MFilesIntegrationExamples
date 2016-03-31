using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFilesExamples
{
    public class Helpers
    {
        public static string ClearTitle(string name)
        {
            return name
                    .Replace("\\", string.Empty)
                    .Replace("/", string.Empty)
                    .Replace(">", string.Empty)
                    .Replace("|", string.Empty)
                    .Replace("<", string.Empty)
                    .Replace(":", string.Empty)
                    .Replace("*", string.Empty)
                    .Replace("\"", string.Empty);

        }
    }
}
