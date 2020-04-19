import { OperationType } from "..";

export interface IAvailableDocument {
    id: string;
    userId: string;
    description: string;
    documentType: OperationType
}

export class AvailableDocument implements IAvailableDocument {
    id: string;
    userId: string;
    description: string;
    documentType: OperationType;

    constructor(
        id: string,
        userId: string,
        description: string,
        documentType: OperationType
    ) {
        this.id = String(id);
        this.userId = String(userId);
        this.description = String(description);
        this.documentType = documentType as OperationType;
    }
}