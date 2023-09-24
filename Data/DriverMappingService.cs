namespace Data
{
    public class DriverInfo
    {
        public string DeviceClass { get; set; } = null;          // "Media"
        public List<string> HardwareIds = new List<string>();    // "PCI\VEN_10DE&DEV_1C20&SUBSYS_00241414&REV_A1"
        public string DriverName { get; set; } = null;           // "Realtek Super Audio driver 2.0"
        public string Account { get; set; } = null;              // "Realtek inc."
        public int AccountId { get; set; } = 0;                  // "4252"
    }

    public class MappingRules
    {
        public string OriginalDeviceClass { get; set; } = null;  // "Media" or "System"
        public string DriverNameKeyword { get; set; } = null;    // "Audio" or "LTE"
        public string DriverAccount { get; set; } = null;        // "Realtek" or "Qualcomm"
        public string NewDeviceClass { get; set; } = null;       // "Audio" or "Mobile Broadband"
    }

    public class DriverMappingService
    {
        public string FindDeviceClass(DriverInfo currentDriver, List<MappingRules> rules)
        {
            // First, filter 'rules' list to only those with rules.OriginalDeviceClass == currentDriver.DeviceClass
            var filteredRules = rules.Where(rule => rule.OriginalDeviceClass.ToUpper() == currentDriver.DeviceClass.ToUpper());

            // Second, analyze all remaining rules, capture and log rules.NewDeviceClass every time there's a match:
            //  currentDriver.DriverName contains rules.DriverNameKeyword AND
            //  currentDriver.Account starts with rules.DriverAccount

            // <Your code here>
            var remainingRules = new List<MappingRules>();
            foreach (var rule in filteredRules)
            {
                if (currentDriver.DriverName.ToUpper().Contains(rule.DriverNameKeyword.ToUpper()) && currentDriver.Account.ToUpper().StartsWith(rule.DriverAccount))
                {
                    remainingRules.Add(rule);
                    Console.WriteLine(rule.NewDeviceClass);
                }
            }

            // Finally, return the first matched device class from the set above

            // Your code here>
            return remainingRules.FirstOrDefault()?.ToString() ?? string.Empty;
        }
    }
}
