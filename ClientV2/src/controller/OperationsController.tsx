import React from "react";
import { Lang, IOperationView, OperationType, DLang, IGamersListView } from '../_services';
import { Table, Breadcrumb, PageHeader, DatePicker, Row, Col, Button, Tooltip } from 'antd';
import { HomeOutlined, RedoOutlined, EditFilled } from '@ant-design/icons';
import { connect } from "react-redux";
import { IStore, formatDateTime, IF } from "../_helpers";
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
import { EditOperationDialog } from "../_components/Operations/EditOperationDialog";

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
    addEditOperationModal: ModalState & { operationId?: string };
    filter: string;
    date: Date,
    columns: ColumnProps<IOperationView>[];
}

export class _OperationsController extends React.Component<IMainProps, IState> {
    constructor(props: IMainProps) {
        super(props);
        this.state = {
            addOperationModal: { show: false },
            addEditOperationModal: { show: false },
            filter: '',
            date: new Date(),
            columns: [
                {
                    title: Lang('DATE'),
                    dataIndex: 'createDate',
                    key: 'createDate',
                    defaultSortOrder: 'descend',
                    width: 150,
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
                    key: 'description',
                    render: (value: string, record: IOperationView, index: number) => {
                        if (record.parrentOperation)
                            return <div>
                                <Tooltip title={record.parrentOperation.description}>
                                   <span>{value}</span>
                                </Tooltip>
                            </div>;
                        return value;
                    }
                },
                {
                    title: Lang('DOCUMENT_DESCRIPTION'),
                    dataIndex: 'documentDescription',
                    key: 'documentDescription',
                    render: (value: string, record: IOperationView, index: number) => {
                        return value;
                    }
                },
                {
                    title: Lang('ACTIONS'),
                    dataIndex: 'id',
                    align: 'center',
                    fixed: 'right',
                    width: 50,
                    render: (value: string, record: IOperationView, index: number) => {
                        return <div>
                            <Tooltip title={Lang('EDIT')}>
                                <Button
                                    disabled={record.documentId !== ''}
                                    type="link"
                                    icon={<EditFilled />}
                                    onClick={() => this.toggleEditOperationDialog(record.id)}
                                />
                            </Tooltip>
                        </div>;
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

    toggleEditOperationDialog = (operationId: string) => {
        this.setState({ addEditOperationModal: { show: !this.state.addEditOperationModal.show, operationId } });
    }

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
                            <Col xs={14} sm={15} md={18} lg={18} xl={21}>
                                <Search
                                    placeholder="введите текст для поиска"
                                    enterButton='search'
                                    onSearch={(value: string) => {
                                        this.setState({ filter: (value || '').toLowerCase() });
                                    }}
                                />
                            </Col>
                            <Col xs={2} sm={1} md={1} lg={1} xl={1}>
                                <Tooltip title="update">
                                    <Button
                                        type="primary"
                                        shape="circle"
                                        icon={<RedoOutlined />}
                                        onClick={() => this.loadData()} />
                                </Tooltip>
                            </Col>
                        </Row>
                        <Row gutter={[16, 16]}>
                            <Col xl={24}>
                                {<Table
                                    size='small'
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
                <IF value={this.state.addEditOperationModal.show}>
                    <EditOperationDialog
                        onClose={this.toggleEditOperationDialog}
                        visible={this.state.addEditOperationModal.show}
                        users={Object.keys(gamersList).map(
                            _ => gamersList[_]
                        )}
                        operation={this.getOperations().filter(_ => _.id === this.state.addEditOperationModal.operationId)[0] || {}}
                    />
                </IF>
            </Content>
        );
    }
}

const OperationsController = connect<{}, {}, {}, IStore>(
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

export default OperationsController;