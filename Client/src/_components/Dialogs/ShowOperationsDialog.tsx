import * as React from "react";
import style from "./dialog.module.less";
import { BaseReactComp } from "../BaseReactComponent";
import { Dialog, Col1, Grid, NamedValue, Item } from "..";
import { Lang } from "../../_services";
import { connect } from "react-redux";
import { IStore } from "../../_helpers";
import { IOperation } from "../../_reducers/operation/operations.reducer";
import { operationsInstance } from "../../_actions";

export interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    operations: IOperation;
    onClose: Function;
    [id: string]: any;
}
interface IState {
    isLoad: boolean;
}

export abstract class _OperationsDialog<TProp = {}, TState = {}> extends BaseReactComp<IProps & TProp, IState & TState> {
    constructor(props: IProps & TProp, state: IState & TState) {
        super(props, state);
        this.state = state;
    }

    formatDate = (data: Date) => {
        let dat = new Date(data);
        return `${dat.getFullYear()}-${dat.getMonth() + 1}-01`;
    }

    getDateList = (): Array<Item> => {
        let result: Array<Item> = [];
        let std = new Date();
        let startMnt = std.getMonth();
        let satartYear = std.getFullYear();
        for (let i = 0; i < 12; i++) {
            if (startMnt - i < 0) {
                startMnt = 11 + i;
                satartYear -= 1;
            }
            let val = this.formatDate(new Date(satartYear, startMnt - i, 1));
            result.push(new Item(val, `${satartYear}-${startMnt - i + 1}`));
        }
        return result;
    }
    
    abstract loadData = () => { }
    abstract subTitle = (): React.ReactNode => {
        return;
    }

    onClose = () => {
        this.props.onClose();
    }

    onInputAndLoadDate = <T extends {} = any>(val: T, valid: boolean, path: string): void => {
        this.onInput(val, valid, path);
        this.loadData();
    }

    render() {
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={<div className={style['header']} >{Lang('MODAL__OPERATIONS')} {this.subTitle()}</div>}
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

class _ShowOperationsDialog extends _OperationsDialog {

    subTitle = () => '';

    loadData = () => { }

    componentDidMount() {
        if (this.props.guildId)
            this.props.dispatch(operationsInstance.GetOperationsByGuildId({
                guildId: this.props.guildId
            }));
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
})(_ShowOperationsDialog);

export { connected_ShowUserOperations as ShowOperationsDialog };

