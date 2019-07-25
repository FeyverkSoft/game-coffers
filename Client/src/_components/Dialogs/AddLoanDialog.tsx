import * as React from "react";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button } from "..";
import { Lang } from "../../_services";
import { gamerInstance } from "../../_actions";
import { connect } from "react-redux";
import { getGuid, formatDateTime } from "../../_helpers";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    userId: string;
    onClose: Function;
    [id: string]: any;
}

interface IState {
    id: string;
    description: IStatedField<string | undefined>;
    borrowDate: IStatedField<Date>;
    expiredDate: IStatedField<Date>;
    amount: IStatedField<number | undefined>;
    isLoad: boolean;
}

class _AddLoanDialog extends BaseReactComp<IProps, IState> {

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
        this.setState({
            id: getGuid(),
            description: { value: undefined },
            borrowDate: { value: new Date() },
            expiredDate: { value: new Date() },
            amount: { value: 0 },
            isLoad: false
        })
        this.props.onClose();
    }

    handleSubmit = () => {
        this.setState({ isLoad: true });
        if (this.props.userId)
            this.props.dispatch(gamerInstance.AddLoan({
                gamerId: this.props.userId,
                id: this.state.id,
                description: this.state.description.value || '',
                amount: this.state.amount.value || 0,
                borrowDate: this.state.borrowDate.value,
                expiredDate: this.state.expiredDate.value,
                onFailure: () => {
                    this.setState({ isLoad: false });
                },
                onSuccess: () => {
                    this.setState({ isLoad: false });
                    this.onClose();
                }
            }))
    }

    render() {
        const { description, borrowDate, expiredDate, amount } = this.state;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('NEW_LOAN_MODAL')}
                onCancel={() => this.onClose()}
            >
                <Form
                    onSubmit={() => this.handleSubmit()}
                    direction="vertical"
                >
                    <Col1>
                        <Input
                            label={Lang('LOAN_AMOUNT')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            type='number'
                            path='amount'
                            value={amount.value}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <Col1>
                        <Input
                            label={Lang('LOAN_BORROWDATE')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='borrowDate'
                            //type='date'
                            value={formatDateTime(borrowDate.value, 'd')}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <Col1>
                        <Input
                            label={Lang('LOAN_EXPIREDDATE')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='expiredDate'
                            //type='date'
                            value={formatDateTime(expiredDate.value, 'd')}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <Col1>
                        <Input
                            label={Lang('LOAN_DESCRIPTION')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='description'
                            value={description.value}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <Col1>
                        {<Button
                            isLoading={this.state.isLoad}
                            isSubmit={true}
                            disabled={!this.isValidForm(this.state)}
                            style={{ marginTop: "1rem" }}
                        >{Lang('ADD')}</Button>}
                    </Col1>
                </Form>
            </Dialog>
        );
    }
}

const connected_AddLoanDialog = connect<{}, {}, IProps, {}>((state, props): IProps => {
    return {
        ...props
    };
})(_AddLoanDialog);

export { connected_AddLoanDialog as AddLoanDialog }; 