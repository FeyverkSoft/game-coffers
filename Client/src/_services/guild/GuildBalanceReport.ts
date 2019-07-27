export class GuildBalanceReport {
    balance: number;
    expectedTaxAmount: number;
    taxAmount: number;
    activeLoansAmount: number;
    gamersBalance: number;

    constructor(balance: number, expectedTaxAmount: number, taxAmount: number, activeLoansAmount: number, gamersBalance: number) {
        this.balance = balance;
        this.expectedTaxAmount = expectedTaxAmount;
        this.taxAmount = taxAmount;
        this.activeLoansAmount = activeLoansAmount;
        this.gamersBalance = gamersBalance;
    }
}