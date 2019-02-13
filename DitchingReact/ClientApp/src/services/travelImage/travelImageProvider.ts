import { ICountry } from 'src/models/worldTraveler';
import ApiProperties from '../apiProperties';
import { IListCountriesResponse } from './models/countriesModels';

export default class TravelImageProvider {
  
  public getCountries() : Promise<ICountry[]> {
    const url = ApiProperties.getBaseUrl() + "worldtraveler/country";
    return fetch(url)
      .then(response => response.json())
      .then(json => {
        const response = (json as IListCountriesResponse);
        return response.countries;
      });
  }
}

