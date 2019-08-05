import * as React from "react";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button, MaterialSelect, Item } from "..";
import { Lang, OperationTypeList, DLang, OperationType } from "../../_services";
import { operationsInstance } from "../../_actions";
import { connect } from "react-redux";
import { getGuid, IF, IStore } from "../../_helpers";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    guildId: string;
    onClose: Function;
    users: Array<Item>;
    [id: string]: any;
}

interface IState {
    id: string;
    type: OperationType;

    fromUserId: IStatedField<string | undefined>;
    toUserId: IStatedField<string | undefined>;
    amount: IStatedField<string | undefined>;
    description: IStatedField<string | undefined>;
    penaltyId: IStatedField<string | undefined>;
    loanId: IStatedField<string | undefined>;

    isLoad: boolean;
}
class _CreateOperationDialog extends BaseReactComp<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            id: getGuid(),
            type: 'Emission',
            fromUserId: { value: undefined },
            toUserId: { value: undefined },
            amount: { value: undefined },
            description: { value: undefined },
            penaltyId: { value: undefined },
            loanId: { value: undefined },
            isLoad: false
        }
    }

    clear = () => {
        this.setState({
            id: getGuid(),
            fromUserId: { value: undefined },
            toUserId: { value: undefined },
            amount: { value: undefined },
            description: { value: undefined },
            penaltyId: { value: undefined },
            loanId: { value: undefined },
            isLoad: false
        })
    }

    onChangeType = <T extends {} = any>(val: T, valid: boolean, path: string): void => {
        this.onInput(val, valid, path);
        this.clear();
    }

    onClose = () => {
        this.clear();
        this.props.onClose();
    }

    handleSubmit = () => {
        this.setState({ isLoad: true });
    }

    render() {
        const { type, amount, description, fromUserId, toUserId } = this.state;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('NEW_OPERATION_MODAL')}
                onCancel={() => this.onClose()}
            >
                <Form
                    onSubmit={() => this.handleSubmit()}
                    direction="vertical"
                >
                    <Col1>
                        <MaterialSelect
                            items={OperationTypeList.map(t => new Item(t, DLang('OPERATIONS_TYPE', t)))}
                            value={type}
                            path="type"
                            onChange={this.onChangeType}
                            type='default'
                        ></MaterialSelect>
                    </Col1>
                    <Col1>
                        <Input
                            label={Lang('OPERATION_AMOUNT')}
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
                            label={Lang('OPERATION_DESCRIPTION')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='description'
                            value={description.value}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <IF value={type} in={['Tax', 'Exchange', 'InternalEmission', 'Other']}>
                        <Col1>
                            <MaterialSelect
                                items={this.props.users}
                                label={Lang('OPERATION_FROMUSERID')}
                                onChange={this.onInputVal}
                                isRequired={true}
                                path='fromUserId'
                                type='default'
                                value={fromUserId.value}
                                isRequiredMessage={Lang('IsRequired')}
                            />
                        </Col1>
                    </IF>
                    <IF value={type} in={['Output', 'InternalOutput', 'Other']}>
                        <Col1>
                            <MaterialSelect
                                items={this.props.users}
                                label={Lang('OPERATION_TOUSERID')}
                                onChange={this.onInputVal}
                                isRequired={true}
                                path='toUserId'
                                type='default'
                                value={toUserId.value}
                                isRequiredMessage={Lang('IsRequired')}
                            />
                        </Col1>
                    </IF>
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

interface _IProps {
    isDisplayed: boolean;
    guildId: string;
    onClose: Function;
    [id: string]: any;
}

const connected_CreateOperationDialog = connect<{}, {}, _IProps, IStore>((state: IStore, props): IProps => {
    const { gamersList } = state.gamers;
    var users = Object.keys(gamersList).map(k => gamersList[k]).map(_ => new Item(_.id, `${_.name} - ${_.characters[0]}`));
    let _temp: any = Object.keys(gamersList)
        .map(k => gamersList[k])
        .map(_ => Object.keys(_.loans)
            .map(l => {
                return {
                    user: _.name,
                    id: _.loans[l].id,
                    description: `${_.loans[l].amount} - ${_.loans[l].description}}`,
                    loanStatus: _.loans[l].loanStatus
                }
            })
        );
    var loans = [].concat(..._temp).filter((_: any) => _.loanStatus == 'Active' || _.loanStatus == 'Expired');
    return {
        ...props,
        users: users
    };
})(_CreateOperationDialog);

export { connected_CreateOperationDialog as CreateOperationDialog }; 