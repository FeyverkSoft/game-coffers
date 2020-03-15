import { GamerStatus } from "../gamer/GamerStatus";
import { GamerRank } from "../gamer/GamerRank";
import { PenaltyStatus } from "./PenaltyStatus";
import { LoanStatus } from "./LoanStatus";
import { Dictionary } from "../../core";

export interface IGamersListView {
    id: string;
    balance: number;
    rank: GamerRank;
    status: GamerStatus;
    dateOfBirth: Date;
    name: string;
    characters: Array<ICharacter>;
    penalties: Dictionary<IPenaltyView>;
    loans: Dictionary<ILoanView>;

    GetLoan(id: string): ILoanView;
    GetPenalty(penaltyId: string): IPenaltyView;
}

export interface ICharacter {
    id: string;
    name: string;
    className: string;
    isMain: boolean;
    userId: string;
}

export interface IPenaltyView {
    id: string;
    createDate: Date;
    amount: number;
    description: string;
    penaltyStatus: PenaltyStatus
}

export interface ILoanView {
    id: string;
    amount: number;
    balance: number;
    createDate: Date;
    expiredDate: Date;
    description: string;
    loanStatus: LoanStatus;
}

export class GamersListView implements IGamersListView {
    id: string;
    characters: Array<ICharacter>;
    balance: number;
    penalties: Dictionary<IPenaltyView>;
    loans: Dictionary<ILoanView>;
    rank: GamerRank;
    status: GamerStatus;
    dateOfBirth: Date;
    name: string;

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
        characters: Array<ICharacter>,
        balance: number,
        penalties: Array<IPenaltyView>,
        loans: Array<ILoanView>,
        rank: GamerRank,
        status: GamerStatus,
        dateOfBirth: string,
        name: string
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
        this.name = name || '';
    }
}