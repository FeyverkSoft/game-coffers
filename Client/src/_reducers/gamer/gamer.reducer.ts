import { GamerActionsType } from "../../_actions";
import { IAction, IHolded, Dictionary } from "../../core";
import { IGamerInfo, IGamersListView } from "../../_services";
import clonedeep from 'lodash.clonedeep';

export class IGamerStore {
    currentGamer: IGamerInfo & IHolded;
    gamersList: Dictionary<IGamersListView & IHolded>;

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
                charCount: 0,
            };
        }
        if (gamersList)
            this.gamersList = {};
        else
            this.gamersList = {};
    }
}

export function gamers(state: IGamerStore = new IGamerStore(), action: IAction<GamerActionsType>):
    IGamerStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        case GamerActionsType.PROC_GET_CURRENT_GAMER:
            return new IGamerStore({ ...clonedState.currentGamer, holding: true });

        case GamerActionsType.SUCC_GET_CURRENT_GAMER:
            return new IGamerStore(action.currentGamer);

        case GamerActionsType.FAILED_GET_CURRENT_GAMER:
            return new IGamerStore({ ...clonedState.currentGamer, holding: false });


        case GamerActionsType.PROC_GET_GUILD_GAMERS:
            return state;

        case GamerActionsType.SUCC_GET_GUILD_GAMERS:
            action.GamersList.array.forEach((gamer: IGamersListView) => {
                clonedState.gamersList[gamer.id] = gamer;
            });;
            return clonedState;

        case GamerActionsType.FAILED_GET_GUILD_GAMERS:
            return state;


        case GamerActionsType.PROC_SET_GAMER_STATUS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_SET_GAMER_STATUS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].rank = action.status;
                return clonedState;
            }
        case GamerActionsType.FAILED_SET_GAMER_STATUS:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }



        case GamerActionsType.PROC_SET_GAMER_RANK:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        case GamerActionsType.SUCC_SET_GAMER_RANK:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].rank = action.rank;
                return clonedState;
            }
        case GamerActionsType.FAILED_SET_GAMER_RANK:
            if (clonedState.gamersList[action.gamerId]) {
                clonedState.gamersList[action.gamerId].holding = true;
                return clonedState;
            }
        default:
            return state
    }
}