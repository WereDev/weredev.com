import IFluxAction from '../fluxAction';
import { WorldTravelerActionTypeKeys } from './actionTypeKeys';

export default class LoadCountriesBeginAction implements IFluxAction {
    public type = WorldTravelerActionTypeKeys.LOAD_COUNTRIES_BEGIN;
}