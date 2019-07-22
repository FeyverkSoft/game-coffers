import * as React from "react";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button, DateTimeInput, MaterialSelect } from "..";
import { Lang, GamerRankList, DLang, GamerStatusList, GamerRank, GamerStatus } from "../../_services";
import { Item } from "../Input/SelectList";
import { guildInstance } from "../../_actions";
import { getGuid } from "../../_helpers";
import { connect } from "react-redux";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    guildId: string;
    onClose: Function;
    [id: string]: any;
}
interface IState {
    id: string;
    name: IStatedField<string | undefined>;
    rank: GamerRank;
    status: GamerStatus;
    dateOfBirth: IStatedField<Date>;
    login: IStatedField<string | undefined>;
    isLoad: boolean;
}
class _AddUserDialog extends BaseReactComp<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            id: getGuid(),
            name: { value: undefined },
            rank: 'Beginner',
            status: 'Active',
            dateOfBirth: { value: new Date() },
            login: { value: undefined },
            isLoad: false
        }
    }

    onClose = () => {
        this.setState({
            id: getGuid(),
            name: { value: undefined },
            rank: 'Beginner',
            status: 'Active',
            dateOfBirth: { value: new Date() },
            login: { value: undefined },
            isLoad: false
        })
        this.props.onClose();
    }

    handleSubmit = () => {
        this.setState({ isLoad: true });
        if (this.props.guildId)
            this.props.dispatch(guildInstance.AddUser({
                guildId: this.props.guildId,
                id: this.state.id,
                name: this.state.name.value || '',
                rank: this.state.rank,
                status: this.state.status,
                dateOfBirth: this.state.dateOfBirth.value,
                login: this.state.login.value || '',
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
        const { name, rank, status, dateOfBirth, login } = this.state;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('NEW_USER_MODAL')}
                onCancel={() => this.onClose()}
            >
                <Form
                    onSubmit={() => this.handleSubmit()}
                    direction="vertical"
                >
                    <Col1>
                        <Input
                            label={'Name'}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='name'
                            value={name.value}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <Col1>
                        <Input
                            label={'Login'}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='login'
                            value={login.value}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <Col1>
                        <Input
                            label={'DateOfBirth'}
                            onChange={this.onInputVal}
                            isRequiredMessage={Lang('IsRequired')}
                            isRequired={true}
                            path='dateOfBirth'
                            //type='date'
                            value={dateOfBirth.value.toISOString()}
                        />
                    </Col1>
                    <Col1>
                        <MaterialSelect
                            items={GamerRankList.map(t => new Item(t, DLang('USER_ROLE', t)))}
                            value={rank}
                            path="rank"
                            onChange={this.onInput}
                            type='default'
                        ></MaterialSelect>
                    </Col1>
                    <Col1>
                        <MaterialSelect
                            items={GamerStatusList.map(t => new Item(t, DLang('USER_STATUS', t)))}
                            value={status}
                            path="status"
                            onChange={this.onInput}
                            type='default'
                        ></MaterialSelect>
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

const connected_AddUserDialog = connect<{}, {}, IProps, {}>((state, props): IProps => {
    return {
        ...props
    };
})(_AddUserDialog);

export { connected_AddUserDialog as AddUserDialog }; 