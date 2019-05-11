using System;
using System.Threading.Tasks;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Domain.Interfaces {
    public interface ICacheProvider
    {
        void Set<T>(string key, T item);
        void Set<T>(string key, T item, TimeSpan expiration);
        T Get<T>(string key);
    }
}