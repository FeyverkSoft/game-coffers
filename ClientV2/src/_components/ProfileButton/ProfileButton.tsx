import { NavLink } from "react-router-dom";
import React from "react";
import { IStore } from "../../_helpers";
import { connect } from "react-redux";
import { gamerInstance } from "../../_actions";
import { Button } from "antd";

interface ProfileButtonProps extends React.Props<any> {
    isLoading: boolean,
    name: String,
    GetCurrentGamer(): void,
}
class _profileButton extends React.Component<ProfileButtonProps> {

    componentDidMount() {
        if (this.props.name == '' && !this.props.isLoading)
            this.props.GetCurrentGamer();
    }
    render() {
        return (
            <NavLink to="/logout"
                exact
            >
                <Button> {this.props.name}</Button>
            </NavLink>
        );
    }
}

const connectedProfileButton = connect<any, any, any, IStore>(
    (state: IStore) => {
        const { currentGamer } = state.gamers;
        return {
            isLoading: currentGamer.holding || false,
            name: currentGamer.name
        };
    },
    (dispatch: Function) => {
        return {
            GetCurrentGamer: () => dispatch(gamerInstance.GetCurrentGamer()),
        }
    })(_profileButton);

export { connectedProfileButton as ProfileButton };
