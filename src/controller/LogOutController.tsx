import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang } from '../_services';
import { } from '../_components';

import { sessionInstance } from '../_actions';
import { IStore } from '../_helpers';

interface ILogOutProps extends DispatchProp<any> {
    isLoading?: boolean
}
class LogOut extends React.Component<ILogOutProps> {
    constructor(props: ILogOutProps) {
        super(props);
    }

    logout = () => {
        const { dispatch } = this.props;
        dispatch(sessionInstance.logOut());
    }

    render() {
        return (<div></div>);
    }
}
const connectedLogOut = connect((state: IStore) => {
    const { session } = state;
    return {
        isLoading: session && session.holding
    };
})(LogOut);

export { connectedLogOut as LogOutController }; 