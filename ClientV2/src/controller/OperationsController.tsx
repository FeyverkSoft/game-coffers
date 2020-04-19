import React from "react";
import { Lang, IOperationView, LangF, OperationType, DLang, IGamersListView } from '../_services';
import { Table, Breadcrumb, PageHeader, DatePicker, Row, Col, Button } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { connect } from "react-redux";
import { IStore, formatDateTime } from "../_helpers";
import { operationsInstance, gamerInstance } from "../_actions";
import { ColumnProps } from "antd/lib/table";
import style from './bd.module.scss';
import { Content } from "../_components/Content/Content";
import { Link } from "react-router-dom";
import Search from "antd/lib/input/Search";
import { IHolded, IDictionary } from "../core";
import moment, { Moment } from "moment";
import { AddOperationDialog } from "../_components/Operations/AddOperationDialog";
import { Private } from "../_components/Private";

interface IMainProps {
    isLoading: boolean;
    operations: IDictionary<Array<IOperationView> & IHolded>;
    gamersList: IDictionary<IDictionary<IGamersListView>>;
    loadData: Function;
}

interface ModalState {
    show: boolean;
}

interface IState {
    addOperationModal: ModalState;
    filter: string;
    date: Date,
    columns: ColumnProps<IOperationView>[];
}

export class _OperationsController extends React.Component<IMainProps, IState> {
    constructor(props: IMainProps) {
        super(props);
        this.state = {
            addOperationModal: { show: false },
            filter: '',
            date: new Date(),
            columns: [
                {
                    title: Lang('DATE'),
                    dataIndex: 'createDate',
                    key: 'createDate',
                    defaultSortOrder: 'ascend',
                    sorter: (a: IOperationView, b: IOperationView) => {
                        return (Number(a.createDate) - Number(b.createDate));
                    },
                    render: (value: Date, record: IOperationView, index: number) => {
                        return formatDateTime(value, 'h');
                    }
                },
                {
                    title: Lang('OPERATION_AMOUNT'),
                    dataIndex: 'amount',
                    key: 'amount',
                    sorter: (a: IOperationView, b: IOperationView) => {
                        return a.amount - b.amount;
                    },
                },
                {
                    title: Lang('NAME'),
                    dataIndex: 'userName',
                    key: 'userName'
                },
                {
                    title: Lang('OPERATION_TYPE'),
                    dataIndex: 'type',
                    key: 'type',
                    render: (value: OperationType, record: IOperationView, index: number) => {
                        return DLang('OPERATIONS_TYPE', value);
                    }
                },
                {
                    title: Lang('OPERATION_DESCRIPTION'),
                    dataIndex: 'description',
                    key: 'description'
                },
                {
                    title: Lang('DOCUMENT_DESCRIPTION'),
                    dataIndex: 'documentDescription',
                    key: 'documentDescription',
                    render: (value: string, record: IOperationView, index: number) => {
                        return value;
                    }
                },
            ]
        }
    }

    loadData = () => {
        const { date } = this.state;
        this.props.loadData(date);
    }

    componentDidMount() {
        this.loadData();
    }

    onSelectDate = (value: Moment | null, dateString: string) => {
        if (value) {
            this.setState({ date: value.toDate() }, this.loadData);
        }
    };

    getOperations = () => {
        const { date, filter } = this.state;
        const strDate: string = formatDateTime(date, 'm');
        const { operations } = this.props;
        const dateOp = (operations[strDate] || []);
        return dateOp.filter(_ => {
            return _.amount.toString() === filter ||
                _.description.toLowerCase().includes(filter) ||
                _.documentDescription.toLowerCase().includes(filter) ||
                _.userName.toLowerCase().includes(filter) ||
                formatDateTime(_.createDate, 'h').includes(filter) ||
                filter === '';
        })
    }

    toggleAddOperationDialog = () => {
        this.setState({ addOperationModal: { show: !this.state.addOperationModal.show } });
    }

    render() {
        const { date } = this.state;
        const gamersList = this.props.gamersList[formatDateTime(date, 'm')] || {};
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <Link to={"/"} >
                            <HomeOutlined />
                        </Link>
                    </Breadcrumb.Item>
                    <Breadcrumb.Item>
                        <Link to={"/operations"}>
                            {Lang("OPERATIONS_PAGE")}
                        </Link>
                    </Breadcrumb.Item>
                </Breadcrumb>
                <div className={style['bd']}>
                    <PageHeader
                        ghost={false}
                        subTitle=""
                        title={Lang("OPERATIONS_PAGE")}
                        className={style['ant-card']}
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
                            <Col xl={24}>
                                {<Table
                                    size='middle'
                                    rowKey="id"
                                    columns={this.state.columns}
                                    pagination={false}
                                    loading={this.props.isLoading}
                                    bordered={false}
                                    dataSource={this.getOperations()}
                                />}
                            </Col>
                        </Row>
                        <Row gutter={[16, 16]} justify='center'>
                            <Private roles={['admin', 'leader', 'officer']}>
                                <Button
                                    type='primary'
                                    size='large'
                                    onClick={this.toggleAddOperationDialog}
                                >{Lang('ADD_NEW_USER')}
                                </Button>
                            </Private>
                        </Row>
                    </PageHeader>
                </div>
                <AddOperationDialog
                    onClose={this.toggleAddOperationDialog}
                    visible={this.state.addOperationModal.show}
                    users={Object.keys(gamersList).map(
                        _ => gamersList[_]
                    )}
                />
            </Content>
        );
    }
}

const connectedOperationsController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { operations } = state.operations;
        const { gamersList } = state.gamers;
        return {
            operations: operations,
            isLoading: operations.holding,
            gamersList: gamersList
        };
    },
    (dispatch: any) => {
        return {
            loadData: (date: Date) => {
                dispatch(operationsInstance.getOperations(date));
                dispatch(gamerInstance.getGamers({ dateMonth: date }));
            },
        }
    })(_OperationsController);

export { connectedOperationsController as OperationsController };
