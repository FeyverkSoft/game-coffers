import * as React from "react";
import memoize from 'lodash.memoize';
import style from "./dialog.module.less";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button, MaterialSelect, Item } from "..";
import { Lang, OperationTypeList, DLang, OperationType, IGamersListView } from "../../_services";
import { operationsInstance } from "../../_actions";
import { connect } from "react-redux";
import { getGuid, IF, IStore } from "../../_helpers";
import { Dictionary } from "../../core";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    users: Array<Item>;
    loans: Array<Item>;
    penalties: Array<Item>;

    onClose: Function;
    onSuccess: Function;
    [id: string]: any;
}

interface IState {
    id: string;
    type: OperationType;

    fromUserId: IStatedField<string | undefined>;
    toUserId: IStatedField<string | undefined>;
    amount: IStatedField<number | undefined>;
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

    GetDesc = (): string => {
        let { description, type, fromUserId, loanId, penaltyId } = this.state;
        let { users, loans, penalties } = this.props;
        switch (type) {
            case 'Tax':
                return `Уплата налога игроком ${users.filter(_ => _.value == fromUserId.value)[0].name}: ${description.value}`;
            case 'Penalty':
                return `Уплата штрафа игроком ${penalties.filter(_ => _.value == penaltyId.value)[0].name}: ${description.value}`;
            case 'Loan':
                return `Погашение в пользу займа (${loans.filter(_ => _.value == loanId.value)[0].name}): ${description.value}`;
            case 'Sell':
                return `Операция продажи предметов со склада: ${description.value}`;
            default:
                return description.value || '';
        }
    }

    handleSubmit = () => {
        this.setState({ isLoad: true });
        this.props.dispatch(operationsInstance.CreateOperation({
            id: this.state.id,
            type: this.state.type,
            toUserId: this.state.toUserId.value,
            fromUserId: this.state.fromUserId.value,
            description: this.GetDesc(),
            amount: this.state.amount.value || 0,
            penaltyId: this.state.penaltyId.value,
            loanId: this.state.loanId.value,
            onFailure: () => {
                this.setState({ isLoad: false });
            },
            onSuccess: () => {
                this.setState({ isLoad: false });
                this.props.onSuccess();
                this.onClose();
            }
        }))
    }

    render() {
        const { type, amount, description, fromUserId, toUserId, loanId, penaltyId } = this.state;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={<span className={style['text']}>{Lang('NEW_OPERATION_MODAL')}</span>}
                onCancel={() => this.onClose()}
            >
                <Form
                    onSubmit={() => this.handleSubmit()}
                    direction="vertical"
                >
                    <Col1>
                        <MaterialSelect
                            label={Lang('OPERATION')}
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
                    <IF value={type} in={['Tax', 'Exchange', 'InternalEmission', 'Other', 'Deal']}>
                        <Col1>
                            <MaterialSelect
                                items={this.props.users}
                                label={Lang('OPERATION_FROMUSERID')}
                                onChange={this.onInputVal}
                                isRequired={type != 'Other' || (type == 'Other' && !toUserId.value)}
                                path='fromUserId'
                                type='default'
                                value={fromUserId.value}
                                isRequiredMessage={Lang('IsRequired')}
                            />
                        </Col1>
                    </IF>
                    <IF value={type} in={['Output', 'InternalOutput', 'Other', 'Deal']}>
                        <Col1>
                            <MaterialSelect
                                items={this.props.users}
                                label={Lang('OPERATION_TOUSERID')}
                                onChange={this.onInputVal}
                                isRequired={type != 'Other' || (type == 'Other' && !fromUserId.value)}
                                path='toUserId'
                                type='default'
                                value={toUserId.value}
                                isRequiredMessage={Lang('IsRequired')}
                            />
                        </Col1>
                    </IF>
                    <IF value={type} in={['Loan']}>
                        <Col1>
                            <MaterialSelect
                                items={this.props.loans}
                                label={Lang('OPERATION_LOANS')}
                                onChange={this.onInputVal}
                                isRequired={true}
                                path='loanId'
                                type='default'
                                value={loanId.value}
                                isRequiredMessage={Lang('IsRequired')}
                            />
                        </Col1>
                    </IF>
                    <IF value={type} in={['Penalty']}>
                        <Col1>
                            <MaterialSelect
                                items={this.props.penalties}
                                label={Lang('OPERATION_PENALTY')}
                                onChange={this.onInputVal}
                                isRequired={true}
                                path='penaltyId'
                                type='default'
                                value={penaltyId.value}
                                isRequiredMessage={Lang('IsRequired')}
                            />
                        </Col1>
                    </IF>
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

    onClose: Function;
    onSuccess: Function;
    [id: string]: any;
}

const Loans = memoize((gamersList: Dictionary<IGamersListView>): Array<Item> => {
    let _temp: any = Object.keys(gamersList)
        .map(k => gamersList[k])
        .map(_ => Object.keys(_.loans)
            .map(l => {
                return {
                    user: _.name,
                    id: _.loans[l].id,
                    description: `${_.loans[l].amount} - ${_.loans[l].description}`,
                    status: _.loans[l].loanStatus
                }
            })
        );
    return [].concat(..._temp)
        .filter((_: any) => _.status == 'Active' || _.status == 'Expired')
        .map((_: any) => new Item(_.id, `${_.user}: ${_.description}`));
}, it => { return JSON.stringify(it) });

const Penalties = memoize((gamersList: Dictionary<IGamersListView>): Array<Item> => {
    let _temp: any = Object.keys(gamersList)
        .map(k => gamersList[k])
        .map(_ => Object.keys(_.penalties)
            .map(l => {
                return {
                    user: _.name,
                    id: _.penalties[l].id,
                    description: `${_.penalties[l].amount} - ${_.penalties[l].description}`,
                    status: _.penalties[l].penaltyStatus
                }
            })
        );
    return [].concat(..._temp)
        .filter((_: any) => _.status == 'Active')
        .map((_: any) => new Item(_.id, `${_.user}: ${_.description}`));
}, it => { return JSON.stringify(it) });

const MemGamers = memoize(gms => gms, it => JSON.stringify(it));

const connected_CreateOperationDialog = connect<{}, {}, _IProps, IStore>((state: IStore, props): IProps => {
    const { gamersList } = state.gamers;
    return {
        ...props,
        users: MemGamers(Object.keys(gamersList).map(k => gamersList[k]).map(_ => new Item(_.id, `${_.name} - ${_.characters[0].name}`))),
        loans: Loans(gamersList),
        penalties: Penalties(gamersList)
    };
})(_CreateOperationDialog);

export { connected_CreateOperationDialog as CreateOperationDialog }; 