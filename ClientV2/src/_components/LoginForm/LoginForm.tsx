import React from 'react';
import { connect } from 'react-redux';
import 'antd/dist/antd.css';
import { Form, Icon, Input, Button } from 'antd';
import { FormComponentProps } from 'antd/lib/form';
import { IStore } from '../../_helpers';
import { Lang } from '../../_services';
import { sessionInstance } from '../../_actions';

interface UserFormProps extends FormComponentProps {
    isLoading: boolean,
    LogIn(username: string, password: string): void;
}

class _LoginForm extends React.Component<UserFormProps> {
    handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        var isValid = true;
        this.props.form.validateFields((err, values) => {
            isValid = isValid && !err;
        });
        if (isValid) {
            this.props.LogIn(this.props.form.getFieldValue('username'),
                this.props.form.getFieldValue('password'));
        }
    };

    render() {
        const { getFieldDecorator } = this.props.form;
        const { isLoading } = this.props;
        return (
            <Form
                onSubmit={this.handleSubmit}
            >
                <Form.Item>
                    {getFieldDecorator('username', {
                        rules: [{ required: true, message: 'Please input your username!' }],
                    })(
                        <Input
                            prefix={<Icon type="user" />}
                            placeholder={Lang('NAME')}
                        />,
                    )}
                </Form.Item>
                <Form.Item>
                    {getFieldDecorator('password', {
                        rules: [{ required: true, message: 'Please input your Password!' }],
                    })(
                        <Input
                            prefix={<Icon type="lock" />}
                            type="password"
                            placeholder={Lang('PASSWORD')}
                        />,
                    )}
                </Form.Item>
                <Form.Item>
                    <Button
                        type="primary"
                        htmlType="submit"
                        loading={isLoading}
                    >
                        {Lang('Login')}
                    </Button>
                </Form.Item>
            </Form>
        );
    }
}
const LoginForm = Form.create({ name: 'login' })(_LoginForm);

const connectedLoginForm = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { session } = state;
        return {
            isLoading: session && session.holding,
        };
    },
    (dispatch: Function) => {
        return {
            LogIn: (username: string, password: string) => dispatch(sessionInstance.logIn(username, password)),
        }
    })(LoginForm);

export { connectedLoginForm as LoginForm };
