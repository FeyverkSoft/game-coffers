import { GamerRank } from "./GamerRank";

export interface IProfile {
    userId: String;
    name: String;
    characterName: String;
    rank: GamerRank;
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
    charCount: number;
    balance: number;
    activeLoanAmount: number;
    activePenaltyAmount: number;

    constructor(userId: String = "", name: String = "", characterName: String = "", balance: number = 0, activeLoanAmount: number = 0,
        activePenaltyAmount: number = 0, rank: GamerRank = 'Beginner', charCount: number = 0) {
        this.userId = userId;
        this.name = name;
        this.characterName = characterName;
        this.balance = balance;
        this.activeLoanAmount = activeLoanAmount;
        this.activePenaltyAmount = activePenaltyAmount;
        this.rank = rank;
        this.charCount = charCount;
    }
}
