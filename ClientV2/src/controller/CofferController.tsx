import React from "react";
import { Lang, IGamersListView, DLang, LangF } from '../_services';
import { Table, Breadcrumb, Row, Col, DatePicker, Layout } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { connect } from "react-redux";
import { IStore, formatDateTime } from "../_helpers";
import { gamerInstance, guildInstance } from "../_actions";
import { ColumnProps } from "antd/lib/table";
import { Content } from "../_components/Content/Content";
import { Link } from "react-router-dom";
import Search from "antd/lib/input/Search";
import moment, { Moment } from "moment";
import { Dictionary } from "../core";
import { Characters } from "../_components/Character/Character";
import { Card } from "../_components/Base/Card";
import { AddCharDialog } from "../_components/Character/AddCharDialog";
import { Loans } from "../_components/Loans/Loans";
import { AddLoanDialog } from "../_components/Loans/AddLoanDialog";
import { Penalties } from "../_components/Penalties/Penalties";
import { AddPenaltyDialog } from "../_components/Penalties/AddPenaltyDialog";
import { ShowLoanDialog } from "../_components/Loans/ShowLoanDialog";


interface IMainProps {
    isLoading: boolean;
    gamers: Dictionary<Dictionary<IGamersListView>>;
    loadGuildInfo(): void;
    loadData(date: Date): void;
    deleteCharacter(userId: string, characterId: string): void;
    addCharacter(userId: string, characterId: string, name: string, className: string, isMain: boolean): void;
    addLoan(userId: string, loanId: string, amount: number, description: string): void;
    addPenalty(userId: string, penaltyId: string, amount: number, description: string): void;
}

interface ModalState {
    show: boolean;
    userId?: string;
}
interface IState {
    addCharacterModal: ModalState;
    addLoanModal: ModalState;
    addPenaltyModal: ModalState;
    showLoanDialog: ModalState & { loanId?: string; userId?: string; };
    filter: string;
    date: Date;
    columns: ColumnProps<IGamersListView>[];
}

export class _CofferController extends React.Component<IMainProps, IState> {
    constructor(props: IMainProps) {
        super(props);
        this.state = {
            addCharacterModal: { show: false },
            addLoanModal: { show: false },
            addPenaltyModal: { show: false },
            showLoanDialog: { show: false },
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
                    render: (value: string, record: IGamersListView) => {
                        return <div>
                            <Col style={{
                                fontWeight: 500,
                                padding: '.25rem .5rem',
                            }}>
                                {LangF('USER_CHAR_LIST', value)}
                            </Col>
                            <Col>
                                <Characters
                                    userId={record.id}
                                    characters={record.characters}
                                    onDeleteChar={this.onDeleteChar}
                                    toggleAddCharModal={this.toggleAddCharModal}
                                />
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
                    render: (value: string) => {
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
                    render: (value: string) => {
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
                    render: (value: number) => {
                        return <div
                            style={{ color: value > 0 ? 'green' : 'red' }}
                        >
                            {value}
                        </div>;
                    }
                },
                {
                    title: Lang('USER_ROW_LOANS'),
                    dataIndex: 'loans',
                    key: 'loans',
                    render: (value: number, record: IGamersListView) => {
                        return <Loans
                            loans={record.loans}
                            userId={record.id}
                            onAddLoan={this.toggleAddLoanModal}
                            onLoanShow={this.toggleShowLoanDialog}
                        />
                    }
                },
                {
                    title: Lang('USER_ROW_PENALTIES'),
                    dataIndex: 'penalties',
                    key: 'penalties',
                    render: (value: number, record: IGamersListView) => {
                        return <Penalties
                            penalties={record.penalties}
                            userId={record.id}
                            onAddLoan={this.toggleAddPenaltyModal}
                        />
                    }
                },
            ]
        }
    }
    loadData = () => {
        const { date } = this.state;
        this.props.loadData(date);
        this.props.loadGuildInfo();
    }

    componentDidMount() {
        this.loadData();
    }

    onSelectDate = (value: Moment | null) => {
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
    onAddCharacter = (id: string, name: string, className: string, isMain: boolean) => {
        let { userId } = this.state.addCharacterModal;
        if (userId)
            this.props.addCharacter(userId, id, name, className, isMain);
    };
    onDeleteChar = (id: string, userId: string) => {
        this.props.deleteCharacter(userId, id);
    };

    toggleAddLoanModal = (userId?: string) => {
        this.setState({ addLoanModal: { show: !this.state.addLoanModal.show, userId } });
    }
    onAddLoan = (loanId: string, amount: number, description: string) => {
        let { userId } = this.state.addLoanModal;
        if (userId)
            this.props.addLoan(userId, loanId, amount, description);
    };

    toggleAddPenaltyModal = (userId?: string) => {
        this.setState({ addPenaltyModal: { show: !this.state.addPenaltyModal.show, userId } });
    }
    onAddPenalty = (penaltyId: string, amount: number, description: string) => {
        let { userId } = this.state.addPenaltyModal;
        if (userId)
            this.props.addPenalty(userId, penaltyId, amount, description);
    };

    toggleShowLoanDialog = (userId?: string, loanId?: string) => {
        this.setState({ showLoanDialog: { show: !this.state.showLoanDialog.show, userId, loanId } });
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
                    onClose={this.toggleAddCharModal}
                    visible={this.state.addCharacterModal.show}
                    onAdd={this.onAddCharacter}
                    isLoading={isLoading}
                />
                <AddLoanDialog
                    onClose={this.toggleAddLoanModal}
                    visible={this.state.addLoanModal.show}
                    isLoading={isLoading}
                    onAdd={this.onAddLoan}
                />
                <AddPenaltyDialog
                    onClose={this.toggleAddPenaltyModal}
                    visible={this.state.addPenaltyModal.show}
                    isLoading={isLoading}
                    onAdd={this.onAddPenalty}
                />
                <ShowLoanDialog
                    onClose={this.toggleShowLoanDialog}
                    visible={this.state.showLoanDialog.show}
                    loanId={this.state.showLoanDialog.loanId}
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
            loadData: (date: Date) => dispatch(gamerInstance.getGamers({ dateMonth: date })),
            loadGuildInfo: () => {
                dispatch(guildInstance.GetGuild());
                dispatch(guildInstance.GetGuildBalanceReport())
            },
            deleteCharacter: (userId: string, characterId: string) =>
                dispatch(gamerInstance.deleteCharacter({ userId, characterId })),
            addCharacter: (userId: string, characterId: string, name: string, className: string, isMain: boolean) =>
                dispatch(gamerInstance.addCharacter({ userId, characterId, name, className, isMain })),
            addLoan: (userId: string, loanId: string, amount: number, description: string) =>
                dispatch(gamerInstance.addLoan({ userId, loanId, amount, description })),
            addPenalty: (userId: string, penaltyId: string, amount: number, description: string) =>
                dispatch(gamerInstance.addPenalty({ userId, penaltyId, amount, description })),
        }
    })(_CofferController);

export { connectedCofferController as CofferController };
