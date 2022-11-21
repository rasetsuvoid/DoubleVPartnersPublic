using Application.Common.Interfaces;

namespace Web.Utils.Settings
{
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }

        public int HoursAllowed { get; set; }
    }
}
