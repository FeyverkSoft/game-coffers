import { GamerRank } from "./GamerRank";

export interface IProfile {
    userId: String;
    name: String;
    characterName: String;
    rank: GamerRank;
    dateOfBirth: Date;
    charCount: number;
    balance: number;
    activeLoanAmount: number;
    activePenaltyAmount: number;
}

export class Profile implements IProfile {
    userId: String;
    name: String;
    characterName: String;
    rank: GamerRank;
    dateOfBirth: Date;
    charCount: number;
    balance: number;
    activeLoanAmount: number;
    activePenaltyAmount: number;

    constructor(userId: String = "", name: String = "", characterName: String = "", balance: number = 0, activeLoanAmount: number = 0,
        activePenaltyAmount: number = 0, rank: GamerRank = 'Beginner', charCount: number = 0,
        dateOfBirth: Date = new Date()) {
        this.userId = userId;
        this.name = name;
        this.characterName = characterName;
        this.balance = Number(balance);
        this.activeLoanAmount = Number(activeLoanAmount);
        this.activePenaltyAmount = Number(activePenaltyAmount);
        this.rank = rank;
        this.charCount = Number(charCount);
        this.dateOfBirth = new Date(dateOfBirth);
    }
}
