import * as React from "react";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button } from "..";
import { Lang } from "../../_services";
import { operationsInstance } from "../../_actions";
import { connect } from "react-redux";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    guildId: string;
    onClose: Function;
    [id: string]: any;
}

interface IState {
    name: IStatedField<string | undefined>;
    className: IStatedField<string | undefined>;
    isLoad: boolean;
}
class _CreateOperationDialog extends BaseReactComp<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            name: { value: undefined },
            className: { value: undefined },
            isLoad: false
        }
    }

    onClose = () => {
        this.setState({
            name: { value: undefined },
            className: { value: undefined },
            isLoad: false
        })
        this.props.onClose();
    }

    handleSubmit = () => {
        this.setState({ isLoad: true });
    }

    render() {
        const { } = this.state;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={Lang('NEW_CHAR_MODAL')}
                onCancel={() => this.onClose()}
            >
                <Form
                    onSubmit={() => this.handleSubmit()}
                    direction="vertical"
                >
                   
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