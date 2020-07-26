import { OperationType, operationService } from '../../_services';
import { alertInstance } from '../alert/alert.actions';
import { OperationActionsType } from './OperationActionsType';

interface CreateOperationProps {
    id: string;
    type: OperationType;
    amount: number;
    description: string;
    userId: string;
    documentId?: string;
    parentOperationId?: string;
}
interface IEditOperationProps {
    id: string;
    type: OperationType;
    documentId: string;
}

class OperationActions {
    /**
     * Метод возвращает список операций по первичному документу
     * @param props 
     */
    getOperations(date: Date): Function {
        return (dispatch: Function) => {
            dispatch(OperationActionsType.PROC_GET_OPERATIONS(date));
            operationService.getOperations(date)
                .then(
                    data => {
                        dispatch(OperationActionsType.SUCC_GET_OPERATIONS(date, data));
                    })
                .catch(
                    ex => {
                        dispatch(OperationActionsType.FAILED_GET_OPERATIONS(date));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Метод cсоздаёт новую операцию
     * @param props
     */
    createOperation(props: CreateOperationProps): Function {
        return (dispatch: Function) => {
            dispatch(OperationActionsType.PROC_CREATE_OPERATION(props.id));
            operationService.createOperation(
                props.id,
                props.type,
                props.amount,
                props.description,
                props.userId,
                props.documentId,
                props.parentOperationId,
            )
                .then(
                    data => {
                        dispatch(OperationActionsType.SUCC_CREATE_OPERATION(props.id, data));
                    })
                .catch(
                    ex => {
                        dispatch(OperationActionsType.FAILED_CREATE_OPERATION(props.id));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    editOperation(props: IEditOperationProps): Function {
        return (dispatch: Function) => {
            dispatch(OperationActionsType.PROC_EDIT_OPERATION(props.id));
            operationService.editOperation(
                props.id,
                props.type,
                props.documentId,
            )
                .then(
                    data => {
                        dispatch(OperationActionsType.SUCC_EDIT_OPERATION(props.id, data));
                    })
                .catch(
                    ex => {
                        dispatch(OperationActionsType.FAILED_EDIT_OPERATION(props.id));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Метод возвращает список документов доступных для оплаты
     */
    getDocuments(): Function {
        return (dispatch: Function) => {
            dispatch(OperationActionsType.PROC_GET_DOCUMENTS());
            operationService.getDocuments()
                .then(
                    data => {
                        dispatch(OperationActionsType.SUCC_GET_DOCUMENTS(data));
                    })
                .catch(
                    ex => {
                        dispatch(OperationActionsType.FAILED_GET_DOCUMENTS());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }
}

export const operationsInstance = new OperationActions();