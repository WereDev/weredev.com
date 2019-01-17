import LoadCountriesBeginAction from './models/worldTraveler/loadCountriesBeginAction';
import LoadCountriesFailResponse from './models/worldTraveler/loadCountriesFailResponse';
import LoadCountriesSuccessResponse from './models/worldTraveler/loadCountriesFailResponse';

export type FluxActionTypes =
    | LoadCountriesBeginAction
    | LoadCountriesFailResponse
    | LoadCountriesSuccessResponse
    | null;