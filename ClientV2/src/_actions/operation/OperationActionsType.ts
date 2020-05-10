import { IAvailableDocument } from '../../_services/operation/IAvailableDocuments';
import { IOperationView } from '../../_services';

export namespace OperationActionsType {
    export const PROC_GET_OPERATIONS = (date: Date) => ({
        type: "PROC_GET_OPERATIONS",
        date: date,
    } as const);
    export const SUCC_GET_OPERATIONS = (date: Date, operations: Array<IOperationView>) => ({
        type: "SUCC_GET_OPERATIONS",
        date: date,
        operations: operations,
    } as const);
    export const FAILED_GET_OPERATIONS = (date: Date) => ({
        type: "FAILED_GET_OPERATIONS",
        date: date,
    } as const);


    export const PROC_CREATE_OPERATION = (id: string) => ({
        type: "PROC_CREATE_OPERATION",
        id: id,
    } as const);
    export const SUCC_CREATE_OPERATION = (id: string, operation: IOperationView) => ({
        type: "SUCC_CREATE_OPERATION",
        id: id,
        operation: operation,
    } as const);
    export const FAILED_CREATE_OPERATION = (id: string) => ({
        type: "FAILED_CREATE_OPERATION",
        id: id,
    } as const);


    export const PROC_GET_DOCUMENTS = () => ({
        type: "PROC_GET_DOCUMENTS",
    } as const);
    export const SUCC_GET_DOCUMENTS = (documents: Array<IAvailableDocument>) => ({
        type: "SUCC_GET_DOCUMENTS",
        documents: documents,
    } as const);
    export const FAILED_GET_DOCUMENTS = () => ({
        type: "FAILED_GET_DOCUMENTS",
    } as const)


    export const PROC_EDIT_OPERATION = (id: string) => ({
        type: "PROC_EDIT_OPERATION",
        id: id,
    } as const);
    export const SUCC_EDIT_OPERATION = (id: string, operation: IOperationView) => ({
        type: "SUCC_EDIT_OPERATION",
        id: id,
        operation: operation,
    } as const);
    export const FAILED_EDIT_OPERATION = (id: string) => ({
        type: "FAILED_EDIT_OPERATION",
        id: id,
    } as const)
}

export type OperationActionsTypes =
    ReturnType<typeof OperationActionsType.PROC_GET_OPERATIONS> |
    ReturnType<typeof OperationActionsType.SUCC_GET_OPERATIONS> |
    ReturnType<typeof OperationActionsType.FAILED_GET_OPERATIONS> |

    ReturnType<typeof OperationActionsType.PROC_CREATE_OPERATION> |
    ReturnType<typeof OperationActionsType.SUCC_CREATE_OPERATION> |
    ReturnType<typeof OperationActionsType.FAILED_CREATE_OPERATION> |

    ReturnType<typeof OperationActionsType.PROC_GET_DOCUMENTS> |
    ReturnType<typeof OperationActionsType.SUCC_GET_DOCUMENTS> |
    ReturnType<typeof OperationActionsType.FAILED_GET_DOCUMENTS> |

    ReturnType<typeof OperationActionsType.PROC_EDIT_OPERATION> |
    ReturnType<typeof OperationActionsType.SUCC_EDIT_OPERATION> |
    ReturnType<typeof OperationActionsType.FAILED_EDIT_OPERATION>;