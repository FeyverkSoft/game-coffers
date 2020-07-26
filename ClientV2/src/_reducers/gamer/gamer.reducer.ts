import { GamerActionsTypes } from "../../_actions/gamer/GamerActionsType";
import { IHolded, Dictionary } from "../../core";
import clonedeep from 'lodash.clonedeep';
import { IGamersListView, ILoanView } from "../../_services";
import { formatDateTime } from "../../_helpers";

export class IGamerStore {
    gamersList: Dictionary<Dictionary<IGamersListView>> & IHolded = {};
    loans: Dictionary<ILoanView & IHolded> = {};
}

export function gamers(state: IGamerStore = new IGamerStore(), action: GamerActionsTypes):
    IGamerStore {
    const clonedState = clonedeep(state);
    switch (action.type) {
        /**
         * Секция получения информации о списке игроков в гильдии
         */
        case 'PROC_GET_GUILD_GAMERS':
            clonedState.gamersList[formatDateTime(action.date, 'm')] = { ...clonedState.gamersList[formatDateTime(action.date, 'm')] };
            clonedState.gamersList.holding = true;
            return clonedState;

        case 'SUCC_GET_GUILD_GAMERS':
            action.gamersList.forEach((gamer: IGamersListView) => {
                clonedState.gamersList[formatDateTime(action.date, 'm')][gamer.id] = gamer;
                clonedState.loans = { ...clonedState.loans, ...gamer.loans }
            });
            clonedState.gamersList.holding = false;
            return clonedState;

        case 'FAILED_GET_GUILD_GAMERS':
            clonedState.gamersList.holding = false;
            return clonedState;

        /**
        * Секция удаления перса у игрока
        */
        case 'PROC_DELETE_CHARS':
            clonedState.gamersList.holding = true;
            return clonedState;

        case 'SUCC_DELETE_CHARS':
            Object.keys(clonedState.gamersList).forEach(k => {
                if (k === 'holding')
                    return;
                delete clonedState.gamersList[k][action.userId].characters[action.characterId];
            });
            clonedState.gamersList.holding = false;
            return clonedState;

        case 'FAILED_DELETE_CHARS':
            clonedState.gamersList.holding = false;
            return clonedState;


        /**
        * Секция добавления перса игроку
        */
        case 'PROC_ADD_NEW_CHARS':
            clonedState.gamersList.holding = true;
            return clonedState;

        case 'SUCC_ADD_NEW_CHARS':
            Object.keys(clonedState.gamersList).forEach(k => {
                if (k === 'holding')
                    return;
                if (action.isMain)
                    Object.keys(clonedState.gamersList[k][action.userId].characters).forEach(c => {
                        clonedState.gamersList[k][action.userId].characters[c].isMain = false;
                    });

                clonedState.gamersList[k][action.userId].characters[action.characterId] = {
                    id: action.characterId,
                    name: action.name,
                    className: action.className,
                    isMain: action.isMain,
                    userId: action.userId
                };
            });
            clonedState.gamersList.holding = false;
            return clonedState;

        case 'FAILED_ADD_NEW_CHARS':
            clonedState.gamersList.holding = false;
            return clonedState;


        case 'PROC_ADD_GAMER_PENALTY':
            clonedState.gamersList.holding = true;
            return clonedState;
        case 'SUCC_ADD_GAMER_PENALTY':
            clonedState.gamersList.holding = false;
            clonedState.gamersList[formatDateTime(new Date(), 'm')][action.userId].penalties[action.penalty.id] = action.penalty;
            return clonedState;
        case 'FAILED_ADD_GAMER_PENALTY':
            clonedState.gamersList.holding = false;
            return clonedState;


        case 'PROC_ADD_GAMER_LOAN':
            clonedState.gamersList.holding = true;
            return clonedState;
        case 'SUCC_ADD_GAMER_LOAN':
            clonedState.gamersList[formatDateTime(new Date(), 'm')][action.userId].loans[action.loan.id] = action.loan;
            clonedState.loans[action.loan.id] = action.loan;
            clonedState.gamersList.holding = false;
            return clonedState;
        case 'FAILED_ADD_GAMER_LOAN':
            clonedState.gamersList.holding = false;
            return clonedState;

        case 'PROC_CANCEL_GAMER_LOAN':
            clonedState.loans[action.loanId].holding = true;
            return clonedState;
        case 'SUCC_CANCEL_GAMER_LOAN':
            clonedState.loans[action.loanId].loanStatus = 'Canceled';
            clonedState.loans[action.loanId].holding = false;
            return clonedState;
        case 'FAILED_CANCEL_GAMER_LOAN':
            clonedState.loans[action.loanId].holding = false;
            return clonedState;


        case 'PROC_SET_GAMER_RANK':
            clonedState.gamersList.holding = true;
            return clonedState;
        case 'SUCC_SET_GAMER_RANK':
            clonedState.gamersList[formatDateTime(new Date(), 'm')][action.userId].rank = action.rank;
            clonedState.gamersList.holding = false;
            return clonedState;
        case 'FAILED_SET_GAMER_RANK':
            clonedState.gamersList.holding = false;
            return clonedState;

        case 'PROC_SET_GAMER_STATUS':
            clonedState.gamersList.holding = true;
            return clonedState;
        case 'SUCC_SET_GAMER_STATUS':
            clonedState.gamersList[formatDateTime(new Date(), 'm')][action.userId].status = action.status;
            clonedState.gamersList.holding = false;
            return clonedState;
        case 'FAILED_SET_GAMER_STATUS':
            clonedState.gamersList.holding = false;
            return clonedState;

        default:
            return state
    }
}