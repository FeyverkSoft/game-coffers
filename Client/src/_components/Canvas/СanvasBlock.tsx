import React from "react";
import style from "./canvas.module.less";
import { Spinner } from "../Spinner/Spinner";
import { IF } from "../../_helpers";

interface IСanvasProps extends React.Props<any> {
    isLoading?: boolean;
    title?: string;
    type: 'error' | 'default' | 'important' | 'success';
}

///Холст
export class СanvasBlock extends React.Component<IСanvasProps, any> {
    render() {
        return <div
            className={`${style['canvas']} ${this.props.title ? style['titled'] : ''}`}
        >
            <IF value={this.props.title}>
                <div className={`${style['title']} ${style[this.props.type]}`}>
                    {this.props.title}
                </div>
            </IF>
            {this.props.children}
            <IF value={this.props.isLoading}>
                <Spinner />
            </IF>
        </div>;
    }
}