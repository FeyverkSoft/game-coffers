import * as React from 'react';
import { IStore } from '../_helpers';
import { connect } from 'react-redux';
import { SessionInfo } from '../_services';

/**
 * Вся логика по разграничению прав на стороне фронта тут
 */
interface _IPrivateProps extends React.Props<any> {
    session: SessionInfo;
    roles?: Array<string>;
    skipRoleTest?: Boolean;
}

class _Private extends React.Component<_IPrivateProps> {
    constructor(props: _IPrivateProps) {
        super(props);
    }

    defIsHidden: Function = (): boolean => {
        let flag = true;
        const { session } = this.props;
        flag = session === undefined || !session.isActive();
        if (!this.props.skipRoleTest && this.props.roles && this.props.roles.length > 0) {
            for (let i = 0; i < this.props.roles.length; i++) {
                let role = this.props.roles[i].toLowerCase();
                if (session.roles.filter(s => s.toLowerCase() === role).length > 0) {
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
interface IPrivateProps extends React.Props<any> {
    roles?: Array<string>;
    skipRoleTest?: Boolean;
}

const connectedPrivate = connect<IPrivateProps, any, any, IStore>(
    (state: IStore, props: IPrivateProps): _IPrivateProps => {
        const { session } = state;
        return {
            session: session,
            roles: props.roles,
            skipRoleTest: props.skipRoleTest
        };
    })(_Private);

export { connectedPrivate as Private };