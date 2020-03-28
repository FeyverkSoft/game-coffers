import React from 'react';
import { Form, Input, Modal } from 'antd';
import { ShoppingCartOutlined, BarsOutlined } from '@ant-design/icons';
import { getGuid } from '../../_helpers';
import { Lang } from '../../_services';

interface FormProps {
    isLoading?: boolean,
    visible: boolean;
    onClose(): void;
    onAdd(loanId: string, amount: number, description: string): void;
}

export const AddLoanDialog = ({ ...props }: FormProps) => {
    const { isLoading, visible } = props;
    const [form] = Form.useForm();
    const loanId = getGuid();
    return (
        <Modal
            title={Lang('NEW_LOAN_MODAL')}
            visible={visible}
            onCancel={() => props.onClose()}
            okText={Lang('Add')}
            confirmLoading={isLoading}
            onOk={() => {

                form.validateFields()
                    .then(values => {
                        form.resetFields();
                        props.onAdd(loanId, values.amount, values.description);
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
                    label={Lang('MODAL_LOAN_AMOUNT')}
                    rules={[
                        {
                            required: true,
                            message: 'Please input loan amount!',
                        },
                    ]}
                >
                    <Input
                        prefix={<ShoppingCartOutlined />}
                        type={'number'}
                        placeholder={Lang('MODAL_LOAN_AMOUNT')}
                    />
                </Form.Item>
                <Form.Item
                    name="description"
                    label={Lang('MODAL_LOAN_DESCRIPTION')}
                    rules={[
                        {
                            required: true,
                            message: 'Please input loan description!',
                        }]}
                >
                    <Input
                        prefix={<BarsOutlined />}
                        placeholder={Lang('MODAL_LOAN_DESCRIPTION')}
                    />
                </Form.Item>
            </Form>
        </Modal>
    );
}