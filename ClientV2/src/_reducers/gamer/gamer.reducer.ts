import { GamerActionsType } from "../../_actions";
import { IAction, IHolded, Dictionary } from "../../core";
import { IGamerInfo } from "../../_services";
import clonedeep from 'lodash.clonedeep';
import { IGamersListView } from "../../_services/gamer/GamersListView";
import { formatDateTime } from "../../_helpers";

export class IGamerStore {
    gamersList: Dictionary<Dictionary<IGamersListView>> & IHolded = {};
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
                if (k != 'holding') {
                    delete clonedState.gamersList[k][action.userId].characters[action.characterId];
                }
            });
            clonedState.gamersList.holding = false;
            return clonedState;

        case GamerActionsType.FAILED_DELETE_CHARS:
            clonedState.gamersList.holding = false;
            return clonedState;

        default:
            return state
    }
}