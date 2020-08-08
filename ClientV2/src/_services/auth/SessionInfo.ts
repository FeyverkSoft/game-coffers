export class SessionInfo {
    sessionId: string;
    guildId: string;
    holding: boolean;
    roles: string[];
    isNew?: boolean = false;
    ConfirmCodeState?: boolean;
    
    constructor(data: any = { holding: false }) {
        let obj;
        if (typeof (data) === typeof ('')) {
            try {
                obj = JSON.parse(data);
            } catch (e) { console.warn(e); }
        }
        obj = obj || data || {};
        this.sessionId = obj.token || obj.sessionId;
        this.holding = obj.holding || false;
        this.roles = obj.roles;
        this.guildId = obj.guildId;
    }

    isActive(): boolean {
        return this.sessionId !== undefined && this.sessionId !== null && this.sessionId !== '';
    }
}
