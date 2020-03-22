import React from "react";
import { Lang, IGamersListView, DLang, LangF } from '../_services';
import { Table, Breadcrumb, PageHeader, Row, Col, DatePicker, Layout } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { connect } from "react-redux";
import { IStore, formatDateTime } from "../_helpers";
import { gamerInstance, guildInstance } from "../_actions";
import { ColumnProps } from "antd/lib/table";
import style from './bd.module.scss';
import { Content } from "../_components/Content/Content";
import { Link } from "react-router-dom";
import Search from "antd/lib/input/Search";
import moment, { Moment } from "moment";
import { Dictionary } from "../core";
import { Character, Characters } from "../_components/Character/Character";
import { Card } from "../_components/Base/Card";
import { AddCharDialog } from "../_components/Character/AddCharDialog";


interface IMainProps {
    isLoading: boolean;
    gamers: Dictionary<Dictionary<IGamersListView>>;
    loadGuildInfo(): void;
    loadData(date: Date): void;
    deleteCharacter(userId: string, characterId: string): void;
}

interface IState {
    addCharacterModal: {
        show: boolean;
        userId?: string;
    };
    filter: string;
    date: Date;
    columns: ColumnProps<IGamersListView>[];
}

export class _CofferController extends React.Component<IMainProps, IState> {
    constructor(props: IMainProps) {
        super(props);
        this.state = {
            addCharacterModal: {
                show: false,
                userId: undefined
            },
            filter: '',
            date: new Date(),
            columns: [
                {
                    title: Lang('USER'),
                    dataIndex: 'name',
                    key: 'name',
                    defaultSortOrder: 'ascend',
                    sorter: (a: IGamersListView, b: IGamersListView) => {

                        return a.name === b.name ? 0 : (a.name > b.name ? 1 : -1);
                    },
                    render: (value: string, record: IGamersListView, index: number) => {
                        return <div>
                            <Col style={{
                                fontWeight: 500,
                                padding: '.25rem .5rem',
                            }}>
                                {LangF('USER_CHAR_LIST', value)}
                            </Col>
                            <Col>
                                <Characters>
                                    {Object.keys(record.characters).map(_ => <Character
                                        key={record.characters[_].id}
                                        character={record.characters[_]}
                                        onDeleteChar={this.onDeleteChar}
                                    />)}
                                </Characters>
                            </Col>
                        </div>
                    }
                },
                {
                    title: Lang('USER_ROW_STATUS'),
                    dataIndex: 'status',
                    key: 'status',
                    defaultSortOrder: 'ascend',
                    sorter: (a: IGamersListView, b: IGamersListView) => {

                        return a.status === b.status ? 0 : (a.status > b.status ? 1 : -1);
                    },
                    render: (value: string, record: IGamersListView, index: number) => {
                        return DLang('USER_STATUS', value);
                    }
                },
                {
                    title: Lang('USER_ROW_RANK'),
                    dataIndex: 'rank',
                    key: 'rank',
                    defaultSortOrder: 'ascend',
                    sorter: (a: IGamersListView, b: IGamersListView) => {

                        return a.rank === b.rank ? 0 : (a.rank > b.rank ? 1 : -1);
                    },
                    render: (value: string, record: IGamersListView, index: number) => {
                        return DLang('USER_RANK', value);
                    }
                },
                {
                    title: Lang('USER_ROW_BALANCE'),
                    dataIndex: 'balance',
                    key: 'balance',
                    defaultSortOrder: 'ascend',
                    sorter: (a: IGamersListView, b: IGamersListView) => {

                        return a.balance - b.balance;
                    },
                    render: (value: number, record: IGamersListView, index: number) => {
                        return <div
                            style={{ color: value > 0 ? 'green' : 'red' }}
                        >
                            {value}
                        </div>;
                    }
                },
            ]
        }
    }

    onAddCharacter = (id: string, name: string, className: string, isMain: boolean) => {

    };

    onDeleteChar = (id: string, userId: string) => {
        this.props.deleteCharacter(userId, id);
    };

    loadData = () => {
        const { date } = this.state;
        this.props.loadData(date);
        this.props.loadGuildInfo();
    }

    componentDidMount() {
        this.loadData();
    }

    onSelectDate = (value: Moment | null, dateString: string) => {
        if (value) {
            this.setState({ date: value.toDate() }, this.loadData);
        }
    };

    getGamers = () => {
        const { date, filter } = this.state;
        const { gamers } = this.props;
        const strDate: string = formatDateTime(date, 'm');
        const datedGamers = Object.keys(gamers[strDate] || {}).map(_ => gamers[strDate][_]);

        return datedGamers.filter(_ => {
            return _.name.toLowerCase().includes(filter) ||
                _.balance.toString().includes(filter) ||
                Object.keys(_.characters).map(c => _.characters[c]).filter(c => c.name.toLowerCase().includes(filter)).length > 0 ||
                formatDateTime(_.dateOfBirth, 'h').includes(filter) ||
                filter === '';
        })
    }

    toggleAddCharModal = (userId?: string) => {
        this.setState({ addCharacterModal: { show: !this.state.addCharacterModal.show, userId } });
    }

    render() {
        const { isLoading } = this.props;
        const { date } = this.state;
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <Link to={"/"} >
                            <HomeOutlined />
                        </Link>
                    </Breadcrumb.Item>
                    <Breadcrumb.Item>
                        <Link to={"/"}>
                            {Lang("MAIN_PAGE")}
                        </Link>
                    </Breadcrumb.Item>
                </Breadcrumb>
                <Layout>
                    <Row gutter={[16, 16]}>
                    </Row>
                    <Row gutter={[16, 16]}>
                        <Col xs={24} sm={24} md={24} lg={24} xl={24}>
                            <Card
                                title={Lang('USERS')}
                                size='small'
                            >
                                <Row gutter={[16, 16]}>
                                    <Col xs={8} sm={8} md={5} lg={5} xl={2}>
                                        <DatePicker
                                            onChange={this.onSelectDate}
                                            value={moment(date)}
                                            picker="month"
                                        />
                                    </Col>
                                    <Col xs={16} sm={16} md={19} lg={19} xl={22}>
                                        <Search
                                            placeholder="введите текст для поиска"
                                            enterButton='search'
                                            onSearch={(value: string) => {
                                                this.setState({ filter: (value || '').toLowerCase() });
                                            }}
                                        />
                                    </Col>
                                </Row>
                                <Row gutter={[16, 16]}>
                                    <Col xs={24} sm={24} md={24} lg={24} xl={24}>
                                        {<Table
                                            size='middle'
                                            rowKey="id"
                                            columns={this.state.columns}
                                            pagination={false}
                                            loading={isLoading}
                                            bordered={false}
                                            dataSource={this.getGamers()}
                                        />}
                                    </Col>
                                </Row>
                            </Card>
                        </Col>
                    </Row>
                </Layout>

                <AddCharDialog
                    onClose={() => this.toggleAddCharModal}
                    visible={this.state.addCharacterModal.show}
                    onAdd={this.onAddCharacter}
                    isLoading={isLoading}
                />
            </Content>
        );
    }
}

const connectedCofferController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const gamersList = state.gamers.gamersList;
        return {
            isLoading: state.gamers.gamersList.holding === true,
            gamers: gamersList
        };
    },
    (dispatch: any) => {
        return {
            loadData: (date: Date) => dispatch(gamerInstance.GetGamers({ dateMonth: date })),
            loadGuildInfo: () => {
                dispatch(guildInstance.GetGuild());
                dispatch(guildInstance.GetGuildBalanceReport())
            },
            deleteCharacter: (userId: string, characterId: string) => dispatch(gamerInstance.deleteCharacter(userId, characterId))

        }
    })(_CofferController);

export { connectedCofferController as CofferController };
