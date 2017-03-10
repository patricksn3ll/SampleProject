using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleProject.Infrastructure
{
    internal static class Constants
    {
        internal const string PasswordSalt = "#Th1s1s4R34llyL0ngS4lt!";
        internal const string HashAlgorithm = "SHA512";
    }
}