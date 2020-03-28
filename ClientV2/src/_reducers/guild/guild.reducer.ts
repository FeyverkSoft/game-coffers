import { GuildActionsType } from "../../_actions";
import { IAction, IHolded } from "../../core";
import { IGuild, GuildBalanceReport, ITariff } from "../../_services";
import clonedeep from 'lodash.clonedeep';

export interface IReports {
    balanceReport: GuildBalanceReport & IHolded;
}
export class IGuildStore {
    guild: IGuild & IHolded;
    tariffs: Array<ITariff> & IHolded = [];
    reports: IReports;

    constructor(guild?: IGuild | IHolded & any) {
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
        this.reports = {
            balanceReport: {
                holding: false,
                balance: 0,
                expectedTaxAmount: 0,
                activeLoansAmount: 0,
                taxAmount: 0,
                repaymentLoansAmount: 0,
                gamersBalance: 0
            }
        };

    }
}

export function guild(state: IGuildStore = new IGuildStore(), action: IAction<GuildActionsType>):
    IGuildStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        case GuildActionsType.PROC_GET_GUILD:
            clonedState.guild.holding = true;
            return clonedState;

        case GuildActionsType.SUCC_GET_GUILD:
            clonedState.guild.holding = false;
            clonedState.guild = { ...clonedState.guild, ...action.guildInfo };
            clonedState.tariffs = action.guildInfo.tariffs;
            return clonedState;

        case GuildActionsType.FAILED_GET_GUILD:
            clonedState.guild.holding = false;
            return clonedState;



        case GuildActionsType.PROC_GET_BALANCE_REPORT:
            clonedState.reports.balanceReport.holding = true;
            return clonedState;

        case GuildActionsType.SUCC_GET_BALANCE_REPORT:
            clonedState.reports.balanceReport = { ...clonedState.reports.balanceReport, holding: false, ...action.BalanceInfo };
            return clonedState;

        case GuildActionsType.FAILED_GET_BALANCE_REPORT:
            clonedState.reports.balanceReport.holding = false;
            return clonedState;



        case GuildActionsType.PROC_GET_TARIFFS:
            clonedState.tariffs.holding = true;
            return clonedState;

        case GuildActionsType.SUCC_GET_TARIFFS:
            clonedState.tariffs = action.tariffs;
            return clonedState;

        case GuildActionsType.FAILED_GET_TARIFFS:
            clonedState.tariffs.holding = false;
            return clonedState;

        default:
            return state
    }
}