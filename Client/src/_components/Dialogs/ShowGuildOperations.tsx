import * as React from "react";
import { connect } from "react-redux";
import style from "./dialog.module.less";
import { IStore } from "../../_helpers";
import { _OperationsDialog, IProps } from "./ShowOperationsDialog";
import { operationsInstance } from "../../_actions";
import { MaterialSelect } from "..";

interface _GIProps extends React.Props<any> {
    isDisplayed: boolean;
    guildId: string;
    onClose: Function;
    subTitle?: React.ReactNode;
    [id: string]: any;
}

interface IState {
    date: Date;
}

export class _GOperationsDialog extends _OperationsDialog<{}, IState> {
    constructor(props: IProps, state: IState) {
        super(props, {
            isLoad: false,
            date: new Date()
        });
    }

    subTitle = (): React.ReactNode => {
        return <MaterialSelect
            className={style['select']}
            items={this.getDateList()}
            value={this.formatDate(this.state.date)}
            path="date"
            onChange={this.onInputAndLoadDate}
            type='default'
        ></MaterialSelect>;
    }

    loadData = () => {
        if (this.props.guildId)
            this.props.dispatch(operationsInstance.GetOperationsByGuildId({
                guildId: this.props.guildId,
                dateMonth: this.formatDate(this.state.date)
            }));
    }

    componentDidMount() {
        this.loadData();
    }
}

const connected_ShowGuildOperations = connect<{}, {}, _GIProps, IStore>((store, props): IProps => {
    return {
        isDisplayed: props.isDisplayed,
        guildId: props.guildId,
        subTitle: props.subTitle,
        onClose: props.onClose,
        operations: store.operations.GetGuildOperations(props.guildId)
    };
})(_GOperationsDialog);

export { connected_ShowGuildOperations as ShowGuildOperationsDialog }; 