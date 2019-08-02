import * as React from "react";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button } from "..";
import { Lang } from "../../_services";
import { gamerInstance } from "../../_actions";
import { connect } from "react-redux";
import { getGuid } from "../../_helpers";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    userId: string;
    guildId: string;
    onClose: Function;
    [id: string]: any;
}
interface IState {
    id: string;
    amount: IStatedField<number>;
    description: IStatedField<string | undefined>;
    isLoad: boolean;
}

class _AddPenaltyDialog extends BaseReactComp<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            id: getGuid(),
            amount: { value: 0 },
            description: { value: undefined },
            isLoad: false
        }
    }

    onClose = () => {
        this.setState({
            id: getGuid(),
            amount: { value: 0 },
            description: { value: undefined },
            isLoad: false
        })
        this.props.onClose();
    }

    handleSubmit = () => {
        this.setState({ isLoad: true });
        if (this.props.userId)
            this.props.dispatch(gamerInstance.AddPenalty({
                gamerId: this.props.userId,
                amount: this.state.amount.value,
                description: this.state.description.value || '',
                id: this.state.id,
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
        const { amount, description } = this.state;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('NEW_PENALTY_MODAL')}
                onCancel={() => this.onClose()}
            >
                <Form
                    onSubmit={() => this.handleSubmit()}
                    direction="vertical"
                >
                    <Col1>
                        <Input
                            label={Lang('PENALTY_AMOUNT')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='amount'
                            type='number'
                            value={amount.value}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <Col1>
                        <Input
                            label={Lang('PENALTY_DESCRIPTION')}
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

const connected_AddPenaltyDialog = connect<{}, {}, IProps, {}>((state, props): IProps => {
    return {
        ...props
    };
})(_AddPenaltyDialog);

export { connected_AddPenaltyDialog as AddPenaltyDialog }; 