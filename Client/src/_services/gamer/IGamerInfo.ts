export type GamerRank = 'Leader' | 'Officer' | 'Veteran' | 'Soldier' | 'Beginner';

export interface IGamerInfo {
    userId: String;
    name: String;
    balance: number;
    activeLoanAmount: number;
    activePenaltyAmount: number;
    rank: GamerRank;
    charCount: number;
}

export class GamerInfo implements IGamerInfo {
    userId: String;
    name: String;
    balance: number;
    activeLoanAmount: number;
    activePenaltyAmount: number;
    rank: GamerRank;
    charCount: number;

    constructor(userId: String, name: String, balance: number, activeLoanAmount: number,
        activePenaltyAmount: number, rank: GamerRank, charCount: number) {
        this.userId = userId;
        this.name = name;
        this.balance = balance;
        this.activeLoanAmount = activeLoanAmount;
        this.activePenaltyAmount = activePenaltyAmount;
        this.rank = rank;
        this.charCount = charCount;
    }
}
