export class GuildBalanceReport {
    balance: number;
    expectedTaxAmount: number;
    taxAmount: number;
    activeLoansAmount: number;
    repaymentLoansAmount: number;
    gamersBalance: number;

    constructor(balance: number,
        expectedTaxAmount: number,
        taxAmount: number,
        activeLoansAmount: number,
        repaymentLoansAmount: number,
        gamersBalance: number) {
        this.balance = balance;
        this.expectedTaxAmount = expectedTaxAmount;
        this.taxAmount = taxAmount;
        this.activeLoansAmount = activeLoansAmount;
        this.repaymentLoansAmount = repaymentLoansAmount;
        this.gamersBalance = gamersBalance;
    }
}