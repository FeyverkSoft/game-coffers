export class SessionInfo {
    sessionId: string;
    userId: string;
    initDate: Date;
    holding: boolean;
    constructor(data: any = { holding: false }) {
        let obj;
        if (typeof (data) == typeof ('')) {
            try {
                obj = JSON.parse(data);
            } catch (e) { console.warn(e); }
        }
        obj = obj || data || {};
        this.sessionId = obj.token || obj.sessionId;
        this.userId = obj.ownerId || obj.userId;
        this.initDate = new Date(obj.initDate || new Date());
        this.holding = obj.holding || false;
    }

    isActive(): boolean {
        return this.sessionId != null || this.sessionId == '';
    }
}
