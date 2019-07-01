import * as React from 'react';
import style from "./profilebutton.module.less";

interface ProfileButtonProps extends React.Props<any> { }
export class ProfileButton extends React.Component<ProfileButtonProps> {
    render() {
        return (
            <div className={style['profile-button-wrapper']}>
                <div className={style['profile-button']} />
            </div>
        );
    }
}
