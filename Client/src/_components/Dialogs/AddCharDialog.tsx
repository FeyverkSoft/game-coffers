import * as React from "react";
import style from "./dialog.module.less";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button } from "..";
import { Lang } from "../../_services";
import { gamerInstance } from "../../_actions";
import { connect } from "react-redux";

interface IProps extends React.Props<any> {
    isDisplayed: boolean;
    userId: string;

    onClose: Function;
    onSuccess: Function;
    [id: string]: any;
}

interface IState {
    name: IStatedField<string | undefined>;
    className: IStatedField<string | undefined>;
    isLoad: boolean;
}
class _AddCharDialog extends BaseReactComp<IProps, IState> {

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
        if (this.props.userId)
            this.props.dispatch(gamerInstance.AddCharacters({
                gamerId: this.props.userId,
                name: this.state.name.value || '',
                className: this.state.className.value || '',
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
        const { name, className } = this.state;
        return (
            <Dialog
                isDisplayed={this.props.isDisplayed}
                title={<span className={style['text']}>{Lang('NEW_CHAR_MODAL')}</span>}
                onCancel={() => this.onClose()}
            >
                <Form
                    onSubmit={() => this.handleSubmit()}
                    direction="vertical"
                >
                    <Col1>
                        <Input
                            label={Lang('NAME')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='name'
                            value={name.value}
                            isRequiredMessage={Lang('IsRequired')}
                        />
                    </Col1>
                    <Col1>
                        <Input
                            label={Lang('CLASS_NAME')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='className'
                            value={className.value}
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

const connected_AddCharDialog = connect<{}, {}, IProps, {}>((state, props): IProps => {
    return {
        ...props
    };
})(_AddCharDialog);

export { connected_AddCharDialog as AddCharDialog }; 