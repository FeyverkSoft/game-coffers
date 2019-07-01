import * as React from 'react';
import { store } from '../_helpers';

/**
 * Вся логика по разграничению прав на стороне фронта тут
 */
export class Private extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
    }

    defIsHidden: Function = (): boolean => {
        let flag = true;
        const { session } = store.getState();
        flag = session == undefined || !session.isActive();
        return flag;
    };

    render() {
        if (this.defIsHidden()) {
            return null;
        }
        return this.props.children;
    }
}