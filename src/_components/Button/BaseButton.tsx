import React from "react";

export interface IBaseButtonProps extends React.Props<any> {
    onClick?: Function;
    disabled?: boolean;
    isLoading?: boolean;
    isSubmit?: boolean;
}
export class BaseButton<TProps = {}, TState = {}> extends React.Component<TProps & IBaseButtonProps, TState> {
    onClick = (e: React.MouseEvent<HTMLDivElement | HTMLButtonElement>) => {
        if (!this.props.disabled && !this.props.isLoading && this.props.onClick) {
            this.props.onClick(e);
        }
    }
}