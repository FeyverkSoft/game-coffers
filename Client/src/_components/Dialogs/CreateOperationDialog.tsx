import * as React from "react";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button, MaterialSelect, Item } from "..";
import { Lang, OperationTypeList, DLang, OperationType } from "../../_services";
import { operationsInstance } from "../../_actions";
import { connect } from "react-redux";
import { getGuid } from "../../_helpers";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    guildId: string;
    onClose: Function;
    [id: string]: any;
}

interface IState {
    id: string;
    type: OperationType;
    className: IStatedField<string | undefined>;
    isLoad: boolean;
}
class _CreateOperationDialog extends BaseReactComp<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            id: getGuid(),
            type: 'Emission',
            className: { value: undefined },
            isLoad: false
        }
    }

    onClose = () => {
        this.setState({
            id: getGuid(),
            type: 'Emission',
            className: { value: undefined },
            isLoad: false
        })
        this.props.onClose();
    }

    handleSubmit = () => {
        this.setState({ isLoad: true });
    }

    render() {
        const { type } = this.state;
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

const connected_CreateOperationDialog = connect<{}, {}, IProps, {}>((state, props): IProps => {
    return {
        ...props
    };
})(_CreateOperationDialog);

export { connected_CreateOperationDialog as CreateOperationDialog }; 