import React, { RefObject, createRef } from 'react';
import { Form, Input, Modal, Select } from 'antd';
import { getGuid, IStore } from '../../_helpers';
import { Lang } from '../../_services';
import { Nest } from '../../_services/nest/Nest';
import { connect } from 'react-redux';
import { nestService } from '../../_services/nest/nest.service';
import { FormInstance } from 'antd/lib/form';
import { IDictionary } from '../../core';
import { nestsInstance } from '../../_actions/nest/nests.actions';

interface FormProps {
    isLoading?: boolean,
    visible: boolean;
    onClose(): void;
    onAdd(id: string, nestId: string, characterName: string, reward: string): void;
    isNestsLoading: boolean;
    nests: Array<Nest>;
    getAvailableNests: Function;
}
interface ISate { }
class addContractDialog extends React.Component<FormProps, ISate> {
    formRef: RefObject<FormInstance> = createRef<FormInstance>();
    RewardList: Array<string> = ['65Gold', '165Gold', '365Gold', '465Сounter', '465Mission6', '465Mission7', '465Mission8', '465Coop6', '465Coop6', '465Coop6'];
    state = { id: getGuid() };

    componentDidMount = () => {
        setTimeout(() => {
            if (this.formRef.current) {
                this.formRef.current.setFieldsValue({});
            }
            if (this.props.getAvailableNests)
                this.props.getAvailableNests();
        }, 150);
    }

    onFinish = (formData: IDictionary<string>) => {
        this.props.onAdd(this.state.id, formData.nestId, formData.characterName, formData.reward);
        this.setState({ id: getGuid() });
        this.props.onClose()
    }

    render = () => {
        const { visible, isLoading, onClose, isNestsLoading, nests } = this.props;
        return <Modal
            title={Lang('NEW_CONTRACT')}
            visible={visible}
            onCancel={() => { this.setState({ id: getGuid() }); onClose(); }}
            okText={Lang('Add')}
            confirmLoading={isLoading}
            okButtonProps={{ form: 'add-contr-form', htmlType: 'submit' }}
        >
            <Form
                layout='vertical'
                id='add-contr-form'
                initialValues={{ type: 'Other' }}
                onFinish={this.onFinish}
                ref={this.formRef}
            >
                <Form.Item
                    name="nestId"
                    label={Lang('NEST_NAME')}
                    rules={[
                        {
                            required: true,
                            message: 'Please select nest!',
                        },
                    ]}
                >
                    <Select
                        showSearch
                        placeholder={Lang('NEST_NAME')}
                        loading={isNestsLoading}
                        filterOption={(input: string, option) => {
                            return option ? option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0 : false
                        }}
                    >
                        {
                            nests.map(t => <Select.Option key={t.id} value={t.id}>{t.name}</Select.Option>)
                        }
                    </Select>
                </Form.Item>
                <Form.Item
                    name="characterName"
                    label={Lang('CHAR_NAME')}
                    rules={[
                        {
                            required: true,
                            message: 'Please input character mame!',
                        },
                    ]}
                >
                    <Input
                        placeholder={Lang('CHAR_NAME')}
                    />
                </Form.Item>
                <Form.Item
                    name="type"
                    label={Lang('REWARD')}
                    rules={[
                        {
                            required: false,
                            message: 'Please input reward!',
                        }]}
                >
                    <Select
                        placeholder={Lang('REWARD')}
                    >
                        {
                            this.RewardList.map((t: string) => <Select.Option key={t} value={t}>{Lang(t)}</Select.Option>)
                        }
                    </Select>
                </Form.Item>
            </Form>
        </Modal>
    }
}


interface AddContractDialogProps {
    nests: Array<Nest>;
}
const connectedAddContractDialog = connect<{}, {}, any, IStore>(
    (state: IStore, props: AddContractDialogProps) => {
        const { nests } = state.nests;
        return {
            nests: nests,
            isNestsLoading: nests.holding
        };
    },
    (dispatch: any) => {
        return {
            onAdd: (id: string,
                nestId: string,
                characterName: string,
                reward: string
            ) => dispatch(nestsInstance.addContract({
                id: String(id),
                nestId: String(nestId),
                characterName: String(characterName),
                reward: String(reward)
            })),
            getAvailableNests: () => dispatch(nestService.getNestList)
        }
    }
)(addContractDialog);

export { connectedAddContractDialog as AddContractDialog };