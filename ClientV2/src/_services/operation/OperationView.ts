import { OperationType } from "..";

export interface IOperationView {
    id: string;
    /**
     * Сумма операции
     */
    amount: number;
    /**
     * Сумма документа
     */
    documentAmount: number;
    /**
     * Описание документа по которому прошла операция
     */
    documentDescription: string;
    /**
     * Идентификатор пользователя создавшего операцию
     */
    userId: string;
    /**
     * Имя пользователя выполнившего операцию
     */
    userName: string;
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
    documentAmount: number;
    documentDescription: string;
    userId: string;
    userName: string;
    documentId: string;
    type: OperationType;
    description: string;
    createDate: Date;

    constructor(
        id: string,
        amount: number,
        documentId: string,
        documentAmount: number,
        documentDescription: string,
        userId: string,
        userName: string,
        type: OperationType,
        description: string,
        createDate: Date
    ) {
        this.id = id;
        this.amount = Number(amount);
        this.documentId = String(documentId || '');
        this.description = String(description || '');
        this.createDate = new Date(createDate);
        this.type = type as OperationType;
        this.documentAmount = Number(documentAmount);
        this.documentDescription = String(documentDescription || '');
        this.userId = String(userId);
        this.userName = String(userName || '');
    }
}