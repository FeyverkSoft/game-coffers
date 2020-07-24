import * as React from 'react';
import { Table, Switch, Button, Tooltip } from 'antd';
import { DeleteFilled } from '@ant-design/icons';
import { Lang } from '../../_services';
import { Card } from '../Base/Card';
import { ICharacter } from '../../_services/profile/ICharacter';
import { ColumnProps } from 'antd/lib/table';
import { IHolded } from '../../core';

export interface INestList {
    SetMainChar(charId: string): void;
    DeleteChar(charId: string): void;
    AddChar: Function;
    characters: Array<ICharacter>;
    loading?: boolean;
}

export const ProfileNestList = ({ ...props }: INestList) => {
    const charactersMap: ColumnProps<ICharacter>[] = [
        {
            title: Lang('NEST_NAME'),
            dataIndex: 'name',
            key: 'name',
            render: (text: string, record: ICharacter) => {
                return {
                    props: {
                        style: { fontWeight: record.isMain ? 500 : 300 },
                    },
                    children: text,
                };
            },
        },
        {
            title: Lang('CHAR_NAME'),
            dataIndex: 'className',
            key: 'className',
            render: (text: string, record: ICharacter) => {
                return {
                    props: {
                        style: { fontWeight: record.isMain ? 500 : 300 },
                    },
                    children: text,
                };
            },
        },
        {
            title: Lang('REWARD'),
            dataIndex: 'className',
            key: 'className',
            render: (text: string, record: ICharacter) => {
                return {
                    props: {
                        style: { fontWeight: record.isMain ? 500 : 300 },
                    },
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
            render: (id: string, record: ICharacter & IHolded) => {
                return {
                    children: <div >
                        <Tooltip title={Lang('DELETE')}>
                            <Button
                                loading={record.holding}
                                type="link"
                                icon={<DeleteFilled />}
                                onClick={() =>{}/* props.DeleteChar(record.id)*/}
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
            dataSource={[]/*props.characters*/}
        />
        <Button
            block
            onClick={() =>{} /*props.AddChar()*/}
        >
            {Lang('ADD')}
        </Button>
    </Card>
};