import { OperationType } from "..";

export interface IOperationView {
    id: string;
    /**
     * Сумма операции
     */
    amount: number;
    /**
     * Основание для операции (первичный документ)
     */
    documentId: string;
    /**
     * Тип операции
     */
    type: OperationType;
    /**
     * Описание операции
     */
    description: string;
    /**
     * Дата проведения операции
     */
    createDate: Date;
}


export class OperationView implements IOperationView {
    id: string;
    amount: number;
    documentId: string;
    type: OperationType;
    description: string;
    createDate: Date;
    constructor(
        id: string,
        amount: number,
        documentId: string,
        type: OperationType,
        description: string,
        createDate: string | Date
    ) {
        this.id = id;
        this.amount = Number(amount);
        this.documentId = documentId;
        this.description = description;
        this.createDate = new Date(createDate);
        this.type = type;
    }
}