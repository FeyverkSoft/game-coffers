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

    onScroll = (event: React.UIEvent<HTMLDivElement>) => {
        if (event.currentTarget.scrollTop > 5)
            this.setState({ showShadow: true })
        else
            this.setState({ showShadow: false })
    }

    render() {
        const { isDisplayed, onCancel, isLoading } = this.props;
        return <div className={`${style["dialog"]} ${isDisplayed ? '' : style['none']}`}>
            <div className={style['wrapper']}>
                <div className={`${style["header"]} ${this.state.showShadow ? style['shadow'] : ''}`}>
                    <IF value={isDisplayed}>
                        {this.props.title}
                    </IF>
                    <div className={style["close"]}
                        onClick={() => onCancel ? onCancel() : null}>
                        ×
                    </div>
                </div>
                <IF value={isDisplayed}>
                    <div className={style["body"]}
                        onScroll={this.onScroll}
                    >
                        <IF value={isLoading}>
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