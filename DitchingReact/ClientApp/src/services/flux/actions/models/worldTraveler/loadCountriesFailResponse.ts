import IFluxAction from '../fluxAction';
import { WorldTravelerActionTypeKeys } from './actionTypeKeys';

export default class LoadCountriesFailResponse implements IFluxAction {
    public type = WorldTravelerActionTypeKeys.LOAD_COUNTRIES_FAIL;
}