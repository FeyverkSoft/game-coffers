import { OperationActionsType } from "../../_actions";
import { IAction, IHolded, IDictionary } from "../../core";
import { IOperationView } from "../../_services";
import clonedeep from 'lodash.clonedeep';

export interface IOperation extends IHolded {
    items: Array<IOperationView>
}

export class IOperationsStore {
    operations: IDictionary<IOperation> = {};
}

export function operations(state: IOperationsStore = new IOperationsStore(), action: IAction<OperationActionsType>):
    IOperationsStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        case OperationActionsType.PROC_GET_OPERATIONS:
            if (clonedState.operations[action.id] == undefined)
                clonedState.operations[action.id] = { items: [] };
            clonedState.operations[action.id].holding = true;
            return clonedState;

        case OperationActionsType.SUCC_GET_OPERATIONS:
            if (clonedState.operations[action.id] == undefined)
                clonedState.operations[action.id] = { items: [] };
            clonedState.operations[action.id].holding = false;
            clonedState.operations[action.id] = { items: action.operations };
            return clonedState;

        case OperationActionsType.FAILED_GET_OPERATIONS:
            if (clonedState.operations[action.id] == undefined)
                clonedState.operations[action.id] = { items: [] };
            clonedState.operations[action.id].holding = false;
            return clonedState;

        default:
            return state
    }
}