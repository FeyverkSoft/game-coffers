import React from "react";
import { Lang, IGamersListView } from '../_services';
import { ICharacter } from '../_services/guild/ICharacter';
import { Card, Table, Breadcrumb, PageHeader } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { memoize } from "lodash";
import { connect } from "react-redux";
import { IStore, BlendColor } from "../_helpers";
import { gamerInstance } from "../_actions";
import { ColumnProps } from "antd/lib/table";
import style from './bd.module.scss';
import { Content } from "../_components/Content/Content";
import { Link } from "react-router-dom";
import Search from "antd/lib/input/Search";
import { string } from "prop-types";

interface IGamerView {
    id: string;
    name: string;
    birthday: string;
    count: number;
    color: string;
}

interface IMainProps {
    isLoading: boolean;
    gamers: Array<IGamerView>;
    loadData: Function;
}
interface IBDState {
    filter: string;
}

const columns: ColumnProps<IGamerView>[] = [
    {
        title: Lang('NAME'),
        dataIndex: 'name',
        key: 'name',
        render: (text: string, record: any) => {
            return {
                props: {
                    style: { background: record.color },
                },
                children: text,
            };
        },
    },
    {
        title: Lang('DATEOFBIRTH'),
        dataIndex: 'birthday',
        key: 'birthday',
        render: (text: string, record: any) => {
            return {
                props: {
                    style: { background: record.color },
                },
                children: text,
            };
        },
    },
    {
        title: Lang('DAYS_COUNT'),
        dataIndex: 'count',
        key: 'count',
        defaultSortOrder: 'ascend',
        sorter: (a: IGamerView, b: IGamerView) => {
            return Number(a.count) - Number(b.count);
        },
        render: (text: string, record: any) => {
            return {
                props: {
                    style: { background: record.color },
                },
                children: text,
            };
        },
    },
];

export class _BirthdayController extends React.Component<IMainProps, IBDState> {
    constructor(props: IMainProps) {
        super(props);
        this.state = {
            filter: ''
        }
    }

    componentDidMount() {
        this.props.loadData();
    }

    render() {
        const { gamers, isLoading } = this.props;
        const { filter } = this.state;
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <Link to={"/"} >
                            <HomeOutlined />
                        </Link>
                    </Breadcrumb.Item>
                    <Breadcrumb.Item>
                        <Link to={"/birthday"}>
                            {Lang("BIRTHDAY_PAGE")}
                        </Link>
                    </Breadcrumb.Item>
                </Breadcrumb>
                <div className={style['bd']}>
                    <PageHeader
                        ghost={false}
                        subTitle="This is a subtitle"
                        title={Lang("BIRTHDAY_PAGE")}
                        className={style['ant-card']}
                    >
                        <Search
                            placeholder="введите текст для поиска"
                            enterButton='search'
                            onSearch={(value: string) => {
                                this.setState({ filter: value });
                            }}
                        />
                        {<Table
                            size='middle'
                            rowKey="id"
                            columns={columns}
                            pagination={false}
                            bordered={false}
                            dataSource={gamers
                                .filter(_ => _.name.toLowerCase().includes(filter.toLowerCase()))
                                .map(_ => {
                                    return {
                                        name: _.name,
                                        birthday: _.birthday,
                                        count: _.count,
                                        color: _.color,
                                        id: _.id
                                    }
                                })}
                        />}
                    </PageHeader>
                </div>
            </Content>
        );
    }
}

const MemGamers = memoize(gamersList => {
    let d = new Date();
    return Object.keys(gamersList)
        .map((k): IGamersListView => gamersList[k])
        .filter(g => !(g.status == 'Banned' || g.status == 'Left'))
        .map((_): IGamerView => {
            let f: any = new Date(_.dateOfBirth.getFullYear(), d.getMonth(), d.getDate());
            let n: any = new Date(_.dateOfBirth.getFullYear(), _.dateOfBirth.getMonth(), _.dateOfBirth.getDate());
            let res = Math.floor((f - n) / 86400000);
            let mo = _.dateOfBirth.getMonth() + 1;
            let dayCount = res > 0 ? 365 - res : -1 * res;
            return {
                id: _.id,
                name: `${_.name} - ${_.characters.length > 0 ? _.characters.firstOrDefault((_: ICharacter) => _.isMain, _.characters[0]).name : ''}`,
                birthday: `${_.dateOfBirth.getDate() > 9 ? _.dateOfBirth.getDate() : '0' + _.dateOfBirth.getDate()}-${mo > 9 ? mo : '0' + mo}`,
                count: dayCount,
                color: BlendColor('#36c13955', '#fb6d2955', (100 / 365) * dayCount) || '',
            };
        })
        .sort((a, b) => a.count - b.count)
}, it => JSON.stringify(it));

const connectedLoginForm = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { gamersList } = state.gamers;
        return {
            isLoading: Object.keys(gamersList).length === 0,
            gamers: MemGamers(gamersList)
        };
    },
    (dispatch: any) => {
        return {
            loadData: () => dispatch(
                (dispatch: Function, getState: Function) =>
                    dispatch(gamerInstance.GetGamers({ dateMonth: new Date() }))),
        }
    })(_BirthdayController);

export { connectedLoginForm as BirthdayController };
