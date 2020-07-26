import { nestService } from '../../_services/nest/nest.service';
import { alertInstance } from '../alert/alert.actions';
import { NestActionType } from './NestActionsType';

class NestActions {

    /**
    * регистрирует новый контракт в системе
    * @param props 
    */
    addContract(props: { id: string; nestId: string; characterName: string; reward: string; }): Function {
        return (dispatch: Function) => {
            dispatch(NestActionType.PROC_ADD_CONTRACT());
            nestService.addContract(props.id, props.nestId, props.characterName, props.reward)
                .then(
                    data => {
                        dispatch(NestActionType.SUCC_ADD_CONTRACT(data));
                    })
                .catch(
                    ex => {
                        dispatch(NestActionType.FAILED_ADD_CONTRACT());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

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

    /**
     * Метод возвращает список контрактов игрока
     * @param props 
     */
    getMyContracts(): Function {
        return (dispatch: Function) => {
            dispatch(NestActionType.PROC_GET_MY_CONTRACTS());
            nestService.getMyContracts()
                .then(
                    data => {
                        dispatch(NestActionType.SUCC_GET_MY_CONTRACTS(data));
                    })
                .catch(
                    ex => {
                        dispatch(NestActionType.FAILED_GET_MY_CONTRACTS());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Метод завершает активный контракт у игрока
     * @param props 
     */
    deleteContract(props: { id: string; }): Function {
        return (dispatch: Function) => {
            dispatch(NestActionType.PROC_DELETE_CONTRACT(props.id));
            nestService.deleteContract(props.id)
                .then(
                    data => {
                        dispatch(NestActionType.SUCC_DELETE_CONTRACT(props.id));
                    })
                .catch(
                    ex => {
                        dispatch(NestActionType.FAILED_DELETE_CONTRACT(props.id));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }
}

export const nestsInstance = new NestActions();