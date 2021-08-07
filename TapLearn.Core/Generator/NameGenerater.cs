using System;
using System.Collections.Generic;
using System.Text;

namespace TapLearn.Core.Generator
{
     public class NameGenerater
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
