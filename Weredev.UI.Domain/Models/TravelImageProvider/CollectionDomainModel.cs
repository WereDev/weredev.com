
using Weredev.UI.Domain.Extensions;

namespace Weredev.UI.Domain.Models.TravelImageProvider
{
    public class CollectionDomainModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string CountryName { get; private set; }
        public string CountryKey { get; private set; }
        public string CityName { get; private set; }
        public string CityKey { get; private set; }
        public Album[] Albums { get; set; }

        private string _Title = null;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                var tempString = value.GetCountryName();
                CountryName = tempString;
                CountryKey = tempString.CreateKey();
                tempString = value.GetCityName();
                CityName = tempString;
                CityKey = tempString.CreateKey();
            }
        }

        public class Album
        {
            public string Id { get; set; }
            public string Description { get; set; }
            public string Key { get; private set; }
            private string _name = null;
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    Key = value?.CreateKey();
                }
            }
        }
    }
}