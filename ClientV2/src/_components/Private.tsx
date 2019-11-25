import * as React from 'react';
import { store } from '../_helpers';

/**
 * Вся логика по разграничению прав на стороне фронта тут
 */
interface IPrivateProps extends React.Props<any> {
    roles?: Array<string>;
    skipRoleTest?: Boolean;
}

class _Private extends React.Component<IPrivateProps> {
    constructor(props: IPrivateProps) {
        super(props);
    }

    defIsHidden: Function = (): boolean => {
        let flag = true;
        const { session } = store.getState();
        flag = session == undefined || !session.isActive();
        if (!this.props.skipRoleTest && this.props.roles && this.props.roles.length > 0) {
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

export const Private = React.memo(({ ...props }: IPrivateProps) => <_Private {...props} />)