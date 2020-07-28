import { NestActionTypes } from "../../_actions/nest/NestActionsType";
import { IHolded, IDictionary } from "../../core";
import clonedeep from 'lodash.clonedeep';
import groupBy from 'lodash.groupby';
import { Nest } from "../../_services/nest/Nest";
import { Contract } from "../../_services/nest/Contract";

export class NestsStore {
    nests: Array<Nest> & IHolded = [];
    userContracts: Array<Contract> & IHolded = [];
    guildContracts: IDictionary<IDictionary<Array<Contract>>> & IHolded = {};
}

export function nests(state: NestsStore = new NestsStore(), action: NestActionTypes):
    NestsStore {
    const clonedState = clonedeep(state);
    switch (action.type) {

        /**
        * Секция обработчика события получения списка контрактов
        */
        case 'PROC_GET_MY_CONTRACTS':
            clonedState.userContracts.holding = true;
            return clonedState;
        case 'SUCC_GET_MY_CONTRACTS':
            clonedState.userContracts = action.userContracts;
            clonedState.userContracts.holding = false;
            return clonedState;
        case 'FAILED_GET_MY_CONTRACTS':
            clonedState.userContracts.holding = false;
            return clonedState;

        /**
         * Добавление нового контракта
         */
        case 'PROC_ADD_CONTRACT':
            clonedState.userContracts.holding = true;
            return clonedState;
        case 'SUCC_ADD_CONTRACT':
            clonedState.userContracts.push(action.contract);
            clonedState.userContracts.holding = false;
        case 'FAILED_ADD_CONTRACT':
            clonedState.userContracts.holding = false;
            return clonedState;

        /**
        * Удаление контракта
        */
        case 'PROC_DELETE_CONTRACT':
            clonedState.userContracts.holding = true;
            return clonedState;
        case 'SUCC_DELETE_CONTRACT':
            clonedState.userContracts = clonedState.userContracts.filter(_ => _.id !== action.id);
            clonedState.userContracts.holding = false;
        case 'FAILED_DELETE_CONTRACT':
            clonedState.userContracts.holding = false;
            return clonedState;

        /**
         * Получить список логовов в ги
         */
        case 'PROC_GET_NEST_LIST':
            clonedState.nests.holding = true;
            return clonedState;
        case 'SUCC_GET_NEST_LIST':
            clonedState.nests = action.nests;
            clonedState.nests.holding = false;
            return clonedState;
        case 'FAILED_GET_NEST_LIST':
            clonedState.nests.holding = false;
            return clonedState;

        /**
         * Список контрактов для гильдии
         */
        case 'PROC_GET_GUILD_CONTRACTS':
            clonedState.guildContracts.holding = true;
            return clonedState;
        case 'SUCC_GET_GUILD_CONTRACTS':
            let result: IDictionary<IDictionary<Array<Contract>>> = {};
            Object.keys(action.guildContracts).forEach((nest: string) => {
                let contracts = action.guildContracts[nest];
                let grouped = groupBy(contracts, _ => _.reward)
                result[nest] = grouped;
            })
            clonedState.guildContracts = result;
            clonedState.guildContracts.holding = false;
            return clonedState;
        case 'FAILED_GET_GUILD_CONTRACTS':
            clonedState.guildContracts.holding = false;
            return clonedState;


        default:
            return state
    }
}