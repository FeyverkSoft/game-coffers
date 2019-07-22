import { GamerStatus } from "../gamer/GamerStatus";
import { GamerRank } from "../gamer/GamerRank";
import { PenaltyStatus } from "./PenaltyStatus";
import { LoanStatus } from "./LoanStatus";
import { Dictionary } from "../../core";

export interface IGamersListView {
    id: string;
    characters: Array<string>;
    balance: number;
    penalties: Dictionary<IPenaltyView>;
    loans: Dictionary<ILoanView>;
    rank: GamerRank;
    status: GamerStatus;
}

export interface IPenaltyView {
    id: string;
    date: Date;
    amount: number;
    description: string;
    penaltyStatus: PenaltyStatus
}

export interface ILoanView {
    id: string;
    date: Date;
    expiredDate: Date;
    amount: number;
    description: string;
    loanStatus: LoanStatus
}

export class GamersListView implements IGamersListView {
    id: string;
    characters: Array<string>;
    balance: number;
    penalties: Dictionary<IPenaltyView>;
    loans: Dictionary<ILoanView>;
    rank: GamerRank;
    status: GamerStatus;
    constructor(id: string,
        characters: Array<string>,
        balance: number,
        penalties: Dictionary<IPenaltyView>,
        loans: Dictionary<ILoanView>,
        rank: GamerRank,
        status: GamerStatus
    ) {
        this.id = id;
        this.characters = characters;
        this.balance = balance;
        this.penalties = penalties;
        this.loans = loans;
        this.rank = rank;
        this.status = status;
    }
}