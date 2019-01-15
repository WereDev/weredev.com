import { EventEmitter } from "events";
import { ICountry } from 'src/models/worldTraveler';
import IFluxAction from '../actions/models/fluxAction';
import { WorldTravelerActionTypeKeys } from '../actions/models/worldTraveler/actionTypeKeys';
import LoadCountriesSuccessResponse from '../actions/models/worldTraveler/loadCountriesSuccessResponse';
import Dispatcher from '../dispatcher';
import { StoreEvents } from './storeEvents';

class WorldTravelerStore extends EventEmitter {
    private countries: ICountry[] = [];

    constructor() {
        super();
    }

    public getCountries () : ICountry[] {
        return this.countries;
    }

    public handleCollectionUpdate(response: IFluxAction)
    {
        switch(response.type)
        {
            case WorldTravelerActionTypeKeys.LOAD_COUNTRIES_SUCCESS:
                this.countries = (response as LoadCountriesSuccessResponse).payload;
                this.emit(StoreEvents.COUNTRIES_CHANGED);
            break;
        }
    }
}

const store = new WorldTravelerStore();
Dispatcher.register(store.handleCollectionUpdate.bind(store));
export default store;