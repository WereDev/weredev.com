using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.Providers.Flickr.Models;
using Weredev.UI.Domain.Extensions;
using Weredev.UI.Domain.Models.Traveler;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.Providers.Flickr.Mappers
{
    public static class CollectionToCountryMapper
    {
        public static CollectionProviderModel ParseTreeResponse(this CollectionsGetTreeResponse.CollectionItem collection)
        {
            var model = new CollectionProviderModel{
                Albums = collection.Set.Select(x => x.ParseToCollectionAlbum()).ToArray(),
                Description = collection.Description,
                Icon = collection.IconLarge,
                Id = collection.Id,
                Title = collection.Title
            };

            if (!model.Icon.StartsWith("http"))
            {
                model.Icon = "https://www.flickr.com/" + model.Icon;
            }

            return model;
        }
        
        private static CollectionProviderModel.Album ParseToCollectionAlbum(this CollectionsGetTreeResponse.Set photoSet)
        {
            return new CollectionProviderModel.Album
            {
                Description = photoSet.Description,
                Id = photoSet.Id,
                Name = photoSet.Title
            };
        }
    }
}