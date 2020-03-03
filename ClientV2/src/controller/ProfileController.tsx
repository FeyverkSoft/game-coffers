import * as React from 'react';
import { Breadcrumb, Layout, Col, Row, Statistic, Card } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { Lang, IProfile } from '../_services';
import style from './profile.module.scss';
import { connect } from 'react-redux';
import { IStore } from '../_helpers/store';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';
import { profileInstance } from '../_actions/profile/profile.actions';
import { IHolded } from '../core';
import { ProfileCard } from '../_components/Profile/ProfileCard';

interface IProfileProps {
    Get: Function;
    profile: IProfile & IHolded;
}

export class _ProfileController extends React.Component<IProfileProps, any> {
    constructor(props: IProfileProps) {
        super(props);
        this.state = {
        }
    }

    componentDidMount() {
        if (this.props.profile == undefined)
            this.props.Get();
    }

    render = () => {
        let { profile } = this.props;
        return <Content>
            <Breadcrumb>
                <Breadcrumb.Item>
                    <Link to={"/"} >
                        <HomeOutlined />
                    </Link>
                </Breadcrumb.Item>
                <Breadcrumb.Item>
                    <Link to={"/profile"} >
                        {Lang("PROFILE_PAGE")}
                    </Link>
                </Breadcrumb.Item>
            </Breadcrumb>
            <Layout>
                <Row gutter={[16, 16]}>
                    <Col xs={24} sm={24} md={8} lg={6} xl={6}>
                        <ProfileCard
                            profile={profile}
                            isLoading={profile.holding !== false}
                        />
                    </Col>
                    <Col xs={24} sm={12} md={4} lg={3} xl={3} >
                        <Card style={{ boxShadow: '0 2px 2px rgba(0, 0, 0, 0.14), 1px 2px 3px rgba(0, 0, 0, 0.12)' }}>
                            <Statistic
                                title={Lang('USER_CHAR_COUNT')}
                                valueStyle={{ color: '#3f8600' }}
                                value={profile.charCount}
                                precision={0} />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={4} lg={3} xl={2} >
                        <Card
                            style={{ boxShadow: '0 2px 2px rgba(0, 0, 0, 0.14), 1px 2px 3px rgba(0, 0, 0, 0.12)' }}
                        >
                            <Statistic
                                title={Lang('USER_ROW_BALANCE')}
                                value={profile.balance}
                                precision={2} />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={6} lg={4} xl={3} >
                        <Card
                            style={{ boxShadow: '0 2px 2px rgba(0, 0, 0, 0.14), 1px 2px 3px rgba(0, 0, 0, 0.12)' }}
                        >
                            <Statistic
                                title={Lang('USER_LOAN_AMOUNT')}
                                value={profile.activeLoanAmount}
                                precision={2} />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={6} lg={4} xl={3} >
                        <Card
                            style={{ boxShadow: '0 2px 2px rgba(0, 0, 0, 0.14), 1px 2px 3px rgba(0, 0, 0, 0.12)' }}
                        >
                            <Statistic
                                title={Lang('USER_ROW_PENALTIES')}
                                valueStyle={{ color: '#cf1322' }}
                                value={profile.activePenaltyAmount}
                                precision={2} />
                        </Card>
                    </Col>
                </Row>
            </Layout>
        </Content>
    }
}

const connectedProfileController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { profile } = state.profile;
        return { profile };
    },
    (dispatch: Function) => {
        return {
            Get: () => dispatch(profileInstance.Get()),
        }
    })(_ProfileController);

export { connectedProfileController as ProfileController };
