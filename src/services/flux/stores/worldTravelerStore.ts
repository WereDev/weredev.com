import { EventEmitter } from "events";
import { ICountry } from 'src/models/worldTraveler';
import LoadCountriesSuccessResponse from '../actions/models/worldTraveler/loadCountriesSuccessResponse';
import Dispatcher from '../dispatcher';
import { WorldTravelerStoreEvents } from './worldTravelerStoreEvents';

class WorldTravelerStore extends EventEmitter {
    private countries: ICountry[] = [];

    constructor() {
        super();
    }

    public getCountries () : ICountry[] {
        return this.countries;
    }

    public handleCollectionUpdate(response: LoadCountriesSuccessResponse)
    {
        this.countries = response.payload;
        this.emit(WorldTravelerStoreEvents.COUNTRIES_CHANGED);
    }
}

const store = new WorldTravelerStore();
Dispatcher.register(store.handleCollectionUpdate.bind(store));
export default store;