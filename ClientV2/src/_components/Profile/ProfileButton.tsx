import * as React from 'react';
import { NavLink } from "react-router-dom";
import { IStore } from "../../_helpers";
import { connect } from "react-redux";
import { profileInstance } from "../../_actions/profile/profile.actions";
import { Button } from "antd";
import { UserOutlined } from '@ant-design/icons';

interface ProfileButtonProps extends React.Props<any> {
    isLoading: boolean,
    name: String,
    GetCurrentGamer(): void,
}
class _profileButton extends React.Component<ProfileButtonProps> {
    componentDidMount() {
        if (this.props.name === '' && !this.props.isLoading)
            this.props.GetCurrentGamer();
    }
    render() {
        return (
            <NavLink to="/logout"
                exact
            >
                <Button
                    loading={this.props.isLoading}
                >
                    <UserOutlined />
                    {this.props.name}
                </Button>
            </NavLink>
        );
    }
}

const connectedProfileButton = connect<any, any, any, IStore>(
    (state: IStore) => {
        const { profile } = state.profile;
        return {
            isLoading: profile.holding || false,
            name: profile.name
        };
    },
    (dispatch: Function) => {
        return {
            GetCurrentGamer: () => dispatch(profileInstance.Get()),
        }
    })(_profileButton);

export { connectedProfileButton as ProfileButton };
