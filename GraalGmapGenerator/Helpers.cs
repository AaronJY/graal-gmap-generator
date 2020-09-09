using System;

namespace GraalGmapGenerator
{
    public static class Helpers
    {
        public static bool YesNoToBool(string input)
        {
            string inputLowered = input.ToLower();
            if (input == "y" || input == "yes")
                return true;
            if (input == "n" || input == "no")
                return false;
            throw new ArgumentException("Invalid input given.", nameof(input));
        }
    }
}