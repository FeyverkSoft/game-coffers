import { GamerActionsType } from "../../_actions";
import { IAction, IHolded } from "../../core";
import { IGamerInfo } from "../../_services";
import clonedeep from 'lodash.clonedeep';

export class IGamerStore {
    currentGamer: IGamerInfo & IHolded;

    constructor(currentGamer?: IGamerInfo | IHolded & any) {
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

        default:
            return state
    }
}