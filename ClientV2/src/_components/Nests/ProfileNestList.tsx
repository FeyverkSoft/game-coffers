import * as React from 'react';
import { Table, Switch, Button, Tooltip } from 'antd';
import { DeleteFilled } from '@ant-design/icons';
import { Lang } from '../../_services';
import { Card } from '../Base/Card';
import { ICharacter } from '../../_services/profile/ICharacter';
import { ColumnProps } from 'antd/lib/table';
import { IHolded } from '../../core';
import { Contract } from '../../_services/nest/Contract';

export interface INestList {
    DeleteContract(id: string): void;
    AddContract: Function;
    nestContract: Array<Contract>;
    loading?: boolean;
}

export const ProfileNestList = ({ ...props }: INestList) => {
    const charactersMap: ColumnProps<Contract>[] = [
        {
            title: Lang('NEST_NAME'),
            dataIndex: 'nestName',
            key: 'nestName',
            render: (text: string, record: Contract) => {
                return {
                    children: text,
                };
            },
        },
        {
            title: Lang('CHAR_NAME'),
            dataIndex: 'characterName',
            key: 'characterName',
            render: (text: string, record: Contract) => {
                return {
                    children: text,
                };
            },
        },
        {
            title: Lang('REWARD'),
            dataIndex: 'reward',
            key: 'reward',
            render: (text: string, record: Contract) => {
                return {
                    children: text,
                };
            },
        },
        {
            title: Lang('CHAR_ACTIONS'),
            dataIndex: 'id',
            key: 'id',
            width: 50,
            fixed: 'right',
            render: (id: string, record: Contract & IHolded) => {
                return {
                    children: <div >
                        <Tooltip title={Lang('DELETE')}>
                            <Button
                                loading={record.holding}
                                type="link"
                                icon={<DeleteFilled />}
                                onClick={() => { }/* props.DeleteChar(record.id)*/}
                            />
                        </Tooltip>
                    </div>
                };
            },
        },
    ];

    return <Card
        title={Lang('PROFILE_NESTS')}
        size='small'
        loading={props.loading}
    >
        <Table
            size='middle'
            rowKey="id"
            columns={charactersMap}
            pagination={false}
            bordered={false}
            dataSource={props.nestContract}
        />
        <Button
            block
            onClick={() => props.AddContract()}
        >
            {Lang('ADD')}
        </Button>
    </Card>
};