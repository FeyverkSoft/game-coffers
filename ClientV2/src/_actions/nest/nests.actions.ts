import { nestService } from '../../_services/nest/nest.service';
import { alertInstance } from '../alert/alert.actions';
import { NestActionType } from './NestActionsType';


class NestActions {
    /**
     * Метод возвращает список логовов
     * @param props 
     */
    getNestList(): Function {
        return (dispatch: Function) => {
            dispatch(NestActionType.PROC_GET_NEST_LIST());
            nestService.getNestList()
                .then(
                    data => {
                        dispatch(NestActionType.SUCC_GET_NEST_LIST(data));
                    })
                .catch(
                    ex => {
                        dispatch(NestActionType.FAILED_GET_NEST_LIST());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

}

export const nestsInstance = new NestActions();