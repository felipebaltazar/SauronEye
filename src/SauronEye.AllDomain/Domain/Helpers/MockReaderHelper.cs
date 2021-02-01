using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SauronEye.AllDomain.Domain.Helpers
{
    public static class MockReaderHelper
    {
        public static string ReadResource(string resourceName)
        {
            string result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            try
            {

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch { }

            return result;
        }
    }
}
