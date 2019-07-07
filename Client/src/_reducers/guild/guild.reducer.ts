import { GuildActionsType } from "../../_actions";
import { IAction, IHolded } from "../../core";
import { ITariffs, IGuild } from "../../_services";


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
                holding: guild.holding || false
            };
        else {
            this.guild = {
                id: '',
                name: 'loading...',
                status: 'Active',
                recruitmentStatus: 'Close',
                holding: false
            };
        }
        if (tariffs)
            this.tariffs = tariffs;
        else {
            this.tariffs = {
                leader: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }, officer: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }, soldier: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }, veteran: {
                    tax: [0],
                    expiredLoanTax: 0,
                    loanTax: 0
                }, beginner: {
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
    switch (action.type) {
        case GuildActionsType.PROC_GET_GUILD:
            return new IGuildStore({ holding: true }, { holding: true });

        case GuildActionsType.SUCC_GET_GUILD:
            return new IGuildStore(action.guildInfo, action.guildInfo.tariffs);

        case GuildActionsType.FAILED_GET_GUILD:
            return new IGuildStore({ holding: false }, { holding: false });

        default:
            return state
    }
}