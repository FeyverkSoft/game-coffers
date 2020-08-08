import React from "react";
import { Lang, DLang, } from '../_services';
import { Breadcrumb, PageHeader, Row, Col, List, Tooltip, Button, } from 'antd';
import { Card } from '../_components/Base/Card'
import { HomeOutlined, RedoOutlined, } from '@ant-design/icons';
import { connect } from "react-redux";
import { IStore, } from "../_helpers";
import style from './contracts.module.scss';
import { Content } from "../_components/Content/Content";
import { Link } from "react-router-dom";
import { IHolded, IDictionary } from "../core";
import { Contract } from "../_services/nest/Contract";
import { nestsInstance } from "../_actions/nest/nests.actions";
import Search from "antd/lib/input/Search";

interface IMainProps {
    isLoading: boolean;
    contracts: IDictionary<IDictionary<Array<Contract>> & IHolded>;
    loadData: Function;
}

interface IState {
    filter: string;
}

export class _ContractController extends React.Component<IMainProps, IState> {
    intervalId?: NodeJS.Timeout;
    constructor(props: IMainProps) {
        super(props);
        this.state = {
            filter: '',
        }
    }

    loadData = () => {
        this.props.loadData();
    }

    componentDidMount() {
        this.loadData();
        this.intervalId = setInterval(this.loadData, 120000);
    }

    componentWillUnmount() {
        if (this.intervalId != undefined)
            clearInterval(this.intervalId);
    }

    render() {
        const { contracts } = this.props;
        const nests = Object.keys(contracts).filter(_ => _ !== 'holding' &&
            _.toLowerCase().includes(this.state.filter));
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <Link to={"/"} >
                            <HomeOutlined />
                        </Link>
                    </Breadcrumb.Item>
                    <Breadcrumb.Item>
                        <Link to={"/contracts"}>
                            {Lang("CONTRACTS_PAGE")}
                        </Link>
                    </Breadcrumb.Item>
                </Breadcrumb>
                <div className={style['bd']}>
                    <PageHeader
                        ghost={false}
                        subTitle=""
                        title={Lang("CONTRACTS_PAGE")}
                        className={style['ant-card']}
                    >
                        <Row>
                            <Col xs={20} sm={21} md={21} lg={22} xl={23} className={style['col']}>
                                <Search
                                    placeholder="введите текст для поиска"
                                    enterButton={Lang('search')}
                                    onSearch={(value: string) => {
                                        this.setState({ filter: (value || '').toLowerCase() });
                                    }}
                                />
                            </Col>
                            <Col xs={4} sm={3} md={3} lg={2} xl={1}>
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
                                <div className={style['cards-wrapper']}>
                                    {
                                        nests.map(nest => {
                                            return <Card
                                                className={style['card']}
                                                size="small"
                                                title={<div className={style['card-name']}>{nest}</div>}
                                            >
                                                {
                                                    Object.keys(contracts[nest]).sort().map(reward => {
                                                        return <div
                                                            className={style['item']}>
                                                            <div
                                                                className={style['title']}>
                                                                {DLang('REWARD_ITEMS', reward)}
                                                            </div>
                                                            <div
                                                                className={style['body']}>
                                                                {
                                                                    contracts[nest][reward].map(contract => {
                                                                        return <div>{contract.characterName}</div>
                                                                    })
                                                                }
                                                            </div>
                                                        </div>
                                                    })
                                                }
                                            </Card>
                                        })
                                    }
                                </div>
                            </Col>
                        </Row>
                    </PageHeader>
                </div>
            </Content>
        );
    }
}

const ContractController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { guildContracts } = state.nests;
        return {
            contracts: guildContracts,
            isLoading: guildContracts.holding,
        };
    },
    (dispatch: any) => {
        return {
            loadData: () => {
                dispatch(nestsInstance.getGuildContracts({}));
            },
        }
    })(_ContractController);

export default ContractController;