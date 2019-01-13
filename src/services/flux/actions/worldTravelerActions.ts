import { ICountry } from 'src/models/worldTraveler';
import FlickrProvider from '../../flickr/flickrProvider'
import Dispatcher from '../dispatcher';
import LoadCountriesBeginAction from './models/worldTraveler/loadCountriesBeginAction';
import LoadCountriesFailResponse from './models/worldTraveler/loadCountriesFailResponse';
import LoadCountriesSuccessResponse from './models/worldTraveler/loadCountriesSuccessResponse';

// TODO: figure out how to DI (or factory?) the flickr provider
class WorldTravelerActions {
    public loadCountries() {
        Dispatcher.dispatch(new LoadCountriesBeginAction);

        const flickr = new FlickrProvider();
        flickr.getCollections().then((collections) => {
            const countries: ICountry[] = [];
            collections.forEach((collection, index, array) => {
                const parts = collection.title.split(',');
                const countryName = parts[parts.length - 1].trim();
                const countryKey = this.createKey(countryName);
                let country = countries.find((x) => x.name === countryName);

                if (country === null || country === undefined) {
                    country = {
                        cities: [],
                        key: countryKey,
                        name: countryName
                    };
                    countries.push(country);
                };
                let cityName = parts[0].trim();
                if (parts.length > 2) {
                    cityName = parts.join(',').trim();
                    parts.pop();                    
                }
                country.cities.push({
                    description: collection.description,
                    iconUrl: collection.iconlarge,
                    id: collection.id,
                    name: cityName
                });
            });

            Dispatcher.dispatch(new LoadCountriesSuccessResponse(countries));

        }).catch((err) => {
            Dispatcher.dispatch(new LoadCountriesFailResponse)
        })
    }

    private createKey(name: string):string {
        return name.toLowerCase().replace(' ', '_');
    }
}

export default new WorldTravelerActions();