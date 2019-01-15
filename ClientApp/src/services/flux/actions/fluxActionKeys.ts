import { WorldTravelerActionTypeKeys } from './models/worldTraveler/actionTypeKeys';

export type FluxActionTypeKeys =
    | WorldTravelerActionTypeKeys.LOAD_COUNTRIES_BEGIN
    | WorldTravelerActionTypeKeys.LOAD_COUNTRIES_FAIL
    | WorldTravelerActionTypeKeys.LOAD_COUNTRIES_SUCCESS;