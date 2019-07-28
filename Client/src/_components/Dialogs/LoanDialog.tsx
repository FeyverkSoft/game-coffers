import * as React from "react";
import style from "./dialog.module.less";
import { BaseReactComp } from "../BaseReactComponent";
import { Dialog, Col1, Button, NamedValue } from "..";
import { Lang, ILoanView, DLang, IOperationView } from "../../_services";
import { gamerInstance } from "../../_actions";
import { connect } from "react-redux";
import { IStore, formatDateTime, IF } from "../../_helpers";
import { Grid } from "../Grid/Grid";
import { IOperation } from "../../_reducers/operation/operations.reducer";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    loan: ILoanView;
    onClose: Function;
    operations: IOperation;
    gamerId: string;
    [id: string]: any;
}

class _LoanDialog extends BaseReactComp<IProps> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            name: { value: undefined },
            className: { value: undefined },
            isLoad: false
        }
    }

    onClose = () => {
        this.props.onClose();
    }

    onCancel = () => {
        this.props.dispatch(gamerInstance.CancelLoan({
            id: this.props.loan.id,
            gamerId: this.props.gamerId,
            onSuccess: () => this.onClose()
        }));
    }

    footer = () => {
        return (
            <IF value={this.props.loan.loanStatus == 'Active'}>
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
        const { loan, operations } = this.props;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('SHOW_LOAN_MODAL')}
                onCancel={() => this.onClose()}
                footer={this.footer()}
                isLoading={operations.holding}
            >
                <Grid
                    direction="vertical"
                >
                    <Col1>
                        <NamedValue name={Lang("MODAL_LOAN_AMOUNT")}>
                            {loan.amount}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_LOAN_DATE")}>
                            {formatDateTime(loan.date, 'd')}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_LOAN_EXPIREDDATE")}>
                            {formatDateTime(loan.expiredDate, 'd')}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_LOAN_DESCRIPTION")}>
                            {loan.description || ''}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_LOAN_STATUS")}>
                            {DLang('LOAN_STATUS', loan.loanStatus)}
                        </NamedValue>
                    </Col1>
                    <Col1 className={style['operation-list']}>
                        <NamedValue name={Lang("MODAL__OPERATIONS")}>
                            {operations.items.map(_ => (
                                <div
                                    key={_.id}
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
    loanId: string;
    gamerId: string;
    onClose: Function;
    [id: string]: any;
}
const connected_LoanDialog = connect<{}, {}, _IProps, IStore>((store, props): IProps => {
    return {
        gamerId: props.gamerId,
        isDisplayed: props.isDisplayed,
        onClose: props.onClose,
        operations: store.operations.GetOperations(props.loanId),
        loan: store.gamers.GetGamer(props.gamerId).GetLoan(props.loanId)
    };
})(_LoanDialog);

export { connected_LoanDialog as ShowLoanDialog }; 