
import * as React from "react";
import style from "./dialog.module.less";
import { BaseReactComp } from "../BaseReactComponent";
import { Dialog, Col1, Button, Grid, NamedValue } from "..";
import { Lang, IPenaltyView, DLang, IOperationView } from "../../_services";
import { gamerInstance } from "../../_actions";
import { connect } from "react-redux";
import { getGuid, IStore, formatDateTime, IF } from "../../_helpers";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    penalty: IPenaltyView;
    gamerId: string;
    operations: Array<IOperationView>;
    onClose: Function;
    [id: string]: any;
}


class _PenaltyDialog extends BaseReactComp<IProps> {
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

    onCancel = () => {
        this.props.dispatch(gamerInstance.CancelPenalty({
            id: this.props.penalty.id,
            gamerId: this.props.gamerId,
            onSuccess: () => this.onClose()
        }));
    }

    footer = () => {
        return (
            <IF value={this.props.penalty.penaltyStatus == 'Active'}>
                <Button
                    type='important'
                    onClick={() => this.onCancel()}
                >
                    {Lang('CANCEL')}
                </Button>
            </IF>
        );
    }

    render() {
        const { penalty } = this.props;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('SHOW_PENALTY_MODAL')}
                onCancel={() => this.onClose()}
                footer={this.footer()}
            >
                <Grid
                    direction="vertical"
                >
                    <Col1>
                        <NamedValue name={Lang("MODAL_PENALTY_AMOUNT")}>
                            {penalty.amount}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_PENALTY_DATE")}>
                            {formatDateTime(penalty.date, 'd')}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_PENALTY_DESCRIPTION")}>
                            {penalty.description}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_PENALTY_STATUS")}>
                            {DLang('PENALTY_STATUS', penalty.penaltyStatus)}
                        </NamedValue>
                    </Col1>
                    <Col1 className={style['operation-list']}>
                        <NamedValue name={Lang("MODAL__OPERATIONS")}>
                            {this.props.operations.map(_ => (
                                <div
                                    title={_.description}
                                    className={style['operations']}
                                >
                                    {_.amount}
                                </div>
                            ))}
                        </NamedValue>
                    </Col1>
                </Grid>
            </Dialog>
        );
    }
}

interface _IProps extends React.Props<any> {
    isDisplayed: boolean;
    penaltyId: string;
    gamerId: string;
    onClose: Function;
    [id: string]: any;
}
const connected_PenaltyDialog = connect<{}, {}, _IProps, IStore>((store, props): IProps => {
    const op = store.operations.operations[props.penaltyId];
    return {
        isDisplayed: props.isDisplayed,
        gamerId: props.gamerId,
        operations: op != undefined ? op.items : [],
        onClose: props.onClose,
        penalty: props.isDisplayed ? store.gamers.gamersList[props.gamerId].penalties[props.penaltyId] : {} as IPenaltyView
    };
})(_PenaltyDialog);

export { connected_PenaltyDialog as ShowPenaltyDialog }; 