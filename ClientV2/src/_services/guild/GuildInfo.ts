import { IDictionary } from "../../core";

export type RecruitmentStatus = 'Open' | 'Close' | 'Internal';
export type GuildStatus = 'Active' | 'InActive';

export interface ITariff {
    loanTax: number;
    expiredLoanTax: number;
    tax: number[];
}

export interface ITariffs extends IDictionary<ITariff> {
    Leader: ITariff;
    Officer: ITariff;
    Veteran: ITariff;
    Soldier: ITariff;
    Beginner: ITariff;
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
    tariffs: ITariffs;
    constructor(id: string, name: string, status: GuildStatus,
        recruitmentStatus: RecruitmentStatus, charactersCount: number,
        gamersCount: number, balance:number, tariffs: ITariffs) {
        this.id = id;
        this.name = name;
        this.status = status;
        this.recruitmentStatus = recruitmentStatus;
        this.tariffs = tariffs;
        this.charactersCount = charactersCount;
        this.gamersCount = gamersCount;
        this.balance = balance;
    }
}