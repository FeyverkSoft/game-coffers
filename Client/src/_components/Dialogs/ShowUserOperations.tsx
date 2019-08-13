import * as React from "react";
import style from "./dialog.module.less";
import { connect } from "react-redux";
import { IStore } from "../../_helpers";
import { operationsInstance } from "../../_actions";
import { IProps, _OperationsDialog } from "./ShowOperationsDialog";
import { BaseSelect } from "..";

interface IState {
    date: Date;
}

export class _UOperationsDialog extends _OperationsDialog<{}, IState> {
    constructor(props: IProps, state: IState) {
        super(props, {
            isLoad: false,
            date: new Date()
        });
    }

    subTitle = (): React.ReactNode => {
        return <BaseSelect
            className={style['select']}
            items={this.getDateList()}
            value={this.formatDate(this.state.date)}
            path="date"
            onChange={this.onInputAndLoadDate}
            type='default'
        ></BaseSelect>;
    }

    loadData = () => {
        if (this.props.gamerId)
            this.props.dispatch(operationsInstance.GetOperationsByUserId({
                userId: this.props.gamerId,
                dateMonth: this.formatDate(this.state.date)
            }));
    }

    componentDidMount() {
        this.loadData();
    }
}

interface _IProps extends React.Props<any> {
    isDisplayed: boolean;
    gamerId: string;
    onClose: Function;
    [id: string]: any;
}
const connected_ShowUserOperations = connect<{}, {}, _IProps, IStore>((store, props): IProps => {
    return {
        isDisplayed: props.isDisplayed,
        gamerId: props.gamerId,
        onClose: props.onClose,
        operations: store.operations.GetGamerOperations(props.gamerId)
    };
})(_UOperationsDialog);

export { connected_ShowUserOperations as ShowUserOperations };

