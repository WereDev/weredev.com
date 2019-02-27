import { FluxActionTypeKeys } from '../fluxActionKeys';

export default interface IFluxAction {
    readonly type: FluxActionTypeKeys;
}