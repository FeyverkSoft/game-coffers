import React from 'react';
import { Form, Input, Modal } from 'antd';
import { ShoppingCartOutlined, BarsOutlined } from '@ant-design/icons';
import { getGuid } from '../../_helpers';
import { Lang } from '../../_services';

interface FormProps {
    isLoading?: boolean,
    visible: boolean;
    onClose(): void;
    onAdd(penaltyId: string, amount: number, description: string): void;
}

export const AddPenaltyDialog = ({ ...props }: FormProps) => {
    const { isLoading, visible } = props;
    const [form] = Form.useForm();
    const penaltyId = getGuid();
    return (
        <Modal
            title={Lang('NEW_PENALTY_MODAL')}
            visible={visible}
            onCancel={() => props.onClose()}
            okText={Lang('Add')}
            confirmLoading={isLoading}
            onOk={() => {

                form.validateFields()
                    .then(values => {
                        form.resetFields();
                        props.onAdd(penaltyId, values.amount, values.description);
                        props.onClose();
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
                    name="amount"
                    label={Lang('MODAL_PENALTY_AMOUNT')}
                    rules={[
                        {
                            required: true,
                            message: 'Please input penalty amount!',
                        },
                    ]}
                >
                    <Input
                        prefix={<ShoppingCartOutlined />}
                        type={'number'}
                        placeholder={Lang('MODAL_PENALTY_AMOUNT')}
                    />
                </Form.Item>
                <Form.Item
                    name="description"
                    label={Lang('MODAL_PENALTY_DESCRIPTION')}
                    rules={[
                        {
                            required: true,
                            message: 'Please input penalty description!',
                        }]}
                >
                    <Input
                        prefix={<BarsOutlined />}
                        placeholder={Lang('MODAL_PENALTY_DESCRIPTION')}
                    />
                </Form.Item>
            </Form>
        </Modal>
    );
}