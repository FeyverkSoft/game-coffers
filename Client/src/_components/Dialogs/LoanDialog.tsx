import * as React from "react";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button } from "..";
import { Lang, ILoanView } from "../../_services";
import { gamerInstance } from "../../_actions";
import { connect } from "react-redux";
import { IStore } from "../../_helpers";

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
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('NEW_CHAR_MODAL')}
                onCancel={() => this.onClose()}
            >

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
        loan: store.gamers.gamersList[props.gamerId].loans[props.loanId]
    };
})(_LoanDialog);

export { connected_LoanDialog as ShowLoanDialog }; 