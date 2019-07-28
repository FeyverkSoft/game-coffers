

import * as React from "react";
import style from "./dialog.module.less";
import { BaseReactComp } from "../BaseReactComponent";
import { Dialog, Col1, Grid, NamedValue } from "..";
import { Lang, IOperationView } from "../../_services";
import { connect } from "react-redux";
import { getGuid, IStore } from "../../_helpers";
import { IOperation } from "../../_reducers/operation/operations.reducer";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    operations: IOperation;
    onClose: Function;
    [id: string]: any;
}


class _UerOperationsDialog extends BaseReactComp<IProps> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            id: getGuid(),
            description: { value: undefined },
            borrowDate: { value: new Date() },
            expiredDate: { value: new Date() },
            amount: { value: 0 },
            isLoad: false
        }
    }

    onClose = () => {
        this.props.onClose();
    }

    render() {
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('MODAL__OPERATIONS')}
                onCancel={() => this.onClose()}
                isLoading={this.props.operations.holding}
            >
                <Grid
                    direction="vertical"
                >
                    <Col1 className={style['operation-list']}>

                        {this.props.operations.items.map(_ => (
                            <NamedValue
                                key={_.id}
                                name={_.description}
                                title={_.type}
                            >
                                {_.amount}
                            </NamedValue>
                        ))}
                    </Col1>
                </Grid>
            </Dialog>
        );
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
        onClose: props.onClose,
        operations: store.operations.GetGamerOperations(props.gamerId)
    };
})(_UerOperationsDialog);


interface _GIProps extends React.Props<any> {
    isDisplayed: boolean;
    guildId: string;
    onClose: Function;
    [id: string]: any;
}
const connected_ShowGuildOperations = connect<{}, {}, _GIProps, IStore>((store, props): IProps => {
    return {
        isDisplayed: props.isDisplayed,
        onClose: props.onClose,
        operations: store.operations.GetGuildOperations(props.guildId)
    };
})(_UerOperationsDialog);

export { connected_ShowUserOperations as ShowOperationsDialog }; 

export { connected_ShowGuildOperations as ShowGuildOperationsDialog }; 