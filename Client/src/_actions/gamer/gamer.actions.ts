
import { gamerService, GamerInfo } from '../../_services';
import { GamerActionsType } from './GamerActionsType';
import { alertInstance } from '..';

export class GamerActions {
    /**
     * Возвращает информацию о текущем игроке
     */
    GetCurrentGamer(): Function {
        return (dispatch: Function) => {
            dispatch(request());
            gamerService.getCurrentGamer()
                .then(
                    data => {
                        dispatch(success(data));
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request() { return { type: GamerActionsType.PROC_GET_CURRENT_GAMER } }
        function success(currentGamer: GamerInfo) { return { type: GamerActionsType.SUCC_GET_CURRENT_GAMER, currentGamer } }
        function failure() { return { type: GamerActionsType.FAILED_GET_CURRENT_GAMER } }
    }
}

export const gamerInstance = new GamerActions();