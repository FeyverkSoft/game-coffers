import { GamerActionsType } from "../../_actions";
import { IAction, IHolded, Dictionary } from "../../core";
import clonedeep from 'lodash.clonedeep';
import { IGamersListView, ILoanView } from "../../_services/gamer/GamersListView";
import { formatDateTime } from "../../_helpers";

export class IGamerStore {
    gamersList: Dictionary<Dictionary<IGamersListView>> & IHolded = {};
    loans: Dictionary<ILoanView & IHolded> = {};
}

export function gamers(state: IGamerStore = new IGamerStore(), action: IAction<GamerActionsType>):
    IGamerStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        /**
         * Секция получения информации о списке игроков в гильдии
         */
        case GamerActionsType.PROC_GET_GUILD_GAMERS:
            clonedState.gamersList[formatDateTime(action.date, 'm')] = { ...clonedState.gamersList[action.date] };
            clonedState.gamersList.holding = true;
            return clonedState;

        case GamerActionsType.SUCC_GET_GUILD_GAMERS:
            action.gamersList.forEach((gamer: IGamersListView) => {
                clonedState.gamersList[formatDateTime(action.date, 'm')][gamer.id] = gamer;
                clonedState.loans = { ...clonedState.loans, ...gamer.loans }
            });
            clonedState.gamersList.holding = false;
            return clonedState;

        case GamerActionsType.FAILED_GET_GUILD_GAMERS:
            clonedState.gamersList.holding = false;
            return clonedState;

        /**
        * Секция удаления перса у игрока
        */
        case GamerActionsType.PROC_DELETE_CHARS:
            clonedState.gamersList.holding = true;
            return clonedState;

        case GamerActionsType.SUCC_DELETE_CHARS:
            Object.keys(clonedState.gamersList).forEach(k => {
                if (k === 'holding')
                    return;
                delete clonedState.gamersList[k][action.userId].characters[action.characterId];
            });
            clonedState.gamersList.holding = false;
            return clonedState;

        case GamerActionsType.FAILED_DELETE_CHARS:
            clonedState.gamersList.holding = false;
            return clonedState;


        /**
        * Секция добавления перса игроку
        */
        case GamerActionsType.PROC_ADD_NEW_CHARS:
            clonedState.gamersList.holding = true;
            return clonedState;

        case GamerActionsType.SUCC_ADD_NEW_CHARS:
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

        case GamerActionsType.FAILED_ADD_NEW_CHARS:
            clonedState.gamersList.holding = false;
            return clonedState;


        case GamerActionsType.PROC_ADD_GAMER_PENALTY:
            clonedState.gamersList.holding = true;
            return clonedState;
        case GamerActionsType.SUCC_ADD_GAMER_PENALTY:
            clonedState.gamersList.holding = false;
            clonedState.gamersList[formatDateTime(new Date(), 'm')][action.userId].penalties[action.penalty.id] = action.penalty;
            return clonedState;
        case GamerActionsType.FAILED_ADD_GAMER_PENALTY:
            clonedState.gamersList.holding = false;
            return clonedState;


        case GamerActionsType.PROC_ADD_GAMER_LOAN:
            clonedState.gamersList.holding = true;
            return clonedState;
        case GamerActionsType.SUCC_ADD_GAMER_LOAN:
            clonedState.gamersList[formatDateTime(new Date(), 'm')][action.userId].loans[action.loan.id] = action.loan;
            clonedState.loans[action.loan.id] = action.loan;
            clonedState.gamersList.holding = false;
            return clonedState;
        case GamerActionsType.FAILED_ADD_GAMER_LOAN:
            clonedState.gamersList.holding = false;
            return clonedState;

        case GamerActionsType.PROC_CANCEL_GAMER_LOAN:
            clonedState.loans[action.loanId].holding = true;
            return clonedState;
        case GamerActionsType.SUCC_CANCEL_GAMER_LOAN:
            clonedState.loans[action.loanId].loanStatus = 'Canceled';
            clonedState.loans[action.loanId].holding = false;
            return clonedState;
        case GamerActionsType.FAILED_CANCEL_GAMER_LOAN:
            clonedState.loans[action.loanId].holding = false;
            return clonedState;


        case GamerActionsType.PROC_SET_GAMER_RANK:
            clonedState.gamersList.holding = true;
            return clonedState;
        case GamerActionsType.SUCC_SET_GAMER_RANK:
            clonedState.gamersList[formatDateTime(new Date(), 'm')][action.userId].rank = action.rank;
            clonedState.gamersList.holding = false;
            return clonedState;
        case GamerActionsType.FAILED_SET_GAMER_RANK:
            clonedState.gamersList.holding = false;
            return clonedState;

        case GamerActionsType.PROC_SET_GAMER_STATUS:
            clonedState.gamersList.holding = true;
            return clonedState;
        case GamerActionsType.SUCC_SET_GAMER_STATUS:
            clonedState.gamersList[formatDateTime(new Date(), 'm')][action.userId].status = action.status;
            clonedState.gamersList.holding = false;
            return clonedState;
        case GamerActionsType.FAILED_SET_GAMER_STATUS:
            clonedState.gamersList.holding = false;
            return clonedState;

        default:
            return state
    }
}