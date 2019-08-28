using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.Providers.Flickr.Models;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.Providers.Flickr.Mappers
{
    public static class PhotosetMapper
    {
        public static PhotosetProviderModel[] ToProviderModel(this PhotosetsGetListResponse response)
        {
            return response.Photosets.PhotoSet.Select(x =>
                new PhotosetProviderModel
                {
                    Description = x.Description?.Text,
                    IconHeight = x.Primary_Photo_Extras?.SmallHeight,
                    IconUrl = x.Primary_Photo_Extras?.SmallUrl,
                    IconWidth = x.Primary_Photo_Extras?.SmallWidth,
                    Id = x.Id,
                    Name = x.Title?.Text,
                }).ToArray();
        }

        public static PhotoListProviderModel ToProviderModel(this PhotosetsGetPhotosResponse response)
        {
            var photoset = response.Set;
            return new PhotoListProviderModel()
            {
                Id = photoset.Id,
                Page = TryGetInt(photoset.Page) ?? 0,
                Pages = TryGetInt(photoset.TotalPages) ?? 0,
                PerPage = TryGetInt(photoset.PerPage) ?? 0,
                Photos = photoset.Photos?.Select(photo => photo.ToProviderModel()).ToArray(),
                Primary = photoset.PrimaryName,
                Total = TryGetInt(photoset.TotalPhotos) ?? 0,
            };
        }

        public static PhotoListProviderModel.Photo ToProviderModel(this PhotosetsGetPhotosResponse.PhotoSet.Photo response)
        {
            var photo = new PhotoListProviderModel.Photo()
            {
                DateTaken = TryGetDateTime(response.DateTaken),
                Id = response.Id,
                Name = response.Title,
                Secret = response.Secret,
            };

            var scales = new List<PhotoListProviderModel.Photo.PhotoScale>();
            if (!string.IsNullOrEmpty(response.MediumUrl))
                scales.Add(response.GetPhotoScale(PhotoListProviderModel.Photo.PhotoScale.ScaleType.Medium));
            if (!string.IsNullOrEmpty(response.OriginalUrl))
                scales.Add(response.GetPhotoScale(PhotoListProviderModel.Photo.PhotoScale.ScaleType.Original));
            if (!string.IsNullOrEmpty(response.SmallUrl))
                scales.Add(response.GetPhotoScale(PhotoListProviderModel.Photo.PhotoScale.ScaleType.Small));
            if (!string.IsNullOrEmpty(response.ThumbnailUrl))
                scales.Add(response.GetPhotoScale(PhotoListProviderModel.Photo.PhotoScale.ScaleType.Thumbnail));
            photo.Scales = scales.ToArray();

            return photo;
        }

        public static PhotoInfoProviderModel ToProviderModel(this PhotosetsGetInfoResponse response)
        {
            var info = response.Photo;

            var model = new PhotoInfoProviderModel()
            {
                Id = info.Id,
                Title = info.Title.Content,
            };

            if (DateTime.TryParse(info.Dates.Taken, out var dateTaken))
                model.DateTaken = dateTaken;

            if (int.TryParse(info.Rotation, out var rotation))
                model.Rotation = rotation;

            model.Tags = info.Tags?.Tags?.Select(x => x.Raw)?.ToArray();

            return model;
        }

        public static int? TryGetInt(string value)
        {
            return int.TryParse(value, out int intValue)
                    ? intValue
                    : default(int?);
        }

        public static bool? TryGetBool(string value)
        {
            return bool.TryParse(value, out bool boolValue)
                ? boolValue
                : default(bool?);
        }

        public static DateTime? TryGetDateTime(string value)
        {
            return DateTime.TryParse(value, out var dateTimeValue)
                ? dateTimeValue
                : default(DateTime?);
        }

        private static PhotoListProviderModel.Photo.PhotoScale GetPhotoScale(
            this PhotosetsGetPhotosResponse.PhotoSet.Photo response,
            PhotoListProviderModel.Photo.PhotoScale.ScaleType scaleType)
        {
            var prefix = scaleType.ToString();
            return new PhotoListProviderModel.Photo.PhotoScale()
            {
                Height = TryGetInt(GetScaleValue(response, prefix + "Height")) ?? 0,
                Scale = scaleType,
                Url = GetScaleValue(response, prefix + "Url"),
                Width = TryGetInt(GetScaleValue(response, prefix + "Width")) ?? 0,
            };
        }

        private static string GetScaleValue(this PhotosetsGetPhotosResponse.PhotoSet.Photo response, string propertyName)
        {
            return response.GetType().GetProperty(propertyName).GetValue(response, null)?.ToString();
        }
    }
}
