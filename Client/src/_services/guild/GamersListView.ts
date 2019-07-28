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
    dateOfBirth: Date;

    GetLoan(id: string): ILoanView;
    GetPenalty(penaltyId: string): IPenaltyView;
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
    dateOfBirth: Date;

    GetPenalty(penaltyId: string): IPenaltyView {
        if (this.penalties[penaltyId])
            return this.penalties[penaltyId]
        return {} as IPenaltyView;
    }

    GetLoan(id: string): ILoanView {
        if (this.loans[id])
            return this.loans[id]
        return {} as ILoanView;
    }

    constructor(id: string,
        characters: Array<string>,
        balance: number,
        penalties: Array<IPenaltyView>,
        loans: Array<ILoanView>,
        rank: GamerRank,
        status: GamerStatus,
        dateOfBirth: string
    ) {
        this.id = id;
        this.characters = characters;
        this.balance = balance;
        this.penalties = {};
        for (let i = 0; i < penalties.length; i++) {
            this.penalties[penalties[i].id] = penalties[i];
        }
        this.loans = {};
        for (let i = 0; i < loans.length; i++) {
            this.loans[loans[i].id] = loans[i];
        }
        this.rank = rank;
        this.status = status;
        this.dateOfBirth = new Date(dateOfBirth);
    }
}