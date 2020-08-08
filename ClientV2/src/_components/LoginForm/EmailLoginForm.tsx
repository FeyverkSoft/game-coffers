import React from 'react';
import { connect } from 'react-redux';
import { Form, Input, Button } from 'antd';
import { LockOutlined, MailOutlined } from '@ant-design/icons';
import { IStore } from '../../_helpers';
import { Lang } from '../../_services';
import { sessionInstance } from '../../_actions/session.actions';

interface UserFormProps {
    isLoading: boolean,
    guildId?: string;
    LogIn(guildId: string, email: string, password: string): void;
}

class _EmailLoginForm extends React.Component<UserFormProps, any> {
    handleSubmit = (values: any) => {
        const guildId: string = this.props.guildId || values.guildId;
        this.props.LogIn(guildId, values.email, values.password);
    };

    render() {
        const { isLoading } = this.props;
        return (
            <Form
                onFinish={this.handleSubmit}
            >
                <Form.Item
                    name="email"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your email!',
                        },
                        {
                            type: 'email',
                            message: 'is not validate email!',
                        }
                    ]}
                >
                    <Input
                        itemID="email"
                        prefix={<MailOutlined />}
                        placeholder={Lang('Email')}
                    />
                </Form.Item>
                <Form.Item
                    name="password"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your password!',
                        },
                        {
                            message: 'Your password is too short.!',
                            min: 8
                        }]}
                >
                    <Input
                        prefix={<LockOutlined />}
                        itemID="password"
                        type="password"
                        placeholder={Lang('PASSWORD')}
                    />
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

interface EmailFormProps {
    guildId?: string;
}
const connectedLoginForm = connect<{}, {}, EmailFormProps & any, IStore>(
    (state: IStore, props: any) => {
        const { session } = state;
        return {
            isLoading: session && session.holding,
            guildId: props.guildId
        };
    },
    (dispatch: Function) => {
        return {
            LogIn: (guildId: string, email: string, password: string) => dispatch(sessionInstance.logInByEmail(guildId, email, password)),
        }
    })(_EmailLoginForm);

export { connectedLoginForm as EmailLoginForm };
