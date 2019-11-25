import { OperationActionsType } from "../../_actions";
import { IAction, IHolded, IDictionary } from "../../core";
import { IOperationView } from "../../_services";
import clonedeep from 'lodash.clonedeep';

export interface IOperation extends IHolded {
    items: Array<IOperationView>
}

export class IOperationsStore {
    operations: IDictionary<IOperation> = {};
    gamers: IDictionary<IOperation> = {};
    guilds: IDictionary<IOperation> = {};

    GetGuildOperations(guildId: string): IOperation {
        if (this.guilds[guildId])
            return this.guilds[guildId];
        return { items: [] };
    }
    GetOperations(opId: string): IOperation {
        if (this.operations[opId])
            return this.operations[opId];
        return { items: [] };
    }
    GetGamerOperations(gamerId: string): IOperation {
        if (this.gamers[gamerId])
        return this.gamers[gamerId];
    return { items: [] };
    }
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



        case OperationActionsType.PROC_GET_OPERATIONS_BY_USER:
            if (clonedState.gamers[action.id] == undefined)
                clonedState.gamers[action.id] = { items: [] };
            clonedState.gamers[action.id].holding = true;
            return clonedState;

        case OperationActionsType.SUCC_GET_OPERATIONS_BY_USER:
            if (clonedState.gamers[action.id] == undefined)
                clonedState.gamers[action.id] = { items: [] };
            clonedState.gamers[action.id].holding = false;
            clonedState.gamers[action.id] = { items: action.operations };
            return clonedState;

        case OperationActionsType.FAILED_GET_OPERATIONS_BY_USER:
            if (clonedState.gamers[action.id] == undefined)
                clonedState.gamers[action.id] = { items: [] };
            clonedState.gamers[action.id].holding = false;
            return clonedState;

        case OperationActionsType.PROC_GET_OPERATIONS_BY_GUILD:
            if (clonedState.guilds[action.id] == undefined)
                clonedState.guilds[action.id] = { items: [] };
            clonedState.guilds[action.id].holding = true;
            return clonedState;

        case OperationActionsType.SUCC_GET_OPERATIONS_BY_GUILD:
            if (clonedState.guilds[action.id] == undefined)
                clonedState.guilds[action.id] = { items: [] };
            clonedState.guilds[action.id].holding = false;
            clonedState.guilds[action.id] = { items: action.operations };
            return clonedState;

        case OperationActionsType.FAILED_GET_OPERATIONS_BY_GUILD:
            if (clonedState.guilds[action.id] == undefined)
                clonedState.guilds[action.id] = { items: [] };
            clonedState.guilds[action.id].holding = false;
            return clonedState;
        default:
            return state
    }
}