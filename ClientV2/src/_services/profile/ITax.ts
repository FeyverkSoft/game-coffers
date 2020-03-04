export interface ITax {
    userId: String;
    taxAmount: number;
    taxTariff: Array<number>;
}

export class UserTax implements ITax {
    userId: String;
    taxAmount: number;
    taxTariff: Array<number>;

    constructor(userId: String = "", taxAmount: number = 0, taxTariff: Array<number> = []) {
        this.userId = userId;
        this.taxAmount = Number(taxAmount);
        this.taxTariff = taxTariff.map(_ => Number(_));
    }
}