export type RecruitmentStatus = 'Open'|'Close'|'Internal';
export type GuildStatus = 'Active'|'InActive';

export interface ITariff {
    loanTax: number;
    expiredLoanTax: number;
    tax: number[];
}

export interface ITariffs {
    leader: ITariff;
    officer: ITariff;
    veteran: ITariff;
    soldier: ITariff;
    beginner: ITariff;
}
export interface IGuild{
    id: string;
    name: string;
    status: GuildStatus;
    recruitmentStatus: RecruitmentStatus;
}

export class GuildInfo implements IGuild{
    id: string;
    name: string;
    status: GuildStatus;
    recruitmentStatus: RecruitmentStatus;
    tariffs: ITariffs;
    constructor(id: string, name: string,status: GuildStatus, recruitmentStatus: RecruitmentStatus, tariffs: ITariffs  ) {
        this.id= id;
        this.name = name;
        this.status = status;
        this.recruitmentStatus = recruitmentStatus;
        this.tariffs = tariffs;
    }
}