import * as React from "react";
import style from "./dialog.module.less";
import { BaseReactComp, IStatedField } from "../BaseReactComponent";
import { Dialog, Form, Col1, Input, Button } from "..";
import { Lang } from "../../_services";
import { profileInstance } from "../../_actions";
import { connect } from "react-redux";
import { Toggle } from "../Input/Toggle";

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
    isMain: IStatedField<boolean | undefined>;
    isLoad: boolean;
}
class _SelfAddCharDialog extends BaseReactComp<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            name: { value: undefined },
            className: { value: undefined },
            isMain: { value: false },
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
            this.props.dispatch(profileInstance.AddCharacters({
                gamerId: this.props.userId,
                name: this.state.name.value || '',
                className: this.state.className.value || '',
                isMain: this.state.isMain.value || false,
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
        const { name, className, isMain } = this.state;
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
                        <Toggle
                            label={Lang('IS_MAIN_CHAR')}
                            onChange={this.onInputVal}
                            isRequired={true}
                            path='isMain'
                            value={`${isMain.value}`}
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

const connected_SelfAddCharDialog = connect<{}, {}, IProps, {}>((state, props): IProps => {
    return {
        ...props
    };
})(_SelfAddCharDialog);

export { connected_SelfAddCharDialog as SelfAddCharDialog }; 