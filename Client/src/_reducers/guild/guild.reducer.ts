import { GuildActionsType } from "../../_actions";
import { IAction, IHolded } from "../../core";
import { ITariffs, IGuild } from "../../_services";
import clonedeep from 'lodash.clonedeep';

export class IGuildStore {
    guild: IGuild & IHolded;
    tariffs: ITariffs;
    constructor(guild?: IGuild | IHolded & any, tariffs?: ITariffs | IHolded & any) {
        if (guild)
            this.guild = {
                id: guild.id || '',
                name: guild.name || 'loading...',
                status: guild.status || 'Active',
                recruitmentStatus: guild.recruitmentStatus || 'Close',
                holding: guild.holding || false,
                gamersCount: guild.gamersCount || 0,
                charactersCount: guild.charactersCount || 0,
                balance: guild.balance || 0
            };
        else {
            this.guild = {
                id: '',
                name: 'loading...',
                status: 'Active',
                recruitmentStatus: 'Close',
                holding: false,
                gamersCount: 0,
                charactersCount: 0,
                balance: 0
            };
        }
        if (tariffs)
            this.tariffs = {
                Leader: tariffs.Leader,
                Officer: tariffs.Officer,
                Soldier: tariffs.Soldier,
                Veteran: tariffs.Veteran,
                Beginner: tariffs.Beginner
            };
        else {
            this.tariffs = {
                Leader: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }, Officer: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }, Soldier: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }, Veteran: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }, Beginner: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }
            };
        }
    }
}

export function guild(state: IGuildStore = new IGuildStore(), action: IAction<GuildActionsType>):
    IGuildStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        case GuildActionsType.PROC_GET_GUILD:
            return new IGuildStore({ ...clonedState.guild, holding: true }, { ...clonedState.tariffs, holding: true });

        case GuildActionsType.SUCC_GET_GUILD:
            return new IGuildStore(action.guildInfo, action.guildInfo.tariffs);

        case GuildActionsType.FAILED_GET_GUILD:
            return new IGuildStore({ ...clonedState.guild, holding: false }, { ...clonedState.tariffs, holding: false });

        default:
            return state
    }
}