import { GamerStatus } from "../gamer/GamerStatus";
import { GamerRank } from "../gamer/GamerRank";
import { PenaltyStatus } from "./PenaltyStatus";
import { LoanStatus } from "./LoanStatus";

export interface IGamersListView {
    id: string;
    description: string;
    characters: string;
    balance: number;
    penalties: Array<IPenaltyView>;
    loans: Array<ILoanView>;
    rank: GamerRank;
    status: GamerStatus;
}

export interface IPenaltyView {
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