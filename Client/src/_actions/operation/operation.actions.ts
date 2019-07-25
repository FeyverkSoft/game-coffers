
import { OperationType, operationService, IOperationView } from '../../_services';
import { alertInstance, ICallback } from '..';
import { OperationActionsType } from './OperationActionsType';

interface GetOperationsProps extends ICallback<any> {
    documentId: string;
    type: OperationType;
}


class OperationActions {

    /**
     * Метод возвращает список операций по первичному документу
     * @param props 
     */
    GetOperations(props: GetOperationsProps): Function {
        return (dispatch: Function) => {
            dispatch(request(props.documentId));
            operationService.GetOperations(props.documentId, props.type)
                .then(
                    data => {
                        dispatch(success(props.documentId, data));
                        if (props.onSuccess)
                            props.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.documentId));
                        dispatch(alertInstance.error(ex));
                        if (props.onFailure)
                            props.onFailure(ex);
                    });
        }
        function request(id: string) { return { type: OperationActionsType.PROC_GET_OPERATIONS, id } }
        function success(id: string, operations: Array<IOperationView>) { return { type: OperationActionsType.SUCC_GET_OPERATIONS, id, operations } }
        function failure(id: string) { return { type: OperationActionsType.FAILED_GET_OPERATIONS, id } }
    }
}

export const operationsInstance = new OperationActions;