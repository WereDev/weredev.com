import { ISiteNavProps } from 'src/models'; 
import WorldTravelerStore from '../services/flux/stores/worldTravelerStore';
import CountryLink from './worldtraveler/countryLink';

class RoutesHelper {
    public getCountryNavProps(travelerPath : string) : ISiteNavProps[] {
        const countries = WorldTravelerStore.getCountries();
        const siteNavProps : ISiteNavProps[] = [];
        countries.forEach((country) => {
            siteNavProps.push({
                component: CountryLink,
                name: country.name,
                parentPath: travelerPath,
                path: travelerPath + '/' + country.name
            });
        });

        return siteNavProps;
    }

}

export default new RoutesHelper();