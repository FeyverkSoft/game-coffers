import React from 'react';
import { Form, Input, Modal, DatePicker, Select } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import { getGuid, IStore } from '../../_helpers';
import { Lang, GamerRankList, DLang } from '../../_services';
import { GamerStatus, GamerStatusList } from '../../_services/gamer/GamerStatus';
import { GamerRank } from '../../_services/profile/GamerRank';
import { connect } from 'react-redux';
import { gamerInstance } from '../../_actions/gamer/gamer.actions';

interface FormProps {
    isLoading?: boolean,
    visible: boolean;
    onClose(): void;
    onAdd(id: string, name: string, rank: GamerRank, status: GamerStatus, dateOfBirth: Date, login: string): void;
}

export const addUserDialog = ({ ...props }: FormProps) => {
    const { isLoading, visible } = props;
    const [form] = Form.useForm();
    const id = getGuid();
    return (<Modal
        title={Lang('NEW_USER_MODAL')}
        visible={visible}
        onCancel={() => props.onClose()}
        okText={Lang('Add')}
        confirmLoading={isLoading}
        onOk={() => {

            form.validateFields()
                .then(values => {
                    form.resetFields();
                    props.onAdd(id, values.name, values.rank, values.status, values.dateOfBirth, values.login);
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
                name="login"
                label={Lang('Login')}
                rules={[
                    {
                        required: true,
                        message: 'Please input login!',
                    },
                ]}
            >
                <Input
                    prefix={<UserOutlined />}
                    placeholder={Lang('Login')}
                />
            </Form.Item>
            <Form.Item
                name="name"
                label={Lang('Name')}
                rules={[
                    {
                        required: true,
                        message: 'Please input name!',
                    },
                ]}
            >
                <Input
                    prefix={<UserOutlined />}
                    placeholder={Lang('NAME')}
                />
            </Form.Item>
            <Form.Item
                name="dateOfBirth"
                label={Lang('DATEOFBIRTH')}
                rules={[
                    {
                        required: true,
                        message: 'Please input date Of Birth!',
                    }]}
            >

                <DatePicker
                    placeholder={Lang('DATEOFBIRTH')}
                />
            </Form.Item>
            <Form.Item
                name="rank"
                label={Lang('RANK')}
                rules={[
                    {
                        required: true,
                        message: 'Please select rank!',
                    }]}
            >

                <Select
                    placeholder={Lang('RANK')}
                >
                    {
                        GamerRankList.map(t => <Select.Option key={t} value={t}>{DLang('USER_RANK', t)}</Select.Option>)
                    }
                </Select>
            </Form.Item>
            <Form.Item
                name="status"
                label={Lang('STATUS')}
                rules={[
                    {
                        required: true,
                        message: 'Please input status!',
                    }]}
            >
                <Select
                    placeholder={Lang('STATUS')}
                >
                    {
                        GamerStatusList.map(t => <Select.Option key={t} value={t}>{DLang('USER_STATUS', t)}</Select.Option>)
                    }
                </Select>
            </Form.Item>
        </Form>
    </Modal>
    );
}

const connectedAddUserDialog = connect<{}, {}, any, IStore>(
    (state: IStore) => { return {} },
    (dispatch: any) => {
        return {
            onAdd: (id: string, name: string, rank: GamerRank, status: GamerStatus, dateOfBirth: Date, login: string) => dispatch(gamerInstance.addUser({ id: id, name: name, rank: rank, status: status, dateOfBirth: dateOfBirth, login: login })),
        }
    }
)(addUserDialog);

export { connectedAddUserDialog as AddUserDialog };