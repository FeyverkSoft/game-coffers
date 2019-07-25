import * as React from "react";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button, NamedValue } from "..";
import { Lang, ILoanView, LangF, DLang } from "../../_services";
import { gamerInstance } from "../../_actions";
import { connect } from "react-redux";
import { IStore } from "../../_helpers";
import { Grid } from "../Grid/Grid";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    loan: ILoanView;
    onClose: Function;
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


    render() {
        const { loan } = this.props;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('SHOW_LOAN_MODAL')}
                onCancel={() => this.onClose()}
            >
                <Grid
                    direction="vertical"
                >
                    <Col1>
                        <NamedValue name={Lang("MODAL_OAN_AMOUNT")}>
                            {loan.amount}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_LOAN_DATE")}>
                            {loan.date}
                        </NamedValue>
                    </Col1>
                    <Col1>
                        <NamedValue name={Lang("MODAL_LOAN_EXPIREDDATE")}>
                            {loan.expiredDate}
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
                </Grid>
            </Dialog>
        );
    }
}

interface _IProps extends React.Props<any> {
    isDisplayed: boolean;
    loanId: string;
    onClose: Function;
    [id: string]: any;
}
const connected_LoanDialog = connect<{}, {}, _IProps, IStore>((store, props): IProps => {
    return {
        isDisplayed: props.isDisplayed,
        onClose: props.onClose,
        loan: props.isDisplayed ? store.gamers.gamersList[props.gamerId].loans[props.loanId] : {} as ILoanView
    };
})(_LoanDialog);

export { connected_LoanDialog as ShowLoanDialog }; 