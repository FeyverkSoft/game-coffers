import React from 'react';
import { connect } from 'react-redux';
import { Form, Input, Button, Modal, Switch } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { IStore, getGuid } from '../../_helpers';
import { Lang } from '../../_services';
import { profileInstance } from '../../_actions';

interface FormProps {
    isLoading: boolean,
    visible: boolean;
    onClose(): void;
    Add(id: string, name: string, className: string, isMain: boolean): void;
}

const _ModalDialog = ({ ...props }: FormProps) => {
    const { isLoading, visible } = props;
    const [form] = Form.useForm();
    const id = getGuid();
    return (
        <Modal
            title={Lang('NEW_CHAR')}
            visible={visible}
            onCancel={props.onClose}
            okText={Lang('Add')}
            confirmLoading={isLoading}
            onOk={() => {

                form.validateFields()
                    .then(values => {
                        form.resetFields();
                        props.Add(id, values.name, values.className, values.isMain);
                    })
                    .catch(info => {
                        console.log('Validate Failed:', info);
                    });
            }}
        >
            <Form
                form={form}
                layout='vertical'
            >
                <Form.Item
                    name="name"
                    label={Lang('Name')}
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
                    name="className"
                    label={Lang('CLASS_NAME')}
                    rules={[
                        {
                            required: true,
                            message: 'Please input your classname!',
                        }]}
                >

                    <Input
                        prefix={<LockOutlined />}
                        placeholder={Lang('CLASS_NAME')}
                    />
                </Form.Item>
                <Form.Item
                    name="isMain"
                    rules={[]}
                    label={Lang('CHAR_IS_MAIN')}
                >
                    <Switch />
                </Form.Item>
            </Form>
        </Modal>
    );
}


const connectedModal = connect<{}, {}, any, IStore>(
    (state: IStore) => {
        const { session } = state;
        return {
            isLoading: session && session.holding,
        };
    },
    (dispatch: Function, props: any) => {
        return {
            Add: (id: string, name: string, className: string, isMain: boolean) => {
                dispatch(profileInstance.AddChar(id, name, className, isMain));
                props.onClose();
            },
        }
    },
)(_ModalDialog);

export { connectedModal as AddCharDialog };
