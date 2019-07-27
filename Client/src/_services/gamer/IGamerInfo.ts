import { GamerRank } from "./GamerRank";

export interface IGamerInfo {
    userId: String;
    name: String;
    balance: number;
    activeLoanAmount: number;
    activePenaltyAmount: number;
    activeExpLoanAmount: number;
    activeLoanTaxAmount: number;
    repaymentLoanAmount: number;
    repaymentTaxAmount: number;
    rank: GamerRank;
    charCount: number;

}

export class GamerInfo implements IGamerInfo {
    userId: String;
    name: String;
    balance: number;
    activeLoanAmount: number;
    activePenaltyAmount: number;
    activeExpLoanAmount: number;
    activeLoanTaxAmount: number;
    repaymentLoanAmount: number;
    repaymentTaxAmount: number;
    rank: GamerRank;
    charCount: number;

    constructor(userId: String, name: String, balance: number, activeLoanAmount: number,
        activePenaltyAmount: number, activeExpLoanAmount: number, activeLoanTaxAmount: number,
        repaymentLoanAmount: number, repaymentTaxAmount: number, rank: GamerRank, charCount: number) {
        this.userId = userId;
        this.name = name;
        this.balance = balance;
        this.activeLoanAmount = activeLoanAmount;
        this.activePenaltyAmount = activePenaltyAmount;
        this.activeExpLoanAmount = activeExpLoanAmount;
        this.activeLoanTaxAmount = activeLoanTaxAmount;
        this.repaymentLoanAmount = repaymentLoanAmount;
        this.repaymentTaxAmount = repaymentTaxAmount;
        this.rank = rank;
        this.charCount = charCount;
    }
}
