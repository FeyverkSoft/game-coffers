import { GamerActionsType } from "../../_actions";
import { IAction, IHolded, Dictionary } from "../../core";
import { IGamerInfo, IGamersListView, GamersListView } from "../../_services";
import clonedeep from 'lodash.clonedeep';

export class IGamerStore {
    currentGamer: IGamerInfo & IHolded;
    gamersList: Dictionary<IGamersListView & IHolded>;

    public GetGamer(id: string): IGamersListView {
        if (this.gamersList[id])
            return this.gamersList[id];
        return new GamersListView('', [], 0, [], [], 'Beginner', 'New', new Date().toISOString(), '');
    }

    constructor(currentGamer?: IGamerInfo | IHolded & any, gamersList?: Array<IGamersListView>) {
        if (currentGamer)
            this.currentGamer = {
                userId: currentGamer.userId || '',
                name: currentGamer.name || '',
                rank: currentGamer.rank || 'Soldier',
                balance: currentGamer.balance || 0,
                activeLoanAmount: currentGamer.activeLoanAmount || 0,
                activePenaltyAmount: currentGamer.activePenaltyAmount || 0,
                activeExpLoanAmount: currentGamer.activeExpLoanAmount || 0,
                activeLoanTaxAmount: currentGamer.activeLoanTaxAmount || 0,
                repaymentLoanAmount: currentGamer.repaymentLoanAmount || 0,
                repaymentTaxAmount: currentGamer.repaymentTaxAmount || 0,
                charCount: currentGamer.charCount || 0,
            };
        else {
            this.currentGamer = {
                userId: '',
                name: '',
                rank: 'Soldier',
                balance: 0,
                activeLoanAmount: 0,
                activePenaltyAmount: 0,
                activeExpLoanAmount: 0,
                activeLoanTaxAmount: 0,
                repaymentLoanAmount: 0,
                repaymentTaxAmount: 0,
                charCount: 0,
            };
        }
        if (gamersList)
            this.gamersList = {};
        else
            this.gamersList = {};
    }
}
const TestStat = (_new: IGamerStore, _old: IGamerStore): IGamerStore => {
    if (JSON.stringify(_new) == JSON.stringify(_old))
        return _old;
    return _new;
};

export function gamers(state: IGamerStore = new IGamerStore(), action: IAction<GamerActionsType>):
    IGamerStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        /**
         * Секция получения ионформации о текущем игроке сессии
         */
        case GamerActionsType.PROC_GET_CURRENT_GAMER:
            Object.assign(state.currentGamer, { holding: true });
            return state;

        case GamerActionsType.SUCC_GET_CURRENT_GAMER:
            clonedState.currentGamer = action.currentGamer;
            return clonedState;

        case GamerActionsType.FAILED_GET_CURRENT_GAMER:
            Object.assign(state.currentGamer, { holding: false });
            return state;

        /**
         * Секция получения информации о списке игроков в гильдии
         */
        case GamerActionsType.PROC_GET_GUILD_GAMERS:
            return state;

        case GamerActionsType.SUCC_GET_GUILD_GAMERS:
            action.GamersList.forEach((gamer: IGamersListView) => {
                if (clonedState.gamersList == undefined)
                    clonedState.gamersList = {};
                clonedState.gamersList[gamer.id] = gamer;
            });
            return TestStat(clonedState, state);

        case GamerActionsType.FAILED_GET_GUILD_GAMERS:
            return state;


        /**
         * Секция обработчика установки статуса игроку
         */
        case GamerActionsType.PROC_SET_GAMER_STATUS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_SET_GAMER_STATUS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].status = action.status;
                return clonedState;
            }
        case GamerActionsType.FAILED_SET_GAMER_STATUS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }


        /**
         * Секция обработчика установки ранга игроку
         */
        case GamerActionsType.PROC_SET_GAMER_RANK:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_SET_GAMER_RANK:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].rank = action.rank;
                return clonedState;
            }
        case GamerActionsType.FAILED_SET_GAMER_RANK:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }


        /**
         * Секция обработчика события добавляения игроку нового персонажа
         */
        case GamerActionsType.PROC_ADD_NEW_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_ADD_NEW_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                if (clonedState.gamersList[action.gamerId].characters == undefined)
                    clonedState.gamersList[action.gamerId].characters = [];
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].characters.push({ name: action.name, className: action.className });
                return clonedState;
            }
        case GamerActionsType.FAILED_ADD_NEW_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }


        /**
         * Секция обработчик события удаления персонажа у игрока
         */
        case GamerActionsType.PROC_DELETE_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_DELETE_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                if (clonedState.gamersList[action.gamerId].characters == undefined)
                    clonedState.gamersList[action.gamerId].characters = [];
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].characters = clonedState.gamersList[action.gamerId].characters.filter(c => c.name != action.name);
                return clonedState;
            }
        case GamerActionsType.FAILED_DELETE_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }


        /***
         * Секция обработчика события добавляения нового ЗАЙМА игроку
         */
        case GamerActionsType.PROC_ADD_GAMER_LOAN:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_ADD_GAMER_LOAN:
            if (clonedState.gamersList[action.gamerId]) {
                if (clonedState.gamersList[action.gamerId].loans == undefined)
                    clonedState.gamersList[action.gamerId].loans = {};
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].loans[action.loan.id] = action.loan;
                return clonedState;
            }
        case GamerActionsType.FAILED_ADD_GAMER_LOAN:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }


        /***
        * Секция обработчика события добавляения нового ШТРАФА игроку
        */
        case GamerActionsType.PROC_ADD_GAMER_PENALTY:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_ADD_GAMER_PENALTY:
            if (clonedState.gamersList[action.gamerId]) {
                if (clonedState.gamersList[action.gamerId].penalties == undefined)
                    clonedState.gamersList[action.gamerId].penalties = {};
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].penalties[action.penalty.id] = action.penalty;
                return clonedState;
            }
        case GamerActionsType.FAILED_ADD_GAMER_PENALTY:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }


        /***
         * Секция обработчика события отмена займа у игрока
         */
        case GamerActionsType.PROC_CANCEL_GAMER_LOAN:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_CANCEL_GAMER_LOAN:
            if (clonedState.gamersList[action.gamerId]) {
                if (clonedState.gamersList[action.gamerId].loans == undefined)
                    clonedState.gamersList[action.gamerId].loans = {};
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].loans[action.loanId].loanStatus = 'Canceled';
                return clonedState;
            }
        case GamerActionsType.FAILED_CANCEL_GAMER_LOAN:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }


        /**
        * Секция обработчика события отмена штрафа у игрока
        */
        case GamerActionsType.PROC_CANCEL_GAMER_PENALTY:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_CANCEL_GAMER_PENALTY:
            if (clonedState.gamersList[action.gamerId]) {
                if (clonedState.gamersList[action.gamerId].penalties == undefined)
                    clonedState.gamersList[action.gamerId].penalties = {};
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].penalties[action.penaltyId].penaltyStatus = 'Canceled';
                return clonedState;
            }
        case GamerActionsType.FAILED_CANCEL_GAMER_PENALTY:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }

        default:
            return state
    }
}