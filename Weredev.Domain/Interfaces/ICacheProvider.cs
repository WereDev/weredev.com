using System;

namespace Weredev.Domain.Interfaces
{
    public interface ICacheProvider
    {
        void Set<T>(string key, T item);

        void Set<T>(string key, T item, TimeSpan expiration);

        T Get<T>(string key);
    }
}
