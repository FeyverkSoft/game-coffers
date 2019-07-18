export class GuildBalanceReport{
    balance: number;
    expectedTaxAmount: number;
    taxAmount: number;
    activeLoansAmount: number;
    
    constructor(balance: number, expectedTaxAmount: number,taxAmount: number, activeLoansAmount: number ){
        this.balance = balance;
        this.expectedTaxAmount = expectedTaxAmount;
        this.taxAmount = taxAmount;
        this.activeLoansAmount = activeLoansAmount;
    }
}