import React from "react";
import style from "./button.module.less";
import { BaseButton, IBaseButtonProps } from "./BaseButton";
import { SmallSpinner } from "../Spinner/Spinner";
import { IF } from "../../_helpers";

interface ButtonProps extends IBaseButtonProps {
    type?: 'default' | 'important' | 'success' | 'none';
    isSmall?: boolean;
    style?: React.CSSProperties;
    extClassName?: string;
}

export class Button extends BaseButton<ButtonProps> {
    render() {
        return <button
            className={
                `${style['button']} ${style[this.props.type || '']}` +
                ` ${this.props.extClassName ? this.props.extClassName : ''}` +
                ` ${this.props.isSmall ? style['small'] : ''}`
            }
            disabled={this.props.disabled || this.props.isLoading}
            onClick={this.onClick}
            type={this.props.isSubmit ? 'submit' : 'button'}
            style={this.props.style}
        >
            {this.props.children}
            <IF value={this.props.isLoading}><SmallSpinner /></IF>
        </button>;
    }
}

export class FloatButton extends BaseButton<ButtonProps> {
    render() {
        return <button
            className={
                `${style['button']} ${style[this.props.type || '']}` +
                ` ${this.props.extClassName ? this.props.extClassName : ''}`
            }
            disabled={this.props.disabled || this.props.isLoading}
            onClick={this.onClick}
            type={this.props.isSubmit ? 'submit' : 'button'}
            style={this.props.style}
        >
            {this.props.children}
            <IF value={this.props.isLoading}><SmallSpinner /></IF>
        </button>;
    }
}