import { GamerStatus } from "./GamerStatus";
import { GamerRank } from "./GamerRank";
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
    characters: Dictionary<ICharacter>;
    penalties: Dictionary<IPenaltyView>;
    loans: Dictionary<ILoanView>;
    mainCharacterId: string;

    getMainCharacter(): ICharacter;
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
    balance: number;
    characters: Dictionary<ICharacter>;
    penalties: Dictionary<IPenaltyView>;
    loans: Dictionary<ILoanView>;
    rank: GamerRank;
    status: GamerStatus;
    dateOfBirth: Date;
    name: string;
    mainCharacterId: string = '';

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
        this.id = String(id);
        this.balance = Number(balance);
        this.rank = rank as GamerRank;
        this.status = status as GamerStatus;
        this.dateOfBirth = new Date(dateOfBirth);
        this.name = String(name || '');
        this.characters = {};
        this.penalties = {};
        this.loans = {};
        characters.forEach(char => {
            this.characters[char.id] = {
                className: String(char.className),
                name: String(char.name),
                userId: String(char.userId),
                isMain: Boolean(char.isMain),
                id: String(char.id)
            };
            if (char.isMain)
                this.mainCharacterId = char.id;
        });
        penalties.forEach(penalty => {
            this.penalties[penalty.id] = {
                id: String(penalty.id),
                createDate: new Date(penalty.createDate),
                amount: Number(penalty.amount),
                description: String(penalty.description || ''),
                penaltyStatus: penalty.penaltyStatus as PenaltyStatus
            };
        });
        loans.forEach(loan => {
            this.loans[loan.id] = {
                id: String(loan.id),
                amount: Number(loan.amount),
                balance: Number(loan.balance),
                createDate: new Date(loan.createDate),
                expiredDate: new Date(loan.expiredDate),
                description: String(loan.description),
                loanStatus: loan.loanStatus as LoanStatus
            };
        });
    }

    getMainCharacter(): ICharacter {
        return this.characters[this.mainCharacterId] || this.characters[Object.keys(this.characters)[0]] || {};
    }
}