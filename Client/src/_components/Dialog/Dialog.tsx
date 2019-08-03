import * as React from 'react';
import style from "./dialog.module.less";
import { IF } from '../../_helpers';
import { Spinner } from '../Spinner/Spinner';

interface IDialogProps extends React.Props<any> {
    isDisplayed: boolean;
    onCancel?: Function;
    footer?: React.ReactNode;
    title?: React.ReactNode
    isLoading?: boolean;

}

///Диалоговое окно
export class Dialog extends React.Component<IDialogProps, any> {
    state: any;
    constructor(props: IDialogProps) {
        super(props);
        this.state = {
            isDisplayed: props.isDisplayed
        };
    }
    componentWillReceiveProps(props: IDialogProps) {
        this.setState({
            isDisplayed: props.isDisplayed
        });
    }
    render() {
        return <div className={`${style["dialog"]} ${this.props.isDisplayed ? '' : style['none']}`}>
            <div className={style['wrapper']}>
                <div className={style["header"]}>
                    <IF value={this.props.isDisplayed}>
                        {this.props.title}
                    </IF>
                    <div className={style["close"]}
                        onClick={() => this.props.onCancel ? this.props.onCancel() : null}>
                        ×
                    </div>
                </div>
                <IF value={this.props.isDisplayed}>
                    <div className={style["body"]}>
                        <IF value={this.props.isLoading}>
                            <Spinner />
                        </IF>
                        {this.props.children}
                    </div>
                    <IF value={this.props.footer != undefined}>
                        <div className={style["footer"]}>
                            {this.props.footer}
                        </div>
                    </IF>
                </IF>
            </div>
        </div>
    }
}