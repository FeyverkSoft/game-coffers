import React from 'react';
import { connect } from 'react-redux';
import { Form, Input, Button, Modal, Switch } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { IStore } from '../../_helpers';
import { Lang } from '../../_services';
import { profileInstance } from '../../_actions';

interface FormProps {
    isLoading: boolean,
    visible: boolean;
    onClose(): void;
    Add(name: string, className: string, isMain: boolean): void;
}
interface FormData {
    name: string;
    className: string;
    isMain: boolean;
}
class _ModalDialog extends React.Component<FormProps, any> {

    handleSubmit = (values: any) => {
        this.props.Add(values.name, values.className, values.isMain);
    };

    render() {
        const { isLoading, visible } = this.props;

        return (
            <Modal
                title={Lang('NEW_CHAR')}
                visible={visible}
                onCancel={this.props.onClose}
            >
                <Form
                    onFinish={this.handleSubmit}
                >
                    <Form.Item
                        name="Name"
                        rules={[
                            {
                                required: true,
                                message: 'Please input your name!',
                            },
                        ]}
                    >
                        <Input
                            prefix={<UserOutlined />}
                            placeholder={Lang('NAME')}
                        />
                    </Form.Item>
                    <Form.Item
                        name="ClassName"
                        rules={[
                            {
                                required: true,
                                message: 'Please input your classname!',
                            }]}
                    >

                        <Input
                            prefix={<LockOutlined />}
                            placeholder={Lang('ClassName')}
                        />
                    </Form.Item>
                    <Form.Item
                        name="IsMain"
                    >
                        <Switch />
                    </Form.Item>
                    <Form.Item>
                        <Button
                            type="primary"
                            htmlType="submit"
                            loading={isLoading}
                        >
                            {Lang('Add')}
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        );
    }
}


const connectedModal = connect<{}, {}, any, IStore>(
    (state: IStore) => {
        const { session } = state;
        return {
            isLoading: session && session.holding,
        };
    },
    (dispatch: Function) => {
        return {
            Add: (name: string, className: string, isMain: boolean) => dispatch(profileInstance.AddChar(name, className, isMain)),
        }
    },
)(_ModalDialog);

export { connectedModal as AddCharDialog };
