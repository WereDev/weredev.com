import { ICountry } from 'src/models/worldTraveler';
import IFluxAction from '../fluxAction';
import { WorldTravelerActionTypeKeys } from './actionTypeKeys';

export default class LoadCountriesSuccessResponse implements IFluxAction {
    public readonly type = WorldTravelerActionTypeKeys.LOAD_COUNTRIES_SUCCESS;
    public readonly payload : ICountry[] = [];

    constructor(countries: ICountry[]){
        this.payload = countries == null ? [] : countries;
    }
}