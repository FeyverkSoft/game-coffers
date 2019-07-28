import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import style from "./profilebutton.module.less";
import { IStore } from '../../_helpers';
import { IGamerInfo } from '../../_services';
import { gamerInstance } from '../../_actions';
import { Link } from 'react-router-dom';

interface ProfileButtonProps extends React.Props<any> {
    isLoading: boolean,
    name: String,
}
class _profileButton extends React.Component<ProfileButtonProps & DispatchProp<any>> {

    componentDidMount() {
        if (this.props.name == '' && !this.props.isLoading)
            this.props.dispatch(gamerInstance.GetCurrentGamer());
    }

    render() {
        return (
            <div className={style['profile-button-wrapper']}>
                <Link to="/logout">
                    <div className={style['profile-button']} >
                        {this.props.name}
                    </div>
                </Link>
            </div>
        );
    }
}
const connectedProfileButton = connect<{}, {}, any, IStore>((state: IStore) => {
    const { currentGamer } = state.gamers;
    return {
        isLoading: currentGamer.holding || false,
        name: currentGamer.name
    }
})(_profileButton);

export { connectedProfileButton as ProfileButton }; 