using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.Providers.Flickr.Models;
using Weredev.UI.Domain.Extensions;
using Weredev.UI.Domain.Models.Traveler;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.Providers.Flickr.Mappers
{
    public static class CollectionToCountry
    {
        public static CollectionDomainModel ParseTreeResponse(this CollectionsGetTreeResponse.CollectionItem collection)
        {
            var model = new CollectionDomainModel{
                Albums = collection.Set.Select(x => x.ParseToCollectionAlbum()).ToArray(),
                Description = collection.Description,
                Icon = collection.IconLarge,
                Id = collection.Id,
                Title = collection.Title

            };
            return model;
        }

        public static AlbumDomainModel[] ToAlbumArray(this PhotoSetsGetListResponse.SetList setList)
        {
            return setList.PhotoSet.Select(x => ToAlbumModel(x)).ToArray();
        }

        public static AlbumDomainModel ToAlbumModel(this PhotoSetsGetListResponse.SetListItem flickrSet)
        {
            return new AlbumDomainModel
            {
                Description = flickrSet.Description._Content,
                Id = flickrSet.Id,
                Name = flickrSet.Title._Content
            };
        }

        private static CollectionDomainModel.Album ParseToCollectionAlbum(this CollectionsGetTreeResponse.Set photoSet)
        {
            return new CollectionDomainModel.Album
            {
                Description = photoSet.Description,
                Id = photoSet.Id,
                Name = photoSet.Title
            };
        }
    }
}