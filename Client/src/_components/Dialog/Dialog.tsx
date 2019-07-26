import * as React from 'react';
import style from "./dialog.module.less";
import { IF } from '../../_helpers';

interface IDialogProps extends React.Props<any> {
    isDisplayed: boolean;
    onCancel?: Function;
    footer?: React.ReactNode;
    title?: React.ReactNode

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
        return <IF value={this.props.isDisplayed}>
            <div className={style["dialog"]}>
                <div className={style['wrapper']}>
                    <div className={style["header"]}>
                        {this.props.title}
                        <div className={style["close"]}
                            onClick={() => this.props.onCancel ? this.props.onCancel() : null}>
                            ×
                        </div>
                    </div>
                    <div className={style["body"]}>
                        {this.props.children}
                    </div>
                    <IF value={this.props.footer != undefined}>
                        <div className={style["footer"]}>
                            {this.props.footer}
                        </div>
                    </IF>
                </div>
            </div>
        </IF>
    }
}