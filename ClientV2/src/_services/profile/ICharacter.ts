export interface ICharacter {
    id: string;
    name: string;
    className: string;
    isMain: boolean;
}

export class Character implements ICharacter {
    id: string;
    name: string;
    className: string;
    isMain: boolean;

    constructor(id: string, name: string, className: string, isMain: boolean) {
        this.id = String(id);
        this.name = String(name);
        this.className = String(className);
        this.isMain = Boolean(isMain);
    }
}