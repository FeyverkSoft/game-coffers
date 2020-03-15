import { GamerActionsType } from "../../_actions";
import { IAction, IHolded, Dictionary } from "../../core";
import { IGamerInfo } from "../../_services";
import clonedeep from 'lodash.clonedeep';
import { IGamersListView, GamersListView } from "../../_services/gamer/GamersListView";

export class IGamerStore {
    gamersList: Dictionary<IGamersListView & IHolded>;

    public GetGamer(id: string): IGamersListView {
        if (this.gamersList[id])
            return this.gamersList[id];
        return new GamersListView('', [], 0, [], [], 'Beginner', 'New', new Date().toISOString(), '');
    }

    constructor(gamersList?: Array<IGamersListView>) {
        if (gamersList) {
            this.gamersList = {};
            gamersList.forEach(_ => {
                this.gamersList[_.id] = _;
            });
        }
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
                    clonedState.gamersList[action.gamerId].characters = {};
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].characters[action.id]={ id: action.id, userId: action.userId, name: action.name, className: action.className, isMain: action.isMain };
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
       /* case GamerActionsType.PROC_DELETE_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_DELETE_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                if (clonedState.gamersList[action.gamerId].characters == undefined)
                    clonedState.gamersList[action.gamerId].characters = {};
                clonedState.gamersList[action.gamerId].holding = false;
                clonedState.gamersList[action.gamerId].characters = clonedState.gamersList[action.gamerId].characters.filter(c => c.name != action.name);
                return clonedState;
            }
        case GamerActionsType.FAILED_DELETE_CHARS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = false;
                return clonedState;
            }
*/

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