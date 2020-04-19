import React from 'react';
import { Modal, Form, Select, Input } from 'antd';
import { Lang, DLang, OperationTypeList, OperationType, IGamersListView } from '../../_services';
import { connect } from 'react-redux';
import { IStore } from '../../_helpers/store';
import { getGuid, IF } from '../../_helpers';
import { operationsInstance } from '../../_actions';
import { IAvailableDocument } from '../../_services/operation/IAvailableDocuments';
import { IDictionary } from '../../core';

interface IAddOperationDialog {
    availableDocuments: Array<IAvailableDocument>;
    isDocLoading?: boolean;
    visible?: boolean;
    users: Array<IGamersListView>;

    onClose(): void;
    onAdd(id: string, type: OperationType, amount: number, description: string, userId: string, documentId: string): void;
    getAvailableDocuments(): void;
}
interface ISate {
    showDocument: boolean;
    documentType?: OperationType;
    userId?: string;
}
class addOperationDialog extends React.Component<IAddOperationDialog, ISate> {
    constructor(props: IAddOperationDialog) {
        super(props);
        this.state = {
            showDocument: false
        }
    }

    onFinish = (formData: IDictionary<string>) => {
        this.props.onAdd(getGuid(), formData.type as OperationType, Number(formData.amount), formData.description, formData.userId, formData.documentId);
        this.props.onClose()
    }

    onChange = (value: any, store: IDictionary<string>) => {
        if (store.type === 'Loan' || store.type === 'Penalty') {
            if (!this.state.showDocument)
                this.props.getAvailableDocuments();
            this.setState({
                showDocument: true,
                userId: store.userId,
                documentType: store.type as OperationType
            });
        } else {
            this.setState({
                showDocument: false,
                userId: store.userId,
                documentType: store.type as OperationType
            });
        }
    }

    render = () => {
        const { isDocLoading, availableDocuments, visible, users } = this.props;
        return (
            <Modal
                title={Lang('NEW_OPERATION_MODAL')}
                okButtonProps={{ form: 'add-op-form', htmlType: 'submit' }}
                visible={visible}
                onCancel={() => this.props.onClose()}
            >
                <Form
                    layout='vertical'
                    id='add-op-form'
                    onValuesChange={this.onChange}
                    onFinish={this.onFinish}
                >
                    <Form.Item
                        name="amount"
                        label={Lang('amount')}
                        rules={[
                            {
                                required: true,
                                message: 'Please input amount!',
                            },
                        ]}
                    >
                        <Input
                            type='number'
                            placeholder={Lang('amount')}
                        />
                    </Form.Item>
                    <Form.Item
                        name="userId"
                        label={Lang('OPERATION_TOUSERID')}
                    >
                        <Select
                            placeholder={Lang('OPERATION_TOUSERID')}
                            loading={isDocLoading}
                        >
                            {
                                users.map(t => <Select.Option key={t.id} value={t.id}>{`${t.name}`}</Select.Option>)
                            }
                        </Select>
                    </Form.Item>
                    <Form.Item
                        name="description"
                        label={Lang('description')}
                        rules={[
                            {
                                required: true,
                                message: 'Please input description!',
                            },
                        ]}
                    >
                        <Input
                            placeholder={Lang('description')}
                        />
                    </Form.Item>
                    <Form.Item
                        name="type"
                        label={Lang('OPERATION_TYPE')}
                        rules={[
                            {
                                required: false,
                                message: 'Please input document type!',
                            }]}
                    >
                        <Select
                            placeholder={Lang('OPERATION_TYPE')}
                        >
                            {
                                OperationTypeList.map(t => <Select.Option key={t} value={t}>{DLang('OPERATIONS_TYPE', t)}</Select.Option>)
                            }
                        </Select>
                    </Form.Item>
                    <IF value={this.state.showDocument}>
                        <Form.Item
                            name="documentId"
                            label={Lang('DOCUMENT')}
                        >
                            <Select
                                placeholder={Lang('DOCUMENT')}
                                loading={isDocLoading}
                            >
                                {
                                    availableDocuments
                                        .filter(_ => _.documentType === this.state.documentType &&
                                            _.userId === this.state.userId)
                                        .map(t => <Select.Option key={t.id} value={t.id}>{`${t.description}`}</Select.Option>)
                                }
                            </Select>
                        </Form.Item>
                    </IF>
                </Form>
            </Modal>
        );
    }
}

interface AddOperationDialogProps {
    loanId?: string;
    users: Array<IGamersListView>;
}
const connectedAddOperationDialog = connect<{}, {}, any, IStore>(
    (state: IStore, props: AddOperationDialogProps) => {
        const { documents } = state.operations;
        return {
            availableDocuments: documents,
            isDocLoading: documents.holding
        };
    },
    (dispatch: any) => {
        return {
            onAdd: (id: string,
                type: OperationType,
                amount: number,
                description: string,
                userId: string,
                documentId: string
            ) => dispatch(operationsInstance.createOperation({
                id,
                type,
                amount: Number(amount),
                description,
                userId,
                documentId
            })),
            getAvailableDocuments: () => dispatch(operationsInstance.getDocuments())
        }
    }
)(addOperationDialog);

export { connectedAddOperationDialog as AddOperationDialog };