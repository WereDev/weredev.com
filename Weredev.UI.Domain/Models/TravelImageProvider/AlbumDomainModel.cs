using Weredev.UI.Domain.Extensions;

namespace Weredev.UI.Domain.Models.TravelImageProvider
{
    public class AlbumDomainModel
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