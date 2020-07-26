export class Contract {
    id: string;
    nestName: string;
    reward: string;
    characterName: string;

    constructor(id: string,
        nestName: string,
        characterName: string,
        reward: string) {
        this.id = String(id);
        this.nestName = String(nestName);
        this.reward = String(reward);
        this.characterName = String(characterName);
    }
}
