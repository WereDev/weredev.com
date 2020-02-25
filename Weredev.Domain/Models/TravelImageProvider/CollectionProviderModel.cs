using Weredev.Domain.Extensions;

namespace Weredev.Domain.Models.TravelImageProvider
{
    public class CollectionProviderModel
    {
        private string _title = null;

        public string Id { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public string CountryName { get; private set; }

        public string CountryKey { get; private set; }

        public string CityName { get; private set; }

        public string CityKey { get; private set; }

        public Album[] Albums { get; set; }

        public string Title
        {
            get
            {
                return this._title;
            }

            set
            {
                _title = value;
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
            private string _name = null;

            public string Id { get; set; }

            public string Description { get; set; }

            public string Key { get; private set; }

            public string Name
            {
                get
                {
                    return _name;
                }

                set
                {
                    _name = value;
                    Key = value?.CreateKey();
                }
            }
        }
    }
}
