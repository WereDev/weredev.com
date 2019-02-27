import TravelImageProvider from '../../travelImage/travelImageProvider'
import Dispatcher from '../dispatcher';
import LoadCountriesBeginAction from './models/worldTraveler/loadCountriesBeginAction';
import LoadCountriesFailResponse from './models/worldTraveler/loadCountriesFailResponse';
import LoadCountriesSuccessResponse from './models/worldTraveler/loadCountriesSuccessResponse';

// TODO: figure out how to DI (or factory?) the flickr provider
class WorldTravelerActions {
    public loadCountries() {
        Dispatcher.dispatch(new LoadCountriesBeginAction);
        const tip = new TravelImageProvider();
        tip.getCountries().then((countries) => {
            Dispatcher.dispatch(new LoadCountriesSuccessResponse(countries));
        }).catch((err) => {
            Dispatcher.dispatch(new LoadCountriesFailResponse)
        })
    }

}

export default new WorldTravelerActions();