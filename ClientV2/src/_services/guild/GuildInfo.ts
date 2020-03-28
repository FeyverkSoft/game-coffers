import { IDictionary } from "../../core";

export type RecruitmentStatus = 'Open' | 'Close' | 'Internal';
export type GuildStatus = 'Active' | 'InActive';

export interface ITariff {
    guildId: string;
    userRole: string;
    loanTax: number;
    expiredLoanTax: number;
    tax: number[];
}

export class Tariff implements ITariff {
    guildId: string;
    userRole: string;
    loanTax: number;
    expiredLoanTax: number;
    tax: Array<number>;

    constructor(guildId: string, userRole: string, loanTax: number, expiredLoanTax: number, tax: Array<number>) {
        this.guildId = String(guildId);
        this.userRole = String(userRole);
        this.loanTax = Number(loanTax);
        this.expiredLoanTax = Number(expiredLoanTax);
        this.tax = tax.map(_ => Number(_));
    }
}

export interface IGuild {
    id: string;
    name: string;
    status: GuildStatus;
    gamersCount: number;
    charactersCount: number;
    balance: number;
    recruitmentStatus: RecruitmentStatus;
}

export class GuildInfo implements IGuild {
    id: string;
    name: string;
    status: GuildStatus;
    recruitmentStatus: RecruitmentStatus;
    gamersCount: number;
    balance: number;
    charactersCount: number;
    constructor(id: string, name: string, status: GuildStatus,
        recruitmentStatus: RecruitmentStatus, charactersCount: number,
        gamersCount: number, balance: number) {
        this.id = id;
        this.name = name;
        this.status = status;
        this.recruitmentStatus = recruitmentStatus;
        this.charactersCount = charactersCount;
        this.gamersCount = gamersCount;
        this.balance = balance;
    }
}