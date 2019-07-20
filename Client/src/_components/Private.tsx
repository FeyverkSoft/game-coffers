import * as React from 'react';
import { store } from '../_helpers';

/**
 * Вся логика по разграничению прав на стороне фронта тут
 */
export class Private extends React.Component<{ roles?: string[] } & any, any> {
    constructor(props: { roles?: string[] } & any) {
        super(props);
    }

    defIsHidden: Function = (): boolean => {
        let flag = true;
        const { session } = store.getState();
        flag = session == undefined || !session.isActive();
        if (this.props.roles && this.props.roles.length > 0) {
            for (let i = 0; i < this.props.roles.length; i++) {
                let role = this.props.roles[i].toLowerCase();
                if (session.roles.filter(s => s.toLowerCase() == role).length > 0) {
                    return false;
                }
            }
            return true;
        }
        return flag;
    };

    render() {
        if (this.defIsHidden()) {
            return null;
        }
        return this.props.children;
    }
}