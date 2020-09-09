using System;
using System.IO;
using System.Linq;

namespace GraalGmapGenerator.Validators
{
    public static class GmapPropertyValidators
    {
        public static bool IsValidDimension(string input)
        {
            return int.TryParse(input, out int width) && width > 0;
        }

        public static bool IsValidYesNoInput(string input)
        {
            var inputLowered = input.ToLower();
            if (inputLowered == "yes" || inputLowered == "y")
                return true;
            if (inputLowered == "no" || inputLowered == "n")
                return true;
            return false;
        }

        public static bool IsValidDirectory(string input)
        {
            char[] inputChars = input.ToCharArray();
            char[] fsInvalidPathChars = Path.GetInvalidPathChars();
            foreach (char inputChar in inputChars)
            {
                if (fsInvalidPathChars.Contains(inputChar))
                {
                    return false;
                }
            }

            return true;
        }
    }
}