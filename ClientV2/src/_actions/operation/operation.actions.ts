import { OperationType, operationService, IOperationView, OperationView } from '../../_services';
import { alertInstance } from '..';
import { OperationActionsType } from './OperationActionsType';
import { IAvailableDocument } from '../../_services/operation/IAvailableDocuments';

interface CreateOperationProps {
    id: string;
    type: OperationType;
    amount: number;
    description: string;
    userId: string;
    documentId?: string;
    parentOperationId?: string;
}

class OperationActions {

    /**
     * Метод возвращает список операций по первичному документу
     * @param props 
     */
    getOperations(date: Date): Function {
        return (dispatch: Function) => {
            dispatch(request(date));
            operationService.getOperations(date)
                .then(
                    data => {
                        dispatch(success(date, data));
                    })
                .catch(
                    ex => {
                        dispatch(failure(date));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(date: Date) { return { type: OperationActionsType.PROC_GET_OPERATIONS, date } }
        function success(date: Date, operations: Array<IOperationView>) { return { type: OperationActionsType.SUCC_GET_OPERATIONS, date, operations } }
        function failure(date: Date) { return { type: OperationActionsType.FAILED_GET_OPERATIONS, date } }
    }

    /**
     * Метод cсоздаёт новую операцию
     * @param props
     */
    createOperation(props: CreateOperationProps): Function {
        return (dispatch: Function) => {
            dispatch(request(props.id, props.type));
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
                        dispatch(success(props.id, props.type, data));
                        dispatch(operationsInstance.getOperations(new Date()));
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.id, props.type));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(id: string, otype: OperationType) { return { type: OperationActionsType.PROC_CREATE_OPERATION, id, otype } }
        function success(id: string, otype: OperationType, operation: IOperationView) { return { type: OperationActionsType.SUCC_CREATE_OPERATION, id, otype, operation } }
        function failure(id: string, otype: OperationType) { return { type: OperationActionsType.FAILED_CREATE_OPERATION, id, otype } }
    }

    /**
     * Метод возвращает список документов доступных для оплаты
     */
    getDocuments(): Function {
        return (dispatch: Function) => {
            dispatch(request());
            operationService.getDocuments()
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
        function request() { return { type: OperationActionsType.PROC_GET_DOCUMENTS } }
        function success(documents: Array<IAvailableDocument>) { return { type: OperationActionsType.SUCC_GET_DOCUMENTS, documents } }
        function failure() { return { type: OperationActionsType.FAILED_GET_DOCUMENTS } }
    }
}

export const operationsInstance = new OperationActions();