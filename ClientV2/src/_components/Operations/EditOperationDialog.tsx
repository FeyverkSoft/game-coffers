import React, { RefObject, createRef } from 'react';
import { Modal, Form, Select } from 'antd';
import { Lang, DLang, OperationTypeList, OperationType, IGamersListView, IOperationView } from '../../_services';
import { connect } from 'react-redux';
import { IStore } from '../../_helpers/store';
import { operationsInstance } from '../../_actions';
import { IAvailableDocument } from '../../_services/operation/IAvailableDocuments';
import { IDictionary } from '../../core';
import { IF } from '../../_helpers';
import { FormInstance } from 'antd/lib/form';

interface IEditOperationDialog {
    availableDocuments: Array<IAvailableDocument>;
    isDocLoading?: boolean;
    visible?: boolean;
    users: Array<IGamersListView>;
    operation: IOperationView;

    onClose(): void;
    onEdit(id: string, type: OperationType, documentId: string): void;
    getAvailableDocuments(): void;
}
interface ISate {
    showDocument: boolean;
    documentType?: OperationType;
}
class editOperationDialog extends React.Component<IEditOperationDialog, ISate> {
    formRef: RefObject<FormInstance> = createRef<FormInstance>();
    state: ISate = {
        showDocument: false
    }

    componentDidMount = () => {
        setTimeout(() => {
            if (this.formRef.current) {
                const { operation } = this.props;
                this.formRef.current.setFieldsValue(operation);
            }
        }, 150);
    }

    onFinish = (formData: IDictionary<string>) => {
        this.props.onEdit(this.props.operation.id, formData.type as OperationType, formData.documentId);
        this.props.onClose()
    }

    onChange = (value: any, store: IDictionary<string>) => {
        if (store.type === 'Loan' || store.type === 'Penalty') {
            if (!this.state.showDocument)
                this.props.getAvailableDocuments();
            this.setState({
                showDocument: true,
                documentType: store.type as OperationType
            });
        } else {
            this.setState({
                showDocument: false,
                documentType: store.type as OperationType
            });
        }
    }

    render = () => {
        const { isDocLoading, availableDocuments, visible, operation } = this.props;
        return (
            <Modal
                title={Lang('EDIT_OPERATION_MODAL')}
                okButtonProps={{ form: 'edit-op-form', htmlType: 'submit' }}
                visible={visible}
                onCancel={() => this.props.onClose()}
            >
                <Form
                    layout='vertical'
                    id='edit-op-form'
                    onValuesChange={this.onChange}
                    onFinish={this.onFinish}
                    ref={this.formRef}
                >
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
                                            _.userId === operation.userId)
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
    operation: IOperationView;
    users: Array<IGamersListView>;
}
const connectedEditOperationDialog = connect<{}, {}, any, IStore>(
    (state: IStore, props: AddOperationDialogProps) => {
        const { documents } = state.operations;
        return {
            availableDocuments: documents,
            isDocLoading: documents.holding
        };
    },
    (dispatch: any) => {
        return {
            onEdit: (id: string,
                type: OperationType,
                documentId: string
            ) => dispatch(operationsInstance.editOperation({
                id,
                type,
                documentId
            })),
            getAvailableDocuments: () => dispatch(operationsInstance.getDocuments())
        }
    }
)(editOperationDialog);

export { connectedEditOperationDialog as EditOperationDialog };