import React from 'react';
import { connect } from 'react-redux';
import { Form, Input, Button, Result } from 'antd';
import { UserOutlined, LockOutlined, MailOutlined } from '@ant-design/icons';
import { IStore, getGuid } from '../../_helpers';
import { Lang } from '../../_services';
import { sessionInstance } from '../../_actions/session.actions';

interface RegFormProps {
    isLoading: boolean,
    guildId?: string;
    isNew: boolean;
    Reg(id: string, guildId: string, username: string, email: string, password: string): void;
}

interface ISate {
    id: string;
}

class _RegForm extends React.Component<RegFormProps, any> {
    state: ISate = {
        id: getGuid()
    };

    handleSubmit = (values: any) => {
        const guildId: string = this.props.guildId || values.guildId;
        this.props.Reg(this.state.id, guildId, values.username, values.email, values.password);
    };

    render() {
        const { isLoading, isNew } = this.props;
        if (isNew)
            return (
                <Result
                    status="success"
                    title="Регистрация успешно завершена!"
                    subTitle="Осталось подтвердить ваш емайл."
                />
            );
        return (
            <Form
                onFinish={this.handleSubmit}
            >
                <Form.Item
                    name="username"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your username!',
                        },
                    ]}
                >
                    <Input
                        itemID="name"
                        prefix={<UserOutlined />}
                        placeholder={Lang('NAME')}
                    />
                </Form.Item>
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
                        }]}
                >
                    <Input
                        prefix={<LockOutlined />}
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
                        {Lang('Reg')}
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
    (state: IStore, props: EmailFormProps) => {
        const { session } = state;
        return {
            isLoading: session && session.holding,
            guildId: props.guildId,
            isNew: session.isNew || false
        };
    },
    (dispatch: Function) => {
        return {
            Reg: (id: string, guildId: string, username: string, email: string, password: string) =>
                dispatch(sessionInstance.reg({ id: id, guildId: guildId, username: username, email: email, password: password })),
        }
    })(_RegForm);

export { connectedLoginForm as RegForm };
